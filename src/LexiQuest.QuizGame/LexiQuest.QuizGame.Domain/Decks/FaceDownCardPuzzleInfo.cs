using LexiQuest.Framework.Domain;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.QuizGame.Domain.Decks;

public class FaceDownCardPuzzleInfo : ValueObject
{
    public string ForeignWord { get; private init; }
    public string PartsOfSpeech { get; private init; }
    public string? Transcription { get; private init; }
    public string? From { get; private init; }
    public Language Language { get; private init; }

    private List<string> _definitions;
    public IReadOnlyList<string> Definitions => _definitions.AsReadOnly();

    private List<string> _synonims;
    public IReadOnlyList<string> Synonims => _synonims.AsReadOnly();

    private List<string> _examples;
    public IReadOnlyList<string> Examples => _examples.AsReadOnly();

    public string? Level { get; init; }

    private FaceDownCardPuzzleInfo()
    {
        _definitions = [];
        _synonims    = [];
        _examples    = [];
    }

    private void Validate()
    {
    }

    public FaceDownCardPuzzleInfo(string foreignWord, string partsOfSpeech, string? transcription, string? from,
                      Language language, List<string> definitionsIntl, List<string> synonims, List<string> examples, string? level)
    {
        Validate();

        ForeignWord   = foreignWord;
        PartsOfSpeech = partsOfSpeech;
        Transcription = transcription;
        From          = from;
        Language      = language;
        _definitions  = definitionsIntl;
        _synonims     = synonims;
        _examples     = examples;
        Level         = level;
    }
}