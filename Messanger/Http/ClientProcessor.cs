using Messanger.Http.Base;
using Messanger.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger.Http {
    public static class ClientProcessor {
        private static readonly ILogger _logger = LogFactory.Factory.GetLogger();

        private static object _lockObj;
        private static List<HttpAsyncState> _clientStates;

        static ClientProcessor() {
            _lockObj = new object();
            _clientStates = new List<HttpAsyncState>();
        }

        public static void PushData(string data) {
            var clientStates = new List<HttpAsyncState>();

            lock (_lockObj) {
               foreach (var state in _clientStates) {
                    clientStates.Add(state);
                }
            }

            foreach (var state in clientStates) {
                if (state.Context.Session != null) {
                    state.Context.Response.Write(data);
                    state.CompleteRequest();
                }
            }
        }

        public static void AddClient(HttpAsyncState asyncState) {
            Guid guid;

            lock (_lockObj) {
                guid = Guid.NewGuid();
                if (_clientStates.Find(s => s.ClientGuid.Equals(guid)) == null) {
                    asyncState.ClientGuid = guid;
                }
                _clientStates.Add(asyncState);
                _logger.Log(LogLevel.TRACE, $"AddClient Guid: {guid.ToString()}");
            }
        }

        public static void UpdateClient(HttpAsyncState asyncState, string guidKey) {
            Guid guid = new Guid(guidKey);

            lock (_lockObj) {
                HttpAsyncState foundState = _clientStates.Find(s => s.ClientGuid == guid);
                if (foundState != null) {
                    foundState.Extra = asyncState.Extra;
                }
            }
        }

        public static void RemoveClient(HttpAsyncState asyncState) {
            lock (_lockObj) {
                _clientStates.Remove(asyncState);
            }
        }
    }
}
