﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleManagerWeb.Models
{
    public class CityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<RegionModel> Region { get; set; }
    }
}
