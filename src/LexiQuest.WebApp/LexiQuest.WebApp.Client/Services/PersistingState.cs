using LexiQuest.WebApp.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace LexiQuest.WebApp.Client.Services;

public class PersistingState(PersistentComponentState applicationState) : IPersistingState
{
    public Task<T> Persist<TComponent, T>(TComponent component, Func<Task<T>> fetchFunc, string? key = null)
    {
        var componentTypeName = typeof(TComponent).FullName;
        key = key != null ? $"{componentTypeName}:{key}" : componentTypeName;

        if (applicationState.TryTakeFromJson<T>(key, out var restored))
        {
            return Task.FromResult(restored!);
        }

        return fetchFunc();
    }
}