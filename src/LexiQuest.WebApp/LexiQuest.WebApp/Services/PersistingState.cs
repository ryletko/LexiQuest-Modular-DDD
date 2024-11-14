using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LexiQuest.WebApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LexiQuest.WebApp.Services;

internal class PersistingState(PersistentComponentState persistentComponentState) : IPersistingState, IDisposable
{
    private List<PersistingComponentStateSubscription> PersistingSubscription = [];

    public async Task<T?> Persist<TComponent, T>(TComponent component, Func<Task<T>> fetchFunc, string? key)
    {
        var componentTypeName = typeof(TComponent).FullName;
        key = key != null ? $"{componentTypeName}:{key}" : componentTypeName;

        var objectToPersist = await fetchFunc();
        PersistingSubscription.Add(persistentComponentState.RegisterOnPersisting(async () => await PersistData(key, objectToPersist), RenderMode.InteractiveWebAssembly));
        return objectToPersist;
    }

    private async Task PersistData(string key, object objectToPersist)
    {
        persistentComponentState.PersistAsJson(key, objectToPersist);
    }


    public void Dispose()
    {
        PersistingSubscription.ForEach(x => x.Dispose());
    }
}