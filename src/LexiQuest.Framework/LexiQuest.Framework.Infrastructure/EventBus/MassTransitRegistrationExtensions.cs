using System.Reflection;
using MassTransit;
using MassTransit.Metadata;

namespace LexiQuest.Framework.Infrastructure.EventBus;

public static class MassTransitRegistrationExtensions
{
    /// <summary>
    /// Дефолтный метод AddConsumers не добавляет internal Consumers, только public
    /// Этот метод добавляет и те и другие
    /// </summary>
    public static void AddConsumersFromAssemblyContaining(this IBusRegistrationConfigurator configurator, Assembly assembly,
                                                          Func<Type, bool>? filter = null)
    {
        var types = assembly.GetTypes().Where(RegistrationMetadata.IsConsumerOrDefinition);
        if (filter != null)
            types = types.Where(filter);

        configurator.AddConsumers(types.ToArray());
    }

    public static void AddConsumersFromAssemblyContaining(this IMediatorRegistrationConfigurator configurator, Assembly assembly,
                                                          Func<Type, bool>? filter = null)
    {
        var types = assembly.GetTypes().Where(RegistrationMetadata.IsConsumerOrDefinition);
        if (filter != null)
            types = types.Where(filter);

        configurator.AddConsumers(types.ToArray());
    }

}