using System;
using System.Threading.Tasks;
using Messanger.Auth;
using Messanger.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Messanger.App_Start.Startup))]
namespace Messanger.App_Start {

public class Startup {

        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app) {
            app.CreatePerOwinContext(AuthDbContext.GetInstance);
        }
    }
}
