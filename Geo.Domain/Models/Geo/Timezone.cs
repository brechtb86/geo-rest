using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Domain.Models.Geo
{
    public class Timezone
    {
        public string ZoneName { get; set; }
        public int GmtOffset { get; set; }
        public string GmtOffsetName { get; set; }
        public string Abbreviation { get; set; }
        public string TimezoneName { get; set; }
    }
}
