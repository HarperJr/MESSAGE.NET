using Messanger.Logger;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Messanger
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger _logger = LogFactory.Factory.GetLogger<MvcApplication>();

        protected void Application_Start()
        {
            _logger.Trace("ApplicationStart");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
