using LexiQuest.Framework.Domain;

namespace LexiQuest.QuizGame.Domain.FaceUpCards;

public class FaceUpCardAnswer : ValueObject
{
    private readonly string _answerText;

    public FaceUpCardAnswer(string answerText)
    {
        Validate();
        
        _answerText = answerText;
    }

    private void Validate()
    {
        
    }

    public bool IsEmpty()
    {
        return String.IsNullOrWhiteSpace(_answerText);
    }
    
    public override string ToString() => _answerText;
}