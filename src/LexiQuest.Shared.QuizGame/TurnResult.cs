namespace LexiQuest.Shared.QuizGame;

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