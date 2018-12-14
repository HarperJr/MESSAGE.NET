using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Http.DataTypes;
using Messanger.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Messanger.Http {
    /// <summary>
    /// Summary description for MessangerHandler
    /// </summary>
    public class MessangerHandler : IHttpHandler {

        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
        private readonly IMessageRepository _messageRepository;
        private readonly IDialogRepository _dialogRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IContactRepository _contactRepository;

        public MessangerHandler() {
            _messageRepository = new MessageRepository(new LocalDbContext());
            _dialogRepository = new DialogRepository(new LocalDbContext());
            _participantRepository = new ParticipantRepository(new LocalDbContext());
            _contactRepository = new ContactRepository(new LocalDbContext());
        }

        public void ProcessRequest(HttpContext context) {
            if (context.IsWebSocketRequest) {
                context.AcceptWebSocketRequest(HandleWebSocket);
            }
        }

        private async Task HandleWebSocket(AspNetWebSocketContext context) {
            var socket = context.WebSocket;

            JObject request = await GetJsonRequestAsync(socket);
            string consumerGuid = request.GetValue("consumerGuid").Value<string>();

            if (!_sockets.ContainsKey(consumerGuid)) {
                _sockets.TryAdd(consumerGuid, socket);
            } else {
                _sockets.TryRemove(consumerGuid, out WebSocket removedSocket);
            }

            while (true) {
                if (socket.State.Equals(WebSocketState.Open)) {
                    request = await GetJsonRequestAsync(socket);

                    Enum.TryParse(request.GetValue("requestType")
                        .Value<string>(), out WebSocketRequestType requestType);
                    switch (requestType) {
                        case WebSocketRequestType.SEND_MESSAGE: {
                                await HandleMessageSending(
                                    consumerGuid,
                                    request);
                                break;
                            }
                        case WebSocketRequestType.INVITE: {
                                await HandleInviting(
                                    consumerGuid,
                                    request);
                                break;
                            }
                        case WebSocketRequestType.NOTIFY: {
                                await HandleNotifying(
                                    consumerGuid,
                                    request);
                                break;
                            }
                        case WebSocketRequestType.ADD_CONTACT: {
                                await HandleContactAdding(
                                    consumerGuid,
                                    request);
                                break;
                            }
                    }
                } else {
                    _sockets.TryRemove(consumerGuid, out WebSocket removedSocket);
                    if (removedSocket != null) {
                        await removedSocket.CloseAsync(
                            WebSocketCloseStatus.NormalClosure, "closing", CancellationToken.None);
                    }
                    break;
                }
            }
        }

        private async Task<JObject> GetJsonRequestAsync(WebSocket socket) {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result = await socket.ReceiveAsync(
                    buffer, CancellationToken.None);
            if (result.MessageType.Equals(WebSocketMessageType.Text)) {
                string request = Encoding
                    .UTF8
                    .GetString(buffer.Array, 0, result.Count);
                return JObject.Parse(request);
            } else {
                return null;
            }
        }

        private async Task SendJsonResponseAsync(WebSocket socket, JObject response) {
            ArraySegment<byte> buffer = new ArraySegment<byte>(
                Encoding.UTF8.GetBytes(response.ToString()));
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task HandleMessageSending(string guid, JObject jsonRequest) {
            JToken message = jsonRequest.GetValue("message");
            string dialogId = message.Value<string>("dialogId");
            string content = message.Value<string>("content");

            _messageRepository.Insert(new Message {
                Id = Guid.NewGuid().ToString(),
                DialogId = dialogId,
                SenderId = guid,
                Content = content,
                Time = DateTime.UtcNow,
                HasMultimedia = false,
                Viewed = false
            });

            ICollection<Participant> participants = _participantRepository.GetParticipantsByDialogId(dialogId);
            foreach (Participant participant in participants) {
                WebSocket receiverSocket = _sockets[participant.ParticipantId];
                await SendJsonResponseAsync(receiverSocket, jsonRequest);
            }
        }

        private async Task HandleInviting(string guid, JObject jsonRequest) {
            string receiver = jsonRequest.GetValue("receiverId").Value<string>();
            await SendJsonResponseAsync(_sockets[receiver], jsonRequest);
        }

        private async Task HandleNotifying(string guid, JObject jsonRequest) {

        }
   
        private async Task HandleContactAdding(string guid, JObject jsonRequest) {
            string receiver = jsonRequest.GetValue("receiverId").Value<string>();

            _contactRepository.Insert(new Contact {
                InitTime = DateTime.UtcNow,
                InitialConsumerId = guid,
                RelatedConsumerId = receiver,
                Status = "Unaccepted"
            });

            JObject response = new JObject {
                { "senderId", guid },
            };
            await SendJsonResponseAsync(_sockets[receiver], response);
        }

        public bool IsReusable {
            get {
                return true;
            }
        }
    }
}