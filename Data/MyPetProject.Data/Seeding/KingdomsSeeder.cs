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

            var kingdoms = new Dictionary<string, string>
            {
                { "Dogs", "dog.png" },
                { "Cats", "cat.png" },
                { "Birds", "bird.png" },
            };

            foreach (var kingdom in kingdoms)
            {
                await dbContext.Kingdoms.AddAsync(new Kingdom { Name = kingdom.Key, PicUrl = kingdom.Value });
            }
        }
    }
}
