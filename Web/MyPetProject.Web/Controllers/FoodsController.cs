namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;

    public class FoodsController : BaseController
    {
        private readonly IDeletableEntityRepository<FoodType> foodtypesRepository;
        private readonly IDeletableEntityRepository<Food> foodsRepository;
        private readonly IDeletableEntityRepository<Subbreed> subbreedsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationsRepository;

        public FoodsController(
            IDeletableEntityRepository<FoodType> foodtypesRepository,
            IDeletableEntityRepository<Food> foodsRepository,
            IDeletableEntityRepository<Subbreed> subbreedsRepository,
            IDeletableEntityRepository<ApplicationUser> applicationsRepository)
        {
            this.foodtypesRepository = foodtypesRepository;
            this.foodsRepository = foodsRepository;
            this.subbreedsRepository = subbreedsRepository;
            this.applicationsRepository = applicationsRepository;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.foodsRepository
                .All()
                .Include(f => f.FoodType)
                .Include(f => f.Subbreed)
                .Include(f => f.User);

            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Foods/{name}
        [HttpGet("/Foods/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var result = this.foodsRepository
                .All()
                .Include(b => b.User)
                .Where(x => x.FoodTypeName == name)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        // GET: Foods/Details/{name}
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.foodsRepository
                .All()
                .Include(f => f.FoodType)
                .Include(f => f.Subbreed)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.View(result);
        }

        // GET: Foods/Create
        [HttpGet("/Foods/Create/")]
        public IActionResult Create()
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/Home/ErrorPage");
            }

            this.ViewData["FoodTypeId"] = new SelectList(this.foodtypesRepository.All(), "Id", "Description");
            this.ViewData["SubbreedId"] = new SelectList(this.subbreedsRepository.All(), "Id", "Description");
            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id");
            this.ViewData["FoodTypeName"] = new SelectList(this.foodtypesRepository.All().OrderBy(x => x.Name), "Name", "Name");
            return this.View();
        }

        // POST: Foods/Create
        [HttpPost("/Foods/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,PicUrl,Description,FoodTypeName,FoodTypeId,SubbreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Food food)
        {
            if (this.ModelState.IsValid)
            {
                food.UserId = this.User.Claims.ToList()[0].Value;
                await this.foodsRepository.AddAsync(food);
                await this.foodsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["FoodTypeId"] = new SelectList(this.foodtypesRepository.All(), "Id", "Description", food.FoodTypeId);
            this.ViewData["SubbreedId"] = new SelectList(this.subbreedsRepository.All(), "Id", "Description", food.SubbreedId);
            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", food.UserId);
            this.ViewData["FoodTypeName"] = new SelectList(this.foodtypesRepository.All(), "Name", "Name", food.FoodTypeName);
            return this.View(food);
        }

        // GET: Foods/Edit/{name}
        [HttpGet("/Foods/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/Home/ErrorPage");
            }

            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.foodsRepository
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

            this.ViewData["FoodTypeId"] = new SelectList(this.foodtypesRepository.All(), "Id", "Description", this.foodsRepository.All().Include(x => x.FoodTypeId));
            this.ViewData["SubbreedId"] = new SelectList(this.subbreedsRepository.All(), "Id", "Description", this.foodsRepository.All().Include(x => x.SubbreedId));
            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", this.foodsRepository.All().Include(x => x.UserId));
            this.ViewData["FoodTypeName"] = new SelectList(this.foodtypesRepository.All(), "Name", "Name");
            return this.View(result);
        }

        // POST: Foods/Edit/{name}
        [HttpPost("/Foods/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string name, [Bind("Name,PicUrl,Description,FoodTypeName,FoodTypeId,SubbreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Food food)
        {
            if (name != food.Name)
            {
                return this.NotFound();
            }

            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();

            if (this.ModelState.IsValid)
            {
                try
                {
                    var editName = await this.foodsRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Name == oldName);

                    foreach (var currentFood in this.foodsRepository.All().Where(x => x.Name == oldName))
                    {
                        currentFood.Name = name;
                    }

                    this.foodsRepository.Delete(editName);
                    food.UserId = this.User.Claims.ToList()[0].Value;
                    await this.foodsRepository.AddAsync(food);
                    await this.foodsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.FoodExists(food.Name))
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

            this.ViewData["FoodTypeId"] = new SelectList(this.foodtypesRepository.All(), "Id", "Description", food.FoodTypeId);
            this.ViewData["SubbreedId"] = new SelectList(this.subbreedsRepository.All(), "Id", "Description", food.SubbreedId);
            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", food.UserId);
            this.ViewData["FoodTypeName"] = new SelectList(this.foodtypesRepository.All(), "Name", "Name", food.FoodTypeName);
            return this.View(food);
        }

        // GET: Foods/Delete/{name}
        [HttpGet("/Foods/Delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/Home/ErrorPage");
            }

            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.foodsRepository
                .All()
                .Include(f => f.FoodType)
                .Include(f => f.Subbreed)
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

        // POST: Foods/Delete/{name}
        [HttpPost("/Foods/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string name)
        {
            var result = await this.foodsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            this.foodsRepository.Delete(result);
            await this.foodsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool FoodExists(string name)
        {
            return this.foodsRepository
                .All()
                .Any(e => e.Name == name);
        }
    }
}
