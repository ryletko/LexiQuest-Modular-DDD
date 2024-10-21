using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Infrastructure.EventBus;

public class CommandContextAccessorImpl : ICommandContextAccessor
{
    public MessageContext? CurrentContext
    {
        get => CommandContextAccessor.CurrentCommandContext;
        set => CommandContextAccessor.CurrentCommandContext = value;
    }
}

// списано с HttpContextAccessor
public static class CommandContextAccessor
{
    private sealed class CommandContextHolder
    {
        public MessageContext? Context;
    }
    
    private static readonly AsyncLocal<CommandContextHolder> _commandContextCurrent = new();

    public static MessageContext? CurrentCommandContext
    {
        get
        {
            return _commandContextCurrent.Value?.Context;
        }
        set
        {
            var holder = _commandContextCurrent.Value;
            if (holder != null)
            {
                holder.Context = null;
            }

            if (value != null)
            {
                _commandContextCurrent.Value = new CommandContextHolder { Context = value };
            }
        }
    } 
}