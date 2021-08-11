using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Exceptions
{
    public class PropertyIsNotSimpleTypeException : Exception
    {
        public PropertyIsNotSimpleTypeException(string message) : base(message)
        {

        }

        public PropertyIsNotSimpleTypeException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
