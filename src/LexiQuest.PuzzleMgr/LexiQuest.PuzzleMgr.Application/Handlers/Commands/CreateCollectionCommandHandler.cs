using LexiQuest.PuzzleMgr.Contracts.Commands;
using LexiQuest.PuzzleMgr.Contracts.Events;
using LexiQuest.PuzzleMgr.Domain.PuzzleCollections;
using MassTransit;

namespace LexiQuest.PuzzleMgr.Application.Handlers.Commands;

internal class CreateCollectionCommandHandler(IBus bus, IPuzzleCollectionRepository puzzleCollectionRepository): IConsumer<CreateCollectionCommand>
{
    public async Task Consume(ConsumeContext<CreateCollectionCommand> context)
    {
        var newCollection = PuzzleCollection.CreateNew(CollectionName.FromString(context.Message.Name));
        await puzzleCollectionRepository.AddAsync(newCollection, context.CancellationToken);
        await bus.Publish(new PuzzleCollectionAddedEvent(newCollection.Id.Value));
    }
}