namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class BreedsControllerShould
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
        public void BreedsControllerWithCreateActionShouldReturnViewPage()
          => MyController<BreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void BreedsControllerWithEditActionShouldReturnViewPage()
          => MyController<BreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Edit("Bulldogs"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void BreedsControllerWithDeleteActionShouldReturnViewPage()
          => MyController<BreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Bulldogs"))
          .ShouldHave()
          .ValidModelState();
    }
}
