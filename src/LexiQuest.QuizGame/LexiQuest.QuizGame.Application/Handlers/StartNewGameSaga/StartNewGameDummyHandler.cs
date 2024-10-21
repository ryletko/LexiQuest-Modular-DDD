using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Contracts.Commands;
using Serilog;

namespace LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga;

// public class StartNewGameDummyHandler(ILogger logger): CommandHandlerBase<StartNewGameCommand>, IEventBusMessageHandler
// {
//     public override async Task Handle(StartNewGameCommand command, CancellationToken cancellationToken)
//     {
//         logger.Information("StartNewGameDummyHandler WORKED");
//     }
// }