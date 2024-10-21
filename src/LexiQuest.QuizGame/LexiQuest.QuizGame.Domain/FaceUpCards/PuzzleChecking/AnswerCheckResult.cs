namespace LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

// public class AnswerCheckResult: Enumeration
// {
//     public static readonly AnswerCheckResult Success = new SuccessResult();
//     public static readonly AnswerCheckResult Mistake = new MistakeResult();
//     public static readonly AnswerCheckResult GiveUp = new GiveUpResult();
//     public static readonly AnswerCheckResult Synonim = new SynonimResult();
//     public static readonly AnswerCheckResult WrongArticle = new WrongArticleResult();
//     public static readonly AnswerCheckResult WrongPluralForm = new WrongPluralFormResult();
//
//     internal AnswerCheckResult(int id, string name) : base(id, name)
//     {
//     }
//
//     private class SuccessResult() : AnswerCheckResult(1, "Success");
//     private class MistakeResult() : AnswerCheckResult(2, "Mistake");
//     private class GiveUpResult() : AnswerCheckResult(3, "GiveUp");
//     private class SynonimResult() : AnswerCheckResult(4, "Synonim");
//     private class WrongArticleResult() : AnswerCheckResult(5, "WrongArticle");
//     private class WrongPluralFormResult() : AnswerCheckResult(6, "WrongPluralForm");
//     
// }

public enum AnswerCheckResult
{
    Success,
    Mistake,
    GiveUp,
    Synonim,
    WrongArticle,
    WrongPluralForm
} 