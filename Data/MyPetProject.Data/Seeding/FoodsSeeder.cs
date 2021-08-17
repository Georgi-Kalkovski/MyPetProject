namespace MyPetProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyPetProject.Data.Models;

    public class FoodsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Foods.Any())
            {
                return;
            }

            await dbContext.Foods.AddAsync(new Food
            {
                Name = "Checken meat",
                PicUrl = "https://i.imgur.com/C0Qf233.png",
                Description = "Chicken is the most common type of poultry in the world. Owing to the relative ease and low cost of raising chickens—in comparison to mammals such as cattle or hogs—chicken meat and chicken eggs have become prevalent in numerous cuisines.",
                FoodTypeName = "Meat",
            });
        }
    }
}
