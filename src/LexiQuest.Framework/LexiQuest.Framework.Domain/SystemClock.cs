namespace LexiQuest.Framework.Domain;

public static class SystemClock
{
    private static DateTimeOffset? _customDate;

    public static DateTimeOffset Now
    {
        get
        {
            if (_customDate.HasValue)
            {
                return _customDate.Value;
            }

            return DateTimeOffset.UtcNow;
        }
    }

    public static void Set(DateTimeOffset customDate) => _customDate = customDate;

    public static void Reset() => _customDate = null;
}