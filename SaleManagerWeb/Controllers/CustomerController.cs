using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaleManagerWeb.Models;

namespace SaleManagerWeb.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private IContext Context { get; set; }
        public CustomerController(IContext context)
        {
            Context = context;
        }

        private IEnumerable<CustomerModel> List(string name = null, int gender = 0, int city = 0, int region = 0, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            var userRole = User.Claims.FirstOrDefault(x => x.Type == "role").Value;
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;


            var list = Context
                .Customer.AsQueryable();

            if (userRole == "Seller")
            {
                list = list.Where(x => x.UserId == Convert.ToInt32(userId));
            }

            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(x => x.Name == name);
            }

            if (gender != 0)
            {
                list = list.Where(x => x.GenderId == gender);
            }

            if (city != 0)
            {
                list = list.Where(x => x.CityId == city);
            }

            if (region != 0)
            {
                list = list.Where(x => x.RegionId == region);
            }

            if (initialDate != null && finalDate != null)
            {
                list = list.Where(x => x.LastPurchase >= initialDate && x.LastPurchase <= finalDate);
            }

            var listToReturn = list.Select(x => new
            {
                x.Name,
                x.Phone,
                x.LastPurchase,
                City = Context.City.FirstOrDefault(c => c.Id == x.CityId).Name,
                Gender = Context.Gender.FirstOrDefault(g => g.Id == x.GenderId).Name,
                Region = Context.Region.FirstOrDefault(r => r.Id == x.RegionId).Name,
                Classification = Context.Classification.FirstOrDefault(c => c.Id == x.ClassificationId).Name,
                User = Context.UserSys.FirstOrDefault(u => u.Id == x.UserId).Login

            })
                .ToList()
                .Select(x => new CustomerModel()
                {
                    Name = x.Name,
                    LastPurchase = x.LastPurchase,
                    Phone = x.Phone,
                    Classification = new ClassificaionModel() { Name = x.Classification },
                    City = new CityModel() { Name = x.City },
                    Region = new RegionModel() { Name = x.Region },
                    Gender = new GenderModel() { Name = x.Name },
                    User = new UserModel() { Login = x.User }
                });
            return listToReturn;
        }

        public IActionResult Index(string name, int gender, int city, int region, DateTime? initialDate, DateTime? finalDate)
        {
            ViewBag.Cities = Context.City.ToList();
            ViewBag.Sellers = Context.UserSys.ToList();
            ViewBag.Gender = Context.Gender.ToList();
            ViewBag.Region = Context.Region.ToList();
            return View(List(name, gender, city, region));
        }
    }
}
