using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public class LastAnswerCheckResult: Enumeration
{
    public static readonly LastAnswerCheckResult Success = new SuccessResult();
    public static readonly LastAnswerCheckResult Mistake = new MistakeResult();
    public static readonly LastAnswerCheckResult GiveUp = new GiveUpResult();
    public static readonly LastAnswerCheckResult Synonim = new SynonimResult();
    public static readonly LastAnswerCheckResult WrongArticle = new WrongArticleResult();
    public static readonly LastAnswerCheckResult WrongPluralForm = new WrongPluralFormResult();

    internal LastAnswerCheckResult(int id, string name) : base(id, name)
    {
    }

    private class SuccessResult() : LastAnswerCheckResult(1, "Success");
    private class MistakeResult() : LastAnswerCheckResult(2, "Mistake");
    private class GiveUpResult() : LastAnswerCheckResult(3, "GiveUp");
    private class SynonimResult() : LastAnswerCheckResult(4, "Synonim");
    private class WrongArticleResult() : LastAnswerCheckResult(5, "WrongArticle");
    private class WrongPluralFormResult() : LastAnswerCheckResult(6, "WrongPluralForm");

    public static LastAnswerCheckResult Map(AnswerCheckResult answerCheckResult)
    {
        if (answerCheckResult == AnswerCheckResult.Success)
            return Success;
        if (answerCheckResult == AnswerCheckResult.Mistake)
            return Mistake;
        if (answerCheckResult == AnswerCheckResult.GiveUp)
            return GiveUp;
        if (answerCheckResult == AnswerCheckResult.Synonim)
            return Synonim;
        if (answerCheckResult == AnswerCheckResult.WrongArticle)
            return WrongArticle;
        if (answerCheckResult == AnswerCheckResult.WrongPluralForm)
            return WrongPluralForm;

        throw new InvalidOperationException("Unsupported answer check result.");
    } 
    
}