namespace MyPetProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyPetProject.Data.Common.Models;

    public class Food : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PicUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string FoodTypeName { get; set; }

        public int? FoodTypeId { get; set; }

        public virtual FoodType FoodType { get; set; }

        public int? SubbreedId { get; set; }

        public virtual Subbreed Subbreed { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
