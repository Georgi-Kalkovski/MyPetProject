namespace MyPetProject.Web.ViewModels.Foods
{
    using System.ComponentModel.DataAnnotations;

    using MyPetProject.Data.Models;
    using MyPetProject.Services.Mapping;

    using static MyPetProject.Common.GlobalConstants;

    public class FoodInputModel : IMapTo<Food>
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
        [Display(Name = "Food Type Name")]
        public string FoodTypeName { get; set; }

        public string UserId { get; set; }
    }
}
