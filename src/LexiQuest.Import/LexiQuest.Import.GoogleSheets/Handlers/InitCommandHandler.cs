using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Import.GoogleSheets.Commands;
using LexiQuest.Import.GoogleSheets.Events;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.Import.GoogleSheets.Handlers;

internal class InitCommandHandler(GoogleImportDbContext db,
                                  IEventBus eventBus) : CommandHandlerBase<InitializeImport>
{
    public override async Task Handle(InitializeImport command, CancellationToken cancellationToken)
    {
        var importSource = await db.ImportSources.Where(x => x.ImporterId == command.MessageContext.UserId && x.Id == command.ImportSourceId).FirstAsync(cancellationToken);
        await eventBus.SendEvent(new ImportInitialized(command.ImportSourceId,
                                                       importSource.Url,
                                                       importSource.Language));
    }
}