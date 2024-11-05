using LexiQuest.Shared.Puzzle;

namespace LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

internal class AnswerChecker
{
    private static readonly Lazy<EnglishAnswerChecker> EnglishCheckerLazy = new(() => new EnglishAnswerChecker());
    private ILanguageAnswerChecker EnglishChecker => EnglishCheckerLazy.Value;

    private static readonly Lazy<GermanAnswerChecker> GermanCheckerLazy = new(() => new GermanAnswerChecker());
    private ILanguageAnswerChecker GermanChecker => GermanCheckerLazy.Value;

    public AnswerCheckStatusEnum Check(FaceUpCardAnswer answer, FaceUpCardPuzzleInfo puzzle)
    {
        if (answer.IsEmpty())
            return AnswerCheckStatusEnum.GiveUp;
        
        ILanguageAnswerChecker checker;
        if (puzzle.Language == Language.English)
            checker = EnglishChecker;
        else if (puzzle.Language == Language.German)
            checker = GermanChecker;
        else
            throw new InvalidOperationException("Unsupported language");

        var s = new string[] { };
        
        return checker.Check(answer.ToString(), puzzle.ForeignWord, puzzle.Synonims);
    }
}