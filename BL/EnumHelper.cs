using System;
using System.Collections.Generic;
using System.ComponentModel;
using BO;

namespace BL
{
    public static class EnumHelper
    {
        public static IReadOnlyCollection<EnumView> GetEnumValues<T>()
        {
            var res = new List<EnumView>();
            
            var enumType = typeof(T);
            if (enumType.IsEnum)
            {
                var enumValues = enumType.GetEnumValues();
                foreach (var item in enumValues)
                {
                    var converted = (System.Enum)item;
                    var memInfo = enumType.GetMember(converted.ToString());
                    var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    res.Add(new EnumView(Convert.ToInt32(converted), converted.ToString(), ((DescriptionAttribute)attributes[0]).Description ));
                }
            }
            else
            {
                throw new ArgumentException($"{enumType.FullName} is not enum");
            }

            return res;
        }
    }
}