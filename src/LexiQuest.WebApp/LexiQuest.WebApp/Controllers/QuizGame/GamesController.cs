using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiQuest.Framework.Application.EventBus;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.QuizGame.Contracts.Commands;
using LexiQuest.QuizGame.Contracts.Queries;
using LexiQuest.WebApp.Data;
using LexiQuest.WebApp.Shared.QuizGame;
using LexiQuest.WebApp.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utils.Core;

namespace LexiQuest.WebApp.Controllers.QuizGame;

[ApiController]
[Authorize]
public class GamesController(IEventBus eventBus,
                             WebAppDbContext db) : BaseController
{
    [HttpGet("api/quizgame/games/started")]
    public async Task<IEnumerable<GetStartedGamesRp>> GetStartedGames()
    {
        return Map(await eventBus.Query<GetStartedGamesQuery, GetStartedGamesQueryResult>(new GetStartedGamesQuery()));
    }

    [HttpGet("api/quizgame/games/started/statuses")]
    public async Task<IEnumerable<GetStartedGamesStatusesRp>> GetStartedGamesStatuses()
    {
        return Map(await db.StartNewGameStatuses.Where(x => !x.Completed && !x.Refused).ToListAsync());
    }

    [HttpGet("api/quizgame/games/started/statuses/{id}")]
    public async Task<IActionResult> GetStartedGamesStatusById(Guid id)
    {
        var foundStatus = await db.StartNewGameStatuses.FirstOrDefaultAsync(x => x.Id == id);
        if (foundStatus == null)
            return NotFound();

        return Ok(Map(foundStatus));
    }

    [HttpPost("api/quizgame/games/started")]
    public async Task<IActionResult> StartNewGame()
    {
        var startNewGameId = await eventBus.ExecCommand(new StartNewGameCommand());
        var startNewGameStatus = new StartNewGameStatus(startNewGameId.Value, GetUserId());
        db.StartNewGameStatuses.Add(startNewGameStatus);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStartedGamesStatusById), new {id = startNewGameId}, new StartNewGameRp() {Id = startNewGameId.Value});

        // await bus.Publish(new AddNewPuzzlesCommand([
        //                       new AddNewPuzzlesCommand.AddPuzzleItem("der Apfel",
        //                                                              PartsOfSpeech.Noun,
        //                                                              "apfel",
        //                                                              "урок",
        //                                                              Language.German,
        //                                                              ["an apple"],
        //                                                              [],
        //                                                              [],
        //                                                              "A1")
        //                   ])
        //                   {
        //                       MessageContext = new MessageContext(GetUserId(), Guid.NewGuid())
        //                   });

        // await bus.Publish(new PzlDummyCommand("TEST")
        //                   {
        //                       MessageContext = new MessageContext(GetUserId(), Guid.NewGuid())
        //                   });

        // await bus.Publish(new DummyCommand("TEST"));
        // await publishEndpoint.Publish(new DummyCommand("TEST"));
        // await publishEndpoint.Publish(new PzlDummyCommand("TEST")
        //                               {
        //                                   MessageContext = new MessageContext(GetUserId(), Guid.NewGuid())
        //                               });
        //var requestClient = clientFactory.CreateRequestClient<DummyCommand>();
        //var response = await requestClient.GetResponse<String>(new DummyCommand());
    }


    [HttpGet("api/quizgame/games/{id}")]
    public async Task<IActionResult> GetGameById(Guid id)
    {
        var game = await eventBus.Query<GetGameById, GetGameByIdResult>(new GetGameById(id));
        if (!game.Found || game.Game == null)
            return NotFound();

        return Ok(Map(game.Game));
    }

    private IEnumerable<GetStartedGamesRp> Map(GetStartedGamesQueryResult startedGames) =>
        startedGames.StartedGames.Select(x => new GetStartedGamesRp()
                                              {
                                                  Guid = x.GameId
                                              });

    private GetStartedGamesStatusesRp Map(StartNewGameStatus startNewGamesStatus) =>
        new()
        {
            Id          = startNewGamesStatus.Id,
            Status      = startNewGamesStatus.Status,
            RequestedAt = startNewGamesStatus.Timestamp
        };

    private IEnumerable<GetStartedGamesStatusesRp> Map(IEnumerable<StartNewGameStatus> startNewGamesStatuses) =>
        startNewGamesStatuses.Select(Map);

    private GetGameByIdRp Map(GetGameByIdResult.GameState game) =>
        game.Map(g => new GetGameByIdRp(
                     g.GameId,
                     g.CreatedTimestamp,
                     g.Status,
                     g.Score,
                     g.CardDeckId,
                     g.CurrentFaceUpCard.Map(t => new GetGameByIdRp.FaceUpCard(t.FaceUpCardId, t.Hint, t.IsMistaken, t.LastResult)),
                     g.CurrentPuzzle.Map(p => new GetGameByIdRp.Puzzle(
                                             p.PuzzleId,
                                             p.ForeignWord,
                                             p.PartsOfSpeech,
                                             p.Transcription,
                                             p.From,
                                             p.Language,
                                             p.Definitions,
                                             p.Synonims,
                                             p.Examples,
                                             p.Level
                                         )),
                     g.PreviousFaceUpCard?.Map(t => new GetGameByIdRp.FaceUpCard(t.FaceUpCardId, t.Hint, t.IsMistaken, t.LastResult)),
                     g.PreviousPuzzle?.Map(p => new GetGameByIdRp.Puzzle(
                                               p.PuzzleId,
                                               p.ForeignWord,
                                               p.PartsOfSpeech,
                                               p.Transcription,
                                               p.From,
                                               p.Language,
                                               p.Definitions,
                                               p.Synonims,
                                               p.Examples,
                                               p.Level
                                           )),
                     g.TotalCardsNumber,
                     g.CompletedCardsNumber));

    [HttpPost("api/quizgame/games/{id}/finish")]
    public async Task FinishGame(Guid id)
    {
        await eventBus.ExecCommand(new FinishGameCommand(id));
    }
}
