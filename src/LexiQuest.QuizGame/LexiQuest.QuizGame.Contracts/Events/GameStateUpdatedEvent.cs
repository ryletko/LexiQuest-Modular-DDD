using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Shared.QuizGame;

namespace LexiQuest.QuizGame.Contracts.Events;

public record GameStateUpdatedEvent(Guid GameId, GameStatus GameStatus): CommandBase
{
    
}