using System;
using System.ComponentModel;
using System.Linq;

namespace ContaBancaria.Dominio.Helper
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) 
                                            as DescriptionAttribute[];

            if (descriptionAttributes != null && descriptionAttributes.Any())
                return descriptionAttributes.First().Description;

            return value.ToString();
        }
    }
}
