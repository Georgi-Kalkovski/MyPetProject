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
    using MyPetProject.Web.ViewModels.Subbreeds;

    public class SubbreedsController : BaseController
    {
        private readonly IDeletableEntityRepository<Subbreed> subbreedsRepository;
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;

        public SubbreedsController(
            IDeletableEntityRepository<Subbreed> subbreedsRepository,
            IDeletableEntityRepository<Breed> breedsRepository,
            IDeletableEntityRepository<Kingdom> kingdomsRepository)
        {
            this.subbreedsRepository = subbreedsRepository;
            this.breedsRepository = breedsRepository;
            this.kingdomsRepository = kingdomsRepository;
        }

        // GET: Subbreeds
        public async Task<IActionResult> Index() => await this.IndexWithoutNameMethod();

        // GET: Subbreeds/{name}
        [HttpGet("/Subbreeds/{name}")]
        public async Task<IActionResult> Index(string name) => await this.IndexWithNameMethod(name);

        // GET: Subbreeds/Details/{name}
        [HttpGet("/Subbreeds/Details/{name}")]
        public async Task<IActionResult> Details(string name) => await this.DetailsMethod(name);

        // GET: Subbreeds/Create
        [HttpGet("/Subbreeds/Create/")]
        public IActionResult Create() => this.CreateGet();

        // POST: Subbreeds/Create
        [HttpPost("/Subbreeds/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubbreedInputModel subbreed)
            => await this.CreatePost(subbreed);

        // GET: Subbreeds/Edit/{name}
        [HttpGet("/Subbreeds/Edit/{name}")]
        public async Task<IActionResult> Edit(string name) => await this.EditGet(name);

        // POST: Breeds/Edit/{name}
        [HttpPost("/Subbreeds/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, SubbreedInputModel subbreed)
            => await this.EditPost(name, subbreed);

        // GET: Subbreeds/Delete/{name}
        [HttpGet("/Subbreeds/Delete/{name}")]
        public async Task<IActionResult> Delete(string name) => await this.DeleteGet(name);

        // POST: Subbreeds/Delete/{name}
        [HttpPost("/Subbreeds/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name) => await this.DeletePost(name);

        private async Task<IActionResult> IndexWithoutNameMethod()
        {
            var result = this.subbreedsRepository
                .All()
                .Include(k => k.User)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> IndexWithNameMethod(string name)
        {
            if (name.Contains(" "))
            {
                var result = this.subbreedsRepository
                    .All()
                    .Include(b => b.User)
                    .Where(x => x.BreedName.Replace("%20", " ") == name)
                    .OrderBy(x => x.Name);

                return this.View(await result.ToListAsync());
            }
            else
            {
                var result = this.subbreedsRepository
                    .All()
                    .Include(b => b.User)
                    .Where(x => x.BreedName == name);

                return this.View(await result.ToListAsync());
            }
        }

        private async Task<IActionResult> DetailsMethod(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.subbreedsRepository
                .All()
                .Include(s => s.User)
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

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All().OrderBy(x => x.Name), "Name", "Name");
            this.ViewData["BreedName"] = new SelectList(this.breedsRepository.All().OrderBy(x => x.Name), "Name", "Name");

            // this.ViewData["BreedId"] = new SelectList(this.context.Breeds, "Id", "Id");
            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        private async Task<IActionResult> CreatePost(SubbreedInputModel subbreed)
        {
            if (this.ModelState.IsValid)
            {
                var result = new Subbreed
                {
                    Name = subbreed.Name,
                    PicUrl = subbreed.PicUrl,
                    Description = subbreed.Description,
                    UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    KingdomName = subbreed.KingdomName,
                    BreedName = subbreed.BreedName,
                };

                await this.subbreedsRepository.AddAsync(result);
                await this.subbreedsRepository.SaveChangesAsync();
                return this.RedirectToAction(subbreed.BreedName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", subbreed.KingdomName);
            this.ViewData["BreedName"] = new SelectList(this.breedsRepository.All(), "Name", "Name", subbreed.BreedName);

            return this.View(subbreed);
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

            var repo = await this.subbreedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            var result = new SubbreedInputModel()
            {
                Name = repo.Name,
                PicUrl = repo.PicUrl,
                Description = repo.Description,
                KingdomName = repo.KingdomName,
                BreedName = repo.BreedName,
                UserId = repo.UserId,
            };

            if (result == null)
            {
                return this.NotFound();
            }

            if (this.User.FindFirstValue(ClaimTypes.NameIdentifier) == result.UserId || this.User.IsInRole("Administrator"))
            {
                this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All().OrderBy(x => x.Name), "Name", "Name");
                this.ViewData["BreedName"] = new SelectList(this.breedsRepository.All().OrderBy(x => x.Name), "Name", "Name");
                return this.View(result);
            }
            else
            {
                return this.Redirect("/ErrorPage");
            }
        }

        private async Task<IActionResult> EditPost(string name, SubbreedInputModel subbreed)
        {
            if (name != subbreed.Name)
            {
                return this.NotFound();
            }

            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();

            if (this.ModelState.IsValid)
            {
                try
                {
                    var editName = await this.subbreedsRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Name == oldName);

                    foreach (var animal in this.subbreedsRepository.All().Where(x => x.Name == oldName))
                    {
                        animal.Name = name;
                    }

                    var result = new Subbreed
                    {
                        Name = subbreed.Name,
                        PicUrl = subbreed.PicUrl,
                        Description = subbreed.Description,
                        UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                        KingdomName = subbreed.KingdomName,
                        BreedName = subbreed.BreedName,
                    };

                    this.subbreedsRepository.HardDelete(editName);
                    await this.subbreedsRepository.AddAsync(result);
                    await this.subbreedsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.SubbreedExists(subbreed.Name))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(subbreed.BreedName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", subbreed.KingdomName);
            this.ViewData["BreedName"] = new SelectList(this.breedsRepository.All(), "Name", "Name", subbreed.BreedName);

            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", subbreeds.UserId);
            return this.View(subbreed);
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

            var result = await this.subbreedsRepository
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
            var result = await this.subbreedsRepository
                            .All()
                            .FirstOrDefaultAsync(x => x.Name == name);

            this.subbreedsRepository.Delete(result);
            await this.subbreedsRepository.SaveChangesAsync();
            return this.RedirectToAction(result.BreedName);
        }

        // Checking if Subbreed Exist
        private bool SubbreedExists(string name)
        {
            return this.subbreedsRepository
                .All()
                .Any(e => e.Name == name);
        }
    }
}
