using Dapper;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Infrastructure.DataAccess.SqlConnection;
using LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;
using MassTransit;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;
using LogContext = Serilog.Context.LogContext;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;

public sealed class ProcessOutboxCommandHandler(ISqlConnectionFactory sqlConnectionFactory,
                                         IDomainNotificationsMapper domainNotificationsMapper,
                                         string schemaName) : ICommandHandler<ProcessOutboxCommand>
{
    public async Task Consume(ConsumeContext<ProcessOutboxCommand> context)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();
        var sql =  $"""
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
            foreach (var message in messagesList)
            {
                var type = domainNotificationsMapper.GetType(message.Type);
                var @event = JsonConvert.DeserializeObject(message.Data, type) as IMediatorDomainEvent;

                using (LogContext.Push(new OutboxMessageContextEnricher(@event)))
                {
                    await context.Publish(@event, context.CancellationToken);

                    await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                                                                          {
                                                                              Date = DateTime.UtcNow,
                                                                              message.Id
                                                                          });
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