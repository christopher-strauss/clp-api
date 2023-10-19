using System.Linq;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using CarLinePickup.Domain.Extensions;

namespace CarLinePickup.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidExtendedAscii(this string str)
        {
            return str == null || str.All(c => c <= 255);
        }

        public static string ToDescriptionFromEnum<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }

        public static T ToEnumFromDescription<T>(this string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            T[] values = (T[])Enum.GetValues(typeof(T));
            try
            {
                //-- trim the value before converting
                if (!string.IsNullOrEmpty(value))
                    value = value.Trim();

                foreach (T val in values)
                {
                    var a = ((Enum)Convert.ChangeType(val, typeof(T))).ToValue().ToLower();
                    var b = value.ToLower();
                }


                T enumValue = values.Where(v => ((Enum)Convert.ChangeType(v, typeof(T))).ToValue().ToLower() == value.ToLower()).First();
                return enumValue;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException(string.Format("{0} has no Enum value with a description of \"{1}\"", typeof(T), value));
            }
        }

        public static T ToEnumFromDescriptionFirstDefault<T>(this string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            T[] values = (T[])Enum.GetValues(typeof(T));
            try
            {
                //-- trim the value before converting
                if (!string.IsNullOrEmpty(value))
                    value = value.Trim();

                T enumValue = values.Where(v => ((Enum)Convert.ChangeType(v, typeof(T))).ToValue() == value).FirstOrDefault();
                return enumValue;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException(string.Format("{0} has no Enum value with a description of \"{1}\"", typeof(T), value));
            }
        }

        public static T ToEnum<T>(this string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            try
            {
                T enumValue = (T)Enum.Parse(typeof(T), value);
                return enumValue;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(string.Format("{0} has no Enum value for \"{1}\"", typeof(T), value));
            }
        }

        public static bool Is<T>(this string input)
        {
            try
            {
                int result;
                int.TryParse(input, out result);
                return (bool)Convert.ChangeType(result, typeof(T));
            }
            catch
            {
                return false;
            }
        }

        public static string GetPaddedValue(this string padValue, int padCount, char padCharacter = '0', bool padRight = false)
        {
            return padRight ? padValue.PadRight(padCount, padCharacter) : padValue.PadLeft(padCount, padCharacter);
        }

        public static string ToXmlString(this object basicObject)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(basicObject.GetType());

            serializer.Serialize(stringwriter, basicObject);

            return stringwriter.ToString();
        }

        public static T To<T>(this object valueToConvert)
        {
            var convertingType = typeof(T).Name;

            switch (convertingType.ToLowerInvariant())
            {
                case "int32":
                    int outInt;
                    if (int.TryParse(valueToConvert.ToString(), out outInt))
                    {
                        return (T)Convert.ChangeType(outInt, typeof(int));
                    }
                    break;
                case "double":
                    double outDouble;
                    if (double.TryParse(valueToConvert.ToString(), out outDouble))
                    {
                        return (T)Convert.ChangeType(outDouble, typeof(double));
                    }
                    break;
                case "decimal":
                    decimal outDecimal;
                    if (decimal.TryParse(valueToConvert.ToString(), out outDecimal))
                    {
                        return (T)Convert.ChangeType(outDecimal, typeof(decimal));
                    }
                    break;
                case "datetime":
                    DateTime outDateTime;
                    if (DateTime.TryParse(valueToConvert.ToString(), out outDateTime))
                    {
                        return (T)Convert.ChangeType(outDateTime, typeof(DateTime));
                    }
                    break;
                case "boolean":
                    bool outBool = false;
                    try
                    {
                        outBool = Convert.ToBoolean(valueToConvert.ToString());
                    }
                    catch { }
                    try
                    {
                        outBool = Convert.ToBoolean(Convert.ToInt32(valueToConvert));
                    }
                    catch { }

                    return (T)Convert.ChangeType(outBool, typeof(bool));
            }

            return default(T);
        }

        public static string ToCamelCase(this string value)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(value);
        }
    }
}