namespace MyPetProject.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MyPetProject.Data.Models;

    public class KingdomsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Kingdoms.Any())
            {
                return;
            }

            await dbContext.Kingdoms.AddAsync(new Kingdom { Name = "Dogs", PicUrl = "dog.png", Group = "Mammals", Diet = "Omnivores" });
            await dbContext.Kingdoms.AddAsync(new Kingdom { Name = "Cats", PicUrl = "cat.png", Group = "Mammals", Diet = "Carnivores" });
            await dbContext.Kingdoms.AddAsync(new Kingdom { Name = "Sparrows", PicUrl = "sparrow.png", Group = "Birds", Diet = "Herbivores" });
            await dbContext.Kingdoms.AddAsync(new Kingdom { Name = "Parrots", PicUrl = "parrot.png", Group = "Birds", Diet = "Herbivores" });
            await dbContext.Kingdoms.AddAsync(new Kingdom { Name = "Sharks", PicUrl = "shark.jpg", Group = "Fish", Diet = "Carnivores" });
            await dbContext.Kingdoms.AddAsync(new Kingdom { Name = "Lizards", PicUrl = "lizard.png", Group = "Reptiles", Diet = "Carnivores" });
            await dbContext.Kingdoms.AddAsync(new Kingdom { Name = "Spiders", PicUrl = "spider.png", Group = "Insects", Diet = "Carnivores" });
        }
    }
}
