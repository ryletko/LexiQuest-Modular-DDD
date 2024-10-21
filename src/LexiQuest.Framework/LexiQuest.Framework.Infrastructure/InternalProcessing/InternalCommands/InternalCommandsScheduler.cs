using Dapper;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.InternalProcessing;
using LexiQuest.Framework.Infrastructure.DataAccess;
using LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;
using LexiQuest.Framework.Infrastructure.Serialization;
using Newtonsoft.Json;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;

public class InternalCommandsScheduler(ISqlConnectionFactory sqlConnectionFactory,
                                       string schemaName) : IInternalCommandsScheduler
{
    public async Task EnqueueAsync(ICommand command)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        var sqlInsert = $"""INSERT INTO "{schemaName}"."InternalCommands" ("Id", "EnqueueDate", "Type", "Data") VALUES (@Id, @EnqueueDate, @Type, @Data)""";

        await connection.ExecuteAsync(sqlInsert,
                                      new
                                      {
                                          command.Id,
                                          EnqueueDate = DateTime.UtcNow,
                                          Type        = command.GetType().FullName,
                                          Data = JsonConvert.SerializeObject(command,
                                                                             new JsonSerializerSettings
                                                                             {
                                                                                 ContractResolver = new AllPropertiesContractResolver()
                                                                             })
                                      });
    }

    public async Task EnqueueAsync<T>(ICommand<T> command) where T : class
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        var sqlInsert = $"""INSERT INTO "{schemaName}"."InternalCommands" ("Id", "EnqueueDate", "Type", "Data") VALUES (@Id, @EnqueueDate, @Type, @Data)""";

        await connection.ExecuteAsync(sqlInsert,
                                      new
                                      {
                                          command.Id,
                                          EnqueueDate = DateTime.UtcNow,
                                          Type        = command.GetType().FullName,
                                          Data = JsonConvert.SerializeObject(command,
                                                                             new JsonSerializerSettings
                                                                             {
                                                                                 ContractResolver = new AllPropertiesContractResolver()
                                                                             })
                                      });
    }
}