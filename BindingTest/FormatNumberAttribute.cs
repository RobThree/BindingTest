using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BindingTest
{
    public enum NumberFormat
    {
        E164,
        Original
    }

    public class FormatNumberAttribute : Attribute
    {
        public NumberFormat NumberFormat { get; set; }
        public FormatNumberAttribute(NumberFormat format)
        {
            NumberFormat = format;
        }
    }
}
