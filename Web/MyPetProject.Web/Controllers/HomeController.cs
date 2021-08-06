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
    using MyPetProject.Web.ViewModels.Search;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<Subbreed> subbreedsRepository;
        private readonly IDeletableEntityRepository<FoodType> foodtypesRepository;
        private readonly IDeletableEntityRepository<Food> foodsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public HomeController(
            IDeletableEntityRepository<Kingdom> kingdomsRepository,
            IDeletableEntityRepository<Breed> breedsRepository,
            IDeletableEntityRepository<Subbreed> subbreedsRepository,
            IDeletableEntityRepository<FoodType> foodtypesRepository,
            IDeletableEntityRepository<Food> foodsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.kingdomsRepository = kingdomsRepository;
            this.breedsRepository = breedsRepository;
            this.subbreedsRepository = subbreedsRepository;
            this.foodtypesRepository = foodtypesRepository;
            this.foodsRepository = foodsRepository;
            this.usersRepository = usersRepository;
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

        public async Task<IActionResult> SearchAsync()
        {
            var searchResult = new SearchViewModel();

            searchResult.Kingdoms = this.kingdomsRepository.All();
            searchResult.Breeds = this.breedsRepository.All();
            searchResult.Subbreeds = this.subbreedsRepository.All();
            searchResult.FoodTypes = this.foodtypesRepository.All();
            searchResult.Foods = this.foodsRepository.All();

            return this.View(searchResult);

            //var result = this.kingdomsRepository
            //    .All()
            //    .OrderBy(x => x.Name);
            //
            //return this.View(await result.ToListAsync());
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
