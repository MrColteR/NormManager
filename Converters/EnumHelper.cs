using System;
using System.ComponentModel;
using System.Reflection;

namespace NormManager.Converters
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString())!;
            DescriptionAttribute attribute = (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute)!;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T GetEnumFromDescription<T>(string description)
        {
            Type type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException($"{nameof(T)} must be an enum type.");
            foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null)!;
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null)!;
                    }
                }
            }
            throw new ArgumentException($"Enum value with description '{description}' not found.");
        }
    }
}
