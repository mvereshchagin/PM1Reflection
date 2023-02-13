using System.Reflection;
using System.ComponentModel;

namespace PM1Reflection;

internal static class EnumUtils
{
    public static string GetDescription<T>(T value) where T : struct
    {
        var type = typeof(T);
        var prop = type.GetMember(value.ToString()).First();
        if (prop == null)
            return String.Empty;

        var descAttr = (from attr in prop.GetCustomAttributes(typeof(DescriptionAttribute), true)
                       select attr).FirstOrDefault();

        if (descAttr is DescriptionAttribute dAttr)
            return dAttr.Description;

        return prop.Name;
    }
}
