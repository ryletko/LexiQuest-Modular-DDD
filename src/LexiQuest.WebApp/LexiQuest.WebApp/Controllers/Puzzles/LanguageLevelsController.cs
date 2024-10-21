using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.PuzzleMgr.Contracts.Queries;
using LexiQuest.WebApp.Shared.LanguageLevels;
using LexiQuest.WebApp.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexiQuest.WebApp.Controllers;

[ApiController]
[Authorize]
public class LanguageLevelsController(IEventBus eventBus) : BaseController
{
    [HttpGet("api/[controller]/")]
    public async Task<IEnumerable<GetLanguageLevelsRp>> GetLanguageLevels()
    {
        return Mapper.Map(await eventBus.Query<GetLanguageLevelsQuery, GetLanguageLevelsQueryResult>(new GetLanguageLevelsQuery()));
    }


    private static class Mapper
    {
        public static IEnumerable<GetLanguageLevelsRp> Map(GetLanguageLevelsQueryResult languageLevels) =>
            languageLevels.Items.Select(x => new GetLanguageLevelsRp()
                                             {
                                                 Language  = x.Language,
                                                 LevelName = x.LevelName
                                             });
    }
}