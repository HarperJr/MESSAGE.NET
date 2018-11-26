using Messanger.Logger;
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

        public HomeController() {
            _contactRepository = DependencyResolver.Current.GetService<ContactRepository>();
        }

        // GET: Home
        public ActionResult Index()
        {
            _logger.Trace("Index");
            return View();
        }

        public ActionResult Dialogs(string uid) {
            _logger.Trace($"Dialogs: uid {uid}");
            return PartialView("Dialogs");
        }
    }
}