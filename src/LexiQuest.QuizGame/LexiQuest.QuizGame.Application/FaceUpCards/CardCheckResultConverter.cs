using LexiQuest.QuizGame.Domain.FaceUpCards;
using LexiQuest.Shared.QuizGame;

namespace LexiQuest.QuizGame.Application.FaceUpCards;

public static class CardCheckResultConverter
{
    public static FaceUpCardCheckResult? ToExternalCheckResult(this FaceUpCardCheckStatusEnum? turnResult)
    {
        return (FaceUpCardCheckResult?) turnResult;
    }
}