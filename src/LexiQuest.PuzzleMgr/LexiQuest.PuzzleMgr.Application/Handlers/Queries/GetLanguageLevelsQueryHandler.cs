using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.PuzzleMgr.Contracts.Queries;
using LexiQuest.PuzzleMgr.Domain.Puzzles;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.PuzzleMgr.Application.Handlers.Queries;

internal class GetLanguageLevelsQueryHandler(IQueryContext queryContext): QueryHandlerBase<GetLanguageLevelsQuery, GetLanguageLevelsQueryResult>, IEventBusMessageHandler
{
    public override async Task<GetLanguageLevelsQueryResult> Handle(GetLanguageLevelsQuery query, CancellationToken cancellationToken = default)
    {
        var result = await queryContext.Query<LanguageLevel>()
                                       .Select(x => new GetLanguageLevelsQueryResult.LanguageLevelItem()
                                                    {
                                                        Language = x.Language,
                                                        LevelName = x.TextRepresentation
                                                    })
                                       .AsNoTracking()
                                       .ToListAsync(cancellationToken: cancellationToken);
        return new GetLanguageLevelsQueryResult(result);
    }
}