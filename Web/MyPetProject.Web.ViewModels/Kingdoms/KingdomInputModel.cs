namespace MyPetProject.Web.ViewModels.Kingdoms
{
    using System.ComponentModel.DataAnnotations;

    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    using static MyPetProject.Common.GlobalConstants;

    public class KingdomInputModel : IMapTo<Kingdom>
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
        public string Group { get; set; }

        [Required]
        public string Diet { get; set; }

        public bool IsPet { get; set; }

        public bool IsFarm { get; set; }

        public string UserId { get; set; }
    }
}
