using System;

namespace LexiQuest.WebApp.Data;

public class ImportStatus
{
    public ImportStatus(Guid id, string importerId)
    {
        Id         = id;
        ImporterId = importerId;
        Timestamp  = DateTimeOffset.UtcNow;
        Status     = "Created";
    }

    private ImportStatus()
    {
    }

    public Guid Id { get; }
    public string ImporterId { get; }
    public DateTimeOffset Timestamp { get; }
    public string Status { get; set; }
    public bool Completed { get; set; }
}