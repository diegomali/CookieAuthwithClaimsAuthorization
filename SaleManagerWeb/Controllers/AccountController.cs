using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SaleManagerWeb.Controllers
{

    public class AccountController : Controller
    {
        private IContext Context { get; set; }
        public AccountController(IContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private bool ValidateLogin(string email, string password) => Context.UserSys.Any(x => x.Email == email && x.Password == password);

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ValidateLogin(email, password))
            {
                var claims = new List<Claim>
                {
                    new Claim("user", email),
                    new Claim("userId", Context.UserSys.FirstOrDefault(x => x.Email == email).Id.ToString())
                };


                claims.AddRange(Context
                .UserSys
                .Where(x => x.Email == email)
                .ToList()
                .SelectMany(x =>
                {
                    var role = Context.UserRole.FirstOrDefault(r => r.Id == x.UserRoleId);
                    return new List<Claim>() { new Claim("role", role.Name), new Claim("isAdmin", role.IsAdmin.ToString())};
                }));

                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            ViewBag.LoginFail = true;
            return View();
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
