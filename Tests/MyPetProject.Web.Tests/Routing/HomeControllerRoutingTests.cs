namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerRoutingTests
    {
        [Fact]
        public void HomeIndexRouteShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap("/")
                  .To<HomeController>(c => c.Index());

        [Fact]
        public void GroupsRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("Groups")
                 .To<HomeController>(c => c.Groups());

        [Fact]
        public void AboutRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("About")
                .To<HomeController>(c => c.About());

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());


        [Fact]
        public void ErrorPageRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("Error")
                .To<HomeController>(c => c.ErrorPage());
    }
}
