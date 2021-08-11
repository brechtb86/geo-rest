using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Exceptions
{
    public class PropertyDoesNotExistException : Exception
    {
        public PropertyDoesNotExistException(string message) :base(message)
        {

        }

        public PropertyDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
