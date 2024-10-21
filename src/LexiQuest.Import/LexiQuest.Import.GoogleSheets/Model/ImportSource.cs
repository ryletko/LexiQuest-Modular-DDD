using LexiQuest.Shared.Puzzle;

namespace LexiQuest.Import.GoogleSheets.Model;

public class ImportSource
{
    
    public Guid Id { get; set; }
    public string ImporterId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public Language Language { get; set; }
}