using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VidlyTest.Startup))]
namespace VidlyTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
