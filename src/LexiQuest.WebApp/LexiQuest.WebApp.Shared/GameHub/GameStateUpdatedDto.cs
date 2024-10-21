using LexiQuest.Shared.QuizGame;

namespace LexiQuest.WebApp.Shared.GameHub;

public record GameStateUpdatedDto(Guid GameId, GameStatus GameStatus, Guid CommandId)
{
}