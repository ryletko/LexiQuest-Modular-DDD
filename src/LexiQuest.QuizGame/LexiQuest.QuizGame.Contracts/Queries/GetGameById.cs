using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Shared.Puzzle;
using LexiQuest.Shared.QuizGame;

namespace LexiQuest.QuizGame.Contracts.Queries;

public record GetGameById(Guid GameId) : QueryBase<GetGameByIdResult>;

public record GetGameByIdResult(bool Found,
                                GetGameByIdResult.GameState? Game)
{
    public record GameState(Guid GameId,
                            DateTimeOffset CreatedTimestamp,
                            string Status,
                            int Score,
                            Guid CardDeckId,
                            FaceUpCard CurrentFaceUpCard,
                            Puzzle CurrentPuzzle,
                            FaceUpCard? PreviousFaceUpCard,
                            Puzzle? PreviousPuzzle,
                            int TotalCardsNumber,
                            int CompletedCardsNumber);

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

    public record FaceUpCard(Guid FaceUpCardId,
                                    string? Hint,
                                    bool IsMistaken,
                                    FaceUpCardCheckResult? LastResult);
}
