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

            await dbContext.Breeds.AddAsync(new Breed { Name = "German Shephard", PicUrl = "german-shephard.jpg", Description = "The German Shepherd is a breed of medium to large-sized working dog that originated in Germany. According to the FCI, the breed's English language name is German Shepherd Dog.", KingdomId = 1 });
            await dbContext.Breeds.AddAsync(new Breed { Name = "Bulldog", PicUrl = "bulldog.png", Description = "The Bulldog, also known as the English Bulldog or British Bulldog, is a medium-sized dog breed. It is a muscular, hefty dog with a wrinkled face and a distinctive pushed-in nose. The Kennel Club, the American Kennel Club, and the United Kennel Club oversee breeding records.", KingdomId = 1 });
            await dbContext.Breeds.AddAsync(new Breed { Name = "Poodle", PicUrl = "poodle.png", Description = "The Poodle, called the Pudel in German and the Caniche in French, is a breed of water dog. The breed is divided into four varieties based on size, the Standard Poodle, Medium Poodle, Miniature Poodle and Toy Poodle, although the Medium Poodle variety is not universally recognised.", KingdomId = 1 });
        }
    }
}
