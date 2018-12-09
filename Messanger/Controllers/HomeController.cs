using Messanger.Logger;
using Messanger.Models;
using Messanger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Messanger.Controllers
{
    /**
     * Main Controller
     **/
    public class HomeController : Controller
    {
        private readonly ILogger _logger = LogFactory.Factory.GetLogger<HomeController>();
        private readonly ConsumerRepository _consumerRepository;

        private readonly ICollection<Dialog> dialogs = new List<Dialog>() {
                new Dialog() {
                    Id = 0,
                    Title = "Alexandr"
                },
                new Dialog() {
                    Id = 1,
                    Title = "Sam"
                },
                new Dialog() {
                    Id = 2,
                    Title = "Nikki"
                }
            };

        public HomeController() {
            _consumerRepository = DependencyResolver.Current.GetService<ConsumerRepository>();
        }

        // GET: Home
        public ActionResult Index() {
            _logger.Trace("Index");
            Consumer consumer = _consumerRepository.GetConsumerById("89b1b96b-472b-4338-a668-01ebd1748010");
            return View(consumer);
        }

        public ActionResult Dialogs(string uid) {
            _logger.Trace($"Dialogs: uid {uid}");
            return PartialView(dialogs);
        }
    }
}