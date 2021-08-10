using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Exceptions
{
    public class FilterException : Exception
    {
        public string FilterProperty { get; set; }

        public FilterException(string filterProperty, string message) : base(message)
        {
            this.FilterProperty = filterProperty;
        }

        public FilterException(string filterProperty, string message, Exception innerExcepton) : base(message, innerExcepton)
        {
            this.FilterProperty = filterProperty;
        }
    }
}
