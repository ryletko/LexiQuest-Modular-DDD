using LexiQuest.Framework.Application.Errors;
using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Application.Games.Access;
using LexiQuest.QuizGame.Domain.GameStates;

namespace LexiQuest.QuizGame.Application.Games.StartNewGame;

internal class StartGameCommandHandler(IGameStateRepository gameStateRepository,
                                       IEventBus eventBus) : CommandHandlerBase<StartGameCommand>, IEventBusMessageHandler
{
    public override async Task Handle(StartGameCommand command, CancellationToken cancellationToken)
    {
        var game = await gameStateRepository.GetByIdAsync(new GameId(command.GameId), cancellationToken);
        game.CheckAccess(command, PermissionAction.Edit);
        game.Start();
        await eventBus.SendEvent(new GameStartedEvent(game.GameId.Value));
    }
}