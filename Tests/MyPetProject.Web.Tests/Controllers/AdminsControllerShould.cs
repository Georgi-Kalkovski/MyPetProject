namespace MyPetProject.Web.Tests.Controller
{
    using MyPetProject.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AdminsControllerShould
    {
        [Fact]
        public void AdminsControllerWithIndexActionShouldRedirectToHomeIndex()
            => MyController<AdminsController>
            .Instance()
            .Calling(c => c.Index())
            .ShouldReturn()
            .RedirectToAction("Index", "Home");
    }
}
