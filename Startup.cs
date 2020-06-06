using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChooseEvent2.Startup))]
namespace ChooseEvent2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
