using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veterinary.DAL;
using Veterinary.Model;

namespace Veterinary.Web.Controllers
{
    public class PrescriptionController : BaseController
    {

        private VeterinaryManagerDbContext veterinaryManagerDbContext;
        private UserManager<AppUser> _userManager;

        public PrescriptionController(VeterinaryManagerDbContext veterinaryManagerDbContext, UserManager<AppUser> _userManager) : base(_userManager)
        {
            this.veterinaryManagerDbContext = veterinaryManagerDbContext;
            this._userManager = _userManager;
        }

        [Authorize(Roles = RoleConstants.Doctor)]
        public IActionResult Create(int petid)
        {
            ViewBag.Medication = veterinaryManagerDbContext.Medication.ToList();
            ViewBag.PetID = petid;

            return View();
        }

        [Authorize(Roles = RoleConstants.Doctor)]

        [HttpPost]
        public IActionResult Create(Prescription prescription)
        {
            ViewBag.Medication = veterinaryManagerDbContext.Medication.ToList();

            if (ModelState.IsValid)
            {
                var selectedMedicationIds = Request.Form["Medications"];

                var prescriptionWithMeds = new Prescription
                {
                    Dosage = prescription.Dosage,
                    Diagnosis = prescription.Diagnosis,
                    Date = prescription.Date,
                    PetID = prescription.PetID,
                    Medications = selectedMedicationIds.Select(id => veterinaryManagerDbContext.Medication.Find(int.Parse(id))).ToList() // Eager loading of Medications
                };

                veterinaryManagerDbContext.Prescriptions.Add(prescriptionWithMeds);
                veterinaryManagerDbContext.SaveChanges();
                return RedirectToAction("Index","Pet");
            }

            return View(prescription);
        }

        [Authorize(Roles = RoleConstants.DoctorOrApprentice)]
        public IActionResult Delete(int id)
        {
            var existingPrescription = veterinaryManagerDbContext.Prescriptions.First(pr => pr.ID == id);

            if(existingPrescription == null)
            {
                return NotFound();
            }

            veterinaryManagerDbContext.Prescriptions.Remove(existingPrescription);
            veterinaryManagerDbContext.SaveChanges();

            return RedirectToAction("Details", "Pet", new { id = existingPrescription.PetID });
        }

    }
}
