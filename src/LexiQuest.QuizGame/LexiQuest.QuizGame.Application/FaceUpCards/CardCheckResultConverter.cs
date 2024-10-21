using LexiQuest.Shared.QuizGame;

using FaceUpCardCheckResultInternal = LexiQuest.QuizGame.Domain.FaceUpCards.FaceUpCardCheckResult;

namespace LexiQuest.QuizGame.Application.FaceUpCards;

public static class CardCheckResultConverter
{
    public static FaceUpCardCheckResult? ToExternalCheckResult(this FaceUpCardCheckResultInternal? turnResult)
    {
        return (FaceUpCardCheckResult?) turnResult;
    }
}