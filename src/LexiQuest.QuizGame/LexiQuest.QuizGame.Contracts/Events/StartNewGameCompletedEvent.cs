using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Contracts.Events;

public record StartNewGameCompletedEvent(Guid StartNewGameId,
                                         Guid NewGameId) : CommandBase
{
}