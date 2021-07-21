namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;

    public class BreedsController : Controller
    {
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;

        public BreedsController(
            IDeletableEntityRepository<Breed> breedsRepository,
            IDeletableEntityRepository<Kingdom> kingdomsRepository)
        {
            this.breedsRepository = breedsRepository;
            this.kingdomsRepository = kingdomsRepository;
        }

        // GET: Breeds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.breedsRepository
                .All()
                .Include(b => b.User)
                .OrderBy(x => x.Name);

            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Breeds/{name}
        [HttpGet("/Breeds/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var applicationDbContext = this.breedsRepository
                .All()
                .Include(b => b.User)
                .Where(x => x.KingdomName == name)
                .OrderBy(x => x.Name);

            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Breeds/Details/{name}
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var breed = await this.breedsRepository
                .All()
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (breed == null)
            {
                return this.NotFound();
            }

            return this.View(breed);
        }

        // GET: Breeds/Create
        [HttpGet("/Breeds/Create/")]
        public IActionResult Create()
        {
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All().OrderBy(x => x.Name), "Name", "Name");

            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Breeds/Create
        [HttpPost("/Breeds/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Breed breed)
        {
            if (this.ModelState.IsValid)
            {
                await this.breedsRepository.AddAsync(breed);
                await this.breedsRepository.SaveChangesAsync();
                return this.RedirectToAction(breed.KingdomName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", breed.KingdomName);

            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", breed.UserId);
            return this.View(breed);
        }

        // GET: Breeds/Edit/{name}
        [HttpGet("/Breeds/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var breed = await this.breedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (breed == null)
            {
                return this.NotFound();
            }

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name");

            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", breed.UserId);
            return this.View(breed);
        }

        // POST: Breeds/Edit/{name}
        [HttpPost("/Breeds/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, [Bind("Name,PicUrl,Description,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Breed breed)
        {
            if (name != breed.Name)
            {
                return this.NotFound();
            }

            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();

            if (this.ModelState.IsValid)
            {
                try
                {
                    var editName = await this.breedsRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Name == oldName);

                    foreach (var animals in this.breedsRepository.All().Where(x => x.Name == oldName))
                    {
                        animals.Name = name;
                    }

                    this.breedsRepository.Delete(editName);
                    await this.breedsRepository.AddAsync(breed);
                    await this.breedsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.BreedExists(breed.Name))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(breed.KingdomName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", breed.KingdomName);

            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", breed.UserId);
            return this.View(breed);
        }

        // GET: Breeds/Delete/{name}
        [HttpGet("/Breeds/Delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var breed = await this.breedsRepository
                .All()
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (breed == null)
            {
                return this.NotFound();
            }

            return this.View(breed);
        }

        // POST: Breeds/Delete/{name}
        [HttpPost("/Breeds/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name)
        {
            var breed = await this.breedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            this.breedsRepository.Delete(breed);
            await this.breedsRepository.SaveChangesAsync();
            return this.RedirectToAction(breed.KingdomName);
        }

        // Checking if Breed Exist
        private bool BreedExists(string name)
        {
            return this.breedsRepository
                .All()
                .Any(e => e.Name == name);
        }
    }
}
