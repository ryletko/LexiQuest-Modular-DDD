using LexiQuest.Framework.Application.DataAccess;
using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.Import.GoogleSheets.Contracts.Queries;
using LexiQuest.Import.GoogleSheets.Model;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.Import.GoogleSheets.Handlers;

internal class GetImportSourcesQueryHandler(IQueryContext queryContext) : QueryHandlerBase<GetImportSourcesQuery, GetImportSourcesQueryResult>, IEventBusMessageHandler
{
    public override async Task<GetImportSourcesQueryResult> Handle(GetImportSourcesQuery query, CancellationToken cancellationToken = default)
    {
        var result = await queryContext.Query<ImportSource>()
                                       .Where(x => x.ImporterId == query.ImporterId)
                                       .Select(x => new GetImportSourcesQueryResult.GetImportSourcesQueryResultItem(x.Id, x.Name, x.Url, x.Language))
                                       .ToListAsync(cancellationToken);
        return new GetImportSourcesQueryResult(result);
    }
}