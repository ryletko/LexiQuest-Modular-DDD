using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.GameStates.Rules;

internal class GameIsActiveRule(GameStatus statusEnum) : BusinessRule
{
    protected override bool Rule() => statusEnum == GameStatus.Active;

    public override string ErrorMessage => "Game is not active";
}