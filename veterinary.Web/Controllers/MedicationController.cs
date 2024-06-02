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

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var medication = veterinaryManagerDbContext.Medication.First(m => m.ID == id);

            return View(medication);
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var medication = veterinaryManagerDbContext.Medication.First(m => m.ID == id);

            bool isOk = await TryUpdateModelAsync(medication);

            var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                .SelectMany(E => E.Errors)
                .Select(E => E.ErrorMessage)
                .ToList();


            if (isOk)
            {
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
