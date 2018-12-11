using Messanger.Data.Models;
using Messanger.Http.Models;
using Messanger.Logger;
using Messanger.Repositories;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Messanger.Controllers
{
    /**
     * Main Controller
     **/
    public class HomeController : Controller
    {
        private readonly ILogger _logger = LogFactory.Factory.GetLogger<HomeController>();
        private readonly IConsumerRepository _consumerRepository;

        private readonly Consumer consumer;
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
                },
                 new Dialog() {
                    Id = 3,
                    Title = "Max"
                },
                  new Dialog() {
                    Id = 4,
                    Title = "Vladimir"
                }
            };

        public HomeController() {
            _consumerRepository = DependencyResolver.Current.GetService<ConsumerRepository>();

            consumer = new Consumer() {
                Id = "4b47286f-a4f6-41fd-aec3-cc547d135052",
                Name = "HarperJr",
                AvatarId = "3d388098-c5c1-48d7-b607-a9dc5d3bda9f"
            };
        }

        // GET: Home
        public ActionResult Index()
        {
            _logger.Trace("Index");
            return View(consumer);
        }

        public ActionResult Dialogs(DialogsRequest request) {
            _logger.Trace($"Dialogs: uid {request.Uid}");
            return PartialView(dialogs);
        }
    }
}