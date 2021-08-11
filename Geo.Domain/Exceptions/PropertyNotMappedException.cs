using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Exceptions
{
    public class PropertyNotMappedException : Exception
    {
        public PropertyNotMappedException()
        {

        }

        public PropertyNotMappedException(string message) : base(message)
        {

        }

        public PropertyNotMappedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
