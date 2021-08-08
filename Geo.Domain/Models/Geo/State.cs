using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Domain.Models.Geo
{
    public partial class State : Base
    {
        public State()
        {
        }

        /// <summary>
        /// Gets or sets the fips code.
        /// </summary>
        /// <value>
        /// The fips code.
        /// </value>
        public string FipsCode { get; set; }

        /// <summary>
        /// Gets or sets the two letter iso code.
        /// </summary>
        /// <value>
        /// The two letter iso code.
        /// </value>
        public string TwoLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal? Longitude { get; set; }
    }
}
