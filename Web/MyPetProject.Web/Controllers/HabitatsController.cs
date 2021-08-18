namespace MyPetProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;

    public class HabitatsController : BaseController
    {
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;

        public HabitatsController(
            IDeletableEntityRepository<Kingdom> kingdomsRepository)
        {
            this.kingdomsRepository = kingdomsRepository;
        }

        // GET: /HomeAnimals
        [HttpGet("/HomeAnimals")]
        public async Task<IActionResult> HomeAnimals()
        {
            return await this.HomeAnimalsMethod();
        }

        // GET: /FarmAnimals
        [HttpGet("/FarmAnimals")]
        public async Task<IActionResult> FarmAnimals()
        {
            return await this.FarmAnimalsMethod();
        }

        // GET: /WildAnimals
        [HttpGet("/WildAnimals")]
        public async Task<IActionResult> WildAnimals()
        {
            return await this.WildAnimalMethod();
        }

        private async Task<IActionResult> HomeAnimalsMethod()
        {
            var result = this.kingdomsRepository
                            .All()
                            .Include(k => k.User)
                            .Where(a => a.IsPet == true)
                            .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> FarmAnimalsMethod()
        {
            var result = this.kingdomsRepository
                            .All()
                            .Include(k => k.User)
                            .Where(a => a.IsFarm == true)
                            .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        private async Task<IActionResult> WildAnimalMethod()
        {
            var result = this.kingdomsRepository
                            .All()
                            .Include(k => k.User)
                            .Where(a => a.IsPet == false && a.IsFarm == false)
                            .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }
    }
}
