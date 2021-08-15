namespace MyPetProject.Web.ViewModels.Foods
{
    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    public class FoodInputModel : IMapTo<Food>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PicUrl { get; set; }

        public string Description { get; set; }

        public string FoodTypeName { get; set; }
    }
}
