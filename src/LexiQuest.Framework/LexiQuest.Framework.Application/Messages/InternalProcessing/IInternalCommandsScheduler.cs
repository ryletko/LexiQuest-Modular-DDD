using LexiQuest.Framework.Application.Messages.Commands;

namespace LexiQuest.Framework.Application.Messages.InternalProcessing;

public interface IInternalCommandsScheduler
{
    Task EnqueueAsync(ICommand command);

    Task EnqueueAsync<T>(ICommand<T> command) where T : class;
}