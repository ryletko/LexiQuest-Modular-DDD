namespace LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

internal class EnglishAnswerChecker : ILanguageAnswerChecker
{
    public AnswerCheckStatusEnum Check(string word, string foreignWord, IReadOnlyCollection<string> synonims)
    {
        var optionalEnglishWords = new[]
                                   {
                                       new[]
                                       {
                                           "smb", "sb", "somebody", "someone", "smth", "something", "sth",
                                           "somebody's", "someone's", "one's", "sb's", "smb's", "somebodys", "someones", "ones", "sbs", "smbs",
                                           "a", "the", "an"
                                       }
                                   };

        if (foreignWord.Split(';').Any(x => Match(word, x, optionalEnglishWords)))
            return AnswerCheckStatusEnum.Success;

        if (synonims.Any(x => Match(word, x, optionalEnglishWords)))
            return AnswerCheckStatusEnum.Synonim;

        return AnswerCheckStatusEnum.Mistake;
    }

    private bool Match(string str1, string str2, string[][] keywords) => Matching.Match(ToShorts(str1), ToShorts(str2), keywords);

    private string ToShorts(string str)
    {
        var sh = new[]
                 {
                     new[] {"'ll ", " will "},
                     new[] {"'s ", " is "},
                     new[] {"'re ", " are "},
                     new[] {"'d ", " would "}
                 };
        return sh.Aggregate(str, (x, y) => x.Replace(y[1], y[0]));
    }
}