using Autofac;
using MassTransit;

namespace LexiQuest.Framework.Module.Config;

public interface IModuleStartupConfigurationBuilder
{
    IModuleStartupConfigurationBuilder AddEventBusConfig(Action<IBusRegistrationConfigurator> configAction);
    IModuleStartupConfigurationBuilder OverwriteEventBusScanFilter(Func<Type?, bool> filter = null);
    IModuleStartupConfigurationBuilder RegisterServices(Action<ContainerBuilder> configServices);
}

internal interface IModuleStartupConfiguration
{
    Action<IBusRegistrationConfigurator>? AdditionalMassTransitConfg { get; }
    ModuleStartupConfiguration.OverwrittenFilter? OverwriteFilter { get; }
    Action<ContainerBuilder>? RegisterServicesAction { get; }
}

internal class ModuleStartupConfiguration : IModuleStartupConfigurationBuilder, IModuleStartupConfiguration
{
    public Action<IBusRegistrationConfigurator>? AdditionalMassTransitConfg { get; private set; }

    public class OverwrittenFilter(Func<Type?, bool> filter)
    {
        public Func<Type?, bool> Filter { get; private set; } = filter;
    }

    public OverwrittenFilter? OverwriteFilter { get; private set; }

    public Action<ContainerBuilder>? RegisterServicesAction { get; private set; }

    public IModuleStartupConfigurationBuilder AddEventBusConfig(Action<IBusRegistrationConfigurator> configAction)
    {
        AdditionalMassTransitConfg = configAction;
        return this;
    }

    public IModuleStartupConfigurationBuilder OverwriteEventBusScanFilter(Func<Type?, bool> filter = null)
    {
        OverwriteFilter = new OverwrittenFilter(filter);
        return this;
    }

    public IModuleStartupConfigurationBuilder RegisterServices(Action<ContainerBuilder> configServices)
    {
        RegisterServicesAction = configServices;
        return this;
    }

    public IModuleStartupConfiguration Build(Action<IModuleStartupConfigurationBuilder>? config)
    {
        if (config != null)
            config(this);

        return this;
    }
}