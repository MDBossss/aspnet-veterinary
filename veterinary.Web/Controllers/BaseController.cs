using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Veterinary.Model;

namespace Veterinary.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<AppUser> _userManager;
        public string UserID { get; set; }

        public BaseController(UserManager<AppUser> _userManager)
        {
            this._userManager = _userManager;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserID = _userManager.GetUserId(User);
            base.OnActionExecuting(context);
        }
    }
}
