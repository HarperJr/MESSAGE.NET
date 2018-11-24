using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http.Base {

 
    public interface IHttpAsyncHandler : IHttpHandler {

        IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object extra);

        void EndProcessRequest(IAsyncResult asyncResult);
    }
}