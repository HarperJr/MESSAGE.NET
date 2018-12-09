using Messanger.Auth.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Auth {
    public class AuthDbContext : IdentityDbContext<IdentityUser> {

        private static AuthDbContext _instance;

        public static AuthDbContext GetInstance() {
            if (_instance == null) {
                _instance = new AuthDbContext();
            }
            return _instance;
        }

        public AuthDbContext() : base("AuthDb") {
           
        }

    }
}