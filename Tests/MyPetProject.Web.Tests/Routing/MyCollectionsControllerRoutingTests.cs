namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class MyCollectionsControllerRoutingTests
    {
        [Fact]
        public void MyKingdomsPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/MyKingdoms")
              .To<MyCollectionsController>(p => p.MyKingdoms());

        [Fact]
        public void MyBreedsIndexPageShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/MyBreeds")
             .To<MyCollectionsController>(p => p.MyBreeds());

        [Fact]
        public void MySubbreedsIndexPageShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/MySubbreeds")
             .To<MyCollectionsController>(p => p.MySubbreeds());

        [Fact]
        public void MyFoodTypesIndexPageShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap("/MyFoodTypes")
            .To<MyCollectionsController>(p => p.MyFoodTypes());

        [Fact]
        public void MyFoodsIndexPageShouldBeMapped()
       => MyRouting
           .Configuration()
           .ShouldMap("/MyFoods")
           .To<MyCollectionsController>(p => p.MyFoods());
    }
}