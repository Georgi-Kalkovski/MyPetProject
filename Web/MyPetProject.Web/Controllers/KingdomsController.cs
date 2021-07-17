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

    public class KingdomsController : Controller
    {
        private readonly ApplicationDbContext context;

        public KingdomsController(ApplicationDbContext inputContext)
        {
            this.context = inputContext;
        }

        // GET: Kingdoms
        public async Task<IActionResult> Index()
        {
            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();
            var applicationDbContext = this.context.Kingdoms.Include(k => k.User).OrderBy(x => x.Name);
            return this.View(await applicationDbContext.ToListAsync());
        }

        [HttpGet("Kingdoms/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();
            var applicationDbContext = this.context.Kingdoms.Include(k => k.User).Where(x=>x.Group == name).OrderBy(x => x.Name);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Kingdoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var kingdom = await this.context.Kingdoms
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kingdom == null)
            {
                return this.NotFound();
            }

            return this.View(kingdom);
        }

        // GET: Kingdoms/Create
        [HttpGet("/Create/")]
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Kingdoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Kingdom kingdom)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(kingdom);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", kingdom.UserId);
            return this.View(kingdom);
        }

        // GET: Kingdoms/Edit/5
        [HttpGet("/Kingdoms/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var kingdom = await this.context.Kingdoms.FirstOrDefaultAsync(x => x.Name == name);
            if (kingdom == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", kingdom.UserId);
            return this.View(kingdom);
        }

        // POST: Kingdoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Kingdoms/Edit/{name}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, [Bind("Name,PicUrl,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Kingdom kingdom)
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
                    var editName = await this.context.Kingdoms.FirstOrDefaultAsync(x => x.Name == oldName);
                    foreach (var breed in this.context.Breeds.Where(x => x.KingdomName == oldName))
                    {
                        breed.KingdomName = name;
                    }

                    this.context.Kingdoms.Remove(editName);

                    // TO DO: Change breeds KingdomName with the new name and save
                    this.context.Update(kingdom);

                    await this.context.SaveChangesAsync();
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

            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", kingdom.UserId);
            return this.View(kingdom);
        }

        // GET: Kingdoms/Delete/5
        [HttpGet("/Kingdoms/Delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var kingdom = await this.context.Kingdoms
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.Name == name);
            if (kingdom == null)
            {
                return this.NotFound();
            }

            return this.View(kingdom);
        }

        // POST: Kingdoms/Delete/5
        [HttpPost("/Kingdoms/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, string name)
        {
            var kingdom = await this.context.Kingdoms.FindAsync(id);
            this.context.Kingdoms.Remove(kingdom);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool KingdomExists(string name)
        {
            return this.context.Kingdoms.Any(e => e.Name == name);
        }
    }
}
