using System.Collections.Concurrent;
using System.Reflection;
using Autofac;
using Autofac.Core;
using LexiQuest.Framework.Application.Messages.Commands;
using LexiQuest.Framework.Application.Messages.Context;
using LexiQuest.Framework.Application.Messages.Decoration;
using LexiQuest.Framework.Application.Messages.Events;
using LexiQuest.Framework.Application.Messages.Queries;
using LexiQuest.Framework.Infrastructure.EventBus.Decorators;
using LexiQuest.Framework.Infrastructure.InternalProcessing;
using LexiQuest.Framework.Module.Config;

namespace LexiQuest.Framework.Module.EventBus;

internal class HandlerDecorators(ModuleContext moduleContext)
{
    private record DecoratorsInfo(FieldInfo DecoratorsFieldInfo,
                                  Type DecoratorsClosedType);

    private readonly ConcurrentDictionary<Type, DecoratorsInfo> _decoratorsInfos = new();

    public HandlerDecorators RegisterDecorators(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(CommandContextDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(CommandContextDecorator<,>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(QueryContextDecorator<,>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(EventContextDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(UnitOfWorkCommandHandlerDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(UnitOfWorkCommandHandlerDecorator<,>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(UnitOfWorkEventHandlerDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(ValidationCommandHandlerDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(ValidationCommandHandlerDecorator<,>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(LoggingCommandHandlerDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(LoggingCommandHandlerDecorator<,>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(ErrorNotifierCommandHandlerDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(ErrorNotifierCommandHandlerDecorator<,>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(ErrorNotifierEventHandlerDecorator<>)).AsSelf().InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(ErrorNotifierQueryHandlerDecorator<,>)).AsSelf().InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(CommandHandlerDecorators<>)).As(typeof(ICommandHandlerDecorators<>)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(CommandHandlerDecorators<,>)).As(typeof(ICommandHandlerDecorators<,>)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(QueryHandlerDecorators<,>)).As(typeof(IQueryHandlerDecorators<,>)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(EventHandlerDecorators<>)).As(typeof(IEventHandlerDecorators<>)).InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(moduleContext.ApplicationAssembly)
               .AsClosedTypesOf(typeof(CommandHandlerBase<>))
               .OnActivated(DecorateCommandHandler)
               .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(moduleContext.ApplicationAssembly)
               .AsClosedTypesOf(typeof(CommandHandlerBase<,>))
               .OnActivated(DecorateCommandHandler)
               .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(moduleContext.ApplicationAssembly)
               .AsClosedTypesOf(typeof(QueryHandlerBase<,>))
               .OnActivated(DecorateQueryHandler)
               .InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(moduleContext.ApplicationAssembly)
               .AsClosedTypesOf(typeof(EventHandlerBase<>))
               .OnActivated(DecorateEventHandler)
               .InstancePerLifetimeScope();
        
        return this;
    }

    private void DecorateCommandHandler(IActivatedEventArgs<object> e)
    {
        var instanceType = e.Instance.GetType();
        if (_decoratorsInfos.TryGetValue(instanceType, out var decoratorsInfo))
        {
            decoratorsInfo.DecoratorsFieldInfo.SetValue(e.Instance, e.Context.Resolve(decoratorsInfo.DecoratorsClosedType));
            return;
        }

        foreach (var baseType in ScanBaseTypes(instanceType))
        {
            if (baseType.GetGenericTypeDefinition() == typeof(CommandHandlerBase<>) ||
                baseType.GetGenericTypeDefinition() == typeof(CommandHandlerBase<,>))
            {
                var genericParameters = baseType.GetGenericArguments();
                var decoratorsType = genericParameters.Length == 1 ? typeof(ICommandHandlerDecorators<>).MakeGenericType(genericParameters) :
                    genericParameters.Length == 2                  ? typeof(ICommandHandlerDecorators<,>).MakeGenericType(genericParameters) :
                                                                     throw new InvalidOperationException("Unsupported number of generic parameters for decorators");
                var decorators = e.Context.Resolve(decoratorsType);
                var fieldInfo = baseType.GetField("_decorators", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(e.Instance, decorators);
                _decoratorsInfos.TryAdd(instanceType, new DecoratorsInfo(fieldInfo, decoratorsType));
                break;
            }
        }
    }

    private void DecorateQueryHandler(IActivatedEventArgs<object> e)
    {
        var instanceType = e.Instance.GetType();
        if (_decoratorsInfos.TryGetValue(instanceType, out var decoratorsInfo))
        {
            decoratorsInfo.DecoratorsFieldInfo.SetValue(e.Instance, e.Context.Resolve(decoratorsInfo.DecoratorsClosedType));
            return;
        }

        foreach (var baseType in ScanBaseTypes(instanceType))
        {
            if (baseType.GetGenericTypeDefinition() == typeof(QueryHandlerBase<,>))
            {
                var genericParameters = baseType.GetGenericArguments();
                var decoratorsType = typeof(IQueryHandlerDecorators<,>).MakeGenericType(genericParameters);

                var decorators = e.Context.Resolve(decoratorsType);
                var fieldInfo = baseType.GetField("_decorators", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(e.Instance, decorators);
                _decoratorsInfos.TryAdd(instanceType, new DecoratorsInfo(fieldInfo, decoratorsType));
                break;
            }
        }
    }

    private void DecorateEventHandler(IActivatedEventArgs<object> e)
    {
        var instanceType = e.Instance.GetType();
        if (_decoratorsInfos.TryGetValue(instanceType, out var decoratorsInfo))
        {
            decoratorsInfo.DecoratorsFieldInfo.SetValue(e.Instance, e.Context.Resolve(decoratorsInfo.DecoratorsClosedType));
            return;
        }

        foreach (var baseType in ScanBaseTypes(instanceType))
        {
            if (baseType.GetGenericTypeDefinition() == typeof(EventHandlerBase<>))
            {
                var genericParameters = baseType.GetGenericArguments();
                var decoratorsType = typeof(IEventHandlerDecorators<>).MakeGenericType(genericParameters);
                var decorators = e.Context.Resolve(decoratorsType);
                var fieldInfo = baseType.GetField("_decorators", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(e.Instance, decorators);
                _decoratorsInfos.TryAdd(instanceType, new DecoratorsInfo(fieldInfo, decoratorsType));
                break;
            }
        }
    }
    
    IEnumerable<Type> ScanBaseTypes(Type type)
    {
        if (type.BaseType != null)
        {
            yield return type.BaseType;
            foreach (var baseType in ScanBaseTypes(type.BaseType))
            {
                yield return baseType;
            }
        }
    }

    public interface IDecoratorResolver<T> where T : class, ICommand
    {
        IEnumerable<IHandlerDecorator<T>> ResolveDecorators();
    }

    private IEnumerable<(Type foundType, Type concreteGenericBaseType)> FindTypesDeriverdFromOpenGenericType(Assembly assembly, Type openGenericType)
    {
        foreach (var t in assembly.GetTypes())
        {
            foreach (var baseType in ScanBaseTypes(t))
            {
                if (baseType is {IsGenericType: true} && baseType.GetGenericTypeDefinition() == openGenericType)
                {
                    yield return (t, baseType);
                }
            }
        }
    }
    
    private class DummyCommand : ICommand
    {
        public Guid Id { get; }
        public MessageContext? MessageContext { get; set; }
    }

    private class DummyCommand<TR> : ICommand<TR> where TR : class
    {
        public Guid Id { get; }
        public MessageContext? MessageContext { get; set; }
    }

    private class DummyDomainEvent : ICommand
    {
        public Guid Id { get; }
        public MessageContext? MessageContext { get; set; }
    }
}