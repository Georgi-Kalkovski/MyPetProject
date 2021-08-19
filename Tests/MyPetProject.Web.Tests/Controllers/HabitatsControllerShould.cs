namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HabitatsControllerShould
    {
        [Fact]
        public void HabitatsControllerWithHomeAnimalsActionShouldReturnViewPage()
              => MyController<HabitatsController>
              .Instance()
              .Calling(c => c.HomeAnimals())
              .ShouldReturn()
              .View();

        [Fact]
        public void HabitatsControllerWithFarmAnimalsActionShouldReturnViewPage()
              => MyController<HabitatsController>
              .Instance()
              .Calling(c => c.FarmAnimals())
              .ShouldReturn()
              .View();

        [Fact]
        public void HabitatsControllerWithWildAnimalsActionShouldReturnViewPage()
              => MyController<HabitatsController>
              .Instance()
              .Calling(c => c.WildAnimals())
              .ShouldReturn()
              .View();
    }
}
