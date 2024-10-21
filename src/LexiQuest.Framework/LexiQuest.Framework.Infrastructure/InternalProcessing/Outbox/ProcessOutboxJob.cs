using Dapper;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;
using LexiQuest.Framework.Infrastructure.Dependencies;
using LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;
using Newtonsoft.Json;
using Quartz;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxJob(OutboxProcessor handler) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await handler.Process();
    }
}

public sealed class OutboxProcessor(ICompositionRoot compositionRoot,
                                    ISqlConnectionFactory sqlConnectionFactory,
                                    IDomainNotificationsMapper domainNotificationsMapper,
                                    string schemaName)
{
    public async Task Process()
    {
        var connection = sqlConnectionFactory.GetOpenConnection();
        var sql = $"""
                   SELECT
                       "OutboxMessage"."Id" AS "{nameof(OutboxMessageDto.Id)}",
                       "OutboxMessage"."Type" AS "{nameof(OutboxMessageDto.Type)}",
                       "OutboxMessage"."Data" AS "{nameof(OutboxMessageDto.Data)}"
                   FROM "{schemaName}"."_OutboxMessages" AS "OutboxMessage"
                   WHERE "OutboxMessage"."ProcessedDate" IS NULL
                   ORDER BY "OutboxMessage"."OccurredOn"
                   """;

        var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
        var messagesList = messages.AsList();

        var sqlUpdateProcessedDate = $"""
                                      UPDATE "{schemaName}"."_OutboxMessages"
                                      SET "ProcessedDate" = @Date
                                      WHERE "Id" = @Id
                                      """;

        if (messagesList.Count > 0)
        {
            await using (var scope = compositionRoot.BeginScope())
            {
                var mediator = scope.GetMediator();
                var outboxErrorNotifier = scope.GetOutboxErrorNotifier();

                foreach (var message in messagesList)
                {
                    var type = domainNotificationsMapper.GetType(message.Type);
                    var @event = JsonConvert.DeserializeObject(message.Data, type) as IMediatorDomainEvent;

                    using (LogContext.Push(new OutboxMessageContextEnricher(@event)))
                    {
                        // TODO тут надо будет потом переделать, потому что я не планировал 
                        // чтобы медиатор выкидывал эксешены из event хэндлеров
                        // из-за того что он так делает outboxmessage не ставится в processed
                        // когда вылетает эксепшн в хэндлере
                        // я думал что должна быть логика отправил и забыл раз это publish
                        try
                        {
                            await mediator.Publish(@event);
                        }
                        catch (Exception ex)
                        {
                            await outboxErrorNotifier.NotifyError(ex);
                        }
                        finally
                        {
                            await connection.ExecuteAsync(sqlUpdateProcessedDate,
                                                          new
                                                          {
                                                              Date = DateTime.UtcNow,
                                                              message.Id
                                                          });
                        }
                    }
                }
            }
        }
    }

    private class OutboxMessageContextEnricher(IMediatorDomainEvent notification) : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"OutboxMessage:{notification.Id.ToString()}")));
        }
    }
}