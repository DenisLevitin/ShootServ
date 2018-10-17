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
                    string description = String.Empty;
                    
                    var memInfo = enumType.GetMember(converted.ToString());
                    if (memInfo.Length > 0)
                    {
                        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attributes.Length > 0)
                        {
                            description = ((DescriptionAttribute) attributes[0]).Description;
                        }
                    }
                    res.Add(new EnumView(Convert.ToInt32(converted), converted.ToString(), description));
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