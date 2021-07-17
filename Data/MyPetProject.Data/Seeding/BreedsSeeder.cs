using MyPetProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetProject.Data.Seeding
{
    public class BreedsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Breeds.Any())
            {
                return;
            }

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "German Shephard",
                PicUrl = "german-shephard.jpg",
                Description = "The German Shepherd is a breed of medium to large-sized working dog that originated in Germany. According to the FCI, the breed's English language name is German Shepherd Dog.",
                KingdomName = "Dogs",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "Bulldog",
                PicUrl = "bulldog.png",
                Description = "The Bulldog, also known as the English Bulldog or British Bulldog, is a medium-sized dog breed. It is a muscular, hefty dog with a wrinkled face and a distinctive pushed-in nose. The Kennel Club, the American Kennel Club, and the United Kennel Club oversee breeding records.",
                KingdomName = "Dogs",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "Poodle",
                PicUrl = "poodle.png",
                Description = "The Poodle, called the Pudel in German and the Caniche in French, is a breed of water dog. The breed is divided into four varieties based on size, the Standard Poodle, Medium Poodle, Miniature Poodle and Toy Poodle, although the Medium Poodle variety is not universally recognised.",
                KingdomName = "Dogs",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "Persian",
                PicUrl = "persian.png",
                Description = "The Persian cat is a long-haired breed of cat characterized by its round face and short muzzle. It is also known as the \"Persian Longhair\" in the English-speaking countries. The first documented ancestors of the Persian were imported into Italy from Persia around 1620.",
                KingdomName = "Cats",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "Parrot",
                PicUrl = "parrot.jpg",
                Description = "Parrots, also known as psittacines, are birds of the roughly 398 species in 92 genera comprising the order Psittaciformes, found mostly in tropical and subtropical regions. The order is subdivided into three superfamilies: the Psittacoidea, the Cacatuoidea, and the Strigopoidea.",
                KingdomName = "Birds",
            });
        }
    }
}
