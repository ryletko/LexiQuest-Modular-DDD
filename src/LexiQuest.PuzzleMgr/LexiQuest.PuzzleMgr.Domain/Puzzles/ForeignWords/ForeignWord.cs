using LexiQuest.Framework.Domain;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords;

public class ForeignWord : ValueObject
{
    private ForeignWord() {}
    
    private ForeignWord(string word, Language lang)
    {
        Text = word;
        Language    = lang;
    }

    public Language Language { get; }
    public string Text { get; }

    public override string ToString() => Text;

    public static ForeignWord FromString(string word, Language lang)
    {
        var wordValidator = new WordValidator();
        if (!wordValidator.Validate(word, lang))
            throw new InvalidOperationException("Foreign word has wrong format.");
   
        return new ForeignWord(word, lang);
    }
}