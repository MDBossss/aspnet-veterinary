using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
