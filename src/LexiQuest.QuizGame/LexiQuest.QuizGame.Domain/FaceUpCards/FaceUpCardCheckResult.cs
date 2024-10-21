namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public enum FaceUpCardCheckResult
{
    PureSuccess,
    SuccessAfterMistake,
    FirstMistake,
    MistakenAgain,
    HintWithoutAttempt,
    HintAfterMistake,
    SuccessAfterHint,
    GaveUp,
    MinorMistake
}