using System.Reflection;

namespace LexiQuest.Framework.Module.Config;

internal class ModuleContext
{
    public string SchemaName { get; set; } 
    public Assembly ApplicationAssembly { get; set; }
    public Assembly InfrastructureAssembly { get; set; }
    public Assembly DomainAssembly { get; set; }
    public string ModuleName { get; set; }
}