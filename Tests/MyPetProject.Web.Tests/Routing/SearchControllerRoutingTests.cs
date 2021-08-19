namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SearchControllerRoutingTests
    {
        [Fact]
        public void SearchKingdomsPageShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/SearchKingdoms")
                .To<SearchController>(p => p.SearchKingdoms());

        [Fact]
        public void SearchBreedsPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/SearchBreeds")
              .To<SearchController>(p => p.SearchBreeds());

        [Fact]
        public void SearchSubbreedsPageShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/SearchSubbreeds")
              .To<SearchController>(p => p.SearchSubbreeds());

        [Fact]
        public void SearchFoodTypesPageShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/SearchFoodTypes")
             .To<SearchController>(p => p.SearchFoodTypes());

        [Fact]
        public void SearchFoodsPageShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/SearchFoods")
             .To<SearchController>(p => p.SearchFoods());
    }
}
