using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Domain.Models.Geo
{
    public class City : Base
    {
        /// <summary>
        /// The latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal Longitude { get; set; }   
    }
}
