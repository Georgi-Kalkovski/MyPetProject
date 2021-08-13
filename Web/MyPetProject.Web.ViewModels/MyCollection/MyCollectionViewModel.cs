using MyPetProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPetProject.Web.ViewModels.MyCollection
{
    public class MyCollectionViewModel
    {
        public IEnumerable<Kingdom> Kingdoms { get; set; }

        public IEnumerable<Breed> Breeds { get; set; }

        public IEnumerable<Subbreed> Subbreeds { get; set; }

        public IEnumerable<FoodType> FoodTypes { get; set; }

        public IEnumerable<Food> Foods { get; set; }
    }
}
