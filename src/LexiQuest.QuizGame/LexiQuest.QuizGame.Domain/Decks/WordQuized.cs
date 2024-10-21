using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.Decks;

public class WordQuized: ValueObject
{
    private readonly string _puzzleAnswer;

    private WordQuized()
    {
        
    }

    internal WordQuized(string puzzleAnswer)
    {
        Validate();
        _puzzleAnswer = puzzleAnswer;
    }

    public void Validate()
    {
        
    }

    public override string ToString()
    {
        return _puzzleAnswer;
    }
}