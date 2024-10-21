using System;
using System.Threading.Tasks;
using LexiQuest.Framework.Application.Messages.EventBus;
using LexiQuest.QuizGame.Contracts.Commands;
using LexiQuest.WebApp.Shared.QuizGame;
using LexiQuest.WebApp.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexiQuest.WebApp.Controllers.QuizGame;

[ApiController]
[Authorize]
public class FaceUpCardsController(IEventBus eventBus) : BaseController
{
    [HttpPost("api/quizgame/faceupcards/{cardId}/answer")]
    public async Task<IActionResult> SubmitAnswer(Guid cardId, [FromBody] SubmitAnswerRq model)
    {
        var commandId = await eventBus.ExecCommand(new SubmitAnswerCommand(cardId, model.Answer));
        return Ok(commandId);
    }
}