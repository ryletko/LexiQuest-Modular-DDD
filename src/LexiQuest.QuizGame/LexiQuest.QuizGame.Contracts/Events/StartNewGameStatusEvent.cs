using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Contracts.Events;

public record StartNewGameStatusEvent(Guid StartNewGameId,
                                      string Status) : CommandBase
{
}