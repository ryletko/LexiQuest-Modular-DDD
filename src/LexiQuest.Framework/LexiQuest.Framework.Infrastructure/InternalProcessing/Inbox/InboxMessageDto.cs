namespace LexiQuest.Framework.Infrastructure.InternalProcessing.Inbox;

internal class InboxMessageDto
{
    public Guid Id { get; set; }

    public string Type { get; set; }

    public string Data { get; set; }
}