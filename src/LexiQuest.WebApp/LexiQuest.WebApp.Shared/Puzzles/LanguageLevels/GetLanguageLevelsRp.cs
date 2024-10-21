using LexiQuest.Shared.Puzzle;

namespace LexiQuest.WebApp.Shared.LanguageLevels;

public class GetLanguageLevelsRp
{
    public Language Language { get; set; }
    public string LevelName { get; set; }
}