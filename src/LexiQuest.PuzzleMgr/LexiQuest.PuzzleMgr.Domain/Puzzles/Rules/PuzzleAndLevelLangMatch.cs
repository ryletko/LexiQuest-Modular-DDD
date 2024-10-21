using LexiQuest.Framework.Domain;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles.Rules;

public sealed class PuzzleAndLevelLangMatchRule(Language puzzleLang, Language levelLang) : BusinessRule
{
    protected override bool Rule() => puzzleLang == levelLang;
    public override string ErrorMessage => "Wrong level format for this puzzle.";
}