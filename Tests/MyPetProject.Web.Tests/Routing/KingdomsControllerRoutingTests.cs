namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.Kingdoms;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class KingdomsControllerRoutingTests
    {
        [Fact]
        public void KingdomsIndexPageWithoutNameShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Kingdoms")
              .To<KingdomsController>(p => p.Index());

        [Fact]
        public void KingdomsIndexPageWithNameShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Kingdoms/Mammals")
              .To<KingdomsController>(p => p.Index());

        [Fact]
        public void KingdomsDetailsPageWithNameShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/Kingdoms/Details/Cats")
             .To<KingdomsController>(p => p.Details("Cats"));

        [Fact]
        public void KingdomsCreateGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Kingdoms/Create")
              .To<KingdomsController>(p => p.Create());

        [Fact]
        public void KingdomsCreatePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Kingdoms/Create")
                    .WithMethod(HttpMethod.Post))
                .To<KingdomsController>(c => c.Create(With.Any<KingdomInputModel>()));

        [Fact]
        public void KingdomsEditGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Kingdoms/Edit/Cats")
              .To<KingdomsController>(p => p.Edit("Cats"));

        [Fact]
        public void KingdomsEditPostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Kingdoms/Edit/Cats")
                    .WithMethod(HttpMethod.Post))
                .To<KingdomsController>(c => c.Edit("Cats"));

        [Fact]
        public void KingdomsDeleteGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Kingdoms/Delete/Cats")
              .To<KingdomsController>(p => p.Delete("Cats"));

        [Fact]
        public void KingdomsDeletePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Kingdoms/Delete/Cats")
                    .WithMethod(HttpMethod.Post))
                .To<KingdomsController>(c => c.Delete("Cats"));

        [Fact]
        public void KingdomsEditGetPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Kingdoms/Edit/White%20Persian")
              .To<KingdomsController>(p => p.Edit("White Persian"));

        [Fact]
        public void KingdomsEditPostPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Kingdoms/Edit/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<KingdomsController>(c => c.Edit("White%20Persian"));

        [Fact]
        public void KingdomsDeleteGetPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Kingdoms/Delete/White%20Persian")
              .To<KingdomsController>(p => p.Delete("White Persian"));

        [Fact]
        public void KingdomDeletePostPageWithWhitespaceShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/Kingdoms/Delete/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<KingdomsController>(c => c.Delete("White%20Persian"));
    }
}
