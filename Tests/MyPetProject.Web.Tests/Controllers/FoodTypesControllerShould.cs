namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class FoodTypesControllerShould
    {
        [Fact]
        public void FoodTypesControllerWithIndexActionShouldReturnViewPage()
              => MyController<FoodTypesController>
              .Instance()
              .Calling(c => c.Index())
              .ShouldReturn()
              .View();

        [Fact]
        public void FoodTypesControllerWithIndexAndNameActionShouldReturnViewPage()
           => MyController<FoodTypesController>
           .Instance()
           .Calling(c => c.Index("Fish"))
           .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void FoodTypesControllerWithDetailsActionShouldReturnViewPage()
           => MyController<FoodTypesController>
           .Instance()
           .Calling(c => c.Details("Fish"))
           .ShouldHave()
            .ValidModelState();

        [Fact]
        public void FoodTypesControllerWithCreateActionShouldReturnViewPage()
          => MyController<FoodTypesController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void FoodTypesControllerWithEditActionShouldReturnViewPage()
          => MyController<FoodTypesController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Edit("Fish"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void FoodTypesControllerWithDeleteActionShouldReturnViewPage()
          => MyController<FoodTypesController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Fish"))
          .ShouldHave()
          .ValidModelState();
    }
}
