namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Data.Models;
    using MyPetProject.Web.ViewModels.Kingdoms;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class KingdomsControllerTests
    {
        [Fact]
        public void KingdomsControllerWithIndexActionShouldReturnViewPage()
           => MyController<KingdomsController>
           .Instance()
           .Calling(c => c.Index())
           .ShouldReturn()
           .View();

        [Fact]
        public void KingdomsControllerWithIndexAndNameActionShouldReturnViewPage()
           => MyController<KingdomsController>
           .Instance()
           .Calling(c => c.Index("Cats"))
           .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void KingdomsControllerWithDetailsActionShouldReturnViewPage()
           => MyController<KingdomsController>
           .Instance()
           .Calling(c => c.Details("Cats"))
           .ShouldHave()
            .ValidModelState();

        [Fact]
        public void KingdomsControllerWithCreateGetActionShouldReturnViewPage()
          => MyController<KingdomsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void KingdomsControllerWithCreatePostActionShouldReturnViewPage()
        => MyController<KingdomsController>
        .Instance(i => i.WithUser())
              .Calling(c => c.Create(
              With.Empty<KingdomInputModel>()))
              .ShouldHave()
          .ValidModelState();

        [Fact]
        public void KingdomsControllerWithEditPostActionShouldReturnViewPage()
        => MyController<KingdomsController>
        .Instance(i => i.WithUser())
              .Calling(c => c.Edit(
              "Cats",
              With.Empty<KingdomInputModel>()))
              .ShouldHave()
          .ValidModelState();

        [Fact]
        public void KingdomsControllerWithDeleteGetActionShouldReturnViewPage()
          => MyController<KingdomsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Cats"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void KingdomsControllerWithDeletePostActionShouldReturnViewPage()
       => MyController<KingdomsController>
       .Instance(i => i.WithUser())
            .Calling(c => c.DeleteConfirmed(With.Empty<int>(), "Cats"));
    }
}
