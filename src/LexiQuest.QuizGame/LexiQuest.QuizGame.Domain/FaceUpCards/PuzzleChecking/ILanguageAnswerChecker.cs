namespace LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

internal interface ILanguageAnswerChecker
{
    AnswerCheckResult Check(string word, string foreignWord, IReadOnlyCollection<string> synonims);
}