using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using xtcx.Models;

namespace xtcx.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected xtcxEntities db = new xtcxEntities();

        


        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}