using Microsoft.AspNetCore.Mvc;
using Veterinary.DAL;
using Veterinary.Model;

namespace Veterinary.Web.Controllers
{

    [Route("api/medication")]
    [ApiController]
    public class MedicationApiController : Controller
    {

        private VeterinaryManagerDbContext veterinaryManagerDbContext;

        public MedicationApiController(VeterinaryManagerDbContext veterinaryManagerDbContext)
        {
            this.veterinaryManagerDbContext = veterinaryManagerDbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var medication = veterinaryManagerDbContext.Medication.ToList();

            return Ok(medication);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var medication = veterinaryManagerDbContext.Medication
                .Where(c => c.ID == id)
                .FirstOrDefault();

            return medication == null ? NotFound() : Ok(medication);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Medication medication)
        {
            veterinaryManagerDbContext.Add(medication);
            veterinaryManagerDbContext.SaveChanges();

            return Get(medication.ID);
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] Medication medication)
        {
            var existingMedication = veterinaryManagerDbContext.Medication.First(c => c.ID == id);

            if (existingMedication == null)
            {
                return NotFound();
            }

            existingMedication.Name = medication.Name;
            existingMedication.DosageUnit = medication.DosageUnit;
            existingMedication.SideEffect = medication.SideEffect;
            existingMedication.ActiveIngredient = medication.ActiveIngredient;


            veterinaryManagerDbContext.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var existingMedication = veterinaryManagerDbContext.Medication.First(c => c.ID == id);

            if (existingMedication == null)
            {
                return NotFound();
            }

            veterinaryManagerDbContext.Medication.Remove(existingMedication);
            veterinaryManagerDbContext.SaveChanges();

            return Ok();

        }
    }
}
