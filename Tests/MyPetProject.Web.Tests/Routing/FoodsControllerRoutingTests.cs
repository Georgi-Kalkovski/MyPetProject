namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.Foods;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class FoodsControllerRoutingTests
    {
        [Fact]
        public void FoodsIndexPageWithoutNameShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Foods")
              .To<FoodsController>(p => p.Index());

        [Fact]
        public void FoodsIndexPageWithNameShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Foods/Mammals")
              .To<FoodsController>(p => p.Index());

        [Fact]
        public void FoodsDetailsPageWithNameShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/Foods/Details/Cats")
             .To<FoodsController>(p => p.Details("Cats"));

        [Fact]
        public void FoodsCreateGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Foods/Create")
              .To<FoodsController>(p => p.Create());

        [Fact]
        public void FoodsCreatePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Foods/Create")
                    .WithMethod(HttpMethod.Post))
                .To<FoodsController>(c => c.Create(With.Any<FoodInputModel>()));

        [Fact]
        public void FoodsEditGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Foods/Edit/Cats")
              .To<FoodsController>(p => p.Edit("Cats"));

        [Fact]
        public void FoodsEditPostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Foods/Edit/Cats")
                    .WithMethod(HttpMethod.Post))
                .To<FoodsController>(c => c.Edit("Cats"));

        [Fact]
        public void FoodsDeleteGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Foods/Delete/Cats")
              .To<FoodsController>(p => p.Delete("Cats"));

        [Fact]
        public void FoodsDeletePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Foods/Delete/Cats")
                    .WithMethod(HttpMethod.Post))
                .To<FoodsController>(c => c.Delete("Cats"));

        [Fact]
        public void FoodsEditGetPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Foods/Edit/White%20Persian")
              .To<FoodsController>(p => p.Edit("White Persian"));

        [Fact]
        public void FoodsEditPostPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Foods/Edit/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<FoodsController>(c => c.Edit("White%20Persian"));

        [Fact]
        public void FoodsDeleteGetPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Foods/Delete/White%20Persian")
              .To<FoodsController>(p => p.Delete("White Persian"));

        [Fact]
        public void FoodsDeletePostPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Foods/Delete/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<FoodsController>(c => c.Delete("White%20Persian"));
    }
}
