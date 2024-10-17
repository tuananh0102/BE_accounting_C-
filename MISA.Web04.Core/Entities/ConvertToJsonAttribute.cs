using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ConvertToJsonAttribute : Attribute
    {
        public string SourcePropertyName { get; }

        public ConvertToJsonAttribute(string sourcePropertyName)
        {
            SourcePropertyName = sourcePropertyName;
        }
    }
}
