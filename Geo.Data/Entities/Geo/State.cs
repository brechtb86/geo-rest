using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Data.Entities.Geo
{
    public partial class State : Base
    {
        public State()
        {
            Cities = new HashSet<City>();
        }
       
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string FipsCode { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }        

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
