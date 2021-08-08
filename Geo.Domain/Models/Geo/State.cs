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
        
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string FipsCode { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }        
        public string WikiDataId { get; set; }

    }
}
