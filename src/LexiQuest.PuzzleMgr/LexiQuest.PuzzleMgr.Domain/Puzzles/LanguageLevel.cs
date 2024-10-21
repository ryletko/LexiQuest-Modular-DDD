using LexiQuest.Framework.Domain;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles;

public class LanguageLevel: ValueObject
{
    
    private LanguageLevel() {}
     
    
    public Language Language { get; }
    public string TextRepresentation { get; }
    
}