using System;
using System.Linq;
using System.Threading.Tasks;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Import.GoogleSheets.Contracts.Commands;
using LexiQuest.WebApp.Data;
using LexiQuest.WebApp.Shared.Import;
using LexiQuest.WebApp.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexiQuest.WebApp.Controllers.Import;

[ApiController]
[Authorize]
public class ImportController(IEventBus eventBus,
                              WebAppDbContext db) : BaseController
{
    [HttpPost("api/[controller]/")]
    public async Task<IActionResult> StartImport(StartImportRq import)
    {
        var importId = await eventBus.ExecCommand(new ImportCommand(import.ImportSourceId));
        db.ImportStatuses.Add(new ImportStatus(importId.Value, GetUserId())
                              {
                                  Status = "Sent to queue"
                              });
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetImportStatusById), new {id = importId.Value}, null);
    }


    [HttpGet("api/[controller]/{id}")]
    public async Task<IActionResult> GetImportStatusById(Guid id)
    {
        var userId = GetUserId();
        var importStatus = await db.ImportStatuses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.ImporterId == userId);
        if (importStatus == null)
            return NotFound();

        return Ok(new ImportStatusRp()
                  {
                      CreatedAt = importStatus.Timestamp,
                      Status    = importStatus.Status
                  });
    }

    [HttpGet("api/[controller]/")]
    public async Task<IActionResult> GetImportStatuses()
    {
        var userId = GetUserId();
        var result = await db.ImportStatuses
                             .Where(x => !x.Completed && x.ImporterId == userId)
                             .Select(x => new GetImportStatusesRp()
                                          {
                                              Id        = x.Id,
                                              StartedAt = x.Timestamp,
                                              Status    = x.Status,
                                          })
                             .AsNoTracking()
                             .ToListAsync();
        return Ok(result);
    }
}
