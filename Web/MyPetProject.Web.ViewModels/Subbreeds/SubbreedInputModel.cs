namespace MyPetProject.Web.ViewModels.Subbreeds
{
    using System.ComponentModel.DataAnnotations;

    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    using static MyPetProject.Common.GlobalConstants;

    public class SubbreedInputModel : IMapTo<Subbreed>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = "Picture Url")]
        public string PicUrl { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Breed Name")]
        public string BreedName { get; set; }

        [Required]
        [Display(Name = "Kingdom Name")]
        public string KingdomName { get; set; }

        public string UserId { get; set; }
    }
}
