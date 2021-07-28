namespace MyPetProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyPetProject.Data.Models;

    public class FoodTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FoodTypes.Any())
            {
                return;
            }

            await dbContext.FoodTypes.AddAsync(new FoodType
            {
                Name = "Meat",
                PicUrl = "https://www.netclipart.com/pp/m/105-1058315_steak-svg-png-icon-free-download-steak-icon.png",
                Description = "Meat is animal flesh that is eaten as food. Humans have hunted and killed animals for meat since prehistoric times. The advent of civilization allowed the domestication of animals such as chickens, sheep, rabbits, pigs and cattle.",
            });
            await dbContext.FoodTypes.AddAsync(new FoodType
            {
                Name = "Grains",
                PicUrl = "https://img.freepik.com/free-vector/wheat-ears-icons_176411-849.jpg?size=626&ext=jpg&ga=GA1.2.501542633.1626566400",
                Description = "A grain is a small, hard, dry seed - with or without an attached hull or fruit layer - harvested for human or animal consumption. A grain crop is a grain-producing plant. The two main types of commercial grain crops are cereals and legumes.",
            });
            await dbContext.FoodTypes.AddAsync(new FoodType
            {
                Name = "Plants",
                PicUrl = "https://media.istockphoto.com/vectors/plants-silhouette-vector-images-vector-id1093572324",
                Description = "Plants are mainly multicellular organisms, predominantly photosynthetic eukaryotes of the kingdom Plantae. Historically, plants were treated as one of two kingdoms including all living things that were not animals, and all algae and fungi were treated as plants. However, all current definitions of Plantae exclude the fungi and some algae, as well as the prokaryotes (the archaea and bacteria). By one definition, plants form the clade Viridiplantae (Latin name for \"green plants\"), a group that includes the flowering plants, conifers and other gymnosperms, ferns and their allies, hornworts, liverworts, mosses, and the green algae, but excludes the red and brown algae.",
            });
        }
    }
}
