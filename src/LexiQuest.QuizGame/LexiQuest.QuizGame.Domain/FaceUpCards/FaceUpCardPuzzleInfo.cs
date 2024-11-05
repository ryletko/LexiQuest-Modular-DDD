using LexiQuest.Framework.Domain;
using LexiQuest.QuizGame.Domain.Decks;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public class FaceUpCardPuzzleInfo : ValueObject
{
    public FaceDownCardId FaceDownCardId { get; set; }
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

    private FaceUpCardPuzzleInfo()
    {
        _definitions = [];
        _synonims    = [];
        _examples    = [];
    }

    private void Validate()
    {
    }

    public FaceUpCardPuzzleInfo(FaceDownCardId faceDownCardId, string foreignWord, string partsOfSpeech, string? transcription, string? from,
                                Language language, List<string> definitions, List<string> synonims, List<string> examples, string? level)
    {
        Validate();

        FaceDownCardId = faceDownCardId;
        ForeignWord    = foreignWord;
        PartsOfSpeech  = partsOfSpeech;
        Transcription  = transcription;
        From           = from;
        Language       = language;
        _definitions   = definitions;
        _synonims      = synonims;
        _examples      = examples;
        Level          = level;
    }
}