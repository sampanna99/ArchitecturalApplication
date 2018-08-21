using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArchitecturalApplication.Startup))]
namespace ArchitecturalApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
