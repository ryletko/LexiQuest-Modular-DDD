using System.Drawing;

namespace Utils.Core;

public static class ConvertHelper
{
    public static int ToNotNullInt32(this string arg)
    {
        int result;
        Int32.TryParse(arg, out result);
        return result;
    }


    public static int? ToInt32Null(this string str)
    {
        int result;
        if (Int32.TryParse(str, out result))
        {
            return result;
        }
        return null;
    }

    public static float? ToSingleNull(this string arg)
    {
        float result;
        if (Single.TryParse(arg, out result))
        {
            return result;
        }
        return null;
    }


    public static double? ToDoubleNull(this string str)
    {
        double result;
        if (Double.TryParse(str, out result))
        {
            return result;
        }
        return null;
    }

    public static decimal? ToDecimalNull(this string str)
    {
        decimal result;
        if (Decimal.TryParse(str, out result))
        {
            return result;
        }
        return null;
    }

    public static DateTime? ToDateTimeNull(this string str)
    {
        DateTime result;
        if (DateTime.TryParse(str, out result))
        {
            return result;
        }
        return null;
    }

    public static bool? ToBooleanNull(this string arg)
    {
        if (Boolean.TryParse(arg, out var result))
        {
            return result;
        }
        return null;
    }

    public static bool ToNotNullBoolean(this string arg)
    {
        Boolean.TryParse(arg, out var result);
        return result;
    } 
       
    public static Guid ToNotNullGuid(this string arg)
    {
        Guid result;
        Guid.TryParse(arg, out result);
          
        return result;
    }


    public static Guid? ToGuidNull(this string arg)
    {
        Guid result;
        if (Guid.TryParse(arg, out result))
        {
            return result;
        }
        return null;
    }


    public static string ToHex(this Color c)
    {
        return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }


    public static decimal RoundToQuarter(this decimal value)
    {
        return Math.Round(value * 4, MidpointRounding.ToEven) / 4;
    }

    public static int ToInt32(this bool? arg)
    {
        if (arg == true)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}