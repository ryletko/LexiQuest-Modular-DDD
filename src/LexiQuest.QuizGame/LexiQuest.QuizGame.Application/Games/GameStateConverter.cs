using LexiQuest.Shared.QuizGame;
using InternalGameStatus = LexiQuest.QuizGame.Domain.GameStates.GameStatus;

namespace LexiQuest.QuizGame.Application.Games;

public static class GameStateConverter
{
    public static GameStatus ToExternalGameStatus(this InternalGameStatus gameStatus) => (GameStatus) gameStatus;
}