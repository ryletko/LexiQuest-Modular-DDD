using LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;

public record ProcessOutboxCommand : IInternalCommand, IRecurringCommand;