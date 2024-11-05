namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public enum FaceUpCardCheckStatusEnum
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