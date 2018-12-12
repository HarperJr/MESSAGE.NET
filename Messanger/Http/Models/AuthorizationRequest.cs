using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http.Models {
    public class AuthorizationRequest {

        public string Name { get; set; }

        public string Password { get; set; }
    }
}