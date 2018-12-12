using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http.Models {
    public class IdentificationRequest {

        public string Name { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
    }
}