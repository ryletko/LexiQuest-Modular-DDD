namespace LexiQuest.QuizGame.Domain.FaceUpCards.Hints;

internal class HintGenerator
{
    public string GenerateHint(string word)
    {
        var random = new Random();
        var result = new Char[word.Length];
        for (var i = 0; i < word.Length; i++)
        {
            var r = Math.Floor((double)random.Next(0, 3));
            if (r > 0)
            {
                result[i] = '*';
            }
            else
            {
                result[i] = word[i];
            }
        }

        return new String(result);
    }
}