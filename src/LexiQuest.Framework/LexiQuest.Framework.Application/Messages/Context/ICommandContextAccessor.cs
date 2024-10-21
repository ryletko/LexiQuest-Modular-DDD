namespace LexiQuest.Framework.Application.Messages.Context;

public interface ICommandContextAccessor
{
    public MessageContext? CurrentContext { get; set; }
}