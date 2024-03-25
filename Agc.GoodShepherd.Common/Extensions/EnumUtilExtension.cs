using System.ComponentModel;
using System.Reflection;
using Agc.GoodShepherd.Common.Models;

namespace Agc.GoodShepherd.Common.Extensions
{
    public static class EnumUtilExtension
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetDescription(this Enum value)
        {
            return ((DescriptionAttribute)Attribute.GetCustomAttribute(
                value.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Single(x => x.GetValue(null).Equals(value)),
                typeof(DescriptionAttribute)))?.Description ?? value.ToString();
        }
        
        public static List<EnumModel> GetEnumeratedValues<T>() where T:Enum
        {
            return ((T[])Enum.GetValues(typeof(T)))
                .Select(c => new EnumModel() { Id = c.GetHashCode(), Name = c.GetDescription() }).OrderBy(a => a.Id)
                .ToList();
        }
    }
}
