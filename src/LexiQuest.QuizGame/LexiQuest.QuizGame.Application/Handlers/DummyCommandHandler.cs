
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.QuizGame.Contracts;
using MassTransit;

namespace LexiQuest.QuizGame.Application.Handlers;

public class DummyCommandHandler(): IConsumer<DummyCommand>, IEventBusMessageHandler
{
    public async Task Consume(ConsumeContext<DummyCommand> context)
    {
    }
}