using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Http.Models;
using Messanger.Repositories;
using System;
using System.Web.Mvc;

namespace Messanger.Controllers
{
    public class AuthController : Controller {

        private readonly AuthDbContext _authDbContext;
        private readonly LocalDbContext _localDbContext;
        private readonly IConsumerRepository _consumerRepository;

        public AuthController() {
            _authDbContext = new AuthDbContext();
            _localDbContext = new LocalDbContext();
            _consumerRepository = new ConsumerRepository(_localDbContext);
        }

        public ActionResult SignIn() {
            return View();
        }

        // GET: Auth
        public ActionResult SignUp() {
            return View();
        }

        public ActionResult IdentifyUser(IdentificationRequest request) {
            //Here we identify new identity user
            //After we get new guid and insert him in Consumers
            Guid guid = new Guid();
            _consumerRepository.Insert(new Consumer {
                Id = guid.ToString(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
            });
            return RedirectToAction("SignIn", "Auth");
        }

        public ActionResult Authorize(AuthorizationRequest request) {
            Consumer consumer = _consumerRepository
                .GetById("");
            if (consumer == null) {
                return RedirectToAction("SignIn", "Auth");
            } else {
                return RedirectToAction("Index", "Home", consumer);
            }
        }
    }
}