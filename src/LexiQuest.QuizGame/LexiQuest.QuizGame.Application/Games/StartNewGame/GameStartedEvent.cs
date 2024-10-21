using LexiQuest.Framework.Application.Messages.Events;

namespace LexiQuest.QuizGame.Application.Games.StartNewGame;

public record GameStartedEvent(Guid gameId): EventBase
{
    
}