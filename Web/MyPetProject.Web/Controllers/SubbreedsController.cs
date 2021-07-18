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
    using MyPetProject.Data.Models;

    public class SubbreedsController : Controller
    {
        private readonly ApplicationDbContext context;

        public SubbreedsController(ApplicationDbContext inputContext)
        {
            this.context = inputContext;
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
                var applicationDbContext = this.context.Subbreeds.Include(b => b.User)
                .Where(x => x.BreedName.Replace("%20", " ") == name);
                return this.View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = this.context.Subbreeds.Include(b => b.User)
                    .Where(x => x.BreedName == name);
                return this.View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Subbreeds/Details/5
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var subbreed = await this.context.Subbreeds
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
            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms.OrderBy(x => x.Name), "Name", "Name");
            this.ViewData["BreedName"] = new SelectList(this.context.Breeds.OrderBy(x => x.Name), "Name", "Name");
            this.ViewData["BreedId"] = new SelectList(this.context.Breeds, "Id", "Id");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Subbreeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Subbreeds/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,KingdomName,IsPet,BreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subbreed subbreed)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(subbreed);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(subbreed.BreedName);
            }

            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms, "Name", "Name", subbreed.KingdomName);
            this.ViewData["BreedName"] = new SelectList(this.context.Breeds, "Name", "Name", subbreed.BreedName);
            this.ViewData["BreedId"] = new SelectList(this.context.Breeds, "Id", "Id", subbreed.BreedId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", subbreed.UserId);
            return this.View(subbreed);
        }

        // GET: Subbreeds/Edit/5
        [HttpGet("/Subbreeds/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var subbreed = await this.context.Subbreeds.FirstOrDefaultAsync(x => x.Name == name);
            if (subbreed == null)
            {
                return this.NotFound();
            }

            this.ViewData["SubbreedName"] = new SelectList(this.context.Kingdoms, "Name", "Name");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", subbreed.UserId);
            return this.View(subbreed);
        }

        // POST: Breeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Subbreeds/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, [Bind("Name,PicUrl,Description,KingdomName,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subbreed subbreeds)
        {
            if (name != subbreeds.Name)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(subbreeds);
                    await this.context.SaveChangesAsync();
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

            this.ViewData["BreedName"] = new SelectList(this.context.Breeds, "Name", "Name", subbreeds.BreedName);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", subbreeds.UserId);
            return this.View(subbreeds);
        }

        // GET: Subbreeds/Delete/5
        [HttpGet("/Subbreeds/Delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var subbreeds = await this.context.Subbreeds
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Name == name);
            if (subbreeds == null)
            {
                return this.NotFound();
            }

            return this.View(subbreeds);
        }

        // POST: Subbreeds/Delete/5
        [HttpPost("/Subbreeds/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name)
        {
            var subbreed = await this.context.Subbreeds.FirstOrDefaultAsync(x => x.Name == name);
            this.context.Subbreeds.Remove(subbreed);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(subbreed.BreedName);
        }

        private bool SubbreedExists(string name)
        {
            return this.context.Breeds.Any(e => e.Name == name);
        }
    }
}
