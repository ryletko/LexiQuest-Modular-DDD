using LexiQuest.PuzzleMgr.Domain.Puzzles;
using LexiQuest.PuzzleMgr.Infrastructure.DataAccess;
using LexiQuest.Shared.Puzzle;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.PuzzleMgr.Infrastructure.Domain.LanguageLevels;

internal class LanguageLevelRepository(PuzzleMgrDbContext db) : ILanguageLevelRepository
{
    public async Task<LanguageLevel?> GetByName(Language language, string name, CancellationToken cancellationToken = default)
    {
        return await db.LanguageLevels.FirstOrDefaultAsync(x => x.Language == language && x.TextRepresentation == name, cancellationToken);
    }
}