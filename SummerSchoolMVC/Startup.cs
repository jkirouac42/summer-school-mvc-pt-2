using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SummerSchoolMVC.Startup))]
namespace SummerSchoolMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
