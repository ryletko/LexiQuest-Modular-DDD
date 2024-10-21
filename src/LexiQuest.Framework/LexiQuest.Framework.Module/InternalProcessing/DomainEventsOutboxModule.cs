using Autofac;
using LexiQuest.Framework.Application.Messages.InternalProcessing;
using LexiQuest.Framework.Domain;
using LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;
using LexiQuest.Framework.Infrastructure.InternalProcessing.Outbox;
using LexiQuest.Framework.Module.Config;
using Utils.Core;

namespace LexiQuest.Framework.Module.InternalProcessing;

internal class DomainEventsOutboxModule(BiDictionary<string, Type> domainNotificationsMap,
                                        ModuleContext moduleContext) : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OutboxAccessor>()
               .As<IOutbox>()
               .FindConstructorsWith(new AllConstructorFinder())
               .InstancePerLifetimeScope();

        this.CheckMappings();

        // builder.RegisterType<DomainNotificationsMapper>()
        //        .As<IDomainNotificationsMapper>()
        //        .FindConstructorsWith(new AllConstructorFinder())
        //        .WithParameter("domainNotificationsMap", domainNotificationsMap)
        //        .SingleInstance();
        builder.RegisterInstance(new SimpleDomainNotificationMapper())
               .As<IDomainNotificationsMapper>()
               .SingleInstance();
    }

    private void CheckMappings()
    {
        var domainEvents = moduleContext.ApplicationAssembly
                                        .GetTypes()
                                        .Where(x => x.GetInterfaces().Contains(typeof(IDomainEvent)))
                                        .ToList();

        List<Type> notMappedNotifications = [];
        foreach (var domainEvent in domainEvents)
        {
            domainNotificationsMap.TryGetBySecond(domainEvent, out var name);

            if (name == null)
            {
                notMappedNotifications.Add(domainEvent);
            }
        }

        if (notMappedNotifications.Any())
        {
            throw new ApplicationException($"Domain Event Notifications {notMappedNotifications.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
        }
    }
}