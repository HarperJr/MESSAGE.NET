using Messanger.DatabaseAuth.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.DatabaseAuth.Managers {
    public class AuthUserManager : UserManager<AuthUser> {

        public AuthUserManager(UserStore<AuthUser> userStore) : base(userStore) {

        }

        public static AuthUserManager Initialize(IdentityFactoryOptions<AuthUserManager> options, 
            IOwinContext context) {
            AuthDbContext authDbContext = context.Get<AuthDbContext>();
            return new AuthUserManager(new UserStore<AuthUser>(authDbContext));
        }
    }
}