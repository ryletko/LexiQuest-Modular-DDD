using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.FaceUpCards.CardPicking;
using LexiQuest.QuizGame.Domain.FaceUpCards.Events;
using LexiQuest.QuizGame.Domain.FaceUpCards.Hints;
using LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;
using LexiQuest.QuizGame.Domain.FaceUpCards.Rules;
using LexiQuest.QuizGame.Domain.GameStates;
using LexiQuest.QuizGame.Domain.Players;

namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public class FaceUpCardId(Guid value) : TypedIdValueBase(value);

public class FaceUpCard : Entity, IAggregateRoot
{
    private static readonly AnswerChecker AnswerChecker = new();
    private static readonly HintGenerator HintGenerator = new();

    public FaceUpCardId Id { get; private init; }
    public GameId GameId { get; private set; }
    public PlayerId PlayerId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public string? Hint { get; private set; }
    public bool Mistaken { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public FaceUpCardPuzzleInfo PuzzleInfo { get; private set; } //ii DDD не про производительность и экономию места 
    public FaceUpCardCheckResult? LastResult { get; private set; }

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

        FaceUpCardCheckResult cardCheckResult;
        var foreingWord = PuzzleInfo.ForeignWord;
        var answerCheckResult = AnswerChecker.Check(answer, PuzzleInfo);

        if (answerCheckResult == AnswerCheckResult.Success)
        {
            if (Hint != null)
                cardCheckResult = FaceUpCardCheckResult.SuccessAfterHint;
            else if (Mistaken)
                cardCheckResult = FaceUpCardCheckResult.SuccessAfterMistake;
            else
                cardCheckResult = FaceUpCardCheckResult.PureSuccess;

            CompletedAt = SystemClock.Now;
        }
        else if (answerCheckResult == AnswerCheckResult.Mistake)
        {
            if (!Mistaken)
                cardCheckResult = FaceUpCardCheckResult.FirstMistake;
            else
                cardCheckResult = FaceUpCardCheckResult.MistakenAgain;

            Mistaken = true;
        }
        else if (answerCheckResult == AnswerCheckResult.GiveUp)
        {
            if (Hint == null)
            {
                if (!Mistaken)
                    cardCheckResult = FaceUpCardCheckResult.HintWithoutAttempt;
                else
                    cardCheckResult = FaceUpCardCheckResult.HintAfterMistake;

                Hint = HintGenerator.GenerateHint(foreingWord);
            }
            else
            {
                cardCheckResult = FaceUpCardCheckResult.GaveUp;
            }
        }
        else if (answerCheckResult is AnswerCheckResult.Synonim or AnswerCheckResult.WrongArticle or AnswerCheckResult.WrongPluralForm)
        {
            cardCheckResult = FaceUpCardCheckResult.MinorMistake;
        }
        else
        {
            throw new InvalidOperationException("Unsupported puzzle result");
        }

        LastResult = cardCheckResult;

        AddDomainEvent(new FaceUpCardProcessed(GameId, PuzzleInfo.FaceDownCardId, cardCheckResult));
        if (CompletedAt != null)
            AddDomainEvent(new FaceUpCardCompleted(GameId, cardCheckResult));
    }
}