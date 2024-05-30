
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
    public DateTime BirthDate { get; set; }

    [Required]
    public double Weight { get; set; }

    [ForeignKey("Owner")]
    public int OwnerID { get; set; }

    public virtual Owner Owner { get; set; }

    [Required]
    public abstract string Species { get; }

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
