using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http {
    /// <summary>
    /// Summary description for MessangerHandler
    /// </summary>
    public class MessangerHandler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            
        }

        public bool IsReusable {
            get {
                return true;
            }
        }
    }
}