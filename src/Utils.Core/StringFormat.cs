namespace Utils.Core;

public static class StringFormatting
{
    public const string DefaultDateFormat = "MM/dd/yyyy";
    public const string DefaultDateTimeFormat = "MM/dd/yyyy HH:mm:ss";

    public static string ToString(this DateTime? value, string format)
    {
        return value != null ? value.Value.ToString(format) : System.String.Empty;
    }

    public static string FormatDate(this DateTimeOffset value)
    {
        return value.ToLocalTime().ToString(DefaultDateFormat);
    }

    public static string FormatDateTime(this DateTimeOffset value)
    {
        return value.ToLocalTime().ToString(DefaultDateTimeFormat);
    }
    
    public static string FormatGuid(Guid guid)
    {
        return guid.ToString().ToUpper();
    }

    public static string FormatException(this Exception ex)
    {
        return ex is AggregateException exception ? System.String.Join(";", exception.Flatten().InnerExceptions.Select(x => x.Message)) : ex.Message;
    }

    /// <summary>
    /// удаляет цифры после запятой, если их нет. Не добавляет тыссячные разделители
    /// </summary>
    /// <returns></returns>
    public static string FormatAsNumber(this decimal d)
    {
        return d.ToString("0.############################");
    }

    /// <summary>
    /// Удаляет центы если их нет
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static string FormatDollars(this decimal d)
    {
        var result = d.ToString("C2");
        return result.EndsWith(".00") ? result.Split('.')[0] : result;
    }
}