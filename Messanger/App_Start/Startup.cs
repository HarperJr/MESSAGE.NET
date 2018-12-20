using System;
using System.Threading.Tasks;
using Messanger.DatabaseAuth;
using Messanger.DatabaseAuth.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Messanger.App_Start.Startup))]
namespace Messanger.App_Start {

public class Startup {
       
        public void Configuration(IAppBuilder app) {
            ConfigureOwin(app);
        }

        private void ConfigureOwin(IAppBuilder app) {
            app.CreatePerOwinContext<AuthDbContext>(AuthDbContext.Initialize);
            app.CreatePerOwinContext<AuthUserManager>(AuthUserManager.Initialize);
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/SignIn")
            });
        }
    }
}
