using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords;

internal class LanguageWordValidator(Regex wordValidator)
{
    public bool Validate(string str) => wordValidator.IsMatch(str);
}

internal class WordValidator : IWordValidator
{
    private static readonly Regex NoValidation = new("\\.?");
    private static readonly Regex EnglishWordValidator = NoValidation;
    private static readonly Regex GermanWordValidator = NoValidation;

    private static readonly ReadOnlyDictionary<Language, LanguageWordValidator> Validators = new Dictionary<Language, LanguageWordValidator>()
                                                                                             {
                                                                                                 {Language.English, new LanguageWordValidator(EnglishWordValidator)},
                                                                                                 {Language.German, new LanguageWordValidator(GermanWordValidator)}
                                                                                             }.AsReadOnly();

    private static LanguageWordValidator FindForLanguage(Language lang) => Validators[lang];
    public bool Validate(string str, Language lang) => FindForLanguage(lang).Validate(str);
}

internal interface IWordValidator
{
    bool Validate(string str, Language lang);
}