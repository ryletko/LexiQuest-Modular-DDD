using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.FaceUpCards.Rules;

public class FaceUpCardNotCompleted(FaceUpCard faceUpCard) : BusinessRule
{
    protected override bool Rule() => faceUpCard.CompletedAt == null;
    public override string ErrorMessage => "Face-up card can't receive result because it's completed";
}