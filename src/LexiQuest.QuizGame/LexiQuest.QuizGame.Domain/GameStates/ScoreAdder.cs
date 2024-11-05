using LexiQuest.QuizGame.Domain.FaceUpCards;

namespace LexiQuest.QuizGame.Domain.GameStates;

internal static class ScoreAdder
{
    public static Score ApplyTurnResult(this Score score, FaceUpCardCheckStatusEnum cardCheckStatusEnum)
    {
        if (cardCheckStatusEnum is FaceUpCardCheckStatusEnum.PureSuccess or 
                          FaceUpCardCheckStatusEnum.SuccessAfterHint or 
                          FaceUpCardCheckStatusEnum.SuccessAfterMistake)
            return Score.FromInt(score.IntVal + 12);
        
        if (cardCheckStatusEnum is FaceUpCardCheckStatusEnum.FirstMistake or 
                          FaceUpCardCheckStatusEnum.MistakenAgain)
            return Score.FromInt(score.IntVal - 2);
        
        if (cardCheckStatusEnum == FaceUpCardCheckStatusEnum.HintAfterMistake)
            return Score.FromInt(score.IntVal - 6);
        
        if (cardCheckStatusEnum == FaceUpCardCheckStatusEnum.HintWithoutAttempt)
            return Score.FromInt(score.IntVal - 8);
        
        if (cardCheckStatusEnum == FaceUpCardCheckStatusEnum.GaveUp)
            return Score.FromInt(score.IntVal - 8);

        return score;
    }
}
