using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using xtcx.Helper;
using xtcx.Models;
using xtcx.ViewModel;

namespace xtcx.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        /// <summary>
        /// 我的个人资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new MemberViewModel() {
                Name=CurrentUser.Name,
                Email=CurrentUser.Email,
                Phone=CurrentUser.Phone,
                WorkingPlace=CurrentUser.WorkingPlace,
                Address=CurrentUser.Address
            };
            return View(model);
        }

        public ActionResult MemberMgr()
        {
            IQueryable<Member> memebers = db.Members.Where(m=>m.Email!="admin@admin.com");
            return View(memebers);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditMember(int? id)
        {
            if(id.HasValue && id.Value>0)
            {
                Member member = db.Members.SingleOrDefault(m => m.Id == id);
                if(member==null)
                {
                    return new HttpStatusCodeResult(400);
                }

                return View(member);
            }
            return View();
        }
        /// <summary>
        /// 修改用户提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult EditMember(Member model)
        {
            if (ModelState.IsValid)
            {
                if(CurrentUser.Type!= 会员角色.管理员.ToString())
                {
                    return new HttpStatusCodeResult(400);
                }
                Member member = db.Members.SingleOrDefault(m => m.Id == model.Id);
                if(member==null)
                {
                    return new HttpStatusCodeResult(400);
                }

                member.Name = model.Name;
                member.Email = model.Email;
                member.Phone = model.Phone;
                member.Type = model.Type;
                member.MemberType = model.MemberType;
                member.WorkingPlace = model.WorkingPlace;
                member.Password = model.Password;

                db.Entry<Member>(member).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MemberMgr");
            }
            SetMyAccountViewModel();
            return View(model);
        }
        /// <summary>
        /// 管理员删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelUser(int? id)
        {
            if(id.HasValue && id.Value>0)
            {
                if(CurrentUser.Type==会员角色.管理员.ToString())
                {
                    Member member = db.Members.SingleOrDefault(m => m.Id == id);
                    if(member==null)
                    {
                        return new HttpStatusCodeResult(400);
                    }
                    db.Members.Remove(member);
                    db.SaveChanges();
                    return RedirectToAction("MemberMgr");
                }
            }
            return new HttpStatusCodeResult(400);
        }

        /// <summary>
        /// 编辑个人资料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                CurrentUser.Name = model.Name;
                CurrentUser.Phone = model.Phone;
                CurrentUser.WorkingPlace = model.WorkingPlace;
                CurrentUser.Address = model.Address;

                db.SaveChanges();
                TempData["ActionMessage"] = "个人资料更新成功!";
                
            }
            SetMyAccountViewModel();
            return View(model);
        }

        /// <summary>
        /// 我的机构
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrganizations()
        {
            IEnumerable<Organization> model = db.Organizations.Where(o=>o.Member.Id==CurrentUser.Id);

            SetMyAccountViewModel();
            return View(model);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]       
        public ActionResult UpdatePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string str;
                
                str = "登录密码";
                if (CurrentUser.Password != model.OldPassword)
                {
                    ModelState.AddModelError("", "原密码错误");
                    TempData["ModelState"] = ModelState;
                    return RedirectToAction("Index");
                }
                CurrentUser.Password = model.Password;
               
                
                db.SaveChanges();
                TempData["ActionMessage"] = "密码修改成功!";
                return RedirectToAction("Index");
            }
            TempData["ModelState"] = ModelState;
            SetMyAccountViewModel();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        /// <summary>
        /// 登录post
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Member member = db.Members.SingleOrDefault((Member m) => m.Email.Equals(model.Email, StringComparison.InvariantCultureIgnoreCase) && m.Password.Equals(model.Password));
                if (member != null)
                {
                    if (member.State.Equals(会员状态.正常.ToString()))
                    {
                        FormsAuthentication.SetAuthCookie(member.Email, false);

                        return RedirectToLocal(returnUrl);
                    }
                    if (member.State.Equals(会员状态.待审核.ToString()))
                    {
                        ModelState.AddModelError("", "用户待审核!");
                    }
                    else
                    {
                        if (member.State.Equals(会员状态.锁定.ToString()))
                        {
                            ModelState.AddModelError("", "帐号锁定，请与管理员联系!");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "用户名/密码错误!");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="referral"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 注册post
        /// </summary>
        /// <param name="model"></param>
        /// <param name="referral"></param>
        /// <returns></returns>
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, int? referral)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }            
            if (this.db.Members.Count((Member m) => m.Email.Equals(model.Email, StringComparison.InvariantCultureIgnoreCase)) > 0)
            {
                ModelState.AddModelError("", "邮箱已被注册!");
                return View(model);
            }
           
            try
            {
                Member entity = new Member
                {
                    Email = model.Email,
                    Password = model.Password,
                    RegTime = DateTime.Now,
                    Name = model.Name,
                    Phone = model.Phone,
                    State = 会员状态.正常.ToString(),
                    Address = model.Address,
                    MemberType = model.MemberType,
                    Type = 会员角色.用户.ToString()
                };
                db.Members.Add(entity);
                db.SaveChanges();

                FormsAuthentication.SignOut();
                //直接登录
                FormsAuthentication.SetAuthCookie(model.Email, false);

                TempData["ActionMessage"] = "注册成功！";
                return RedirectToAction("Index", "Home");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult current in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError arg_46D_0 in current.ValidationErrors)
                    {
                    }
                }
            }
            return View(new RegisterViewModel());
        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult LogOff(string returnUrl)
        {
            FormsAuthentication.SignOut();
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 重定向到本地地址
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}