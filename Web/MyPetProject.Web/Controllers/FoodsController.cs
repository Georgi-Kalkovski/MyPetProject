using Microsoft.AspNetCore.Mvc;
using MyPetProject.Web.ViewModels.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPetProject.Web.Controllers
{
    public class FoodsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public Task<IActionResult> Create(FoodViewModel inputModel)
        {
            return this.View();
        }
    }
}
