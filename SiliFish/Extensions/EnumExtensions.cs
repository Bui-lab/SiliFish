using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SiliFish.Extensions
{
    public static class EnumExtensions
    {
        //used https://benjaminray.com/codebase/c-enum-display-names-with-spaces-and-special-characters/
        public static string GetDisplayName(this Enum enumValue)
        {
            string displayName;
            displayName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
            return displayName ?? enumValue.ToString();
        }
        public static string GetDescription(this Enum enumValue)
        {
            string desc;
            desc = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description;
            return desc ?? "";
        }

        //https://stackoverflow.com/questions/33225729/enum-value-from-display-name
        public static T GetValueFromName<T>(this string name) where T : Enum
        {
            var type = typeof(T);

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.Name == name)
                    {
                        return (T)field.GetValue(null);
                    }
                }

                if (field.Name == name)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(name));
        }

    }
}
