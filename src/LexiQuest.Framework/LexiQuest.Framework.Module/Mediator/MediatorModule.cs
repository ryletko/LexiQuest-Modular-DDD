using System.Reflection;
using LexiQuest.Framework.Application.Messages.Registration;
using LexiQuest.Framework.Infrastructure.EventBus;
using LexiQuest.Framework.Module.Config;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace LexiQuest.Framework.Module.Mediator;

internal class MediatorModule(IServiceCollection services,
                              ModuleContext moduleContext) : Autofac.Module
{
    public void Register()
    {
        services.AddMediator(cfg =>
                             {
                                 var filter = (Type t) => typeof(IInternalMessageHandler).IsAssignableFrom(t) || t.GetCustomAttribute(typeof(InternalMessageHandlerAttribute)) != null;
                                 cfg.AddConsumersFromAssemblyContaining(moduleContext.ApplicationAssembly, filter);
                             });
    }
}