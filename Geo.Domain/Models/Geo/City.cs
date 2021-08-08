using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Domain.Models.Geo
{
    public partial class City : Base
    {        
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }        
        public string WikiDataId { get; set; }

        public Country Country { get; set; }
        public State State { get; set; }
    }
}
