namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.FoodTypes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class FoodTypesControllerTests
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
        public void FoodTypesControllerWithCreateGetActionShouldReturnViewPage()
          => MyController<FoodTypesController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void FoodTypesControllerWithCreatePostActionShouldReturnViewPage()
        => MyController<FoodTypesController>
        .Instance(i => i.WithUser())
              .Calling(c => c.Create(
              With.Empty<FoodTypeInputModel>()))
              .ShouldHave()
          .ValidModelState();

        [Fact]
        public void FoodTypesControllerWithEditPostActionShouldReturnViewPage()
       => MyController<FoodTypesController>
       .Instance(i => i.WithUser())
             .Calling(c => c.Edit(
             "Fish",
             With.Empty<FoodTypeInputModel>()))
             .ShouldHave()
         .ValidModelState();

        [Fact]
        public void FoodTypesControllerWithDeleteGetActionShouldReturnViewPage()
          => MyController<FoodTypesController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Fish"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void FoodTypesControllerWithDeletePostActionShouldReturnViewPage()
      => MyController<FoodTypesController>
      .Instance(i => i.WithUser())
           .Calling(c => c.DeleteConfirmed(With.Empty<int>(), "Fish"));
    }
}
