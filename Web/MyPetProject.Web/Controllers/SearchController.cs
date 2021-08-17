namespace MyPetProject.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;
    using MyPetProject.Web.ViewModels.Search;

    public class SearchController : BaseController
    {
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<Subbreed> subbreedsRepository;
        private readonly IDeletableEntityRepository<FoodType> foodtypesRepository;
        private readonly IDeletableEntityRepository<Food> foodsRepository;

        public SearchController(
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

        [HttpGet("/SearchKingdoms")]
        public IActionResult SearchKingdoms()
        {
            var result = this.SearchMethod();

            return this.View(result);
        }

        [HttpGet("/SearchBreeds")]
        public IActionResult SearchBreeds()
        {
            var result = this.SearchMethod();

            return this.View(result);
        }

        [HttpGet("/SearchSubbreeds")]
        public IActionResult SearchSubbreeds()
        {
            var result = this.SearchMethod();

            return this.View(result);
        }

        [HttpGet("/SearchFoodTypes")]
        public IActionResult SearchFoodTypes()
        {
            var result = this.SearchMethod();

            return this.View(result);
        }

        [HttpGet("/SearchFoods")]
        public IActionResult SearchFoods()
        {
            var result = this.SearchMethod();

            return this.View(result);
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
    }
}
