using System.Threading.Tasks;
using LexiQuest.Import.GoogleSheets.Contracts.Events;
using LexiQuest.WebApp.Data;
using LexiQuest.WebApp.Hubs;
using LexiQuest.WebApp.Shared.Import;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.WebApp.EventBus.Handlers;

internal class ImportStateChangedHandler(WebAppDbContext db,
                                         IHubContext<ImportPuzzlesStateHub> importHub) : IConsumer<ImportStatusChangedEvent>, IConsumer<ImportCompletedEvent>
{
    public async Task Consume(ConsumeContext<ImportStatusChangedEvent> context)
    {
        var import = await db.ImportStatuses.FirstOrDefaultAsync(x => x.Id == context.Message.ImportId);
        if (import != null)
        {
            import.Status = context.Message.Status;
            await db.SaveChangesAsync(context.CancellationToken);
            await importHub.Clients.User(import.ImporterId).SendAsync("ImportStatusUpdate",
                                                                      new ImportStatusUpdateDto()
                                                                      {
                                                                          ImportId  = import.Id,
                                                                          Status    = import.Status,
                                                                          Completed = import.Completed
                                                                      });
        }
    }

    public async Task Consume(ConsumeContext<ImportCompletedEvent> context)
    {
        var import = await db.ImportStatuses.FirstOrDefaultAsync(x => x.Id == context.Message.ImportId);
        if (import != null)
        {
            import.Status    = "Completed";
            import.Completed = true;
            await db.SaveChangesAsync(context.CancellationToken);
            await importHub.Clients.User(import.ImporterId).SendAsync("ImportStatusUpdate",
                                                                      new ImportStatusUpdateDto()
                                                                      {
                                                                          ImportId  = import.Id,
                                                                          Status    = import.Status,
                                                                          Completed = import.Completed
                                                                      });
        }
    }
    
}