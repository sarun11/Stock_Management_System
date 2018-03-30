using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Coursework2_Group5.Startup))]
namespace Coursework2_Group5
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
