using System.Reflection;
using LexiQuest.PuzzleMgr.Application;
using LexiQuest.PuzzleMgr.Domain;

namespace LexiQuest.PuzzleMgr.Infrastructure.Config;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(IPuzzleMgrModule).Assembly;
    public static readonly Assembly Infrastructure = typeof(Assemblies).Assembly;
    public static readonly Assembly Domain = typeof(IPuzzleMgrDomain).Assembly;
}