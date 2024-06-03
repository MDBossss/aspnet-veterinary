using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veterinary.DAL;
using Veterinary.Model;

namespace Veterinary.Web.Controllers
{
    public class OwnerController : BaseController
    {

        private VeterinaryManagerDbContext veterinaryManagerDbContext;
        private UserManager<AppUser> _userManager;

        public OwnerController(VeterinaryManagerDbContext veterinaryManagerDbContext, UserManager<AppUser> _userManager) : base(_userManager)
        {
            this.veterinaryManagerDbContext = veterinaryManagerDbContext;
            this._userManager = _userManager;
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [HttpGet]
        public IActionResult Index()
        {
            var owners = veterinaryManagerDbContext.Owners.ToList();

            return View(owners.ToList());
        }


        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [HttpPost]
        public IActionResult Create(Owner owner)
        {
            if (ModelState.IsValid)
            {
                veterinaryManagerDbContext.Owners.Add(owner);
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(owner);
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var owner = veterinaryManagerDbContext.Owners.First(m => m.ID == id);

            return View(owner);
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var owner = veterinaryManagerDbContext.Owners.First(m => m.ID == id);

            bool isOk = await TryUpdateModelAsync(owner);

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


        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult Delete(int id)
        {
            var existingOwner = veterinaryManagerDbContext.Owners.First(o => o.ID == id);

            if(existingOwner == null)
            {
                return NotFound();
            }

            veterinaryManagerDbContext.Owners.Remove(existingOwner);
            veterinaryManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
