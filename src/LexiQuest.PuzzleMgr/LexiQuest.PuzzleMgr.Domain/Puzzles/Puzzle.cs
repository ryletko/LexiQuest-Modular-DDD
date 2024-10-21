using LexiQuest.Framework.Domain;
using LexiQuest.PuzzleMgr.Domain.PuzzleOwners;
using LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords;
using LexiQuest.PuzzleMgr.Domain.Puzzles.Rules;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles;

public class PuzzleId(Guid value) : TypedIdValueBase(value);

public class Puzzle : Entity, IAggregateRoot
{
    public DateTimeOffset CreatedAt { get; private set; }
    public PuzzleId Id { get; private set; }
    public PuzzleOwnerId PuzzleOwnerId { get; private set; }


    public ForeignWord ForeignWord { get; private set; }
    public PartsOfSpeech PartsOfSpeech { get; private set; }
    public Transcription? Transcription { get; private set; }
    public string? From { get; private set; }
    public Language Language { get; private set; }

    private List<Definition> _definitions;
    public IReadOnlyList<Definition> Definitions => _definitions.AsReadOnly();

    private List<ForeignWord> _synonims;
    public IReadOnlyList<ForeignWord> Synonims => _synonims.AsReadOnly();

    private List<Example> _examples;
    public IReadOnlyList<Example> Examples => _examples.AsReadOnly();

    public LanguageLevel? Level { get; private set; }

    private Puzzle()
    {
        _definitions = [];
        _synonims    = [];
        _examples    = [];
    }

    private Puzzle(IReadOnlyList<Definition> definitions, IReadOnlyList<Example> examples, IReadOnlyList<ForeignWord> synonims)
    {
        _definitions = definitions.ToList();
        _examples    = examples.ToList();
        _synonims    = synonims.ToList();
    }

    public static Puzzle CreateNew(PuzzleOwnerId puzzleOwnerId, Language language,
                                   ForeignWord foreignWord, PartsOfSpeech partsOfSpeech, Transcription? transcription, string? from,
                                   IReadOnlyList<Definition> definitions, IReadOnlyList<Example> examples, IReadOnlyList<ForeignWord> synonims, LanguageLevel? level)
    {
        BusinessRule.Check(new PuzzleAndWordLangMatchRule(language, foreignWord.Language));
        BusinessRule.Check(new PuzzleAndSynonimsLangMatchRule(language, synonims));
        if (level != null)
            BusinessRule.Check(new PuzzleAndLevelLangMatchRule(language, level.Language));

        return new(definitions, examples, synonims)
               {
                   Id            = new PuzzleId(Guid.NewGuid()),
                   PuzzleOwnerId = puzzleOwnerId,
                   CreatedAt     = SystemClock.Now,
                   ForeignWord   = foreignWord,
                   PartsOfSpeech = partsOfSpeech,
                   Transcription = transcription,
                   From          = from,
                   Language      = language,
                   Level         = level,
               };
    }
}