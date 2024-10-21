namespace LexiQuest.Framework.Application.Messages.Context;

public record MessageContext(string UserId, Guid CorrelationId);