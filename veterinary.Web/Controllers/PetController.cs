using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Veterinary.DAL;
using Veterinary.Model;
using Veterinary.Web.Models;

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


        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult IndexAjax(PetFilterModel petFilterModel)
        {
            var pets = veterinaryManagerDbContext.Pets
                .Include(p => p.Owner)
                .Include(p => p.Prescriptions)
                .ToList();


            var filteredPets = FilterPets(pets, petFilterModel);

            return PartialView("_IndexTable", filteredPets.ToList());
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        [HttpPost]
        public IActionResult Filter(PetFilterModel petFilterModel)
        {
            var pets = veterinaryManagerDbContext.Pets
                .Include(p => p.Owner)
                .Include(p => p.Prescriptions)
                .ToList();

            var filteredPets = FilterPets(pets, petFilterModel);

            return View("Index", pets.ToList());
        }


        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult Details(int? id)
        {
            var pet = id != null ? veterinaryManagerDbContext.Pets
                .Include(p => p.Owner)
                .Include(p => p.Prescriptions)
                .Where(p => p.ID == id)
                .FirstOrDefault() : null;

            var prescriptions = veterinaryManagerDbContext.Prescriptions
                .Include(pr => pr.Medications)
                .Include(pr => pr.Pet)
                .Where(pr => pr.PetID == id)
                .ToList();

            ViewBag.prescriptions = prescriptions.ToList();

            return View(pet);
        }

        [Authorize(Roles = RoleConstants.Doctor)]
        public IActionResult Create(string species)
        {
            ViewBag.Owners = veterinaryManagerDbContext.Owners.ToList();

            Pet pet;
            if(species == "Dog")
            {
                pet = new Dog();
            }
            else if(species == "Bird")
            {
                pet = new Bird();
            }
            else
            {
                pet = new Hamster();
            }

            return View(pet);
        }

        [Authorize(Roles = RoleConstants.Doctor)]
        [HttpPost]
        public IActionResult CreateDog(Dog dog)
        {
            ViewBag.Owners = veterinaryManagerDbContext.Owners.ToList();
            if (ModelState.IsValid)
            {
                veterinaryManagerDbContext.Pets.Add(dog);
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(dog);
        }

        [Authorize(Roles = RoleConstants.Doctor)]
        [HttpPost]
        public IActionResult CreateBird(Bird bird)
        {
            ViewBag.Owners = veterinaryManagerDbContext.Owners.ToList();
            if (ModelState.IsValid)
            {
                veterinaryManagerDbContext.Pets.Add(bird);
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(bird);
        }

        [Authorize(Roles = RoleConstants.Doctor)]
        [HttpPost]
        public IActionResult CreateHamster(Hamster hamster)
        {
            ViewBag.Owners = veterinaryManagerDbContext.Owners.ToList();
            if (ModelState.IsValid)
            {
                veterinaryManagerDbContext.Pets.Add(hamster);
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(hamster);
        }


        [Authorize(Roles = RoleConstants.Doctor)]
        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            ViewBag.Owners = veterinaryManagerDbContext.Owners.ToList();

            var pet = veterinaryManagerDbContext.Pets.First(p => p.ID == id);

            return View(pet);
        }


        [Authorize(Roles = RoleConstants.Doctor)]
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var pet = veterinaryManagerDbContext.Pets.Include(p => p.Owner).First(p => p.ID == id);

            bool isOk = await TryUpdateModelAsync(pet);

            var validationErrors = ModelState.Values
                .Where(e => e.Errors.Count > 0)
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            if(isOk)
            {
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public List<Pet> FilterPets(List<Pet> pets, PetFilterModel filterModel)
        {
            // Create a copy of the list to avoid modifying the original list
            var filteredPets = pets.ToList();

            // Apply filters based on the provided filter model properties
            if (!string.IsNullOrEmpty(filterModel.Name))
            {
                filteredPets = filteredPets.Where(pet => pet.Name.ToLowerInvariant().Contains(filterModel.Name.ToLowerInvariant())).ToList();
            }

            if (!string.IsNullOrEmpty(filterModel.OwnerName))
            {
                filteredPets = filteredPets.Where(pet => pet.Owner.FullName.ToLowerInvariant().Contains(filterModel.OwnerName.ToLowerInvariant())).ToList();
            }

            if (!string.IsNullOrEmpty(filterModel.Species))
            {
                filteredPets = filteredPets.Where(pet => pet.Species.ToLowerInvariant().Contains(filterModel.Species.ToLowerInvariant())).ToList();
            }

            return filteredPets;
        }

    }
}
