using Messanger.Data.Models;
using Messanger.Database;
using Messanger.DatabaseAuth.Managers;
using Messanger.Http.Models;
using Messanger.Repositories;
using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Messanger.DatabaseAuth.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace Messanger.Controllers
{
    public class AuthController : Controller {

        private AuthUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<AuthUserManager>();
        private IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;
        private readonly IConsumerRepository _consumerRepository;

        public AuthController() {
            _consumerRepository = new ConsumerRepository(new LocalDbContext());
        }

        public ActionResult SignIn() {
            return View();
        }

        // GET: Auth
        public ActionResult SignUp() {
            return View();
        }

        public async Task<ActionResult> Identify(IdentificationRequest request) {
            if (ModelState.IsValid) {
                var authUser = new AuthUser {
                    UserName = request.Name,
                    PhoneNumber = request.PhoneNumber,
                };
                IdentityResult result = await UserManager.CreateAsync(authUser, request.Password);
                if (result.Succeeded) {
                    var identifiedUser = await UserManager.FindAsync(request.Name, request.Password);
                    _consumerRepository.Insert(new Consumer {
                        Id = identifiedUser.Id,
                        Name = request.Name,
                        PhoneNumber = request.PhoneNumber,
                    });
                } else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("identificationError", error);
                    }
                }
            }
            return RedirectToAction("SignIn", "Auth", request);
        }

        public async Task<ActionResult> Authorize(AuthorizationRequest request) {
            var authUser = await UserManager.FindAsync(request.Name, request.Password);
            if (authUser == null) {
                ModelState.AddModelError("authorizationError", "Invalid name or password");
            } else {
                ClaimsIdentity claimsIdentity = await UserManager.CreateIdentityAsync(authUser,
                    DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties {
                    IsPersistent = false
                }, claimsIdentity);
                var consumer = _consumerRepository.GetById(authUser.Id);
                return RedirectToAction("Index", "Home", new IndexRequest { Consumer = consumer.Id });
            }
            return RedirectToAction("SignIn", "Auth", request);
        }
    }
}