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

        public HomeController() {
            _localDbContext = new LocalDbContext();
            _consumerRepository = new ConsumerRepository(_localDbContext);
        }

        // GET: Home
        public ActionResult Index() {
            Consumer consumer = _consumerRepository
                .GetById("4b47286f-a4f6-41fd-aec3-cc547d135052");
            return View(consumer);
        }

        public ActionResult Dialogs(DialogsRequest request) {
            ICollection<Dialog> dialogs = _dialogRepository
                .GetDialogsByConsumerIdWithOffsetAndLimit(request.Uid, request.Offset, request.Limit);
            return PartialView(dialogs);
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