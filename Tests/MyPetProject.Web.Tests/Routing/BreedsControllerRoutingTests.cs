namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.Breeds;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class BreedsControllerRoutingTests
    {
        [Fact]
        public void BreedsIndexPageWithoutNameShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Breeds")
                 .To<BreedsController>(p => p.Index());

        [Fact]
        public void BreedsIndexPageWithNameShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Breeds/Persian")
              .To<BreedsController>(p => p.Index());

        [Fact]
        public void BreedsDetailsPageWithNameShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/Breeds/Details/Persian")
             .To<BreedsController>(p => p.Details("Persian"));

        [Fact]
        public void BreedsCreateGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Breeds/Create")
              .To<BreedsController>(p => p.Create());

        [Fact]
        public void BreedsCreatePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Breeds/Create")
                    .WithMethod(HttpMethod.Post))
                .To<BreedsController>(c => c.Create(With.Any<BreedInputModel>()));

        [Fact]
        public void BreedsEditGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Breeds/Edit/Persian")
              .To<BreedsController>(p => p.Edit("Persian"));

        [Fact]
        public void BreedsEditPostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Breeds/Edit/Persian")
                    .WithMethod(HttpMethod.Post))
                .To<BreedsController>(c => c.Edit("Persian"));

        [Fact]
        public void BreedsDeleteGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Breeds/Delete/Persian")
              .To<BreedsController>(p => p.Delete("Persian"));

        [Fact]
        public void BreedsDeletePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Breeds/Delete/Persian")
                    .WithMethod(HttpMethod.Post))
                .To<BreedsController>(c => c.Delete("Persian"));

        [Fact]
        public void BreedsEditGetPageWithWhitespaceShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/Breeds/Edit/White%20Persian")
             .To<BreedsController>(p => p.Edit("White Persian"));

        [Fact]
        public void BreedsEditPostPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Breeds/Edit/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<BreedsController>(c => c.Edit("White%20Persian"));

        [Fact]
        public void BreedsDeleteGetPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Breeds/Delete/White%20Persian")
              .To<BreedsController>(p => p.Delete("White Persian"));

        [Fact]
        public void BreedsDeletePostPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Breeds/Delete/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<BreedsController>(c => c.Delete("White%20Persian"));
    }
}
