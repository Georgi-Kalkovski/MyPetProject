namespace MyPetProject.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyPetProject.Common;
    using MyPetProject.Data.Models;

    public class AdminsController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminsController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (this.signInManager.IsSignedIn(this.User))
            {
                var user = await this.userManager.GetUserAsync(this.User);
                if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) == false)
                {
                    await this.userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }

            this.SignOut();
            return this.RedirectToAction("Index","Home");
        }
    }
}
