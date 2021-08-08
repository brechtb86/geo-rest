using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Data.Entities.Geo
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short Flag { get; set; }
        public string WikiDataId { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
    }
}
