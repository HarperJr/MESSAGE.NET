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
        private readonly ContactRepository _contactRepository;

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
                }
            };

        public HomeController() {
            _contactRepository = DependencyResolver.Current.GetService<ContactRepository>();

            consumer = new Consumer() {
                Id = "4b47286f-a4f6-41fd-aec3-cc547d135052",
                Name = "HarperJr",
                MultimediaId = "3d388098-c5c1-48d7-b607-a9dc5d3bda9f"
            };
        }

        // GET: Home
        public ActionResult Index()
        {
            _logger.Trace("Index");
            return View(consumer);
        }

        public ActionResult Dialogs(string uid) {
            _logger.Trace($"Dialogs: uid {uid}");
            return PartialView(dialogs);
        }
    }
}