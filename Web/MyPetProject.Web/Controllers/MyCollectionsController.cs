namespace MyPetProject.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MyPetProject.Data.Common.Repositories;
    using MyPetProject.Data.Models;
    using MyPetProject.Web.ViewModels.MyCollection;

    public class MyCollectionsController : BaseController
    {
        private readonly IDeletableEntityRepository<Kingdom> kingdomsRepository;
        private readonly IDeletableEntityRepository<Breed> breedsRepository;
        private readonly IDeletableEntityRepository<Subbreed> subbreedsRepository;
        private readonly IDeletableEntityRepository<FoodType> foodtypesRepository;
        private readonly IDeletableEntityRepository<Food> foodsRepository;

        public MyCollectionsController(
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

        [HttpGet("/MyKingdoms")]
        public IActionResult MyKingdoms()
        {
            MyCollectionViewModel result = this.MyKingdomsMethod();

            return this.View(result);
        }

        [HttpGet("/MyBreeds")]
        public IActionResult MyBreeds()
        {
            MyCollectionViewModel result = this.MyBreedsMethod();

            return this.View(result);
        }

        [HttpGet("/MySubbreeds")]
        public IActionResult MySubbreeds()
        {
            MyCollectionViewModel result = this.MySubbreedsMethod();

            return this.View(result);
        }

        [HttpGet("/MyFoodTypes")]
        public IActionResult MyFoodTypes()
        {
            MyCollectionViewModel result = this.MyFoodTypesMethod();

            return this.View(result);
        }

        [HttpGet("/MyFoods")]
        public IActionResult MyFoods()
        {
            MyCollectionViewModel result = this.MyFoodsMethod();

            return this.View(result);
        }

        private MyCollectionViewModel MyKingdomsMethod()
        {
            var result = new MyCollectionViewModel();

            result.Kingdoms = this.kingdomsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            return result;
        }

        private MyCollectionViewModel MyBreedsMethod()
        {
            var result = new MyCollectionViewModel();

            result.Breeds = this.breedsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            return result;
        }

        private MyCollectionViewModel MySubbreedsMethod()
        {
            var result = new MyCollectionViewModel();

            result.Subbreeds = this.subbreedsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            return result;
        }

        private MyCollectionViewModel MyFoodTypesMethod()
        {
            var result = new MyCollectionViewModel();

            result.FoodTypes = this.foodtypesRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            return result;
        }

        private MyCollectionViewModel MyFoodsMethod()
        {
            var result = new MyCollectionViewModel();

            result.Foods = this.foodsRepository.All().Where(x => x.UserId == this.User.Claims.ToList()[0].Value);
            return result;
        }
    }
}
