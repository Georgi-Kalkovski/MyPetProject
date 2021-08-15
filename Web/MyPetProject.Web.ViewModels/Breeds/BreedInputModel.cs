namespace MyPetProject.Web.ViewModels.Breeds
{
    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    public class BreedInputModel : IMapTo<Breed>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PicUrl { get; set; }

        public string Description { get; set; }

        public string KingdomName { get; set; }

        public string UserId { get; set; }
    }
}
