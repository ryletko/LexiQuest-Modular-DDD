using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.Import.GoogleSheets.Events;

public record ImportedDataFetched(List<ImportedDataFetched.FetchedPuzzleItem> PuzzleItems) : CommandBase
{
    public record FetchedPuzzleItem(string ForeignWord,
                                    PartsOfSpeech PartsOfSpeech,
                                    string Transcription,
                                    string From,
                                    Language Language,
                                    List<string> Definitions,
                                    List<string> Synonims,
                                    List<string> Examples,
                                    string Level);
}