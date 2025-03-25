using System;
using System.Collections.Generic;

public static class NounConverter
{
    // 不规则名词单复数映射表
    private static readonly Dictionary<string, string> IrregularSingularToPlural = new Dictionary<string, string>
    {
        { "man", "men" },
        { "woman", "women" },
        { "child", "children" },
        { "tooth", "teeth" },
        { "foot", "feet" },
        { "mouse", "mice" },
        { "goose", "geese" },
        { "person", "people" }
    };

    private static readonly Dictionary<string, string> IrregularPluralToSingular = new Dictionary<string, string>();

    static NounConverter()
    {
        // 初始化不规则名词复数到单数的映射表
        foreach (var pair in IrregularSingularToPlural)
        {
            IrregularPluralToSingular[pair.Value] = pair.Key;
        }
    }

    // 将单数名词转换为复数名词
    public static string ToPlural(string singular)
    {
        // 检查是否为不规则名词
        if (IrregularSingularToPlural.TryGetValue(singular, out string plural))
        {
            return plural;
        }

        // 规则变化处理
        if (singular.EndsWith("s", StringComparison.OrdinalIgnoreCase) ||
            singular.EndsWith("x", StringComparison.OrdinalIgnoreCase) ||
            singular.EndsWith("z", StringComparison.OrdinalIgnoreCase) ||
            singular.EndsWith("ch", StringComparison.OrdinalIgnoreCase) ||
            singular.EndsWith("sh", StringComparison.OrdinalIgnoreCase))
        {
            return singular + "es";
        }
        else if (singular.EndsWith("y", StringComparison.OrdinalIgnoreCase) &&
                 !IsVowel(singular[singular.Length - 2]))
        {
            return singular.Substring(0, singular.Length - 1) + "ies";
        }
        else if (singular.EndsWith("f", StringComparison.OrdinalIgnoreCase))
        {
            return singular.Substring(0, singular.Length - 1) + "ves";
        }
        else if (singular.EndsWith("fe", StringComparison.OrdinalIgnoreCase))
        {
            return singular.Substring(0, singular.Length - 2) + "ves";
        }
        else
        {
            return singular + "s";
        }
    }

    // 将复数名词转换为单数名词
    public static string ToSingular(string plural)
    {
        // 检查是否为不规则名词
        if (IrregularPluralToSingular.TryGetValue(plural, out string singular))
        {
            return singular;
        }

        // 规则变化处理
        if (plural.EndsWith("es", StringComparison.OrdinalIgnoreCase))
        {
            if (plural.EndsWith("ies", StringComparison.OrdinalIgnoreCase))
            {
                return plural.Substring(0, plural.Length - 3) + "y";
            }
            return plural.Substring(0, plural.Length - 2);
        }
        else if (plural.EndsWith("ves", StringComparison.OrdinalIgnoreCase))
        {
            if (plural.EndsWith("fves", StringComparison.OrdinalIgnoreCase))
            {
                return plural.Substring(0, plural.Length - 4) + "f";
            }
            return plural.Substring(0, plural.Length - 3) + "fe";
        }
        else if (plural.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        {
            return plural.Substring(0, plural.Length - 1);
        }

        return plural;
    }

    // 判断字符是否为元音字母
    private static bool IsVowel(char c)
    {
        c = char.ToLower(c);
        return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
    }
}