using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Drole.Startup))]
namespace Drole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
