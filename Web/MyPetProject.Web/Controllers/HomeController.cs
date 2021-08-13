namespace MyPetProject.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;
    using MyPetProject.Web.ViewModels;
    using MyPetProject.Web.ViewModels.MyCollection;
    using MyPetProject.Web.ViewModels.Search;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<Subbreed> subbreedsRepository;
        private readonly IDeletableEntityRepository<FoodType> foodtypesRepository;
        private readonly IDeletableEntityRepository<Food> foodsRepository;

        public HomeController(
            IDeletableEntityRepository<Kingdom> kingdomsRepository,
            IDeletableEntityRepository<Breed> breedsRepository,
            IDeletableEntityRepository<Subbreed> subbreedsRepository,
            IDeletableEntityRepository<FoodType> foodtypesRepository,
            IDeletableEntityRepository<Food> foodsRepository)
        {
            this.kingdomsRepository = kingdomsRepository;
            this.breedsRepository = breedsRepository;
            this.subbreedsRepository = subbreedsRepository;
            this.foodtypesRepository = foodtypesRepository;
            this.foodsRepository = foodsRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Groups()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        // GET: /HomeAnimals
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

        // GET: /FarmAnimals
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

        // GET: /WildAnimals
        [HttpGet("/WildAnimals")]
        public async Task<IActionResult> WildAnimals()
        {
            var result = this.kingdomsRepository
                .All()
                .Include(k => k.User)
                .Where(a => a.IsPet == false && a.IsFarm == false)
                .OrderBy(x => x.Name);

            return this.View(await result.ToListAsync());
        }

        [HttpGet("/Search")]
        public async Task<IActionResult> SearchAsync()
        {
            var result = new SearchViewModel();

            result.Kingdoms = this.kingdomsRepository.All();
            result.Breeds = this.breedsRepository.All();
            result.Subbreeds = this.subbreedsRepository.All();
            result.FoodTypes = this.foodtypesRepository.All();
            result.Foods = this.foodsRepository.All();

            return this.View(result);
        }

        [HttpGet("/MyCollection")]
        public async Task<IActionResult> MyCollection()
        {
            var result = new MyCollectionViewModel();

            result.Kingdoms = this.kingdomsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.Breeds = this.breedsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.Subbreeds = this.subbreedsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.FoodTypes = this.foodtypesRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.Foods = this.foodsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);

            return this.View(result);
        }

        public IActionResult ErrorPage()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
