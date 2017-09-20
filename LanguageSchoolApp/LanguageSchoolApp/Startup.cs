using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LanguageSchoolApp.Startup))]
namespace LanguageSchoolApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
