using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eCom.Web.Startup))]
namespace eCom.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
