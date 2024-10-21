using LexiQuest.Framework.Domain;
using LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords;
using LexiQuest.Shared.Puzzle;

namespace LexiQuest.PuzzleMgr.Domain.Puzzles.Rules;

public sealed class PuzzleAndSynonimsLangMatchRule(Language puzzleLang, IReadOnlyList<ForeignWord> synonims) : BusinessRule
{
    protected override bool Rule() => synonims.All(x => x.Language == puzzleLang);
    public override string ErrorMessage => "Language of synonims must be same as puzzle.";
}