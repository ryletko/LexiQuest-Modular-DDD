using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.PuzzleMgr.Contracts;
using Microsoft.Extensions.Logging;

namespace LexiQuest.PuzzleMgr.Application.Handlers;

public class PzlDummyCommandHandler: CommandHandlerBase<PzlDummyCommand>, IEventBusMessageHandler
{
    public override async Task Handle(PzlDummyCommand command, CancellationToken cancellationToken)
    {
    }
}