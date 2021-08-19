namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class SearchControllerShould
    {
        [Fact]
        public void SearchControllerWithSearchKingdomsActionShouldReturnViewPage()
             => MyController<SearchController>
             .Instance()
             .Calling(c => c.SearchKingdoms())
             .ShouldReturn()
             .View();

        [Fact]
        public void SearchControllerWithSearchBreedsActionShouldReturnViewPage()
             => MyController<SearchController>
             .Instance()
             .Calling(c => c.SearchBreeds())
             .ShouldReturn()
             .View();

        [Fact]
        public void SearchControllerWithSearchSubbreedsActionShouldReturnViewPage()
             => MyController<SearchController>
             .Instance()
             .Calling(c => c.SearchSubbreeds())
             .ShouldReturn()
             .View();

        [Fact]
        public void SearchControllerWithSearchFoodTypesActionShouldReturnViewPage()
             => MyController<SearchController>
             .Instance()
             .Calling(c => c.SearchFoodTypes())
             .ShouldReturn()
             .View();

        [Fact]
        public void SearchControllerWithSearchFoodsActionShouldReturnViewPage()
             => MyController<SearchController>
             .Instance()
             .Calling(c => c.SearchFoods())
             .ShouldReturn()
             .View();
    }
}
