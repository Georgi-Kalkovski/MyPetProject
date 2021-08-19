namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.Breeds;
    using MyPetProject.Web.ViewModels.Subbreeds;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SubbreedsControllerRoutingTests
    {
        [Fact]
        public void SubbreedsIndexPageWithoutNameShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Subbreeds")
                 .To<SubbreedsController>(p => p.Index());

        [Fact]
        public void SubbreedsIndexPageWithNameShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Subbreeds/White%20Persian")
              .To<SubbreedsController>(p => p.Index());

        [Fact]
        public void SubbreedsCreateGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Subbreeds/Create")
              .To<SubbreedsController>(p => p.Create());

        [Fact]
        public void SubbreedsCreatePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Subbreeds/Create")
                    .WithMethod(HttpMethod.Post))
                .To<SubbreedsController>(c => c.Create(With.Any<SubbreedInputModel>()));

        [Fact]
        public void SubbreedsEditGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Subbreeds/Edit/White%20Persian")
              .To<SubbreedsController>(p => p.Edit("White Persian"));

        [Fact]
        public void SubbreedsEditPostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Subbreeds/Edit/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<SubbreedsController>(c => c.Edit("White%20Persian"));

        [Fact]
        public void SubbreedsDeleteGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Subbreeds/Delete/White%20Persian")
              .To<SubbreedsController>(p => p.Delete("White Persian"));

        [Fact]
        public void SubbreedsDeletePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Subbreeds/Delete/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<SubbreedsController>(c => c.Delete("White%20Persian"));
    }
}
