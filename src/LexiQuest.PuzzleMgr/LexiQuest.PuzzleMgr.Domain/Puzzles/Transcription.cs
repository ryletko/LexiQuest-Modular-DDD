using LexiQuest.Framework.Domain;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles;

public class Transcription: ValueObject
{
    private Transcription() {}
    
    private Transcription(string transcription)
    {
        // validation 
        
        if (String.IsNullOrWhiteSpace(transcription))
            throw new ArgumentException("Transcription can't be empty", nameof(transcription));
        
        StrVal = transcription;
    }

    public string StrVal { get; }

    public override string ToString() => StrVal;

    public static Transcription FromString(string transcription) => new(transcription);
    
}