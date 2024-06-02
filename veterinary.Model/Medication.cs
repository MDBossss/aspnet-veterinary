
using System.ComponentModel.DataAnnotations;

namespace Veterinary.Model;

public class Medication
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public ICollection<Prescription>? Prescriptions { get; set; }
}
