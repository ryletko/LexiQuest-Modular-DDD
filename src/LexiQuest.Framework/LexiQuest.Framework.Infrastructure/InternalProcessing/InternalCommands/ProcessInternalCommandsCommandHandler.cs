using System.Reflection;
using Dapper;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;
using MassTransit;
using Newtonsoft.Json;
using Polly;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;

internal class ProcessInternalCommandsCommandHandler(ISqlConnectionFactory sqlConnectionFactory,
                                                     CommandsExecutor commandsExecutor,
                                                     string schemaName,
                                                     Assembly applicationAssembly) : ICommandHandler<ProcessInternalCommandsCommand>
{

    private async Task ProcessCommand(InternalCommandDto internalCommand)
    {
        Type type = applicationAssembly.GetType(internalCommand.Type);
        dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

        await commandsExecutor.Execute(commandToProcess);
    }

    private class InternalCommandDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }
    }

    public async Task Consume(ConsumeContext<ProcessInternalCommandsCommand> context)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        string sql = $"""
                      SELECT
                          "Command"."Id" AS "{nameof(InternalCommandDto.Id)}",
                          "Command"."Type" AS "{nameof(InternalCommandDto.Type)}",
                          "Command"."Data" AS "{nameof(InternalCommandDto.Data)}"
                      FROM "{schemaName}"."_InternalCommands" AS "Command"
                      WHERE "Command"."ProcessedDate" IS NULL
                      ORDER BY "Command"."EnqueueDate"
                      """;
        var commands = await connection.QueryAsync<InternalCommandDto>(sql);

        var internalCommandsList = commands.AsList();

        var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(new[]
                                       {
                                           TimeSpan.FromSeconds(1),
                                           TimeSpan.FromSeconds(2),
                                           TimeSpan.FromSeconds(3)
                                       });
        foreach (var internalCommand in internalCommandsList)
        {
            var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(internalCommand));

            if (result.Outcome == OutcomeType.Failure)
            {
                await connection.ExecuteScalarAsync($"""
                                                     UPDATE "{schemaName}"."_InternalCommands"
                                                     SET "ProcessedDate" = @NowDate, "Error" = @Error
                                                     WHERE "Id" = @Id
                                                     """,
                                                    new
                                                    {
                                                        NowDate = DateTime.UtcNow,
                                                        Error   = result.FinalException.ToString(),
                                                        internalCommand.Id
                                                    });
            }
        }
    }
}