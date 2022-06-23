namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.Breeds;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class BreedsControllerTests
    {
        [Fact]
        public void BreedsControllerWithIndexActionShouldReturnViewPage()
           => MyController<BreedsController>
           .Instance()
           .Calling(c => c.Index())
           .ShouldReturn()
           .View();

        [Fact]
        public void BreedsControllerWithIndexAndNameActionShouldReturnViewPage()
           => MyController<BreedsController>
           .Instance()
           .Calling(c => c.Index("Bulldogs"))
           .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void BreedsControllerWithDetailsActionShouldReturnViewPage()
           => MyController<BreedsController>
           .Instance()
           .Calling(c => c.Details("Bulldogs"))
           .ShouldHave()
            .ValidModelState();

        [Fact]
        public void BreedsControllerWithCreateGetActionShouldReturnViewPage()
          => MyController<BreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void BreedsControllerWithCreatePostActionShouldReturnViewPage()
        => MyController<BreedsController>
        .Instance(i => i.WithUser())
              .Calling(c => c.Create(
              With.Empty<BreedInputModel>()))
              .ShouldHave()
          .ValidModelState();

        [Fact]
        public void BreedsControllerWithEditPostActionShouldReturnViewPage()
        => MyController<BreedsController>
        .Instance(i => i.WithUser())
              .Calling(c => c.Edit(
              "Cats",
              With.Empty<BreedInputModel>()))
              .ShouldHave()
          .ValidModelState();

        [Fact]
        public void BreedsControllerWithDeleteGetActionShouldReturnViewPage()
          => MyController<BreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Bulldogs"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void BreedsControllerWithDeletePostActionShouldReturnViewPage()
      => MyController<BreedsController>
      .Instance(i => i.WithUser())
           .Calling(c => c.DeleteConfirmed("Cats"));
    }
}
