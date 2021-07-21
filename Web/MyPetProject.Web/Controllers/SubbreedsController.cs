namespace MyPetProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;

    public class SubbreedsController : Controller
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

        // GET: Subbreeds/{name}
        [HttpGet("/Subbreeds/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            if (name == "Create")
            {
                return this.View();
            }

            if (name.Contains(" "))
            {
                var applicationDbContext = this.subbreedsRepository
                    .All()
                    .Include(b => b.User)
                    .Where(x => x.BreedName.Replace("%20", " ") == name)
                    .OrderBy(x => x.Name);

                return this.View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = this.subbreedsRepository
                    .All()
                    .Include(b => b.User)
                    .Where(x => x.BreedName == name);

                return this.View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Subbreeds/Details/{name}
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var subbreed = await this.subbreedsRepository
                .All()
                .Include(s => s.Breed)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (subbreed == null)
            {
                return this.NotFound();
            }

            return this.View(subbreed);
        }

        // GET: Subbreeds/Create
        [HttpGet("/Subbreeds/Create/")]
        public IActionResult Create()
        {
            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All().OrderBy(x => x.Name), "Name", "Name");
            this.ViewData["BreedName"] = new SelectList(this.breedsRepository.All().OrderBy(x => x.Name), "Name", "Name");

            // this.ViewData["BreedId"] = new SelectList(this.context.Breeds, "Id", "Id");
            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Subbreeds/Create
        [HttpPost("/Subbreeds/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,BreedName,KingdomName,IsPet,IsFarm,BreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subbreed subbreed)
        {
            if (this.ModelState.IsValid)
            {
                await this.subbreedsRepository.AddAsync(subbreed);
                await this.subbreedsRepository.SaveChangesAsync();
                return this.RedirectToAction(subbreed.BreedName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", subbreed.KingdomName);
            this.ViewData["BreedName"] = new SelectList(this.breedsRepository.All(), "Name", "Name", subbreed.BreedName);

            // this.ViewData["BreedId"] = new SelectList(this.context.Breeds, "Id", "Id", subbreed.BreedId);
            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", subbreed.UserId);
            return this.View(subbreed);
        }

        // GET: Subbreeds/Edit/{name}
        [HttpGet("/Subbreeds/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var subbreed = await this.subbreedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (subbreed == null)
            {
                return this.NotFound();
            }

            this.ViewData["SubbreedName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name");

            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", subbreed.UserId);
            return this.View(subbreed);
        }

        // POST: Breeds/Edit/{name}
        [HttpPost("/Subbreeds/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, [Bind("Name,PicUrl,Description,BreedName,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subbreed subbreeds)
        {
            if (name != subbreeds.Name)
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

                    this.subbreedsRepository.Delete(editName);
                    await this.subbreedsRepository.AddAsync(subbreeds);
                    await this.subbreedsRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.SubbreedExists(subbreeds.Name))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(subbreeds.BreedName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.kingdomsRepository.All(), "Name", "Name", subbreeds.KingdomName);
            this.ViewData["BreedName"] = new SelectList(this.breedsRepository.All(), "Name", "Name", subbreeds.BreedName);

            // this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", subbreeds.UserId);
            return this.View(subbreeds);
        }

        // GET: Subbreeds/Delete/{name}
        [HttpGet("/Subbreeds/Delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var subbreeds = await this.subbreedsRepository
                .All()
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Name == name);

            if (subbreeds == null)
            {
                return this.NotFound();
            }

            return this.View(subbreeds);
        }

        // POST: Subbreeds/Delete/{name}
        [HttpPost("/Subbreeds/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name)
        {
            var subbreed = await this.subbreedsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Name == name);

            this.subbreedsRepository.Delete(subbreed);
            await this.subbreedsRepository.SaveChangesAsync();
            return this.RedirectToAction(subbreed.BreedName);
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
