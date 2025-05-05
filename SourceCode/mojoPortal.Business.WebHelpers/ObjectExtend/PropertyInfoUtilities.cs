using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.ObjectExtend
{
    public static class PropertyInfoUtilities
    {
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
            where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).FirstOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.InvariantCulture,
                    "The {0} attribute must be defined on member {1}",
                        typeof(T).Name, member.Name));
            }

            return (T)attribute;
        }
    }
}