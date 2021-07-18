namespace MyPetProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyPetProject.Data.Common.Models;
    using MyPetProject.Data.Models.Enums;

    public class Kingdom : BaseDeletableModel<int>
    {
        public Kingdom()
        {
            this.Breeds = new HashSet<Breed>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PicUrl { get; set; }

        [Required]
        public string Group { get; set; }

        [Required]
        public string Diet { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<Breed> Breeds { get; set; }
    }
}
