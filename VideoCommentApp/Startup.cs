using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoCommentApp.Startup))]
namespace VideoCommentApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
