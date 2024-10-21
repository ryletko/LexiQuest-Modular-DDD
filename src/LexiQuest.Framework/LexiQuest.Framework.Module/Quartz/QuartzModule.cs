using Autofac;
using LexiQuest.Framework.Module.Config;
using Quartz;

namespace LexiQuest.Framework.Module.Quartz;

internal class QuartzModule(ModuleContext moduleContext) : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(moduleContext.InfrastructureAssembly)
               .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
    }
}