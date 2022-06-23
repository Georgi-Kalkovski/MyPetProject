namespace MyPetProject.Web.Tests
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyPetProject.Web;
    using MyTested.AspNetCore.Mvc;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }
    }
}
