using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Import.GoogleSheets.Commands;
using LexiQuest.Import.GoogleSheets.Contracts.Commands;
using LexiQuest.Import.GoogleSheets.Contracts.Events;
using LexiQuest.Import.GoogleSheets.Events;
using LexiQuest.PuzzleMgr.Contracts.Commands;
using LexiQuest.PuzzleMgr.Contracts.Events;
using MassTransit;

namespace LexiQuest.Import.GoogleSheets.ImportSaga;

public class ImportStateMachine : MassTransitStateMachine<ImportSagaData>
{
    public State Initializing { get; set; }
    public State FetchingDataFromGoogle { get; set; }
    public State SavingInPuzzleMgr { get; set; }

    public Event<ImportCommand> ImportCommand { get; set; }
    public Event<ImportInitialized> ImportInitialized { get; set; }
    public Event<ImportedDataFetched> FetchedEvent { get; set; }
    public Event<NewPuzzlesAdded> PuzzlesAddedEvent { get; set; }

    public ImportStateMachine()
    {
        InstanceState(x => x.CurrentState);

        // CorrelationId нужен просто чтобы идентифицировать сагу для того чтобы сопоставлять прилетающие сообщения с ней.
        // в изначальном ивенте можно назначить CorrelatedBy(...), где будет обозначен айди какого то изначального объекта, для которого 
        // запускается сага, чтобы избежать повтороного дублирующего запуска для него, например, orderid. Для одного заказа мы не должны паралельно
        // иметь две дублирующие саги для процессинга оплаты одного заказа, вторая должна быть отвергнута, если отправлена повторно. 
        // Если у нас нет уникально идентифицируещего Guid для CorrelationId саги, например OrderNumber это какая то строка, то предлагается использовать SelectId:
        //
        //
        // public OrderStateMachine()
        // {
        //     Event(() => ExternalOrderSubmitted, e => e
        //                                             .CorrelateBy((instance,context) => instance.OrderNumber == context.Message.OrderNumber)
        //                                             .SelectId(x => NewId.NextGuid()));
        // }
        // When the event doesn't have a Guid that uniquely correlates to an instance, the .SelectId expression must be configured. In the above example,
        // NewId is used to generate a sequential identifier which will be assigned to the instance CorrelationId. Any property on the event can be
        // used to initialize the CorrelationId.
        // 
        // Initial events that do not correlate on CorrelationId, and use SelectId to generate a CorrelationId should use a unique constraint
        // on the instance property (OrderNumber in this example) to avoid duplicate instances. If two events correlate to the same property
        // value at the same time, only one of the two will be able to store the instance, the other will fail (and, if retry is configured,
        // which it should be when using a saga) and retry at which time the event will be dispatched based upon the current instance state
        // (which is likely no longer Initial). Failure to apply a unique constraint (on OrderNumber in this example) will result in duplicates.
        // https://masstransit.io/documentation/patterns/saga/state-machine#event-1
        Event(() => ImportCommand, e => e.CorrelateById(x => x.Message.MessageContext.CorrelationId));
        Event(() => ImportInitialized, e => e.CorrelateById(x => x.Message.MessageContext.CorrelationId));
        Event(() => FetchedEvent, e => e.CorrelateById(x => x.Message.MessageContext.CorrelationId));
        Event(() => PuzzlesAddedEvent, e => e.CorrelateById(x => x.Message.MessageContext.CorrelationId));

        Initially(
            When(ImportCommand)
               .Then(ctx =>
                     {
                         ctx.Saga.ImportId       = ctx.Message.MessageContext.CorrelationId;
                         ctx.Saga.Timestamp      = DateTimeOffset.UtcNow;
                         ctx.Saga.ImporterId     = ctx.Message.MessageContext.UserId;
                         ctx.Saga.ImportSourceId = ctx.Message.ImportSourceId;

                         // это нужно для того чтобы вернуть ImportId на следующем шаге https://masstransit.io/documentation/patterns/saga/state-machine#respond
                         // ctx.Saga.RequestId       = ctx.RequestId;
                         // ctx.Saga.ResponseAddress = ctx.ResponseAddress;
                     })
               .TransitionTo(Initializing)
               .Publish(ctx => new ImportStatusChangedEvent(ctx.Saga.ImportId, ctx.Saga.CurrentState).ContextFrom(ctx.Message))
               .Publish(ctx => new InitializeImport(ctx.Saga.ImportSourceId).ContextFrom(ctx.Message)));

        During(Initializing,
               When(ImportInitialized)
                  .Then(ctx =>
                        {
                            ctx.Saga.Initialized = true;
                            ctx.Saga.Url         = ctx.Message.Url;
                            ctx.Saga.Language    = ctx.Message.Language;
                        })
                   // .ThenAsync(async context =>
                   //            {
                   //                var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
                   //                await endpoint.Send(new CommandGuidResult(context.Saga.ImportId), r => r.RequestId = context.Saga.RequestId);
                   //            })
                  .TransitionTo(FetchingDataFromGoogle)
                  .Publish(ctx => new ImportStatusChangedEvent(ctx.Saga.ImportId, ctx.Saga.CurrentState).ContextFrom(ctx.Message))
                  .Publish(ctx => new FetchDataFromGoogle(ctx.Message.Url, ctx.Saga.Language).ContextFrom(ctx.Message)));

        During(FetchingDataFromGoogle,
               When(FetchedEvent)
                  .Then(ctx => ctx.Saga.Fetched = true)
                  .TransitionTo(SavingInPuzzleMgr)
                  .Publish(ctx => new ImportStatusChangedEvent(ctx.Saga.ImportId, ctx.Saga.CurrentState).ContextFrom(ctx.Message))
                  .Publish(ctx => new AddNewPuzzlesCommand(ctx.Message.PuzzleItems.Select(x => new AddNewPuzzlesCommand.AddPuzzleItem(x.ForeignWord,
                                                                                                                                      x.PartsOfSpeech,
                                                                                                                                      x.Transcription,
                                                                                                                                      x.From,
                                                                                                                                      x.Language,
                                                                                                                                      x.Definitions,
                                                                                                                                      x.Synonims,
                                                                                                                                      x.Examples,
                                                                                                                                      x.Level)).ToList()).ContextFrom(ctx.Message)));

        During(SavingInPuzzleMgr,
               When(PuzzlesAddedEvent)
                  .Then(ctx => ctx.Saga.SavedInPuzzleMgr = true)
                  .Publish(ctx => new ImportCompletedEvent(ctx.Saga.ImportId).ContextFrom(ctx.Message))
                  .Finalize());

        // When the instance consumes the OrderCompleted event, the instance is finalized (which transitions the instance to the Final state). The SetCompletedWhenFinalized
        // method defines an instance in the Final state as completed – which is then used by the saga repository to remove the instance.
        
        SetCompletedWhenFinalized();
    }
}