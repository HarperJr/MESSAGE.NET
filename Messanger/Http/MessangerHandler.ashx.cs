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

        private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        private readonly IMessageRepository _messageRepository;
        private readonly IDialogRepository _dialogRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IContactRepository _contactRepository;

        public MessangerHandler() {
            LocalDbContext localDbContext = new LocalDbContext();
            // Hardcoded injection
            _messageRepository = new MessageRepository(localDbContext);
            _dialogRepository = new DialogRepository(localDbContext);
            _participantRepository = new ParticipantRepository(localDbContext);
            _contactRepository = new ContactRepository(localDbContext);
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

            if (_sockets.ContainsKey(consumerGuid)) {
                _sockets.TryRemove(consumerGuid, out WebSocket removedSocket);
            }
            _sockets.TryAdd(consumerGuid, socket);

            while (true) {
                if (socket.State.Equals(WebSocketState.Open)) {
                    request = await GetJsonRequestAsync(socket);
                    request.Add("senderId", consumerGuid);

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
                        case WebSocketRequestType.DELETE_CONTACT: {
                                await HandleContactDeleting(
                                    consumerGuid,
                                    request);
                                break;
                            }
                        case WebSocketRequestType.CREATE_DIALOG: {
                                await HandleDialogCreating(
                                    consumerGuid,
                                    request);
                                break;
                            }
                    }
                } else {
                    if (_sockets.TryRemove(consumerGuid, out WebSocket removedSocket)) {
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

        private async Task SendJsonResponseAsync(ICollection<WebSocket> sockets, JObject response) {
            ArraySegment<byte> buffer = new ArraySegment<byte>(
                Encoding.UTF8.GetBytes(response.ToString()));
            foreach (var socket in sockets) {
                await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
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

            ICollection<Participant> participants = _participantRepository
                .GetParticipantsByDialogId(dialogId);
            foreach (Participant participant in participants) {
                await SendJsonResponseAsync(getSockets(participant.ParticipantId), jsonRequest);
            }
        }

        private async Task HandleInviting(string guid, JObject jsonRequest) {
            string receiverId = jsonRequest.GetValue("receiverId").Value<string>();
            string dialogId = jsonRequest.GetValue("dialogId").Value<string>();
            if (dialogId != null) {
                _participantRepository.Insert(new Participant {
                    DialogId = dialogId,
                    InvitorId = guid,
                    ParticipantId = receiverId
                });
            }
            await SendJsonResponseAsync(getSockets(receiverId), jsonRequest);
        }

        private async Task HandleNotifying(string guid, JObject jsonRequest) {
           
        }

        private async Task HandleContactDeleting(string guid, JObject jsonRequest) {

        }

        private async Task HandleDialogCreating(string guid, JObject jsonRequest) {
            string receiverId = jsonRequest.GetValue("receiverId").Value<string>();
            if (receiverId != null) {
                var dialogId = Guid.NewGuid().ToString();
                _dialogRepository.Insert(new Dialog {
                    Id = dialogId,
                    InitTime = DateTime.UtcNow,
                    OwnerId = guid
                });
                _participantRepository.Insert(new Participant {
                    DialogId = dialogId,
                    InvitorId = receiverId,
                    ParticipantId = guid
                });
                jsonRequest.Add("dialogId", dialogId);
                await SendJsonResponseAsync(getSockets(guid), jsonRequest);
            }
        }

        private async Task HandleContactAdding(string guid, JObject jsonRequest) {
            string receiver = jsonRequest.GetValue("receiverId").Value<string>();
            string status = jsonRequest.GetValue("status").Value<string>();
            switch (status) {
                case "request": {
                        Contact contact = _contactRepository
                            .GetByRelatedConsumersId(guid, receiver);
                        if (contact == null) {
                            _contactRepository.Insert(new Contact {
                                InitialConsumerId = guid,
                                RelatedConsumerId = receiver,
                                InitTime = DateTime.UtcNow,
                                Status = status
                            });
                        }
                        break;
                    }
                case "response": {
                        Contact contact = _contactRepository
                            .GetByRelatedConsumersId(receiver, guid);
                        if (contact != null) {
                            _contactRepository.UpdateStatus(contact, status);
                        }
                        break;
                    }
            }
            await SendJsonResponseAsync(getSockets(receiver), jsonRequest);
        }

        private ICollection<WebSocket> getSockets(string guid) {
            return _sockets.Where(socket => socket.Key.Equals(guid))
                .Select(socket => socket.Value)
                .ToList();
        }

        public bool IsReusable {
            get {
                return true;
            }
        }
    }
}