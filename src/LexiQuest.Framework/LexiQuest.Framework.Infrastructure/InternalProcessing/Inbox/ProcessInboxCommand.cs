using LexiQuest.Framework.Infrastructure.InternalProcessing.InternalCommands;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Inbox;

internal record ProcessInboxCommand : IInternalCommand, IRecurringCommand;