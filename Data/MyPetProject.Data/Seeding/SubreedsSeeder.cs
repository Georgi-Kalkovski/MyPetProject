namespace MyPetProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyPetProject.Data.Models;

    public class SubreedsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Subbreeds.Any())
            {
                return;
            }

            await dbContext.Subbreeds.AddAsync(new Subbreed
            {
                Name = "Saddle Coat German Shepherd",
                PicUrl = "https://dogexpress.in/wp-content/uploads/2020/08/Saddle-German-Shepherd.jpg",
                Description = "German Shepherd Dog is a breed of large-sized dog that originated in Germany. The German Shepherd is a relatively new breed of dog, with its origin dating to 1899. As part of the Herding group, the German Shepherd is a working dog developed originally for herding and guarding sheep. ",
                KingdomName = "Dogs",
                BreedName = "German Shepherd",
            });
            await dbContext.Subbreeds.AddAsync(new Subbreed
            {
                Name = "Panda German Shepherd",
                PicUrl = "https://deutscher-schaeferhund.org/wp-content/uploads/2020/10/panda-german-shepherd.png",
                Description = "The Panda Shepherd Dog is a piebald German Shepherd that has occurred in a single GSD bloodline. It is 35% white, while the remainder of coloring is black and tan. It is a spontaneous mutation and has no White German Shepherds in its ancestry.",
                KingdomName = "Dogs",
                BreedName = "German Shepherd",
            });
            await dbContext.Subbreeds.AddAsync(new Subbreed
            {
                Name = "Black German Shepherd",
                PicUrl = "https://animalso.com/wp-content/uploads/2016/12/black-german-shepherd_2.jpg",
                Description = "The Black German Shepherd or Black Shepherd is not a separate breed. They are purebred German Shepherds with a solid black color. Even the American Kennel Club (AKC) recognizes and includes them in the German Shepherd breed standard. This breed's history started in Germany, hence the name.",
                KingdomName = "Dogs",
                BreedName = "German Shepherd",
            });
            await dbContext.Subbreeds.AddAsync(new Subbreed
            {
                Name = "Sable German Shepherd",
                PicUrl = "https://animalcorner.org/wp-content/uploads/2020/06/Sable-German-Shepherd-3.jpg",
                Description = "The sable color of the German Shepherd means that almost all their hairs will have a black tip to them, while the rest of the hair can be a different color. Normally this other color is tan, but there are a range of colors that the GSD can come in. These include white, parti, blue, liver, red and gold.",
                KingdomName = "Dogs",
                BreedName = "German Shepherd",
            });
            await dbContext.Subbreeds.AddAsync(new Subbreed
            {
                Name = "White German Shepherd",
                PicUrl = "https://www.allthingsdogs.com/wp-content/uploads/2019/07/White-German-Shepherd-Feature.jpg",
                Description = "The White Shepherd is an intelligent and hard-working dog breed. Genetically no different from the standard tan German Shepherd, The white German Shepherd has just one exception, their snowy colored fur.",
                KingdomName = "Dogs",
                BreedName = "German Shepherd",
            });
        }
    }
}
