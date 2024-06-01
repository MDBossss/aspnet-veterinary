
using System.Buffers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinary.Model;

public abstract class Pet
{
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    public DateTime? BirthDate { get; set; }

    [Required]
    public double Weight { get; set; }

    [ForeignKey(nameof(Owner))]
    [Required(AllowEmptyStrings = false, ErrorMessage = "The Owner field is required")]
    public int OwnerID { get; set; }

    public virtual Owner? Owner { get; set; }

    public ICollection<Prescription>? Prescriptions { get; set; }

    [Required]
    public abstract string Species { get; }


    public string Age
    {
        get
        {
            if(BirthDate == null)
            {
                return "1 month";
            }

            var today = DateTime.Now;
            var birthDate = BirthDate ?? DateTime.MinValue;
            var age = today.Year - birthDate.Year;

            // Check if birthday has passed in the current year
            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day))
            {
                age--; // Decrement age if birthday hasn't passed yet
            }

            if (age < 1)
            {
                var months = today.Month - birthDate.Month;
                if (months < 0)
                {
                    months += 12; // Adjust for months that haven't passed in the current year
                }
                return $"{months+1} month{(months+1 > 1 ? "s" : "")}";
            }
            else
            {
                return $"{age} year{(age > 1 ? "s" : "")}";
            }
        }
    }

}



public class Dog: Pet
{
    public override string Species => "Dog";

    public string Breed { get; set; }
}

public class Bird: Pet
{
    public override string Species => "Bird";
    public double Wingspan {  get; set; }

}

public class Hamster: Pet
{
    public override string Species => "Hamster";
    public int TimesSteppedOn { get; set; }
}
