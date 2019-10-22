using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
}
