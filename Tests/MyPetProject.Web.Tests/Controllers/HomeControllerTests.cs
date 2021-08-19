namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void HomeControllerWithGroupsActionShouldReturnViewPage()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Groups())
            .ShouldReturn()
            .View();

        [Fact]
        public void HomeControllerWithAboutActionShouldReturnViewPage()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.About())
            .ShouldReturn()
            .View();

        [Fact]
        public void HomeControllerWithErrorPageActionShouldReturnViewPage()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.ErrorPage())
            .ShouldReturn()
            .View();

        [Fact]
        public void HomeControllerWithErrorActionShouldReturnViewPage()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Error())
            .ShouldReturn()
            .View();

    }
}
