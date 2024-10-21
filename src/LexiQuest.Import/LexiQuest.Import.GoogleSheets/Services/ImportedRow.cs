using LexiQuest.Shared.Puzzle;

namespace LexiQuest.Import.GoogleSheets.Services;

public class ImportedWord
{
    public string ForeignWord { get; set; }
    public PartsOfSpeech Class { get; set; }
    public string Transcription { get; set; }
    public string Flags { get; set; }
    public string FirstMention { get; set; }
    public int SourceId { get; set; }
    public string Level { get; set; }
    public string SourceRowData { get; set; }
    
    public List<string> Definition { get; set; }
    public List<string> Synonims { get; set; }
    public List<string> Examples { get; set; }
}