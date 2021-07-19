namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data;
    using MyPetProject.Data.Models;

    public class BreedsController : Controller
    {
        private readonly ApplicationDbContext context;

        public BreedsController(ApplicationDbContext inputContext)
        {
            this.context = inputContext;
        }

        // GET: Breeds
        public async Task<IActionResult> Index()
        {
            var oldName = this.HttpContext.Request.Path.Value.Split("/").Last();
            var applicationDbContext = this.context.Breeds.Include(b => b.User).OrderBy(x => x.Name);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Breeds/{name}
        [HttpGet("/Breeds/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            if (name == "Create")
            {
                return this.View();
            }

            var applicationDbContext = this.context.Breeds.Include(b => b.User)
                .Where(x => x.KingdomName == name).OrderBy(x => x.Name);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Breeds/Details/5
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var breed = await this.context.Breeds
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
            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms.OrderBy(x => x.Name), "Name", "Name");
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
        [HttpGet("/Breeds/Edit/{name}")]
        public async Task<IActionResult> Edit(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var breed = await this.context.Breeds.FirstOrDefaultAsync(x => x.Name == name);
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
                    var editName = await this.context.Breeds.FirstOrDefaultAsync(x => x.Name == oldName);
                    foreach (var animals in this.context.Breeds.Where(x => x.Name == oldName))
                    {
                        animals.Name = name;
                    }

                    this.context.Breeds.Remove(editName);

                    this.context.Update(breed);
                    await this.context.SaveChangesAsync();
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

            this.ViewData["KingdomName"] = new SelectList(this.context.Kingdoms, "Name", "Name", breed.KingdomName);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", breed.UserId);
            return this.View(breed);
        }

        // GET: Breeds/Delete/5
        [HttpGet("/Breeds/Delete/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            if (name == null)
            {
                return this.NotFound();
            }

            var breed = await this.context.Breeds
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Name == name);
            if (breed == null)
            {
                return this.NotFound();
            }

            return this.View(breed);
        }

        // POST: Breeds/Delete/5
        [HttpPost("/Breeds/Delete/{name}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string name)
        {
            var breed = await this.context.Breeds.FirstOrDefaultAsync(x => x.Name == name);
            this.context.Breeds.Remove(breed);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(breed.KingdomName);
        }

        private bool BreedExists(string name)
        {
            return this.context.Breeds.Any(e => e.Name == name);
        }
    }
}
