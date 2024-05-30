
using System.ComponentModel.DataAnnotations;

namespace Veterinary.Model;

public class Owner
{
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
    public string PhoneNumber { get; set; }

    public virtual ICollection<Pet>? Pets { get; set; }

}
