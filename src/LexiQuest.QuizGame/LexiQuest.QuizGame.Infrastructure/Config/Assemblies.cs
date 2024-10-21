using System.Reflection;
using LexiQuest.QuizGame.Application;
using LexiQuest.QuizGame.Domain;

namespace LexiQuest.QuizGame.Infrastructure.Config;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(IQuizGameModule).Assembly;
    public static readonly Assembly Infrastructure = typeof(Assemblies).Assembly;
    public static readonly Assembly Domain = typeof(IQuizGameDomain).Assembly;

}