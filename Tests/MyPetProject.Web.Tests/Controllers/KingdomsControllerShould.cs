namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class KingdomsControllerShould
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
        public void KingdomsControllerWithCreateActionShouldReturnViewPage()
          => MyController<KingdomsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void KingdomsControllerWithEditActionShouldReturnViewPage()
          => MyController<KingdomsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Edit("Cats"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void KingdomsControllerWithDeleteActionShouldReturnViewPage()
          => MyController<KingdomsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Cats"))
          .ShouldHave()
          .ValidModelState();
    }
}
