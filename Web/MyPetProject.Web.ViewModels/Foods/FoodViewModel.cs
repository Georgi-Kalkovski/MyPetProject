namespace MyPetProject.Web.ViewModels.Foods
{
    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    public class FoodViewModel : IMapFrom<Food>
    {
        public string Name { get; set; }

        public string PicUrl { get; set; }

        public string Description { get; set; }
    }
}
