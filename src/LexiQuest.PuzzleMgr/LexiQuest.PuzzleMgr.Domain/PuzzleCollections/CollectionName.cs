using LexiQuest.Framework.Domain;

namespace LexiQuest.PuzzleMgr.Domain.PuzzleCollections;

public class CollectionName: ValueObject
{
    private CollectionName()
    {
        
    }
    
    private CollectionName(string text)
    {
        // validation
        
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Collection name can't be empty", nameof(text));
        

        Text = text;
    }

    public string Text { get; }

    public override string ToString() => Text;

    public static CollectionName FromString(string example) => new CollectionName(example);

}