namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class MyCollectionsShould
    {
        [Fact]
        public void MyCollectionsControllerWithMyKingdomsActionShouldReturnViewPage()
             => MyController<MyCollectionsController>
             .Instance()
             .Calling(c => c.MyKingdoms())
             .ShouldReturn()
             .View();

        [Fact]
        public void MyCollectionsControllerWithMyBreedsActionShouldReturnViewPage()
             => MyController<MyCollectionsController>
             .Instance()
             .Calling(c => c.MyBreeds())
             .ShouldReturn()
             .View();

        [Fact]
        public void MyCollectionsControllerWithMySubbreedsActionShouldReturnViewPage()
             => MyController<MyCollectionsController>
             .Instance()
             .Calling(c => c.MySubbreeds())
             .ShouldReturn()
             .View();

        [Fact]
        public void MyCollectionsControllerWithMyFoodTypesActionShouldReturnViewPage()
             => MyController<MyCollectionsController>
             .Instance()
             .Calling(c => c.MyFoodTypes())
             .ShouldReturn()
             .View();

        [Fact]
        public void MyCollectionsControllerWithMyFoodsActionShouldReturnViewPage()
             => MyController<MyCollectionsController>
             .Instance()
             .Calling(c => c.MyFoods())
             .ShouldReturn()
             .View();
    }
}
