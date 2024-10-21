namespace LexiQuest.WebApp.Shared.QuizGame;

public class StartNewGameStatusDto
{
    public Guid Id { get; }
    public DateTimeOffset RequestedAt { get; set; }
    public string Status { get; set; }
    public bool Completed { get; set; }
    public bool Refused { get; set; }
    public Guid? GameId { get; set; }
}