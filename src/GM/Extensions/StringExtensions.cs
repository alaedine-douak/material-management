using System.Globalization;

namespace GM.Extensions;

public static class StringExtensions
{
    public static string CapitalizeEachWord(this string value) => 
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
}
