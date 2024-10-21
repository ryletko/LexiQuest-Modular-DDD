using Dapper;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;
using MassTransit;
using Newtonsoft.Json;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Inbox;

internal class ProcessInboxCommandHandler(ISqlConnectionFactory sqlConnectionFactory, string schemaName) : ICommandHandler<ProcessInboxCommand>
{
    public async Task Consume(ConsumeContext<ProcessInboxCommand> context)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();
        var sql = $"""
                   SELECT [InboxMessage].[Id] AS [{nameof(InboxMessageDto.Id)}],
                          [InboxMessage].[Type] AS [{nameof(InboxMessageDto.Type)}],
                          [InboxMessage].[Data] AS [{nameof(InboxMessageDto.Data)}]
                   FROM [{schemaName}].[InboxMessages] AS [InboxMessage]
                   WHERE [InboxMessage].[ProcessedDate] IS NULL
                   ORDER BY [InboxMessage].[OccurredOn]
                   """;

        var messages = await connection.QueryAsync<InboxMessageDto>(sql);

        var sqlUpdateProcessedDate = $"""
                                      UPDATE [{schemaName}].[InboxMessages]
                                      SET [ProcessedDate] = @Date
                                      WHERE [Id] = @Id
                                      """;

        foreach (var message in messages)
        {
            var messageAssembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));
            Type type = messageAssembly.GetType(message.Type);
            var request = JsonConvert.DeserializeObject(message.Data, type);

            try
            {
                await context.Publish(request, context.CancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await connection.ExecuteScalarAsync(sqlUpdateProcessedDate, new
                                                                        {
                                                                            Date = DateTime.UtcNow,
                                                                            message.Id
                                                                        });
        }
    }
}