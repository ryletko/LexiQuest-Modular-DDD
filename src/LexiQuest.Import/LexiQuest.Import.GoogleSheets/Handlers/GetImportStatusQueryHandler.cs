using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.Import.GoogleSheets.Contracts.Queries;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.Import.GoogleSheets.Handlers;

internal class GetImportStatusQueryHandler(GoogleImportDbContext db) : QueryHandlerBase<GetImportStatusQuery, GetImportStatusQueryResult>, IEventBusMessageHandler
{
    public override async Task<GetImportStatusQueryResult> Handle(GetImportStatusQuery query, CancellationToken cancellationToken = default)
    {
        var result = await db.ImportSagaData.FirstOrDefaultAsync(x => x.ImporterId == query.MessageContext.UserId && x.CorrelationId == query.ImportId, cancellationToken);
        return new GetImportStatusQueryResult()
               {
                   Found  = result != null,
                   Status = result?.CurrentState
               };
    }
}