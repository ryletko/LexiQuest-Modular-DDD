using LexiQuest.Shared.Puzzle;
using LexiQuest.Shared.QuizGame;

namespace LexiQuest.WebApp.Shared.QuizGame;

public record GetGameByIdRp(Guid GameId,
                            DateTimeOffset CreatedTimestamp,
                            string Status,
                            int Score,
                            Guid CardDeckId,
                            GetGameByIdRp.FaceUpCard CurrentFaceUpCard,
                            GetGameByIdRp.Puzzle CurrentPuzzle,
                            GetGameByIdRp.FaceUpCard? PreviousFaceUpCard,
                            GetGameByIdRp.Puzzle? PreviousPuzzle,
                            int TotalCardsNumber,
                            int CompletedCardsNumber)
{
    public record Puzzle(Guid PuzzleId,
                         string ForeignWord,
                         string PartsOfSpeech,
                         string? Transcription,
                         string? From,
                         Language Language,
                         List<string> Definitions,
                         List<string> Synonims,
                         List<string> Examples,
                         string? Level);

    public record FaceUpCard(Guid CardId,
                             string? Hint,
                             bool IsMistaken,
                             FaceUpCardCheckResult? LastResult,
                             double AnswerDistance);
}