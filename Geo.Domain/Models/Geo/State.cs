using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Domain.Models.Geo
{
    public partial class State
    {
        public State()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string FipsCode { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short Flag { get; set; }
        public string WikiDataId { get; set; }

    }
}
