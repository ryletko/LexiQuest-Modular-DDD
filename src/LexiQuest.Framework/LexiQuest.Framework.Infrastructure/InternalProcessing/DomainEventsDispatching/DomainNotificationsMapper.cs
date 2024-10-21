using System.Reflection;
using LexiQuest.Framework.Application.Messages.Events;
using Utils.Core;

namespace LexiQuest.Framework.Infrastructure.InternalProcessing.DomainEventsDispatching;

public class DomainNotificationsMapper(BiDictionary<string, Type> domainNotificationsMap) : IDomainNotificationsMapper
{
    public string GetName(Type type)
    {
        return domainNotificationsMap.TryGetBySecond(type, out var name) ? name : null;
    }

    public Type GetType(string name)
    {
        return domainNotificationsMap.TryGetByFirst(name, out var type) ? type : null;
    }
}

public class SimpleDomainNotificationMapper() : IDomainNotificationsMapper
{
    private static readonly Assembly BaseApplicationAssembly = typeof(IMediatorDomainEvent).Assembly; // TODO возможно есть и лучше способ добраться то нужного типа, например см. выше  
    
    public string GetName(Type type)
    {
        return type.FullName;
    }

    public Type GetType(string name)
    {
        return BaseApplicationAssembly.GetType(name);
    }
}