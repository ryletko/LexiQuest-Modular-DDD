using LexiQuest.QuizGame.Domain.FaceUpCards;

namespace LexiQuest.QuizGame.Domain.GameStates;

internal static class ScoreAdder
{
    public static Score ApplyTurnResult(this Score score, FaceUpCardCheckResult cardCheckResult)
    {
        if (cardCheckResult is FaceUpCardCheckResult.PureSuccess or 
                          FaceUpCardCheckResult.SuccessAfterHint or 
                          FaceUpCardCheckResult.SuccessAfterMistake)
            return Score.FromInt(score.IntVal + 12);
        
        if (cardCheckResult is FaceUpCardCheckResult.FirstMistake or 
                          FaceUpCardCheckResult.MistakenAgain)
            return Score.FromInt(score.IntVal - 2);
        
        if (cardCheckResult == FaceUpCardCheckResult.HintAfterMistake)
            return Score.FromInt(score.IntVal - 6);
        
        if (cardCheckResult == FaceUpCardCheckResult.HintWithoutAttempt)
            return Score.FromInt(score.IntVal - 8);
        
        if (cardCheckResult == FaceUpCardCheckResult.GaveUp)
            return Score.FromInt(score.IntVal - 8);

        return score;
    }
}
