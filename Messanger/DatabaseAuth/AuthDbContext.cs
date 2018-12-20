using Messanger.DatabaseAuth.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.DatabaseAuth {
    public class AuthDbContext : IdentityDbContext<AuthUser> {

        public AuthDbContext() : base("AuthDb") {

        }

        public static AuthDbContext Initialize() {
            return new AuthDbContext();
        }
    }
}