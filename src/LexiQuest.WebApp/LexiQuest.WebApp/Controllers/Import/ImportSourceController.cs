using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.Import.GoogleSheets.Contracts.Queries;
using LexiQuest.WebApp.Shared.ImportSources;
using LexiQuest.WebApp.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexiQuest.WebApp.Controllers.Import;

[ApiController]
[Authorize]
public class ImportSourcesController(IEventBus eventBus) : BaseController
{
    [HttpPost("api/[controller]/")]
    public async Task<IActionResult> Add()
    {
        throw new NotImplementedException();
    }

    [HttpGet("api/[controller]/")]
    public async Task<IEnumerable<ImportSourceRp>> Get()
    {
        var result = await eventBus.Query<GetImportSourcesQuery, GetImportSourcesQueryResult>(new GetImportSourcesQuery(GetUserId()));
        return result.Items.Select(x => new ImportSourceRp()
                                        {
                                            Id       = x.Id,
                                            Name     = x.Name,
                                            Url      = x.Url,
                                            Language = x.Language
                                        });
    }
}