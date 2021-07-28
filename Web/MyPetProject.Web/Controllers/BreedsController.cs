namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;

    public class BreedsController : BaseController
    {
        private readonly IDeletableEntityRepository<Subbreed> subbreedsRepository;
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationsRepository;

        public BreedsController(
            IDeletableEntityRepository<Subbreed> subbreedsRepository,
            IDeletableEntityRepository<Breed> breedsRepository,
            IDeletableEntityRepository<Kingdom> kingdomsRepository,
            IDeletableEntityRepository<ApplicationUser> applicationsRepository)
        {
            this.subbreedsRepository = subbreedsRepository;
            this.breedsRepository = breedsRepository;
            this.kingdomsRepository = kingdomsRepository;
            this.applicationsRepository = applicationsRepository;
        }

        // GET: Breeds
        public async Task<IActionResult> Index()
        {
            var result = this.breedsRepository
                .All()
                .Include(b => b.User)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        // GET: Breeds/{name}
        [HttpGet("/Breeds/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var result = this.breedsRepository
                .All()
                .Include(b => b.User)
                .Where(x => x.KingdomName == name)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        // GET: Breeds/Details/{name}
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.breedsRepository
                .All()
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.View(result);
        }

        // GET: Breeds/Create
        [HttpGet("/Breeds/Create/")]
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id");
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All().OrderBy(x => x.Name), "Name", "Name");
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

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", breed.UserId);
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", breed.KingdomName);

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

            var result = await this.breedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", this.breedsRepository.All().Include(x => x.UserId));
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name");

            return this.View(result);
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

                    foreach (var animals in this.subbreedsRepository.All().Where(x => x.BreedName == oldName))
                    {
                        animals.BreedName = name;
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

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", breed.UserId);
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", breed.KingdomName);

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

            var result = await this.breedsRepository
                .All()
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.View(result);
        }

        // POST: Breeds/Delete/{name}
        [HttpPost("/Breeds/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name)
        {
            var result = await this.breedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            this.breedsRepository.Delete(result);
            await this.breedsRepository.SaveChangesAsync();
            return this.RedirectToAction(result.KingdomName);
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
