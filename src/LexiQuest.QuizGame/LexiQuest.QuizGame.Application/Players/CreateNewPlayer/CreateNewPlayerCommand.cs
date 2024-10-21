using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.QuizGame.Application.Players.CreateNewPlayer;

internal record CreateNewPlayerCommand(string PlayerId) : CommandBase;