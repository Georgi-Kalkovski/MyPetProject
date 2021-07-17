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

    public class BreedsController : Controller
    {
        private readonly ApplicationDbContext context;

        public BreedsController(ApplicationDbContext inputContext)
        {
            this.context = inputContext;
        }

        // GET: Breeds
        [HttpGet("/Breeds/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            if (name == "Create")
            {
                return this.View();
            }

            var applicationDbContext = this.context.Breeds.Include(b => b.User)
                .Where(x => x.KingdomName == name);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Breeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var breed = await this.context.Breeds
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms, "Name", "Name");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Breeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Breeds/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Breed breed)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(breed);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(breed.KingdomName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms, "Name", "Name", breed.KingdomName);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", breed.UserId);
            return this.View(breed);
        }

        // GET: Breeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var breed = await this.context.Breeds.FindAsync(id);
            if (breed == null)
            {
                return this.NotFound();
            }

            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms, "Name", "Name");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", breed.UserId);
            return this.View(breed);
        }

        // POST: Breeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PicUrl,Description,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Breed breed)
        {
            if (id != breed.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(breed);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.BreedExists(breed.Id))
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

            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms, "Name", "Name", breed.KingdomName);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", breed.UserId);
            return this.View(breed);
        }

        // GET: Breeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var breed = await this.context.Breeds
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breed == null)
            {
                return this.NotFound();
            }

            return this.View(breed);
        }

        // POST: Breeds/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var breed = await this.context.Breeds.FindAsync(id);
            this.context.Breeds.Remove(breed);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(breed.KingdomName);
        }

        private bool BreedExists(int id)
        {
            return this.context.Breeds.Any(e => e.Id == id);
        }
    }
}
