using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.ComponentModel;
using FluentValidation;

namespace CarLinePickup.API.Validators.Extensions
{
    public static class RuleBuilderExtension
    {
        public static IRuleBuilderOptions<T, string> IsValidEnumValue<T>(this IRuleBuilder<T, string> ruleBuilder, Type enumType)
        {
            {
                IList<string> enumDescriptions = new List<string>();
                Array values = Enum.GetValues(enumType);

                foreach (int val in values)
                {
                    var memInfo = enumType.GetMember(enumType.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        enumDescriptions.Add(descriptionAttribute.Description);
                    }
                }

                return ruleBuilder.Must((rootObject, str, context) =>
                {                 
                        return enumDescriptions.Any(s => s.Equals(str, StringComparison.OrdinalIgnoreCase));

                })
                .WithMessage("{PropertyName} must be one of the following values: (" + String.Join(", ", enumDescriptions.ToArray()) + ")");
            }
        }
    }
}
