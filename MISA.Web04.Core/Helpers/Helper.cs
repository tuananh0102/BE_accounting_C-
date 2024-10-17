using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Helpers
{
    public static class Helper
    {
        public static void ConvertPropertiesToJson(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<ConvertToJsonAttribute>();
                if (attribute != null)
                {
                    var sourcePropertyName = attribute.SourcePropertyName;
                    var sourceProperty = type.GetProperty(sourcePropertyName);
                    if (sourceProperty != null)
                    {
                        var sourceValue = sourceProperty.GetValue(obj);

                        if (sourceValue != null)
                        {
                            var json = JsonSerializer.Serialize(sourceValue);
                            property.SetValue(obj, json);
                        }
                    }
                }
            }
        }
    }
}
