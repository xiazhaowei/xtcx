using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using xtcx.Models;
using xtcx.ViewModel;

namespace xtcx.Controllers
{
    public class BaseController : Controller
    {
        protected xtcxEntities db = new xtcxEntities();

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        protected Member CurrentUser
        {
            get
            {
                if (HttpContext.User.Identity.Name != null && !string.IsNullOrEmpty(HttpContext.User.Identity.Name))
                {
                    Member member = db.Members.SingleOrDefault(m=> m.Email.Equals(HttpContext.User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (member == null)
                    {
                        FormsAuthentication.SignOut();
                        Response.Redirect("~/Account/Login");
                    }
                    return member;
                }
                return null;
            }
        }


        /// <summary>
        /// 设置当前用户到 视图tempData中 TempData["MyAccount"]
        /// </summary>
        protected void SetMyAccountViewModel()
        {
            Member currentUser = CurrentUser;
            //TempData["MyAccount"] = GetMyAccountViewModel(currentUser);
            ViewBag.MyAccount = GetMyAccountViewModel(currentUser);
        }

        /// <summary>
        /// 获取 登录用户View Model
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected MyAccountViewModel GetMyAccountViewModel(Member user)
        {
            if (user != null)
                return new MyAccountViewModel
                {
                    Name = user.Name,
                    Type = user.Type,
                    Email = user.Email
                };
            else
                return null;
        }

        /// <summary>
        /// 重写 Action执行方法 加载当前用户到tempdata中
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetMyAccountViewModel();
        }
    }
}