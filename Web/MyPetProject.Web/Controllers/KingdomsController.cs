namespace MyPetProject.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;
    using MyPetProject.Web.ViewModels.Kingdoms;

    public class KingdomsController : BaseController
    {
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public KingdomsController(
            IDeletableEntityRepository<Kingdom> kingdomsRepository,
            IDeletableEntityRepository<Breed> breedsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.kingdomsRepository = kingdomsRepository;
            this.breedsRepository = breedsRepository;
            this.usersRepository = usersRepository;
        }

        // GET: Kingdoms
        public async Task<IActionResult> Index() => await this.IndexWithoutNameMethod();

        // GET: Kingdoms/{name}
        [HttpGet("/Kingdoms/{name}")]
        public async Task<IActionResult> Index(string name) => await this.IndexWithNameMethod(name);

        // GET: Kingdoms/Details/{name}
        [HttpGet("/Kingdoms/Details/{name}")]
        public async Task<IActionResult> Details(string name) => await this.DetailsMethod(name);

        // GET: Kingdoms/Create
        [HttpGet("/Kingdoms/Create/")]
        public IActionResult Create() => this.CreateGet();

        // POST: Kingdoms/Create
        [HttpPost("/Kingdoms/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KingdomInputModel kingdom) => await this.CreatePost(kingdom);

        // GET: Kingdoms/Edit/{name}
        [HttpGet("/Kingdoms/Edit/{name}")]
        public async Task<IActionResult> Edit(string name) => await this.EditGet(name);

        // POST: Kingdoms/Edit/{name}
        [HttpPost("/Kingdoms/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
        string name, KingdomInputModel kingdom)
            => await this.EditPost(name, kingdom);

        // GET: Kingdoms/Delete/{name}
        [HttpGet("/Kingdoms/Delete/{name}")]
        public async Task<IActionResult> Delete(string name) => await this.DeleteGet(name);

        // POST: Kingdoms/Delete/{name}
        [HttpPost("/Kingdoms/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string name) => await this.DeletePost(id);

        private async Task<IActionResult> IndexWithoutNameMethod()
        {
            var result = this.kingdomsRepository
                            .All()
                            .Include(k => k.User)
                            .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> IndexWithNameMethod(string name)
        {
            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();

            if (oldName == "Herbivores" || oldName == "Carnivores" || oldName == "Omnivores")
            {
                var applicationDbContext = this.kingdomsRepository
                    .All()
                    .Include(k => k.User)
                    .Where(x => x.Diet == name)
                    .OrderBy(x => x.Name);

                return this.View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var result = this.kingdomsRepository
                    .All()
                    .Include(k => k.User)
                    .Where(x => x.Group == name)
                    .OrderBy(x => x.Name);

                return this.View(await result.ToListAsync());
            }
        }

        private async Task<IActionResult> DetailsMethod(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var result = await this.kingdomsRepository
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

            this.ViewData["UserId"] = new SelectList(this.usersRepository.All(), "Id", "Id");
            return this.View();
        }

        private async Task<IActionResult> CreatePost(KingdomInputModel kingdom)
        {
            if (this.ModelState.IsValid)
            {
                if (this.User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
                {
                    kingdom.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                var result = new Kingdom
                {
                    Name = kingdom.Name,
                    PicUrl = kingdom.PicUrl,
                    Description = kingdom.Description,
                    Group = kingdom.Group,
                    Diet = kingdom.Diet,
                    IsPet = kingdom.IsPet,
                    IsFarm = kingdom.IsFarm,
                    UserId = kingdom.UserId,
                };

                await this.kingdomsRepository.AddAsync(result);
                await this.kingdomsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", kingdom.UserId);
            return this.View(kingdom);
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

            var result = await this.kingdomsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            if (this.User.FindFirstValue(ClaimTypes.NameIdentifier) != result.UserId)
            {
                return this.Redirect("/Home/ErrorPage");
            }

            this.ViewData["UserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", this.kingdomsRepository.All().Include(x => x.UserId));
            return this.View(result);
        }

        private async Task<IActionResult> EditPost(string name, KingdomInputModel kingdom)
        {
            if (name != kingdom.Name)
            {
                return this.NotFound();
            }

            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();

            if (this.ModelState.IsValid)
            {
                try
                {
                    var editName = await this.kingdomsRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Name == oldName);

                    foreach (var breed in this.breedsRepository.All().Where(x => x.KingdomName == oldName))
                    {
                        breed.KingdomName = name;
                    }

                    var result = new Kingdom
                    {
                        Id = kingdom.Id,
                        Name = kingdom.Name,
                        PicUrl = kingdom.PicUrl,
                        Description = kingdom.Description,
                        Group = kingdom.Group,
                        Diet = kingdom.Diet,
                        IsPet = kingdom.IsPet,
                        IsFarm = kingdom.IsFarm,
                        UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    };

                    this.kingdomsRepository.HardDelete(editName);
                    await this.kingdomsRepository.AddAsync(result);
                    await this.kingdomsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.KingdomExists(kingdom.Name))
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

            this.ViewData["UserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", kingdom.UserId);
            return this.View(kingdom);
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

            var result = await this.kingdomsRepository
                .All()
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (result == null)
            {
                return this.NotFound();
            }

            if (this.User.FindFirstValue(ClaimTypes.NameIdentifier) != result.UserId)
            {
                return this.Redirect("/Home/ErrorPage");
            }

            return this.View(result);
        }

        private async Task<IActionResult> DeletePost(int? id)
        {
            var result = await this.kingdomsRepository
                            .All()
                            .FirstAsync(x => x.Id == id);

            this.kingdomsRepository.Delete(result);
            await this.kingdomsRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        // Checking if Kingdom Exist
        private bool KingdomExists(string name)
        {
            return this.kingdomsRepository
                .All()
                .Any(e => e.Name == name);
        }
    }
}
