using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Messanger.Providers {
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider {

        //Overriding to  OAuthAuthorizationServerProvider classes ValidateClientAuthentication method.
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
            context.Validated();
        }
        //Overriding to  OAuthAuthorizationServerProvider classes GrantResourceOwnerCredentials method.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {
            // CORS settings
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            // Validation for user access
            if (context.UserName == "UserName" && context.Password == "Password") {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                context.Validated(identity);
            } else {
                context.SetError("invalid_grant", "Username or password is incorrect");
            }
        }
    }
}