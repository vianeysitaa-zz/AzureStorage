using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OpentecSoftekMentor.Startup))]
namespace OpentecSoftekMentor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
