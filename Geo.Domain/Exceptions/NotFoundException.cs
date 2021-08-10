using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Exceptions
{
    /// <summary>
    /// An exception that is thrown when a resource is not found (in the repository).
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// The identifier of the resource.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The type of the resource.
        /// </summary>
        public Type ResourceType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException" /> class.
        /// </summary>
        /// <param name="resourceType">The type of the resource that was not found.</param>
        /// <param name="id">The identifier of the resource that was not found.</param>
        public NotFoundException(Type resourceType, int id)
        {
            this.Id = id;
            this.ResourceType = resourceType;
        }
    }
}
