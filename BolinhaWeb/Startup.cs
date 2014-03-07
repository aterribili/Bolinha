using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BolinhaWeb.Startup))]
namespace BolinhaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
