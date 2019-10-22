using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Region
    {
        public Region()
        {
            City = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
}
