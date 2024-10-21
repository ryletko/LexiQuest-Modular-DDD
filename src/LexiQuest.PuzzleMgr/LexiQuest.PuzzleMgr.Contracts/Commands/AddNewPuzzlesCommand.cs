using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Contracts.Commands;

public record AddNewPuzzlesCommand(List<AddNewPuzzlesCommand.AddPuzzleItem> PuzzleItems) : CommandBase
{
    public class AddPuzzleItem(string foreignWord,
                               PartsOfSpeech partsOfSpeech,
                               string? transcription,
                               string? from,
                               Language language,
                               List<string> definitions,
                               List<string> synonims,
                               List<string> examples,
                               string? level)
    {
        public string ForeignWord { get; } = foreignWord;
        public PartsOfSpeech PartsOfSpeech { get; } = partsOfSpeech;
        public string? Transcription { get; } = transcription;
        public string From { get; } = from;
        public Language Language { get; } = language;

        public List<string> Definitions { get; } = definitions;
        public List<string> Synonims { get; } = synonims;
        public List<string> Examples { get; } = examples;

        public string? Level { get; } = level;
    }
}