using LexiQuest.Framework.Application.Messages.Events;

namespace LexiQuest.QuizGame.Application.Games.CreateNewGame;

public record NewGameCreatedEvent(Guid NewGameId): EventBase
{
    
}