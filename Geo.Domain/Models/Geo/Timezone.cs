using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Models.Geo
{
    public class TimeZone
    {
        public string Name { get; set; }
        public int? GmtOffset { get; set; }
        public string GmtOffsetName { get; set; }
        public string Abbreviation { get; set; }
        public string TimeZoneName { get; set; }
    }
}
