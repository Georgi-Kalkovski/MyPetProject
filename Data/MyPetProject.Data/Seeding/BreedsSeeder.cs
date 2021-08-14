namespace MyPetProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyPetProject.Data.Models;

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
                Name = "German Shepherd",
                PicUrl = "https://i.imgur.com/i2zKbHo.jpg",
                Description = "The German Shepherd is a breed of medium to large-sized working dog that originated in Germany. According to the FCI, the breed's English language name is German Shepherd Dog.",
                KingdomName = "Dogs",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "Bulldog",
                PicUrl = "https://i.imgur.com/jCJHSzS.jpg",
                Description = "The Bulldog, also known as the English Bulldog or British Bulldog, is a medium-sized dog breed. It is a muscular, hefty dog with a wrinkled face and a distinctive pushed-in nose. The Kennel Club, the American Kennel Club, and the United Kennel Club oversee breeding records.",
                KingdomName = "Dogs",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "Poodle",
                PicUrl = "https://i.imgur.com/cgJm7ij.jpg",
                Description = "The Poodle, called the Pudel in German and the Caniche in French, is a breed of water dog. The breed is divided into four varieties based on size, the Standard Poodle, Medium Poodle, Miniature Poodle and Toy Poodle, although the Medium Poodle variety is not universally recognised.",
                KingdomName = "Dogs",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "Persian",
                PicUrl = "https://i.imgur.com/LTWlJl2.png",
                Description = "The Persian cat is a long-haired breed of cat characterized by its round face and short muzzle. It is also known as the \"Persian Longhair\" in the English-speaking countries. The first documented ancestors of the Persian were imported into Italy from Persia around 1620.",
                KingdomName = "Cats",
            });

            await dbContext.Breeds.AddAsync(new Breed
            {
                Name = "True Parrot",
                PicUrl = "https://i.imgur.com/DiJQjiE.jpg",
                Description = "The true parrots are about 350 species of colorful flighted hook-billed, mostly herbivorous birds forming the superfamily Psittacoidea, one of the three superfamilies in the biological order Psittaciformes.",
                KingdomName = "Parrots",
            });
        }
    }
}
