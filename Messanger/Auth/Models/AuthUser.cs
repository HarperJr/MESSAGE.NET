using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Auth.Models {
    public class AuthUser {

        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }
    }
}