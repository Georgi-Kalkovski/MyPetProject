namespace MyPetProject.Web.ViewModels.FoodTypes
{
    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    public class FoodTypeInputModel : IMapTo<FoodType>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PicUrl { get; set; }

        public string Description { get; set; }
    }
}
