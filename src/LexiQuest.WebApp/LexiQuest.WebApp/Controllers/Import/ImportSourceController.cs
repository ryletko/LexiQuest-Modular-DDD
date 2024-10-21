using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LexiQuest.WebApp.Shared.ImportSources;
using LexiQuest.WebApp.WebApi;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexiQuest.WebApp.Controllers.Import;

[ApiController]
[Authorize]
public class ImportSourcesController(IBus bus) : BaseController
{

    [HttpPost("api/[controller]/")]
    public async Task<IActionResult> Add()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("api/[controller]/")]
    public async Task<IEnumerable<ImportSourceRp>> Get()
    {
        throw new NotImplementedException();
    }
}