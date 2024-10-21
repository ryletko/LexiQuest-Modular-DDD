using System.ComponentModel;

namespace Utils.Core;

public static class EnumHelper
{
    public static IEnumerable<KeyValue<int, string>> GetKeyValues<T>() 
    {
        foreach (var n in Enum.GetNames(typeof(T)))
        {
            var val = Enum.Parse(typeof(T), n);
            yield return new KeyValue<int, string>((int) val, ((Enum) val).GetEnumDescription());
        }
    }
    
    public static string GetEnumDescription(this Enum value, Type filterByAttribute = null)
    {
        var fi = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}