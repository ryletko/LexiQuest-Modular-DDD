using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.FaceUpCards.CardPicking;
using LexiQuest.QuizGame.Domain.FaceUpCards.Events;
using LexiQuest.QuizGame.Domain.FaceUpCards.Hints;
using LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;
using LexiQuest.QuizGame.Domain.FaceUpCards.Rules;
using LexiQuest.QuizGame.Domain.FaceUpCards.SimilarityCalc;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Domain.Players;

namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public class FaceUpCardId(Guid value) : TypedIdValueBase(value);

public class FaceUpCard : Entity, IAggregateRoot
{
    private static readonly AnswerChecker AnswerChecker = new();
    private static readonly HintGenerator HintGenerator = new();
    private static readonly AnswerDistanceCalculator AnswerDistanceCalculator = new();

    public FaceUpCardId Id { get; private init; }
    public GameId GameId { get; private set; }
    public PlayerId PlayerId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public string? Hint { get; private set; }
    public bool Mistaken { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public FaceUpCardPuzzleInfo PuzzleInfo { get; private set; } //ii DDD не про производительность и экономию места 
    public FaceUpCardCheckStatusEnum? LastResult { get; private set; }
    public double AnswerDistance { get; private set; }

    private FaceUpCard()
    {
    }

    public static FaceUpCard CreateNew(RandomLessSolvedCardPicker picker, GameId gameId, PlayerId playerId)
    {
        return new FaceUpCard()
               {
                   Id          = new FaceUpCardId(Guid.NewGuid()),
                   CreatedAt   = SystemClock.Now,
                   GameId      = gameId,
                   PlayerId    = playerId,
                   Hint        = null,
                   Mistaken    = false,
                   CompletedAt = null,
                   PuzzleInfo  = picker.GetNewPuzzle()
               };
    }

    public void GuessAnswer(FaceUpCardAnswer answer)
    {
        BusinessRule.Check(new FaceUpCardNotCompleted(this));

        FaceUpCardCheckStatusEnum cardCheckStatusEnum;
        var foreingWord = PuzzleInfo.ForeignWord;
        var answerCheckResult = AnswerChecker.Check(answer, PuzzleInfo);
        AnswerDistance = AnswerDistanceCalculator.Calculate(answer.ToString(), PuzzleInfo.ForeignWord);

        if (answerCheckResult == AnswerCheckStatusEnum.Success)
        {
            if (Hint != null)
                cardCheckStatusEnum = FaceUpCardCheckStatusEnum.SuccessAfterHint;
            else if (Mistaken)
                cardCheckStatusEnum = FaceUpCardCheckStatusEnum.SuccessAfterMistake;
            else
                cardCheckStatusEnum = FaceUpCardCheckStatusEnum.PureSuccess;

            CompletedAt = SystemClock.Now;
        }
        else if (answerCheckResult == AnswerCheckStatusEnum.Mistake)
        {
            if (!Mistaken)
                cardCheckStatusEnum = FaceUpCardCheckStatusEnum.FirstMistake;
            else
                cardCheckStatusEnum = FaceUpCardCheckStatusEnum.MistakenAgain;

            Mistaken = true;
        }
        else if (answerCheckResult == AnswerCheckStatusEnum.GiveUp)
        {
            if (Hint == null)
            {
                if (!Mistaken)
                    cardCheckStatusEnum = FaceUpCardCheckStatusEnum.HintWithoutAttempt;
                else
                    cardCheckStatusEnum = FaceUpCardCheckStatusEnum.HintAfterMistake;

                Hint = HintGenerator.GenerateHint(foreingWord);
            }
            else
            {
                cardCheckStatusEnum = FaceUpCardCheckStatusEnum.GaveUp;
            }
        }
        else if (answerCheckResult is AnswerCheckStatusEnum.Synonim or AnswerCheckStatusEnum.WrongArticle or AnswerCheckStatusEnum.WrongPluralForm)
        {
            cardCheckStatusEnum = FaceUpCardCheckStatusEnum.MinorMistake;
        }
        else
        {
            throw new InvalidOperationException("Unsupported puzzle result");
        }

        LastResult = cardCheckStatusEnum;

        AddDomainEvent(new FaceUpCardProcessed(GameId, PuzzleInfo.FaceDownCardId, cardCheckStatusEnum));
        if (CompletedAt != null)
            AddDomainEvent(new FaceUpCardCompleted(GameId, cardCheckStatusEnum));
    }
}