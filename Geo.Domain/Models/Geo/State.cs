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
        /// The iso code.
        /// </value>
        public string IsoCode { get; set; }       
    }
}
