namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyPetProject.Web.ViewModels.FoodTypes;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class FoodTypesControllerRoutingTests
    {
        [Fact]
        public void FoodTypesIndexPageWithoutNameShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/FoodTypes")
                 .To<FoodTypesController>(p => p.Index());

        [Fact]
        public void FoodTypesIndexPageWithNameShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/FoodTypes/White%20Persian")
              .To<FoodTypesController>(p => p.Index());

        [Fact]
        public void FoodTypesCreateGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/FoodTypes/Create")
              .To<FoodTypesController>(p => p.Create());

        [Fact]
        public void FoodTypesCreatePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/FoodTypes/Create")
                    .WithMethod(HttpMethod.Post))
                .To<FoodTypesController>(c => c.Create(With.Any<FoodTypeInputModel>()));

        [Fact]
        public void FoodTypesEditGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/FoodTypes/Edit/White%20Persian")
              .To<FoodTypesController>(p => p.Edit("White Persian"));

        [Fact]
        public void FoodTypesEditPostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/FoodTypes/Edit/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<FoodTypesController>(c => c.Edit("White%20Persian"));

        [Fact]
        public void FoodTypesDeleteGetPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/FoodTypes/Delete/White%20Persian")
              .To<FoodTypesController>(p => p.Delete("White Persian"));

        [Fact]
        public void FoodTypesDeletePostPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                    .WithPath("/FoodTypes/Delete/White%20Persian")
                    .WithMethod(HttpMethod.Post))
                .To<FoodTypesController>(c => c.Delete("White%20Persian"));
    }
}
