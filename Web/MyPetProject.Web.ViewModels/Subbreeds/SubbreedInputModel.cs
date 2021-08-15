namespace MyPetProject.Web.ViewModels.Subbreeds
{
    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    public class SubbreedInputModel : IMapTo<Subbreed>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PicUrl { get; set; }

        public string Description { get; set; }

        public string BreedName { get; set; }

        public string KingdomName { get; set; }
    }
}
