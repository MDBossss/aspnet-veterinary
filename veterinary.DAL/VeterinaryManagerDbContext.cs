

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Veterinary.Model;

namespace Veterinary.DAL;

public class VeterinaryManagerDbContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Medication> Medication { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    public VeterinaryManagerDbContext(DbContextOptions<VeterinaryManagerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<Prescription>()
            .HasMany(d => d.Medacations)
            .WithMany(f => f.Prescriptions);


        // Seed roles

        builder.Entity<IdentityRole>()
            .HasData(new IdentityRole { Id = "1", Name = "Doctor", NormalizedName = "DOCTOR" });

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        { Id = "2", Name = "Apprentice", NormalizedName = "APPRENTICE" });



        // Default owner entries

        builder.Entity<Owner>().HasData(new Owner
        {
            ID = 1,
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
        });


        builder.Entity<Owner>().HasData(new Owner
        {
            ID = 2,
            FirstName = "Mary",
            LastName = "Jane",
            PhoneNumber = "7224567892",
        });



        // Default pet entries

        builder.Entity<Pet>().HasData(new Dog
        {
            ID = 1,
            Name = "Buddy",
            Breed = "Labrador",
            BirthDate = new DateTime(),
            Weight = 32,
            OwnerID = 1
        });

        builder.Entity<Pet>().HasData(new Bird
        {
            ID = 2,
            Name = "Koko",
            Wingspan = 15.5,
            BirthDate = DateTime.Now,
            Weight = 3.7,
            OwnerID = 2
        });



        // Default medication entries

        builder.Entity<Medication>().HasData(new Medication
        {
            ID = 1,
            Name = "Amoxicillin"
        });

        builder.Entity<Medication>().HasData(new Medication
        {
            ID = 2,
            Name = "Prednisolone"
        });



 
    }
}