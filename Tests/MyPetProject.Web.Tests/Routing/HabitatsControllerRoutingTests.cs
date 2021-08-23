namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.Foods;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HabitatsControllerRoutingTests
    {
        [Fact]
        public void HomeAnimalsIndexPageShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("/Kingdoms/HomeAnimals")
                    .To<HabitatsController>(p => p.HomeAnimals());

        [Fact]
        public void FarmAnimalsIndexPageShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("/Kingdoms/FarmAnimals")
                    .To<HabitatsController>(p => p.FarmAnimals());

        [Fact]
        public void WildAnimalsIndexPageShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("/Kingdoms/WildAnimals")
                    .To<HabitatsController>(p => p.WildAnimals());
    }
}
