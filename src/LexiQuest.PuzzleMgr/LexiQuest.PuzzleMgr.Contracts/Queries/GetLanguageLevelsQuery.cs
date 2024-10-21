using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Contracts.Queries;

public record GetLanguageLevelsQuery : QueryBase<GetLanguageLevelsQueryResult>
{
}

public record GetLanguageLevelsQueryResult(List<GetLanguageLevelsQueryResult.LanguageLevelItem> Items)
{
    public record LanguageLevelItem
    {
        public Language Language { get; set; }
        public string LevelName { get; set; }
    }
}