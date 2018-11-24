using Messanger.Http.Base;
using System;

using System.Threading;
using System.Web;

namespace Messanger.Http {
    public class HttpAsyncState : IAsyncResult {

        private readonly HttpContext _httpContext;
        private readonly AsyncCallback _asyncCallback;
        private object _extraData;

        private bool _isCompleted;
        private Guid _clientGuid;
        private ManualResetEvent _callCompleteEvent;

        public HttpContext Context => _httpContext;
        public AsyncCallback Callback => _asyncCallback;

        public Guid ClientGuid {
            get => _clientGuid;
            set => _clientGuid = value;
        }

        public object Extra {
            get => _extraData;
            set => _extraData = value;
        }

        public HttpAsyncState(HttpContext httpContext, AsyncCallback asyncCallback, object extraData) {
            _httpContext = httpContext;
            _asyncCallback = asyncCallback;
            _extraData = extraData;
            _isCompleted = false;
        }

        public void CompleteRequest() {
            _isCompleted = true;

            lock(this) {
                _callCompleteEvent?.Set();
            }
            _asyncCallback?.Invoke(this);
        }

        public bool IsCompleted => _isCompleted;

        public WaitHandle AsyncWaitHandle {
            get {
                if (_callCompleteEvent == null) {
                    _callCompleteEvent = new ManualResetEvent(false);
                }
                return _callCompleteEvent;
            }
        }

        public object AsyncState => _extraData;

        public bool CompletedSynchronously => false;

        public bool Completed => _isCompleted;
    }
}