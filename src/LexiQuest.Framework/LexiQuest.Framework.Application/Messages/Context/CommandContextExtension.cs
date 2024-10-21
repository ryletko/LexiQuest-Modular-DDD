using Utils.Core;

namespace LexiQuest.Framework.Application.Messages.Context;

public static class CommandContextExtension
{
    public static T1 ContextFrom<T1, T2>(this T1 command, T2 from, Guid? newCorrelationId = null) where T1 : IContextedMessage
                                                                                                  where T2 : IContextedMessage
    {
        command.MessageContext = from.MessageContext.Map(c => new MessageContext(from.MessageContext.UserId, newCorrelationId ?? from.MessageContext.CorrelationId));
        return command;
    }

    public static T1 ContextFrom<T1, T2>(this T1 command, T2 from) where T1 : IContextedMessage
                                                                   where T2 : IContextedSagaState
    {
        command.MessageContext = new MessageContext(from.UserId, from.CorrelationId);
        return command;
    }
}