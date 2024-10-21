namespace LexiQuest.Framework.Application.Messages.Context;

public interface IContextedMessage
{
    MessageContext? MessageContext { get; set; }
}


