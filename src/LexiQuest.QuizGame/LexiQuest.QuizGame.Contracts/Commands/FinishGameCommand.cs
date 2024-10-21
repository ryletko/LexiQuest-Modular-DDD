using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Contracts.Commands;

public record FinishGameCommand(Guid GameId): CommandBase
{
    
}