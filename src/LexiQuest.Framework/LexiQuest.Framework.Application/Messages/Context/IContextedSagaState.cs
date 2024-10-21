namespace LexiQuest.Framework.Application.Messages.Context;

public interface IContextedSagaState
{
    public string UserId { get; set; }
    public Guid CorrelationId { get; set; }
}