namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data;
    using MyPetProject.Data.Models;

    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext db;

        [BindProperty]
        public Food Food { get; set; }

        public FoodsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create(int? id)
        {
            this.Food = new Food();
            if (id == null)
            {
                return this.View(this.Food);
            }

            this.Food = this.db.Foods.FirstOrDefault(d => d.Id == id);
            if (this.Food == null)
            {
                return this.NotFound();
            }

            return this.View(this.Food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            // Dino = new Dino();
            if (this.ModelState.IsValid)
            {
                if (this.Food.Id == 0)
                {
                    this.db.Foods.Add(this.Food);
                }
                else
                {
                    this.db.Foods.Update(this.Food);
                }

                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(this.Food);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return this.Json(new { data = await this.db.Foods.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var foods = await this.db.Foods.FirstOrDefaultAsync(u => u.Id == id);
            if (foods != null)
            {
                this.db.Foods.Remove(foods);
                await this.db.SaveChangesAsync();
                return this.Json(new { success = true, message = "Delete successful" });
            }

            return this.Json(new { success = false, message = "Error while Deleting" });
        }
    }
}
