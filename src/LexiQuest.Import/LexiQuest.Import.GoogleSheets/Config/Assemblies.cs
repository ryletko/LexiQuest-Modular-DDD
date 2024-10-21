using System.Reflection;
using LexiQuest.Import.GoogleSheets.Services;

namespace LexiQuest.Import.GoogleSheets.Config
{
    internal static class Assemblies
    {
        public static readonly Assembly GoogleImportAssembly = typeof(IGoogleSheetsImportModule).Assembly;
    }
}