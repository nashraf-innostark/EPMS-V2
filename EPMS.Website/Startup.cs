using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EPMS.Website.Startup))]
namespace EPMS.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
