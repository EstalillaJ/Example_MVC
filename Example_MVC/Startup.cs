using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Example_MVC.Startup))]
namespace Example_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
