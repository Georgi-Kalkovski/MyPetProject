namespace MyPetProject.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    public class SearchViewModel : IMapFrom<Kingdom>
    {
        public virtual IEnumerable<Kingdom> Kingdoms { get; set; }

        public virtual IEnumerable<Breed> Breeds { get; set; }

        public virtual IEnumerable<Subbreed> Subbreeds { get; set; }

        public virtual IEnumerable<FoodType> FoodTypes { get; set; }

        public virtual IEnumerable<Food> Foods { get; set; }
    }
}
