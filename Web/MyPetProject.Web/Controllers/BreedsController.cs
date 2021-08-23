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
    using MyPetProject.Web.ViewModels.Breeds;

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
        public async Task<IActionResult> Index() => await this.IndexWithoutNameMethod();

        // GET: Breeds/{name}
        [HttpGet("/Breeds/{name}")]
        public async Task<IActionResult> Index(string name) => await this.IndexWithNameMethod(name);

        // GET: Breeds/Details/{name}
        [HttpGet("/Breeds/Details/{name}")]
        public async Task<IActionResult> Details(string name) => await this.DetailsMethod(name);

        // GET: Breeds/Create
        [HttpGet("/Breeds/Create/")]
        public IActionResult Create() => this.CreateGet();

        // POST: Breeds/Create
        [HttpPost("/Breeds/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BreedInputModel breed) => await this.CreatePost(breed);

        // GET: Breeds/Edit/{name}
        [HttpGet("/Breeds/Edit/{name}")]
        public async Task<IActionResult> Edit(string name) => await this.EditGet(name);

        // POST: Breeds/Edit/{name}
        [HttpPost("/Breeds/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, BreedInputModel breed)
            => await this.EditPost(name, breed);

        // GET: Breeds/Delete/{name}
        [HttpGet("/Breeds/Delete/{name}")]
        public async Task<IActionResult> Delete(string name) => await this.DeleteGet(name);

        // POST: Breeds/Delete/{name}
        [HttpPost("/Breeds/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name) => await this.DeletePost(name);

        private async Task<IActionResult> IndexWithoutNameMethod()
        {
            var result = this.breedsRepository
                            .All()
                            .Include(b => b.User)
                            .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> IndexWithNameMethod(string name)
        {
            var result = this.breedsRepository
                            .All()
                            .Include(b => b.User)
                            .Where(x => x.KingdomName == name)
                            .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> DetailsMethod(string name)
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

            var list = this.kingdomsRepository.All().ToList();

            this.ViewData["Group"] = list.FirstOrDefault(x => x.Name == result.KingdomName).Group;
            this.ViewData["Diet"] = list.FirstOrDefault(x => x.Name == result.KingdomName).Diet;
            this.ViewData["IsPet"] = list.FirstOrDefault(x => x.Name == result.KingdomName).IsPet;
            this.ViewData["IsFarm"] = list.FirstOrDefault(x => x.Name == result.KingdomName).IsFarm;

            return this.View(result);
        }

        private IActionResult CreateGet()
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/ErrorPage");
            }

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id");
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All().OrderBy(x => x.Name), "Name", "Name");
            return this.View();
        }

        private async Task<IActionResult> CreatePost(BreedInputModel breed)
        {
            if (this.ModelState.IsValid)
            {
                var result = new Breed
                {
                    Name = breed.Name,
                    PicUrl = breed.PicUrl,
                    Description = breed.Description,
                    UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    KingdomName = breed.KingdomName,
                };
                await this.breedsRepository.AddAsync(result);
                await this.breedsRepository.SaveChangesAsync();
                return this.RedirectToAction(breed.KingdomName);
            }

            this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", breed.UserId);
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", breed.KingdomName);

            return this.View(breed);
        }

        private async Task<IActionResult> EditGet(string name)
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/ErrorPage");
            }

            if (name == null)
            {
                return this.NotFound();
            }

            var repo = await this.breedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            var result = new BreedInputModel()
            {
                Name = repo.Name,
                PicUrl = repo.PicUrl,
                Description = repo.Description,
                KingdomName = repo.KingdomName,
                UserId = repo.UserId,
            };

            if (result == null)
            {
                return this.NotFound();
            }

            if (this.User.FindFirstValue(ClaimTypes.NameIdentifier) == result.UserId || this.User.IsInRole("Administrator"))
            {
                this.ViewData["UserId"] = new SelectList(this.applicationsRepository.All(), "Id", "Id", this.breedsRepository.All().Include(x => x.UserId));
                this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name");

                return this.View(result);
            }
            else
            {
                return this.Redirect("/ErrorPage");
            }
        }

        private async Task<IActionResult> EditPost(string name, BreedInputModel breed)
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

                    var result = new Breed
                    {
                        Id = breed.Id,
                        Name = breed.Name,
                        PicUrl = breed.PicUrl,
                        Description = breed.Description,
                        UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                        KingdomName = breed.KingdomName,
                    };

                    this.breedsRepository.HardDelete(editName);
                    await this.breedsRepository.AddAsync(result);
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

        private async Task<IActionResult> DeleteGet(string name)
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/ErrorPage");
            }

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

            if (this.User.FindFirstValue(ClaimTypes.NameIdentifier) == result.UserId || this.User.IsInRole("Administrator"))
            {
                var list = this.kingdomsRepository.All().ToList();

                this.ViewData["Group"] = list.FirstOrDefault(x => x.Name == result.KingdomName).Group;
                this.ViewData["Diet"] = list.FirstOrDefault(x => x.Name == result.KingdomName).Diet;
                this.ViewData["IsPet"] = list.FirstOrDefault(x => x.Name == result.KingdomName).IsPet;
                this.ViewData["IsFarm"] = list.FirstOrDefault(x => x.Name == result.KingdomName).IsFarm;

                return this.View(result);
            }
            else
            {
                return this.Redirect("/ErrorPage");
            }
        }

        private async Task<IActionResult> DeletePost(string name)
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
