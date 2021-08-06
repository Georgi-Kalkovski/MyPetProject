namespace MyPetProject.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using MyPetProject.Data.Models;

    public class SearchViewModel
    {
        public IEnumerable<Kingdom> Kingdoms { get; set; }

        public IEnumerable<Breed> Breeds { get; set; }

        public IEnumerable<Subbreed> Subbreeds { get; set; }

        public IEnumerable<FoodType> FoodTypes { get; set; }

        public IEnumerable<Food> Foods { get; set; }
    }
}
