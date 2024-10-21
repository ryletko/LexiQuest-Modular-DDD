using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.GameStates.Rules;

public class GameIsReadyRule(GameStatus statusEnum) : BusinessRule
{
    protected override bool Rule() => statusEnum == GameStatus.Ready;

    public override string ErrorMessage => "Game is at init state";
}