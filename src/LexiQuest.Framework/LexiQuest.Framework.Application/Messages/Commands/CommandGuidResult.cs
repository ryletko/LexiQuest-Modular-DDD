using LexiQuest.Framework.Application.Messages.Context;

namespace LexiQuest.Framework.Application.Messages.Commands;

public record CommandGuidResult(Guid Guid) : IContextedMessage
{
    public MessageContext? MessageContext { get; set; }
}