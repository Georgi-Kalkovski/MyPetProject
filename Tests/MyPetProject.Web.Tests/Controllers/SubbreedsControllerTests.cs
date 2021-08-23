namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Common;
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.Subbreeds;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SubbreedsControllerTests
    {
        [Fact]
        public void SubbreedsControllerWithIndexActionShouldReturnViewPage()
              => MyController<SubbreedsController>
              .Instance()
              .Calling(c => c.Index())
              .ShouldReturn()
              .View();

        [Fact]
        public void SubbreedsControllerWithIndexAndNameActionShouldReturnViewPage()
           => MyController<SubbreedsController>
           .Instance()
           .Calling(c => c.Index("Panda German Shepherd"))
           .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void SubbreedsControllerWithDetailsActionShouldReturnViewPage()
           => MyController<SubbreedsController>
           .Instance()
           .Calling(c => c.Details("Panda German Shepherd"))
           .ShouldHave()
            .ValidModelState();

        [Fact]
        public void SubbreedsControllerWithCreateActionShouldReturnViewPage()
          => MyController<SubbreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void SubbreedsControllerWithCreatePostActionShouldReturnViewPage()
        => MyController<SubbreedsController>
        .Instance(i => i.WithUser())
              .Calling(c => c.Create(
              With.Empty<SubbreedInputModel>()))
              .ShouldHave()
          .ValidModelState();

        [Fact]
        public void SubbreedsControllerWithEditPostActionShouldReturnViewPage()
        => MyController<SubbreedsController>
        .Instance(i => i.WithUser())
              .Calling(c => c.Edit(
              "Panda German Shepherd",
              With.Empty<SubbreedInputModel>()))
              .ShouldHave()
          .ValidModelState();

        [Fact]
        public void SubbreedsControllerWithDeleteActionShouldReturnViewPage()
          => MyController<SubbreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Panda German Shepherd"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void SubbreedsControllerWithDeletePostActionShouldReturnViewPage()
       => MyController<SubbreedsController>
       .Instance(i => i.WithUser())
            .Calling(c => c.DeleteConfirmed("Panda German Shepherd"));
    }
}
