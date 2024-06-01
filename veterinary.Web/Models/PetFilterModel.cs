namespace Veterinary.Web.Models;

public record PetFilterModel(

    string Name = "",
    string OwnerName = "",
    string Species = ""
    );
