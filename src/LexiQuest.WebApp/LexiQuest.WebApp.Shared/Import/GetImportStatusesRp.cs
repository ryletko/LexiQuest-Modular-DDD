namespace LexiQuest.WebApp.Shared.Import;

public class GetImportStatusesRp
{
    public string Status { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public Guid Id { get; set; }
}