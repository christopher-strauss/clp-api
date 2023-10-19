using System;
using System.ComponentModel;
using System.Reflection;

namespace CarLinePickup.Domain.Extensions
{
    public static class EnumerationExtensions
    {
        public static string ToValue(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attribute != null)
                    {
                        return attribute.Description;
                    }
                }
            }

            return value.ToString();
        }

        public static string ToValueLower(this Enum value)
        {
            return value.ToValue().ToLower();
        }

        public static string ToValueUpper(this Enum value)
        {
            return value.ToValue().ToUpper();
        }

        public static T GetValueFromDescription<T>(string description, bool ignoreCase = false)
        {
            var type = typeof(T);

            if (!type.IsEnum) { throw new InvalidOperationException(); }

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                bool isMatching = false;

                if (attribute != null)
                {
                    if (ignoreCase)
                        isMatching = attribute.Description.Trim().ToLower() == description.Trim().ToLower();
                    else
                        isMatching = attribute.Description.Trim() == description.Trim();

                    if (isMatching)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (ignoreCase)
                        isMatching = field.Name.Trim().ToLower() == description.Trim().ToLower();
                    else
                        isMatching = field.Name.Trim() == description.Trim();

                    if (isMatching)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            return default(T);
        }
    }
}
