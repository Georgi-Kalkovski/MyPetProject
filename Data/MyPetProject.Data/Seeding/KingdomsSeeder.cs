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

            await dbContext.Kingdoms.AddAsync(new Kingdom
            {
                Name = "Dogs",
                PicUrl = "https://i.imgur.com/wxefSNj.png",
                Group = "Mammals",
                Diet = "Omnivores",
                Description = "The domestic dog is a domesticated descendant of the wolf. The dog derived from an ancient, extinct wolf, and the modern grey wolf is the dog's nearest living relative. The dog was the first species to be domesticated, by hunter–gatherers over 15,000 years ago, before the development of agriculture.",
                IsPet = true,
                IsFarm = true,
            });
            await dbContext.Kingdoms.AddAsync(new Kingdom
            {
                Name = "Cats",
                PicUrl = "https://i.imgur.com/tos0hBy.png",
                Group = "Mammals",
                Diet = "Carnivores",
                Description = "The cat is a domestic species of small carnivorous mammal. It is the only domesticated species in the family Felidae and is often referred to as the domestic cat to distinguish it from the wild members of the family.",
                IsPet = true,
                IsFarm = true,
            });
            await dbContext.Kingdoms.AddAsync(new Kingdom
            {
                Name = "Sparrows",
                PicUrl = "https://i.imgur.com/byYtSGu.png",
                Group = "Birds",
                Diet = "Herbivores",
                Description = "Old World sparrows are a group of small passerine birds forming the family Passeridae. They are also known as true sparrows, a name also used for a particular genus of the family, Passer.",
                IsPet = true,
                IsFarm = false,
            });
            await dbContext.Kingdoms.AddAsync(new Kingdom
            {
                Name = "Parrots",
                PicUrl = "https://i.imgur.com/f3jJdT3.png",
                Group = "Birds",
                Diet = "Herbivores",
                Description = "Parrots, also known as psittacines, are birds of the roughly 398 species in 92 genera comprising the order Psittaciformes, found mostly in tropical and subtropical regions. The order is subdivided into three superfamilies: the Psittacoidea, the Cacatuoidea, and the Strigopoidea.",
                IsPet = true,
                IsFarm = false,
            });
            await dbContext.Kingdoms.AddAsync(new Kingdom
            {
                Name = "Sharks",
                PicUrl = "https://i.imgur.com/9nfYRby.jpg",
                Group = "Fish",
                Diet = "Carnivores",
                Description = "Sharks are a group of elasmobranch fish characterized by a cartilaginous skeleton, five to seven gill slits on the sides of the head, and pectoral fins that are not fused to the head. Modern sharks are classified within the clade Selachimorpha and are the sister group to the rays. ",
                IsPet = false,
                IsFarm = false,
            });
            await dbContext.Kingdoms.AddAsync(new Kingdom
            {
                Name = "Lizards",
                PicUrl = "https://i.imgur.com/MOlH3vp.png",
                Group = "Reptiles",
                Diet = "Carnivores",
                Description = "Lizards are a widespread group of squamate reptiles, with over 6,000 species, ranging across all continents except Antarctica, as well as most oceanic island chains.",
            });
            await dbContext.Kingdoms.AddAsync(new Kingdom
            {
                Name = "Spiders",
                PicUrl = "https://i.imgur.com/tOXzYih.jpg",
                Group = "Insects",
                Diet = "Carnivores",
                Description = "Spiders are air-breathing arthropods that have eight legs, chelicerae with fangs generally able to inject venom, and spinnerets that extrude silk. They are the largest order of arachnids and rank seventh in total species diversity among all orders of organisms.",
                IsPet = true,
                IsFarm = false,
            });
        }
    }
}
