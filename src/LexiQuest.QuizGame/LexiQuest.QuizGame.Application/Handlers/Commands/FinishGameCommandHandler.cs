using LexiQuest.Framework.Application;
using LexiQuest.Framework.Application.Errors;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Application.Games;
using LexiQuest.QuizGame.Application.Games.Access;
using LexiQuest.QuizGame.Contracts.Commands;
using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Application.Handlers.Commands;

internal class FinishGameCommandHandler(IGameStateRepository gameStateRepository): CommandHandlerBase<FinishGameCommand>, IEventBusMessageHandler
{
    public override async Task Handle(FinishGameCommand command, CancellationToken cancellationToken)
    {
        var game = await gameStateRepository.GetByIdAsync(new GameId(command.GameId), cancellationToken);
        game.CheckAccess(command, PermissionAction.Edit);
        game.Finish();
    }
}