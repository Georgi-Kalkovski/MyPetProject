namespace MyPetProject.Web.Tests.Routing
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AdminsControllerRoutingTests
    {
        [Fact]
        public void GetAdminPageShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap("/Admins/Index")
            .To<AdminsController>(p => p.Index());
    }
}
