using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xtcx.ViewModel;

namespace xtcx.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel()
            {
                OrgCount=db.Organizations.Count()

            };                  
            return View(model);
        }

    }
}