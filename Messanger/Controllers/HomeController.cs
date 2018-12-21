using Database;
using Messanger.Data.Models;
using Messanger.Data.ViewModels;
using Messanger.Database;
using Messanger.Http.Models;
using Messanger.Logger;
using Messanger.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Messanger.Controllers
{
    /**
     * Main Controller
     **/
    public class HomeController : Controller
    {
        private readonly ILogger _logger = LogFactory.Factory.GetLogger<HomeController>();
        private readonly LocalDbContext _localDbContext;
        private readonly IConsumerRepository _consumerRepository;
        private readonly IDialogRepository _dialogRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IParticipantRepository _participantRepository;

        public HomeController() {
            _localDbContext = new LocalDbContext();
            _consumerRepository = new ConsumerRepository(_localDbContext);
            _dialogRepository = new DialogRepository(_localDbContext);
            _messageRepository = new MessageRepository(_localDbContext);
            _participantRepository = new ParticipantRepository(_localDbContext);
        }

        // GET: Home
        [HttpGet]
        [Authorize]
        public ActionResult Index(IndexRequest request) {
            Consumer consumer = _consumerRepository
                .GetById(request.Consumer);
            if (consumer == null) {
                return RedirectToAction("SignIn", "Auth");
            }
            return View(consumer);
        }

        [Authorize]
        public ActionResult Dialogs(DialogsRequest request) {
            ICollection<ViewModelDialog> dialogViewModels = new List<ViewModelDialog>();
            ICollection<Dialog> dialogs = _dialogRepository
                .GetDialogsByConsumerIdWithOffsetAndLimit(request.Uid, request.Offset, request.Limit);
            if (dialogs == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            } else {
                foreach (var dialog in dialogs) {
                    string dialogTitle = "";
                    ICollection<string> participantNames = _participantRepository
                        .GetParticipantNamesByDialogId(dialog.Id);
                    var consumer = _consumerRepository
                        .GetById(request.Uid);
                    foreach (var name in participantNames) {
                        if (name.Equals(consumer.Name)) {
                            continue;
                        }
                        dialogTitle += $"{name} ";
                    }
                    dialogViewModels.Add(new ViewModelDialog {
                        DialogId = dialog.Id,
                        InitTime = dialog.InitTime,
                        DialogTitle = dialogTitle
                    });
                }
            }
            return PartialView(dialogViewModels);
        }

        [Authorize]
        public ActionResult Messages(MessagesRequest request) {
            ICollection<ViewModelMessage> messagesViewModels = new List<ViewModelMessage>();
            ICollection<Message> messages = _messageRepository
                .GetMessagesByDialogIdWithOffsetAndLimit(request.DialogId, request.Offset, request.Limit);
            if (messages == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            } else {
                foreach (var message in messages) {
                    var sender = _consumerRepository
                        .GetById(message.SenderId);
                    messagesViewModels.Add(new ViewModelMessage {
                        SenderName = sender.Name,
                        Content = message.Content,
                        Time = message.Time
                    });
                }
            }
            return PartialView(messagesViewModels);
        }

        [Authorize]
        public ActionResult Consumers(ConsumersRequest request) {
            ICollection<Consumer> consumers = _consumerRepository
                .GetMatchNameWithOffsetAndLimit(request.Name, request.Offset, request.Limit);
            if (consumers == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return PartialView(consumers);
        }

        [Authorize]
        public ActionResult ConsumerInfo(ConsumerInfoRequest request) {
            Consumer consumer = _consumerRepository
                .GetById(request.Consumer);
            if (consumer == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return PartialView(consumer);
        }
    }
}