﻿namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;

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
        public async Task<IActionResult> Index()
        {
            var result = this.kingdomsRepository
                .All()
                .Include(k => k.User)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        [HttpGet("/HomeAnimals")]
        public async Task<IActionResult> HomeAnimals()
        {
            var result = this.kingdomsRepository
                .All()
                .Include(k => k.User)
                .Where(a => a.IsPet == true)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        [HttpGet("/FarmAnimals")]
        public async Task<IActionResult> FarmAnimals()
        {
            var result = this.kingdomsRepository
                .All()
                .Include(k => k.User)
                .Where(a => a.IsFarm == true)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        // GET: Kingdoms/{name}
        [HttpGet("/Kingdoms/{name}")]
        public async Task<IActionResult> Index(string name)
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

        // GET: Kingdoms/Details/{name}
        public async Task<IActionResult> Details(string name)
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

        // GET: Kingdoms/Create
        [HttpGet("/Kingdoms/Create/")]
        public IActionResult Create()
        {
            if (!this.User.Claims.Any())
            {
                return this.Redirect("/Home/ErrorPage");
            }

            this.ViewData["UserId"] = new SelectList(this.usersRepository.All(), "Id", "Id");
            return this.View();
        }

        // POST: Kingdoms/Create
        [HttpPost("/Kingdoms/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,PicUrl,Description,Group,Diet,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Kingdom kingdom)
        {
            if (this.ModelState.IsValid)
            {
                if (this.User.Claims.ToList()[0].Value != null)
                {
                    kingdom.UserId = this.User.Claims.ToList()[0].Value;
                }

                await this.kingdomsRepository.AddAsync(kingdom);
                await this.kingdomsRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", kingdom.UserId);
            return this.View(kingdom);
        }

        // GET: Kingdoms/Edit/{name}
        [HttpGet("/Kingdoms/Edit/{name}")]
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

            var result = await this.kingdomsRepository
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

            this.ViewData["UserId"] = new SelectList(this.usersRepository.All(), "Id", "Id", this.kingdomsRepository.All().Include(x => x.UserId));
            return this.View(result);
        }

        // POST: Kingdoms/Edit/{name}
        [HttpPost("/Kingdoms/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string name, [Bind("Name,PicUrl,Description,Group,Diet,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Kingdom kingdom)
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

                    this.kingdomsRepository.Delete(editName);
                    await this.kingdomsRepository.AddAsync(kingdom);
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

        // GET: Kingdoms/Delete/{name}
        [HttpGet("/Kingdoms/Delete/{name}")]
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

            var result = await this.kingdomsRepository
                .All()
                .Include(k => k.User)
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

        // POST: Kingdoms/Delete/{name}
        [HttpPost("/Kingdoms/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string name)
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
