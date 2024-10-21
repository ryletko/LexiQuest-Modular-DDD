using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.GameStates;

public class Score : ValueObject
{
    private Score()
    {
    }

    private Score(int score)
    {
        IntVal = score;
    }

    public int IntVal { get; }

    public static Score FromInt(int score) => new Score(score);
    public int ToInt() => IntVal;
}