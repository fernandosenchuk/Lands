using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute("LandsAPI", typeof(Lands.Backend.Startup))]
[assembly: OwinStartupAttribute(typeof(Lands.Backend.Startup))]
namespace Lands.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
