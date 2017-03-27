using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRM_DEV.Startup))]
namespace HRM_DEV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
