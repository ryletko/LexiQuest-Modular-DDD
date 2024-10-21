using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Application.Games.StartNewGame;

internal record StartGameCommand(Guid GameId): CommandBase
{
    
}
