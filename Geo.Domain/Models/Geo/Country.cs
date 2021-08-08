using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Geo.Domain.Models.Geo
{
    public partial class Country : Base
    {
        public Country()
        {
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
        [JsonIgnore]
        public string TimezonesJson { get; set; }
        [JsonIgnore]
        public string TranslationsJson { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }        
        public string WikiDataId { get; set; }

        public ICollection<Timezone> Timezones { get; set; }
        public ICollection<Translation> Translations { get; set; }
    }
}
