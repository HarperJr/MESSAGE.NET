using Messanger.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Messanger.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult SignIn() {
            return View();
        }

      
    }
}