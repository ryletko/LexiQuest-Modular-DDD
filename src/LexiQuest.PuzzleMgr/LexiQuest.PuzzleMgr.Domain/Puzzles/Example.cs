using LexiQuest.Framework.Domain;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles;

public class Example: ValueObject
{
    private Example() {}
    
    private Example(string text)
    {
        // validation
        
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Example text can't be empty", nameof(text));
        

        Text = text;
    }

    public string Text { get; }

    public override string ToString() => Text;

    public static Example FromString(string example) => new Example(example);

}