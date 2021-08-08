using Geo.Rest.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Models.Query
{
    public class CountryCollectionQueryParameters : CollectionQueryParameters
    {
        /// <summary>
        /// The two letter isocode for the language, optional. Possible values: br, cn, de, es, fa, fr, hr, it, ja, kr, nl, pt. If an invalid value was given the default of "en" will be used.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }
    }
}
