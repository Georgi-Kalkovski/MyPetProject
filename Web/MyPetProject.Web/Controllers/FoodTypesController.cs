namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;
    using MyPetProject.Web.ViewModels.FoodTypes;

    public class FoodTypesController : BaseController
    {
        private readonly IDeletableEntityRepository<FoodType> foodtypesRepository;
        private readonly IDeletableEntityRepository<Food> foodsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationsRepository;

        public FoodTypesController(
            IDeletableEntityRepository<FoodType> foodtypesRepository,
            IDeletableEntityRepository<Food> foodsRepository,
            IDeletableEntityRepository<ApplicationUser> applicationsRepository)
        {
            this.foodtypesRepository = foodtypesRepository;
            this.foodsRepository = foodsRepository;
            this.applicationsRepository = applicationsRepository;
        }

        // GET: FoodTypes
        public async Task<IActionResult> Index() => await this.IndexWithoutNameMethod();

        // GET: FoodTypes/{name}
        [HttpGet("/FoodTypes/{name}")]
        public async Task<IActionResult> Index(string name) => await this.IndexWithNameMethod(name);

        // GET: FoodTypes/Details/{name}
        [HttpGet("/FoodTypes/Details/{name}")]
        public async Task<IActionResult> Details(string name) => await this.DetailsMethod(name);

        // GET: FoodTypes/Create
        [HttpGet("/FoodTypes/Create/")]
        public IActionResult Create() => this.CreateGet();

        // POST: FoodTypes/Create
        [HttpPost("/FoodTypes/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodTypeInputModel foodType)
            => await this.CreatePost(foodType);

        // GET: FoodTypes/Edit/{name}
        [HttpGet("/FoodTypes/Edit/{name}")]
        public async Task<IActionResult> Edit(string name) => await this.EditGet(name);

        // POST: FoodTypes/Edit/{name}
        [HttpPost("/FoodTypes/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, FoodTypeInputModel foodType)
            => await this.EditPost(name, foodType);

        // GET: FoodTypes/Delete/{name}
        [HttpGet("/FoodTypes/Delete/{name}")]
        public async Task<IActionResult> Delete(string name) => await this.DeleteGet(name);

        // POST: FoodTypes/Delete/{name}
        [HttpPost("/FoodTypes/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string name) => await this.DeletePost(id);

        private async Task<IActionResult> IndexWithoutNameMethod()
        {
            var result = this.foodtypesRepository.
                All()
                .Include(f => f.User)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> IndexWithNameMethod(string name)
        {
            var result = this.foodtypesRepository
                            .All()
                            .Include(b => b.User)
                            .Include(x => x.Foods)
                            .Where(x => x.Name == name)
                            .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> DetailsMethod(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.foodtypesRepository
                .All()
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.View(result);
        }

        private IActionResult CreateGet()
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/Home/ErrorPage");
            }

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id");
            return this.View();
        }

        private async Task<IActionResult> CreatePost(FoodTypeInputModel foodType)
        {
            if (this.ModelState.IsValid)
            {
                var result = new FoodType
                {
                    Name = foodType.Name,
                    PicUrl = foodType.PicUrl,
                    Description = foodType.Description,
                    UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                };

                await this.foodtypesRepository.AddAsync(result);
                await this.foodtypesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(foodType);
        }

        private async Task<IActionResult> EditGet(string name)
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/Home/ErrorPage");
            }

            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.foodtypesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            if (this.User.Claims.ToList()[0].Value != result.UserId)
            {
                return this.Redirect("/Home/ErrorPage");
            }

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", this.foodtypesRepository.All().Include(x => x.UserId));
            return this.View(result);
        }

        private async Task<IActionResult> EditPost(string name, FoodTypeInputModel foodType)
        {
            if (name != foodType.Name)
            {
                return this.NotFound();
            }

            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();

            if (this.ModelState.IsValid)
            {
                try
                {
                    var editName = await this.foodtypesRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Name == oldName);

                    foreach (var foods in this.foodsRepository.All().Where(x => x.FoodType.Name == oldName))
                    {
                        foods.FoodType.Name = name;
                    }

                    var result = new FoodType
                    {
                        Id = foodType.Id,
                        Name = foodType.Name,
                        PicUrl = foodType.PicUrl,
                        Description = foodType.Description,
                        UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    };

                    this.foodtypesRepository.HardDelete(editName);
                    await this.foodtypesRepository.AddAsync(result);
                    await this.foodtypesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.FoodTypeExists(foodType.Name))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(foodType);
        }

        private async Task<IActionResult> DeleteGet(string name)
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/Home/ErrorPage");
            }

            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.foodtypesRepository
                .All()
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            if (this.User.Claims.ToList()[0].Value != result.UserId)
            {
                return this.Redirect("/Home/ErrorPage");
            }

            return this.View(result);
        }

        private async Task<IActionResult> DeletePost(int? id)
        {
            var result = await this.foodtypesRepository
                            .All()
                            .FirstOrDefaultAsync(x => x.Id == id);

            this.foodtypesRepository.Delete(result);
            await this.foodtypesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool FoodTypeExists(string name)
        {
            return this.foodtypesRepository
                .All()
                .Any(e => e.Name == name);
        }
    }
}
