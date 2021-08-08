using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Models.Geo
{
    public abstract class Base
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short Flag { get; set; }
    }
}
