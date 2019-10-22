using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleManagerWeb.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public UserRoleModel Role { get; set; }
    }
}
