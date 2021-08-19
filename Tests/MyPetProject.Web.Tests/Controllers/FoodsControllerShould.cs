namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class FoodsControllerShould
    {
        [Fact]
        public void FoodsControllerWithIndexActionShouldReturnViewPage()
              => MyController<FoodsController>
              .Instance()
              .Calling(c => c.Index())
              .ShouldReturn()
              .View();

        [Fact]
        public void FoodsControllerWithIndexAndNameActionShouldReturnViewPage()
           => MyController<FoodsController>
           .Instance()
           .Calling(c => c.Index("Tuna"))
           .ShouldHave()
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void FoodsControllerWithDetailsActionShouldReturnViewPage()
           => MyController<FoodsController>
           .Instance()
           .Calling(c => c.Details("Tuna"))
           .ShouldHave()
            .ValidModelState();

        [Fact]
        public void FoodsControllerWithCreateActionShouldReturnViewPage()
          => MyController<FoodsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Create())
          .ShouldReturn()
          .View();

        [Fact]
        public void FoodsControllerWithEditActionShouldReturnViewPage()
          => MyController<FoodsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Edit("Tuna"))
          .ShouldHave()
          .ValidModelState();

        [Fact]
        public void FoodsControllerWithDeleteActionShouldReturnViewPage()
          => MyController<BreedsController>
          .Instance(i => i.WithUser())
          .Calling(c => c.Delete("Tuna"))
          .ShouldHave()
          .ValidModelState();
    }
}
