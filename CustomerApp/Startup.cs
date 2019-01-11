using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerApp.Startup))]
namespace CustomerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
