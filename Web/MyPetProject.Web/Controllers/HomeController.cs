namespace MyPetProject.Web.Controllers
{
    using System;
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
            this.TodayRandomEntityMethod();

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

        [HttpGet("/Search")]
        public IActionResult Search()
        {
            SearchViewModel result = this.SearchMethod();

            return this.View(result);
        }

        [HttpGet("/MyCollection")]
        public IActionResult MyCollection()
        {
            MyCollectionViewModel result = this.MyCollectionMethod();

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

        private SearchViewModel SearchMethod()
        {
            var result = new SearchViewModel();

            result.Kingdoms = this.kingdomsRepository.All();
            result.Breeds = this.breedsRepository.All();
            result.Subbreeds = this.subbreedsRepository.All();
            result.FoodTypes = this.foodtypesRepository.All();
            result.Foods = this.foodsRepository.All();
            return result;
        }

        private MyCollectionViewModel MyCollectionMethod()
        {
            var result = new MyCollectionViewModel();

            result.Kingdoms = this.kingdomsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.Breeds = this.breedsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.Subbreeds = this.subbreedsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.FoodTypes = this.foodtypesRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            result.Foods = this.foodsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            return result;
        }

        private void TodayRandomEntityMethod()
        {
            Random r = new Random(DateTime.Now.Day);

            int totalKingdoms = this.kingdomsRepository.All().Count();
            int totalBreeds = this.breedsRepository.All().Count();
            int totalSubreeds = this.subbreedsRepository.All().Count();
            int totalFoodTypes = this.foodtypesRepository.All().Count();
            int totalFoods = this.foodsRepository.All().Count();

            int offsetKingdoms = r.Next(1, totalKingdoms);
            int offsetBreeds = r.Next(1, totalBreeds);
            int offsetSubreeds = r.Next(1, totalSubreeds);
            int offsetFoodTypes = r.Next(1, totalFoodTypes);
            int offsetFoods = r.Next(1, totalFoods);

            var randomKingdom = this.kingdomsRepository.All().Skip(offsetKingdoms - 1).FirstOrDefault();
            var randomBreed = this.breedsRepository.All().Skip(offsetBreeds - 1).FirstOrDefault();
            var randomSubbreed = this.subbreedsRepository.All().Skip(offsetSubreeds - 1).FirstOrDefault();
            var randomFoodType = this.foodtypesRepository.All().Skip(offsetFoodTypes - 1).FirstOrDefault();
            var randomFood = this.foodsRepository.All().Skip(offsetFoods - 1).FirstOrDefault();

            this.ViewData["RandomKingdom"] = randomKingdom;
            this.ViewData["RandomBreed"] = randomBreed;
            this.ViewData["RandomSubbreed"] = randomSubbreed;
            this.ViewData["RandomFoodType"] = randomFoodType;
            this.ViewData["RandomFood"] = randomFood;

            this.ViewData["bGroup"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomBreed.KingdomName).Group;
            this.ViewData["bDiet"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomBreed.KingdomName).Diet;
            this.ViewData["bIsPet"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomBreed.KingdomName).IsPet;
            this.ViewData["bIsFarm"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomBreed.KingdomName).IsFarm;

            this.ViewData["sGroup"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomSubbreed.KingdomName).Group;
            this.ViewData["sDiet"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomSubbreed.KingdomName).Diet;
            this.ViewData["sIsPet"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomSubbreed.KingdomName).IsPet;
            this.ViewData["sIsFarm"] = this.kingdomsRepository.All().FirstOrDefault(x => x.Name == randomSubbreed.KingdomName).IsFarm;
        }
    }
}
