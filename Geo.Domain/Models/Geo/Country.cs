using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Geo.Rest.Domain.Models.Geo
{
    public class Country : Base
    {
        private string _translatedName;

        public Country()
        {
        }

        /// <summary>
        /// The translated name based on the language parameter.
        /// </summary>
        /// <value>
        /// The translated name based on the language parameter.
        /// </value>
        public string TranslatedName
        {
            get
            {
                return string.IsNullOrEmpty(this._translatedName) ? this.Name : this._translatedName;
            }
            set
            {
                this._translatedName = value;
            }
        }

        /// <summary>
        /// The three letter iso code.
        /// </summary>
        /// <value>
        /// The three letter iso code.
        /// </value>
        public string ThreeLetterIsoCode { get; set; }

        /// <summary>
        /// The numeric code.
        /// </summary>
        /// <value>
        /// The numeric code.
        /// </value>
        public string NumericCode { get; set; }

        /// <summary>
        /// The two letter iso code.
        /// </summary>
        /// <value>
        /// The two letter iso code.
        /// </value>
        public string TwoLetterIsoCode { get; set; }

        /// <summary>
        /// The phone code.
        /// </summary>
        /// <value>
        /// The phone code.
        /// </value>
        public string PhoneCode { get; set; }

        /// <summary>
        /// The capital.
        /// </summary>
        /// <value>
        /// The capital.
        /// </value>
        public string Capital { get; set; }

        /// <summary>
        /// The currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// The currency symbol.
        /// </summary>
        /// <value>
        /// The currency symbol.
        /// </value>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// The TLD.
        /// </summary>
        /// <value>
        /// The TLD.
        /// </value>
        public string Tld { get; set; }

        /// <summary>
        /// The native.
        /// </summary>
        /// <value>
        /// The native.
        /// </value>
        public string Native { get; set; }

        /// <summary>
        /// The region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// The sub region.
        /// </summary>
        /// <value>
        /// The sub region.
        /// </value>
        public string SubRegion { get; set; }

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
        /// The emoji.
        /// </summary>
        /// <value>
        /// The emoji.
        /// </value>
        public string Emoji { get; set; }

        /// <summary>
        /// The emoji u.
        /// </summary>
        /// <value>
        /// The emoji u.
        /// </value>
        public string EmojiUnicode { get; set; }

        /// <summary>
        /// The timezones.
        /// </summary>
        /// <value>
        /// The timezones.
        /// </value>
        public ICollection<TimeZone> TimeZones { get; set; }
    }
}
