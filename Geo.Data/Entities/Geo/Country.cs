using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Data.Entities.Geo
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            CountryNameTranslations = new HashSet<CountryNameTranslation>();
            CountryTimeZones = new HashSet<CountryTimeZone>();
            States = new HashSet<State>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
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
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Emoji { get; set; }
        public string EmojiUnicode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short Flag { get; set; }
        public string WikiDataId { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<CountryNameTranslation> CountryNameTranslations { get; set; }
        public virtual ICollection<CountryTimeZone> CountryTimeZones { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
