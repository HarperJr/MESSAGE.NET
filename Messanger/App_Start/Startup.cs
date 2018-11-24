using System;
using System.Threading.Tasks;
using Messanger.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Messanger.App_Start.Startup))]
namespace Messanger.App_Start {

public class Startup {
        private static OAuthAuthorizationServerOptions _owinAuthorizationServerOptions =
            new OAuthAuthorizationServerOptions {
                 Provider = new AuthorizationServerProvider(),
                 AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                 TokenEndpointPath = new PathString("auth/token"),
                 AuthorizeEndpointPath = new PathString("/auth/Account/Login"),
                 AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                 AllowInsecureHttp = true
            };

        public void Configuration(IAppBuilder app) {
            app.UseOAuthBearerTokens(_owinAuthorizationServerOptions);
        }
    }
}
