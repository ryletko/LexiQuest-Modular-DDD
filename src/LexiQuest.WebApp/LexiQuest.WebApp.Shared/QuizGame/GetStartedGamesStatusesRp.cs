namespace LexiQuest.WebApp.Shared.QuizGame;

public class GetStartedGamesStatusesRp
{
    public Guid Id { get; set; }
    public DateTimeOffset RequestedAt { get; set; }
    public string Status { get; set; }
}