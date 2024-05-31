using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinary.DAL;
using Veterinary.Model;

namespace Veterinary.Web.Controllers
{
    public class PetController : BaseController
    {

        private VeterinaryManagerDbContext veterinaryManagerDbContext;
        private UserManager<AppUser> _userManager;

        public PetController(VeterinaryManagerDbContext veterinaryManagerDbContext, UserManager<AppUser> _userManager) :base(_userManager) { 
            this.veterinaryManagerDbContext = veterinaryManagerDbContext;
            this._userManager = _userManager;
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult Index()
        {
            var pets = veterinaryManagerDbContext.Pets
                .Include(p => p.Owner)
                .Include(p => p.Prescriptions)
                .ToList();

            return View(pets.ToList());
        }
    }
}
