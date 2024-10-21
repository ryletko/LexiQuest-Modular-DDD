using Autofac;

namespace LexiQuest.PuzzleMgr.Infrastructure.Config;

internal static class PuzzleMgrCompositionRoot
{
    private static IContainer _container;

    internal static void SetContainer(IContainer container)
    {
        _container = container;
    }

    internal static ILifetimeScope BeginLifetimeScope()
    {
        return _container.BeginLifetimeScope();
    }
}