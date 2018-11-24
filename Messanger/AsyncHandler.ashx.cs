using Messanger.Http;
using Messanger.Logger;
using System;
using System.Web;
using System.Web.SessionState;

namespace Messanger {
  
    public class AsyncHandler : Http.Base.IHttpAsyncHandler, IRequiresSessionState {

        private readonly ILogger _logger = LogFactory.Factory.GetLogger();

        private static readonly string POOL = "ASYNC_HANDLER_POOL";
        private static readonly ThreadPool _threadPool;

        static AsyncHandler() {
            _threadPool = new ThreadPool(2, 50, POOL) {
                PropogateCallContext = true,
                PropogateThreadPrincipal = true,
                PropogateHttpContext = true
            };
            _threadPool.Start();
        }

        public IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object extra) {
            HttpAsyncState currentAsyncRequestState = new HttpAsyncState(httpContext, callback, extra);
            _threadPool.PostRequest(new WorkRequestDelegate(ProcessServiceRequest), currentAsyncRequestState);

            return currentAsyncRequestState;
        }

        public void EndProcessRequest(IAsyncResult asyncResult) {

        }

        private void ProcessServiceRequest(object state, DateTime requestTime) {
            HttpAsyncState currentAsyncRequestState = state as HttpAsyncState;

            if (currentAsyncRequestState.Context.Request.QueryString[ConnectionProtocol.PROTOCOL_GET_PARAMETER_NAME] ==
              ConnectionCommand.CONNECT.ToString()) {
                ClientProcessor.AddClient(currentAsyncRequestState);
                currentAsyncRequestState.Context.Response.Write(currentAsyncRequestState.ClientGuid.ToString());
                currentAsyncRequestState.CompleteRequest();
            } else if (currentAsyncRequestState.Context.Request.QueryString[ConnectionProtocol.PROTOCOL_GET_PARAMETER_NAME] ==
                ConnectionCommand.DISCONNECT.ToString()) {
                ClientProcessor.RemoveClient(currentAsyncRequestState);
                currentAsyncRequestState.CompleteRequest();
            } else {
                if (currentAsyncRequestState.Context.Request.QueryString[ConnectionProtocol.CLIENT_GUID_PARAMETER_NAME] != null) {
                    ClientProcessor.UpdateClient(currentAsyncRequestState,
                      currentAsyncRequestState.Context.Request.QueryString[ConnectionProtocol.CLIENT_GUID_PARAMETER_NAME].ToString());
                }
            }
        }

        public void ProcessRequest(HttpContext context) {
            BeginProcessRequest(context, null, null);
        }

        public bool IsReusable {
            get {
                return true;
            }
        }
    }
}