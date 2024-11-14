namespace LexiQuest.WebApp.Shared.Services;

public interface IPersistingState
{
    Task<T> Persist<TComponent, T>(TComponent component, Func<Task<T>> fetchFunc, string? key = null);
}