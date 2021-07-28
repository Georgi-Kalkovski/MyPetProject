namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;

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
        public async Task<IActionResult> Index()
        {
            var result = this.foodtypesRepository.
                All()
                .Include(f => f.User)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        // GET: FoodTypes/{name}
        [HttpGet("/FoodTypes/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var result = this.foodtypesRepository
                .All()
                .Include(b => b.User)
                .Include(x => x.Foods)
                .Where(x => x.Name == name)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        // GET: FoodTypes/Details/{name}
        public async Task<IActionResult> Details(string name)
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

        // GET: FoodTypes/Create
        [HttpGet("/FoodTypes/Create/")]
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id");
            return this.View();
        }

        // POST: FoodTypes/Create
        [HttpPost("/FoodTypes/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] FoodType foodType)
        {
            if (this.ModelState.IsValid)
            {
                await this.foodtypesRepository.AddAsync(foodType);
                await this.foodtypesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", foodType.UserId);
            return this.View(foodType);
        }

        // GET: FoodTypes/Edit/{name}
        [HttpGet("/FoodTypes/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
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

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", this.foodtypesRepository.All().Include(x => x.UserId));
            return this.View(result);
        }

        // POST: FoodTypes/Edit/{name}
        [HttpPost("/FoodTypes/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, [Bind("Name,PicUrl,Description,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] FoodType foodType)
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

                    this.foodtypesRepository.Delete(editName);
                    await this.foodtypesRepository.AddAsync(foodType);
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

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", foodType.UserId);
            return this.View(foodType);
        }

        // GET: FoodTypes/Delete/{name}
        [HttpGet("/FoodTypes/Delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
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

            return this.View(result);
        }

        // POST: FoodTypes/Delete/{name}
        [HttpPost("/FoodTypes/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string name)
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
