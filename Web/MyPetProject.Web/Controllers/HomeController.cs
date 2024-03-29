﻿namespace MyPetProject.Web.Controllers
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

        [HttpGet("/Groups")]
        public IActionResult Groups()
        {
            return this.View();
        }

        [HttpGet("/About")]
        public IActionResult About()
        {
            return this.View();
        }

        [HttpGet("/ErrorPage")]
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
