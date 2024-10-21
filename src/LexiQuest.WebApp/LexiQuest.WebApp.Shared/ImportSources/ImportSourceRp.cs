using LexiQuest.Shared.Puzzle;

namespace LexiQuest.WebApp.Shared.ImportSources;

public class ImportSourceRp
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public Language Language { get; set; }
}