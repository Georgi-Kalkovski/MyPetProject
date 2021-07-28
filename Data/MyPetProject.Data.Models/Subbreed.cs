namespace MyPetProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyPetProject.Data.Common.Models;

    public class Subbreed : BaseDeletableModel<int>
    {
        public Subbreed()
        {
            this.Foods = new HashSet<Food>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PicUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string BreedName { get; set; }

        [Required]
        public string KingdomName { get; set; }

        public int? BreedId { get; set; }

        public virtual Breed Breed { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Food> Foods { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
