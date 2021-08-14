using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Domain.Models.Geo
{
    public class City : Base
    {
        /// <summary>
        /// The state code.
        /// </summary>
        /// <value>
        /// The state code.
        /// </value>
        public string StateCode { get; set; }

        /// <summary>
        ///The country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }
    }
}
