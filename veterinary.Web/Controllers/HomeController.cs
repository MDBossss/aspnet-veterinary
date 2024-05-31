using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using veterinary.Models;
using Veterinary.DAL;
using Veterinary.Model;
using Veterinary.Web.Controllers;

namespace veterinary.Controllers
{
    public class HomeController : BaseController
    {

        private VeterinaryManagerDbContext veterinaryManagerDbContext;
        private UserManager<AppUser> _userManager;

        public HomeController(VeterinaryManagerDbContext veterinaryManagerDbContext, UserManager<AppUser> _userManager) : base(_userManager)
        {
            this.veterinaryManagerDbContext = veterinaryManagerDbContext;
            this._userManager = _userManager;
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
