namespace MyPetProject.Web.ViewModels.Kingdoms
{
    public class CreateKingdomInputModel
    {
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
