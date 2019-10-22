using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleManagerWeb.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public GenderModel Gender { get; set; }
        public CityModel City { get; set; }
        public RegionModel Region { get; set; }
        public DateTime? LastPurchase { get; set; }
        public ClassificaionModel Classification { get; set; }
        public UserModel User { get; set; }
    }
}
