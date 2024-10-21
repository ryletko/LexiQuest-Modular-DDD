namespace Utils.Core;

public static class ObjectMapper
{
    public static T2 Map<T1, T2>(this T1 obj, Func<T1, T2> map) => map(obj);

    public static async Task<T2?> MapIfNotNullAsync<T1, T2>(this T1 obj, Func<T1, Task<T2>> map)
        where T2 : class?
    {
        if (obj != null)
            return await map(obj);

        return null;
    }

    public static T1 Apply<T1>(this T1 obj, Action<T1> exec)
    {
        exec(obj);
        return obj;
    }

    public static async Task<T1> ApplyAsync<T1>(this T1 obj, Func<T1, Task> exec)
    {
        await exec(obj);
        return obj;
    }

    // public static T2 ResolveNullable<T1, T2>(this T1? obj, Func<T1, T2> mapIfNotNull, Func<T2> mapIfNull) => obj != null ? mapIfNotNull(obj) : mapIfNull();
}