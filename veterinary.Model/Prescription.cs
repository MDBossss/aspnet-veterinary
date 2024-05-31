
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinary.Model;

public class Prescription
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Dosage { get; set; }

    [Required]
    public string Diagnosis { get; set; }

    [Required]
    public DateTime Date { get; set; }
    
    [ForeignKey("Pet")]
    public int PetID { get; set; }

    public virtual Pet? Pet { get; set; }

    public ICollection<Medication> Medications { get; set; }
}
