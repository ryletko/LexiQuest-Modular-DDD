using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Contracts.Events;

public record StartNewGameRefusedEvent(Guid StartNewGameId) : CommandBase
{
}