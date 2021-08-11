using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Models.Geo
{
    public abstract class Base
    {     
        /// <summary>
        /// The identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// The flag.
        /// </summary>
        /// <value>
        /// The flag.
        /// </value>
        public short Flag { get; set; }

        /// <summary>
        /// The latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// The longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// The wiki data identifier.
        /// </summary>
        /// <value>
        /// The wiki data identifier.
        /// </value>
        public string WikiDataId { get; set; }       

        /// <summary>
        /// The created at date.
        /// </summary>
        /// <value>
        /// The created at date.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The updated at date.
        /// </summary>
        /// <value>
        /// The updated at date.
        /// </value>
        public DateTime UpdatedAt { get; set; }

       
    }
}
