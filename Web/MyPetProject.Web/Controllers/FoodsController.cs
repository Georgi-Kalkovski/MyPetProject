namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data;
    using MyPetProject.Data.Models;

    public class FoodsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public FoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Foods.Include(f => f.FoodType).Include(f => f.Subbreed).Include(f => f.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodType)
                .Include(f => f.Subbreed)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Description");
            ViewData["SubbreedId"] = new SelectList(_context.Subbreeds, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PicUrl,Description,FoodTypeId,SubbreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Description", food.FoodTypeId);
            ViewData["SubbreedId"] = new SelectList(_context.Subbreeds, "Id", "Description", food.SubbreedId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", food.UserId);
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Description", food.FoodTypeId);
            ViewData["SubbreedId"] = new SelectList(_context.Subbreeds, "Id", "Description", food.SubbreedId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", food.UserId);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PicUrl,Description,FoodTypeId,SubbreedId,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Description", food.FoodTypeId);
            ViewData["SubbreedId"] = new SelectList(_context.Subbreeds, "Id", "Description", food.SubbreedId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", food.UserId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodType)
                .Include(f => f.Subbreed)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}
