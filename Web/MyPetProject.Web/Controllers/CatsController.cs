namespace MyPetProject.Web.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using MyPetProject.Web.ViewModels.Cats;

    public class CatsController : Controller
    {
        public IActionResult All()
        {
            var cats = new List<CatViewModel>
            {
                 new CatViewModel { Name = "Sharo", Age = 6},
                 new CatViewModel { Name = "Lady", Age = 14},
            };

            return this.View(cats);
            //return Ok(cats);     returning json
        }
    }
}
