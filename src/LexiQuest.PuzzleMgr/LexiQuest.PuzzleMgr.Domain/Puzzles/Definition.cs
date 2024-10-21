using LexiQuest.Framework.Domain;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles;

public class Definition: ValueObject
{
    private Definition() {}
    
    private Definition(string text)
    {
        // validation 
        
        if (String.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Definition text cannot be empty", nameof(text));
            
        Text = text;
    }

    public string Text { get; }

    public override string ToString() => Text;

    public static Definition FromString(string definition) => new Definition(definition);
}