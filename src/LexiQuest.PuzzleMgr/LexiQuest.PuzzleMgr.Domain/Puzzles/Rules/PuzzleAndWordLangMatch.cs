using LexiQuest.Framework.Domain;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles.Rules;

public sealed class PuzzleAndWordLangMatchRule(Language puzzleLang, Language foreignWordLang) : BusinessRule
{
    protected override bool Rule() => puzzleLang == foreignWordLang;
    public override string ErrorMessage => "Puzzle language doesn't match word language";
}