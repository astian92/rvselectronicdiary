using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RED.Startup))]

namespace RED
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
