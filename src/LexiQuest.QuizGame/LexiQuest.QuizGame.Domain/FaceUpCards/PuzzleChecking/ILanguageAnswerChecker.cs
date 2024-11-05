namespace LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

internal interface ILanguageAnswerChecker
{
    AnswerCheckStatusEnum Check(string word, string foreignWord, IReadOnlyCollection<string> synonims);
}