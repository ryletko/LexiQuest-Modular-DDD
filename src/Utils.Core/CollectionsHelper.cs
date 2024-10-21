namespace Utils.Core;

public static class CollectionsHelper
{
    public static void ForEach<T>(this IEnumerable<T> arr, Action<T> action)
    {
        foreach (var a in arr)
        {
            action(a);
        }
    }

    //
    public static TValue? GetOrNullVal<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey? key)
        where TValue : struct
    {
        if (key != null && dict.TryGetValue(key, out var value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }

    public static TValue? GetOrNullRef<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey? key)
        where TKey : struct
        where TValue : class
    {
        if (key != null && dict.TryGetValue(key.Value, out var value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }


    public static TValue? GetOrNullRef<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey? key)
        where TKey : class
        where TValue : class
    {
        if (key != null && dict.TryGetValue(key, out var value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }

    public static TValue GetOrValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey? key, Func<TValue> valueGetter)
        where TKey : class
        where TValue : class
    {
        if (key != null && dict.TryGetValue(key, out var value))
        {
            return value;
        }
        else
        {
            return valueGetter();
        }
    }


    public static IEnumerable<T> Yield<T>(this T item)
    {
        yield return item;
    }

    public static async Task<TValue?> GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue?> dict, TKey key, Func<Task<TValue?>> newValue)
        where TKey : struct
        where TValue : class
    {
        if (!dict.TryGetValue(key, out var result))
            return dict[key] = await newValue();
        
        return result;
    } 
}