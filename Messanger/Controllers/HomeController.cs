using Database;
using Messanger.Data.Models;
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

        public HomeController() {
            _localDbContext = new LocalDbContext();
            _consumerRepository = new ConsumerRepository(_localDbContext);
            _dialogRepository = new DialogRepository(_localDbContext);
            _messageRepository = new MessageRepository(_localDbContext);
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index(IndexRequest request) {
            Consumer consumer = _consumerRepository
                .GetById(request.Consumer);
            if (consumer == null) {
                return RedirectToAction("SignIn", "Auth");
            }
            return View(consumer);
        }

        public ActionResult Dialogs(DialogsRequest request) {
            ICollection<Dialog> dialogs = _dialogRepository
                .GetDialogsByConsumerIdWithOffsetAndLimit(request.Uid, request.Offset, request.Limit);
            if (dialogs == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return PartialView(dialogs);
        }

        public ActionResult Messages(MessagesRequest request) {
            ICollection<Message> messages = _messageRepository
                .GetMessagesByDialogIdWithOffsetAndLimit(request.DialogId, request.Offset, request.Limit);
            return PartialView(messages);
        }

        public ActionResult Consumers(ConsumersRequest request) {
            ICollection<Consumer> consumers = _consumerRepository
                .GetMatchNameWithOffsetAndLimit(request.Name, request.Offset, request.Limit);
            if (consumers == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return PartialView(consumers);
        }
    }
}