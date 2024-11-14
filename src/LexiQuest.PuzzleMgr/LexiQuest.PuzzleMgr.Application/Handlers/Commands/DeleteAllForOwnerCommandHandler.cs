using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.PuzzleMgr.Contracts.Commands;
using LexiQuest.PuzzleMgr.Contracts.Events;
using LexiQuest.PuzzleMgr.Domain.PuzzleOwners;
using LexiQuest.PuzzleMgr.Domain.Puzzles;

namespace LexiQuest.PuzzleMgr.Application.Handlers.Commands;

internal class DeleteAllForOwnerCommandHandler(IPuzzleRepository puzzleRepository,
                                               IEventBus eventBus): CommandHandlerBase<DeleteAllForOwnerCommand>, IEventBusMessageHandler
{
    public override async Task Handle(DeleteAllForOwnerCommand command, CancellationToken cancellationToken)
    {
        await puzzleRepository.DeleteAllForOwnerAsync(new PuzzleOwnerId(command.MessageContext.UserId), cancellationToken);
        await eventBus.SendEvent(new AllPuzzlesDeletedForOwner(command.MessageContext.UserId));
    }
}