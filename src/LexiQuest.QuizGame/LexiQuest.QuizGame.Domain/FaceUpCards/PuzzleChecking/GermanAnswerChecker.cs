﻿namespace LexiQuest.QuizGame.Domain.FaceUpCards.PuzzleChecking;

internal class GermanAnswerChecker : ILanguageAnswerChecker
{
    public AnswerCheckStatusEnum Check(string word, string foreignWord, IReadOnlyCollection<string> synonims)
    {
        string[][] optionalGermanWords = [];

        if (foreignWord.Split(';').Any(x => Match(word, x, optionalGermanWords)))
            return AnswerCheckStatusEnum.Success;

        if (synonims.Any(s => Match(word, s, optionalGermanWords)))
            return AnswerCheckStatusEnum.Synonim;

        if ((foreignWord.StartsWith("das ") || foreignWord.StartsWith("der ") || foreignWord.StartsWith("die ")) &&
            Match(foreignWord.Substring(4, foreignWord.Length - 4), word.Substring(4, word.Length - 4), optionalGermanWords))
        {
            return AnswerCheckStatusEnum.WrongArticle;
        }

        return AnswerCheckStatusEnum.Mistake;
    }

    private bool Match(string str1, string str2, string[][] keywords) => Matching.Match(str1, str2, keywords);
}