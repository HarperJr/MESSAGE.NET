using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http {
    /// <summary>
    /// Summary description for MultimediaHandler
    /// </summary>
    public class MultimediaHandler : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}