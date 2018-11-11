using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CadastrProject.Startup))]
namespace CadastrProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
