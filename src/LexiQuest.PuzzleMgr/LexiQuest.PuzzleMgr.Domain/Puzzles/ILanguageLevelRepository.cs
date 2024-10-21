using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles;

public interface ILanguageLevelRepository
{
    Task<LanguageLevel?> GetByName(Language language, string name, CancellationToken cancellationToken = default);
}