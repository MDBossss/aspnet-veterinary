using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veterinary.DAL;
using Veterinary.Model;

namespace Veterinary.Web.Controllers
{
    public class MedicationController : BaseController
    {

        private VeterinaryManagerDbContext veterinaryManagerDbContext;
        private UserManager<AppUser> _userManager;

        public MedicationController(VeterinaryManagerDbContext veterinaryManagerDbContext, UserManager<AppUser> _userManager) : base(_userManager)
        {
            this.veterinaryManagerDbContext = veterinaryManagerDbContext;
            this._userManager = _userManager;
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [HttpGet]
        public IActionResult Index()
        {
            var medication = veterinaryManagerDbContext.Medication.ToList();

            return View(medication.ToList());
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [HttpPost]

        public IActionResult Create(Medication medication)
        {
            if(ModelState.IsValid)
            {
                veterinaryManagerDbContext.Medication.Add(medication);
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medication);
        }
    }
}
