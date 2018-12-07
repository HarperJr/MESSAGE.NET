using Messanger.Auth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Auth {
    public class AuthUserManager : UserManager<AuthUser> {

        public AuthUserManager(IUserStore<AuthUser> userStore) : base(userStore) {

        }

        public static AuthUserManager Create(IdentityFactoryOptions<AuthUserManager> options, IOwinContext context) {
            AuthDbContext authDbContext = context.Get<AuthDbContext>();
            return new AuthUserManager(new UserStore<AuthUser>(authDbContext));
        }
    }
}