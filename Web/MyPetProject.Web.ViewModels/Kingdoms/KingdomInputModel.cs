namespace MyPetProject.Web.ViewModels.Kingdoms
{
    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    public class KingdomInputModel : IMapTo<Kingdom>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PicUrl { get; set; }

        public string Description { get; set; }

        public string Group { get; set; }

        public string Diet { get; set; }

        public bool IsPet { get; set; }

        public bool IsFarm { get; set; }

        public string UserId { get; set; }
    }
}
