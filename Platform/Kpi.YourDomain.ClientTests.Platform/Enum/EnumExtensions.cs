using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Kpi.YourDomain.ClientTests.Platform.Enum
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)System.Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string GetEnumMemberValue<T>(T value)
            where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString(CultureInfo.InvariantCulture))
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }

        public static string[] GetEnumMemberValues<T>(params T[] members)
            where T : struct, IConvertible
        {
            return members.Select(GetEnumMemberValue).ToArray();
        }
    }
}
