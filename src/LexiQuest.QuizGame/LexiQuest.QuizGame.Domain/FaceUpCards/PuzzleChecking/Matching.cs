using System.Text.RegularExpressions;

namespace LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

internal static class Matching
{
    /// <summary>
    /// Заменяет варианты слов другим конкретным
    /// </summary>
    /// <param name="text"></param>
    /// <param name="words"></param>
    /// <param name="replacer"></param>
    /// <param name="nonLettersIncluded"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static string ReplaceWords(string text, string[] words, string replacer, char[]? nonLettersIncluded)
    {
        if (String.IsNullOrEmpty(text))
            return text;

        if (words == null)
            throw new ArgumentNullException(nameof(words));

        // (?=pattern) - позволяет продолжить поиск следующего вхождения с позиции 
        // ранее конца предыдущего, что позволяет им пересекаться 
        // (например "sb sb sb" без этой конструкции находило бы 
        // только 2 вхождения из за пересечения пробелов)

        // (?<name>pattern) - позволяет сохранить вхождение с заданным именем

        // (pattern1|pattern2) - позволяет выбрать один из двух возможных вариантов (логическое или)

        // RegexOptions.ExplicitCapture - не сохраняет части выражения указанные в скобках в результат,
        // позволяя не писать каждый раз (?:pattern) для выражений в скобках

        // RegexOptions.Compiled - ускоряет процесс поиска, компилируя выражение до старта программы


        var wordsRegEx = String.Join("|", words.Select(Regex.Escape));

        //var nonLetters = String.Join(null, words.Select(x => Regex.Replace(x, @"\W", String.Empty)));

        var nonLettersIncludedJoined = nonLettersIncluded is {Length: > 0} ? Regex.Escape(String.Join(null, nonLettersIncluded)) : String.Empty;
        var nonLettersRegEx = $"[^A-Za-z0-9{nonLettersIncludedJoined}]";

        var pattern = $"(?=(^|{nonLettersRegEx})(?<target>({wordsRegEx}))($|{nonLettersRegEx}))";
        var matches = Regex.Matches(text, pattern, RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        var lenghtChange = 0;
        foreach (Match match in matches)
        {
            var group = match.Groups["target"];
            var index = group.Index + lenghtChange;
            text         =  text.Remove(index, group.Length).Insert(index, replacer);
            lenghtChange += replacer.Length - group.Length;
        }

        return text;
    }

    public static bool Match(string str1, string str2, string[][] keywords)
    {
        str1 = str1.Trim();
        str2 = str2.Trim();
        return str1.MatchWithOptionalWords(str2, keywords, ignoreCase: true, onlyLetters: true);
    }

    private static bool MatchWithOptionalWords(this string str1, string str2, string[][] optionalWords, bool ignoreCase = false, bool onlyLetters = false)
    {
        //:: тут пробегаем по всем наборам опциональных слов и заменяем их в строке на что-то в виде 
        //:: smb|sb|somebody|someone|smth|something|sth
        //:: тем самым приводя к единому виду
        //TODO хотя наверно проще было привести к одному из этих вариантов...
        str1 = optionalWords.Aggregate(str1, (s, ow) => ReplaceWords(s, ow, $"({String.Join("|", ow)})", null));

        if (onlyLetters)
        {
            var regPattern = @"[^\w\|\(\)]";
            str1 = Regex.Replace(str1, regPattern, "");
            str2 = Regex.Replace(str2, regPattern, "");
        }

        var regex = new Regex($"^{str1}$", ignoreCase ? RegexOptions.IgnoreCase : 0);
        return regex.IsMatch(str2);
    }
}