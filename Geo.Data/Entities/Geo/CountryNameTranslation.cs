using System;
using System.Collections.Generic;

#nullable disable

namespace Geo.Rest.Data.Entities.Geo
{
    public partial class CountryNameTranslation
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Language { get; set; }
        public string Value { get; set; }

        public virtual Country Country { get; set; }
    }
}
