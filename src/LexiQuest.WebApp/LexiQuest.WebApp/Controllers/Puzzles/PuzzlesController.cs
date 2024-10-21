using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.PuzzleMgr.Contracts.Commands;
using LexiQuest.PuzzleMgr.Contracts.Queries;
using LexiQuest.WebApp.Shared.Puzzles;
using LexiQuest.WebApp.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexiQuest.WebApp.Controllers.Puzzles;

[ApiController]
[Authorize]
public class PuzzlesController(IEventBus eventBus) : BaseController
{
    [HttpGet("api/[controller]/")]
    public async Task<IEnumerable<GetPuzzlesRp>> GetPuzzles()
    {
        return Mapper.Map(await eventBus.Query<GetPuzzlesForCurrentOwnerQuery, GetPuzzlesForCurrentOwnerQueryResult>(new GetPuzzlesForCurrentOwnerQuery()));
    }

    [HttpPost("api/[controller]/")]
    public async Task Post(AddNewPuzzleRq puzzle)
    {
    }

    [HttpDelete("api/[controller]/")]
    public async Task DeleteAllForCurrentUser()
    {
        await eventBus.ExecCommand(new DeleteAllForOwnerCommand());
    }

    private static class Mapper
    {
        public static IEnumerable<GetPuzzlesRp> Map(GetPuzzlesForCurrentOwnerQueryResult puzzles) =>
            puzzles.PuzzleItems.Select(x => new GetPuzzlesRp()
                                            {
                                                PuzzleId      = x.PuzzleId,
                                                ForeignWord   = x.ForeignWord,
                                                Language      = x.Language,
                                                Definitions   = x.Definitions,
                                                Examples      = x.Examples,
                                                Synonims      = x.Synonims,
                                                Level         = x.Level,
                                                Transcription = x.Transcription,
                                                PartsOfSpeech = x.PartsOfSpeech
                                            });
    }
}