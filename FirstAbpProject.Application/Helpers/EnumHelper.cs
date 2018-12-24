using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Helpers
{
    /// <summary>
    /// Only support enum class which inherits int type
    /// </summary>
    public class EnumHelper
    {
        public static List<EnumDetail> GetEnumDetail<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<Enum>()
                .Select(t => new EnumDetail {
                    Value = Convert.ToInt32(t),
                    Name = t.ToString(),
                    Description = GetDescription(t)
                }).ToList();
        }

        public static EnumDetail GetEnumDetail(Enum value)
        {
            return new EnumDetail {
                Value = Convert.ToInt32(value),
                Name = value.ToString(),
                Description = GetDescription(value)
            };
        }

        public static string GetDescription(Enum value)
        {
            return value.GetType().GetMember(value.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description;
        }
    }

    public class EnumDetail
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
