using LexiQuest.Shared.Puzzle;

namespace LexiQuest.WebApp.Shared.Puzzles;

public class GetPuzzlesRp
{
    public required Guid PuzzleId { get; set; }
    public required string ForeignWord { get; set; }
    public required Language Language { get; set; }
    public required PartsOfSpeech PartsOfSpeech { get; set; }
    public required IEnumerable<string> Definitions { get; set; }
    public required IEnumerable<string> Examples { get; set; }
    public required IEnumerable<string> Synonims { get; set; }
    public required string Transcription { get; set; }
    public required string Level { get; set; }
}