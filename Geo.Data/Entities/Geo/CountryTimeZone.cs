using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Data.Entities.Geo
{
    public partial class CountryTimeZone
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public int? GmtOffset { get; set; }
        public string GmtOffsetName { get; set; }
        public string Abbreviation { get; set; }
        public string TimeZoneName { get; set; }

        public virtual Country Country { get; set; }
    }
}
