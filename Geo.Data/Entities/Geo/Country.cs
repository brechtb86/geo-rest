using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Data.Entities.Geo
{
    public partial class Country : Base
    {
        public Country()
        {
            Cities = new HashSet<City>();
            States = new HashSet<State>();
        }
                       
        public string ThreeLetterIsoCode { get; set; }
        public string NumericCode { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public string Tld { get; set; }
        public string Native { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string Timezones { get; set; }
        public string Translations { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }       

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
