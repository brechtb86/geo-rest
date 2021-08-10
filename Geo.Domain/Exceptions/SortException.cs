using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Exceptions
{
    public class SortException : Exception
    {
        public string SortProperty { get; set; }

        public SortException(string sortProperty, string message) : base(message)
        {
            SortProperty = sortProperty;
        }

        public SortException(string sortProperty, string message, Exception innerExcepton) : base(message, innerExcepton)
        {
            SortProperty = sortProperty;
        }
    }
}
