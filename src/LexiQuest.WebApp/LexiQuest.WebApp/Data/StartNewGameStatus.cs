using System;

namespace LexiQuest.WebApp.Data;

public class StartNewGameStatus
{
    public StartNewGameStatus(Guid id, string playerId)
    {
        Id        = id;
        PlayerId  = playerId;
        Timestamp = DateTimeOffset.UtcNow;
        Status    = "Request sent";
    }

    private StartNewGameStatus()
    {
    }

    public Guid Id { get; }
    public string PlayerId { get; }
    public DateTimeOffset Timestamp { get; }
    public string Status { get; set; }
    public bool Completed { get; set; }
    public bool Refused { get; set; }
    public Guid? GameId { get; set; }
}