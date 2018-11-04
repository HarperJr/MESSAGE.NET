using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Messanger
{
    /// <summary>
    /// Сводное описание для AuthService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class AuthService : System.Web.Services.WebService
    {

        [WebMethod]
        public String getHash()
        {
            return "3e25960a79dbc69b674cd4ec67a72c62";
        }
    }
}
