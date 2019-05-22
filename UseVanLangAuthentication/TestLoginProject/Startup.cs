using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestLoginProject.Startup))]
namespace TestLoginProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
