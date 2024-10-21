using LexiQuest.Shared.Puzzle;

namespace LexiQuest.WebApp.Shared.ImportSources;

public class AddImportSourceRq
{
    public string Url { get; set; }
    public Language Language { get; set; }
}