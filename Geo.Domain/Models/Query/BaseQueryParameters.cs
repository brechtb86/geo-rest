using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Models.Query
{
    public abstract class BaseQueryParameters
    {
        /// <summary>
        /// A pipe-seperated list of the fields that should be returned.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public string Fields { get; set; }
    }
}
