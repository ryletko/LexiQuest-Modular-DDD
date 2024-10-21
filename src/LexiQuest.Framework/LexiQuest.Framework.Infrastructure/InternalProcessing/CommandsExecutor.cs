using LexiQuest.Framework.Infrastructure.Dependencies;
using LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;
using MassTransit;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing;

public class CommandsExecutor(ICompositionRoot compositionRoot)
{
    public async Task Execute<T>(T command) where T : IInternalCommand
    {
        await using (var scope = compositionRoot.BeginScope())
        {
            var mediator = scope.GetMediator();
            await mediator.Send(command);
        }
    }

    public async Task<TResult> Execute<T, TResult>(T request) where T : IInternalCommand<TResult> where TResult : class
    {
        await using (var scope = compositionRoot.BeginScope())
        {
            var mediator = scope.GetMediator();
            return await mediator.SendRequest(request);
        }
    }
}