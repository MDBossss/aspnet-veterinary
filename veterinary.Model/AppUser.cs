using Microsoft.AspNetCore.Identity;

namespace Veterinary.Model
{
    public class AppUser : IdentityUser
    {

    }

    public static class RoleConstants
    {
        public const string Doctor = "Doctor";
        public const string Apprentice = "Apprentice";
        public const string DoctorOrApprentice = "Doctor, Apprentice";
    }
}
