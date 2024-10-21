namespace LexiQuest.WebApp.Shared.Import;

public class ImportStatusUpdateDto
{
    public Guid ImportId { get; set; }
    public string Status { get; set; }
    public bool Completed { get; set; }
}