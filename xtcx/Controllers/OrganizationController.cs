using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using xtcx.Helper;
using xtcx.Models;
using xtcx.ViewModel;

namespace xtcx.Controllers
{
    [Authorize]
    public class OrganizationController : BaseController
    {        
        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            string sState = 机构状态.正常.ToString();
            IEnumerable<Organization> model = db.Organizations.Where(m=>m.State== sState);            
            
            return View(model);            
        }

        #region 管理员


        public ActionResult OrganizationMgr()
        {
            var organizations =
                                from row in db.Organizations
                                select new OrgViewModel() {
                                    Id=row.Id,
                                    Name=row.Name,
                                    Url=row.Url,
                                    CultureDesc=row.CultureDesc,
                                    DomainDesc=row.DomainDesc,
                                    IndustryDesc=row.IndustryDesc,
                                    ScienceDesc=row.ScienceDesc,
                                    State=row.State,
                                    CaseCount=row.Cases.Count,
                                    PerformanceCount=row.Performances.Count,
                                    PolicyCount=row.Policies.Count
                                };
            return View(organizations);
        }

        /// <summary>
        /// 设置机构审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult setstatepass(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                Organization org = db.Organizations.SingleOrDefault(o => o.Id == id);
                if (CurrentUser.Type != 会员角色.管理员.ToString() || org == null)
                {
                    return new HttpStatusCodeResult(400);
                }

                org.State = 机构状态.正常.ToString();
                db.Entry<Organization>(org).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrganizationMgr");
            }
            return new HttpStatusCodeResult(400);
        }

        /// <summary>
        /// 设置机构审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult setstateunpass(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                Organization org = db.Organizations.SingleOrDefault(o => o.Id == id);
                if (CurrentUser.Type != 会员角色.管理员.ToString() || org == null)
                {
                    return new HttpStatusCodeResult(400);
                }

                org.State = 机构状态.未审核通过.ToString();
                db.Entry<Organization>(org).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrganizationMgr");
            }
            return new HttpStatusCodeResult(400);
        }

        /// <summary>
        /// 管理员删除机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Del(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                Organization org = db.Organizations.SingleOrDefault(m => m.Id == id);
                if (org == null || CurrentUser.Type != 会员角色.管理员.ToString())
                { 
                    return new HttpStatusCodeResult(400);
                }
                db.Organizations.Remove(org);
                db.SaveChanges();
                return RedirectToAction("OrganizationMgr");
            }
            return new HttpStatusCodeResult(400);
        }

        /// <summary>
        /// 未审核通过的文件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UnPassFileList()
        {
            List<FileViewModel> files = new List<FileViewModel>();
            var cases =
                        from row in db.Cases
                        where row.State!=资料状态.正常.ToString()
                        select new FileViewModel()
                        {
                            Id = row.Id,
                            Description=row.Description,
                            FilePath=row.FilePath,
                            State=row.State,
                            Name=row.Name,
                            OrgName=row.Organization.Name,
                            Type="典型案例"
                        };
            files.AddRange(cases);
            var policys =
                        from row in db.Policies
                        where row.State != 资料状态.正常.ToString()
                        select new FileViewModel()
                        {
                            Id = row.Id,
                            Description = row.Description,
                            FilePath = row.FilePath,
                            State = row.State,
                            Name = row.Name,
                            OrgName = row.Organization.Name,
                            Type = "政策研究"
                        };
            files.AddRange(policys);
            var performances =
                        from row in db.Performances
                        where row.State != 资料状态.正常.ToString()
                        select new FileViewModel()
                        {
                            Id = row.Id,
                            Description = row.Description,
                            FilePath = row.FilePath,
                            State = row.State,
                            Name = row.Name,
                            OrgName = row.Organization.Name,
                            Type ="绩效评估"
                        };
            files.AddRange(performances);
            
            return View(files);
        }

        /// <summary>
        /// 设置机构审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult setfilepass(int? id,string type)
        {
            if (id.HasValue && id.Value > 0)
            {
                if(type== "典型案例")
                {
                    Case file = db.Cases.SingleOrDefault(o => o.Id == id);
                    if (CurrentUser.Type != 会员角色.管理员.ToString() || file == null)
                    {
                        return new HttpStatusCodeResult(400);
                    }

                    file.State = 资料状态.正常.ToString();
                    db.Entry<Case>(file).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (type == "政策研究")
                {
                    Policy file = db.Policies.SingleOrDefault(o => o.Id == id);
                    if (CurrentUser.Type != 会员角色.管理员.ToString() || file == null)
                    {
                        return new HttpStatusCodeResult(400);
                    }

                    file.State = 资料状态.正常.ToString();
                    db.Entry<Policy>(file).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (type == "绩效评估")
                {
                    Performance file = db.Performances.SingleOrDefault(o => o.Id == id);
                    if (CurrentUser.Type != 会员角色.管理员.ToString() || file == null)
                    {
                        return new HttpStatusCodeResult(400);
                    }

                    file.State = 资料状态.正常.ToString();
                    db.Entry<Performance>(file).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }


                return RedirectToAction("UnPassFileList");
            }
            return new HttpStatusCodeResult(400);
        }
        public ActionResult setfileunpass(int? id, string type)
        {
            if (id.HasValue && id.Value > 0)
            {
                if (type == "典型案例")
                {
                    Case file = db.Cases.SingleOrDefault(o => o.Id == id);
                    if (CurrentUser.Type != 会员角色.管理员.ToString() || file == null)
                    {
                        return new HttpStatusCodeResult(400);
                    }

                    file.State = 资料状态.审核未通过.ToString();
                    db.Entry<Case>(file).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (type == "政策研究")
                {
                    Policy file = db.Policies.SingleOrDefault(o => o.Id == id);
                    if (CurrentUser.Type != 会员角色.管理员.ToString() || file == null)
                    {
                        return new HttpStatusCodeResult(400);
                    }

                    file.State = 资料状态.审核未通过.ToString();
                    db.Entry<Policy>(file).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                if (type == "绩效评估")
                {
                    Performance file = db.Performances.SingleOrDefault(o => o.Id == id);
                    if (CurrentUser.Type != 会员角色.管理员.ToString() || file == null)
                    {
                        return new HttpStatusCodeResult(400);
                    }

                    file.State = 资料状态.审核未通过.ToString();
                    db.Entry<Performance>(file).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }


                return RedirectToAction("UnPassFileList");
            }
            return new HttpStatusCodeResult(400);
        }

        #endregion

        /// <summary>
        /// 机构详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Detail(int? id)
        {
            if(id.HasValue && id.Value>0)
            {
                Organization organization = db.Organizations.SingleOrDefault(o => o.Id == id);
                if(organization==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrganizationViewModel model = new OrganizationViewModel
                {
                    Name=organization.Name,
                    Url=organization.Url,
                    Description=organization.Description,
                    ScienceDesc = organization.ScienceDesc,
                    IndustryDesc = organization.IndustryDesc,
                    DomainDesc = organization.DomainDesc,
                    CultureDesc = organization.CultureDesc
                };
                ViewBag.OrganizationId = id;
                return View(model);
            }
            return View();
        }

        /// <summary>
        /// 案例列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CaseList(int? id)
        {
            ViewBag.OrganizationId = id;
            string sState = 机构状态.正常.ToString();
            if (id.HasValue && id.Value > 0)
            {
                IQueryable<Case> cases = db.Cases.Where(c => c.Organization.Id == id && c.State== sState);                
                return View(cases);
            }
            return View();
        }
        /// <summary>
        /// 政策列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult PolicyList(int? id)
        {
            ViewBag.OrganizationId = id;
            string sState = 机构状态.正常.ToString();
            if (id.HasValue && id.Value > 0)
            {
                IQueryable<Policy> policys = db.Policies.Where(c => c.Organization.Id == id && c.State==sState);
                return View(policys);
            }
            return View();
        }
        /// <summary>
        /// 绩效列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult PerformanceList(int? id)
        {
            ViewBag.OrganizationId = id;
            string sState = 机构状态.正常.ToString();
            if (id.HasValue && id.Value > 0)
            {
                IQueryable<Performance> cases = db.Performances.Where(c => c.Organization.Id == id && c.State==sState);
                return View(cases);
            }
            return View();
        }



        /// <summary>
        /// 添加机构
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(int? id)
        {   
            if(id.HasValue && id.Value>0)
            {
                ViewBag.OrganizationId = id;
                Organization organization = db.Organizations.SingleOrDefault(org => org.Id == id);
                if(organization==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrganizationViewModel model = new OrganizationViewModel
                {
                    Id=organization.Id,
                    Name = organization.Name,
                    Url = organization.Url,
                    Description = organization.Description,
                    ScienceDesc = organization.ScienceDesc,
                    IndustryDesc = organization.IndustryDesc,
                    DomainDesc = organization.DomainDesc,
                    CultureDesc = organization.CultureDesc
                };
                return View(model);
            }         
            return View(new OrganizationViewModel { Id=0});
        }
        /// <summary>
        /// 添加机构 提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(OrganizationViewModel model)
        {
            bool flag = false;

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "验证失败，请重试！");
                flag = true;
            }
            if (model.Id==0 && db.Organizations.Count(o => o.Name.Equals(model.Name, StringComparison.InvariantCultureIgnoreCase)) > 0)
            {
                ModelState.AddModelError("", "机构已经添加，请确认名称是否重复!");
                flag = true;
            }
            if (!flag)
            {                       
                if(model.Id>0)
                {
                    Organization organization = db.Organizations.SingleOrDefault(o => o.Id == model.Id);
                    organization.Name = model.Name;
                    organization.Url = model.Url;
                    organization.Description = model.Description;
                    organization.ScienceDesc = model.ScienceDesc;
                    organization.IndustryDesc = model.IndustryDesc;
                    organization.DomainDesc = model.DomainDesc;
                    organization.CultureDesc = model.CultureDesc;
                    organization.State = 机构状态.待审核.ToString();

                    db.Entry<Organization>(organization).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();                  
                }
                else
                {
                    Organization organization = new Organization {
                        Name = model.Name,
                        Url = model.Url,
                        Description = model.Description,
                        ScienceDesc = model.ScienceDesc,
                        IndustryDesc = model.IndustryDesc,
                        DomainDesc = model.DomainDesc,
                        CultureDesc = model.CultureDesc,
                        CreateTime = DateTime.Now,
                        Member = CurrentUser,
                        Views = 0,
                        State = 机构状态.待审核.ToString()
                    };

                    db.Organizations.Add(organization);
                    db.SaveChanges();
                }
                TempData["ActionMessage"] = "操作成功！";
                return RedirectToAction("MyOrganizations", "Account");                
            }

            SetMyAccountViewModel();
            return View();
        }

        #region 案例
        /// <summary>
        /// 添加案例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddCase(int? id)
        {
            ViewBag.OrganizationId = id;
            if (id.HasValue && id.Value > 0)
            {
                ViewBag.OrganizationId = id;
                IQueryable<Case> cases = db.Cases.Where(c => c.Organization.Id == id);
                return View(cases);
            }
            return View();
        }
        /// <summary>
        /// 添加案例提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddCase(CaseViewModel model)
        {
            bool flag = false;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "验证失败，请重试");
                flag = true;
            }
            RequestHelper requestHelper = new RequestHelper(Request);
            string text = "";
            try
            {
                text=requestHelper.SavePdfToServer("Upload/Topup", true, "请上传文件");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                flag = true;
            }
            if (!flag)
            {
                Organization organization = db.Organizations.SingleOrDefault(or => or.Id == model.OrganizationId);
                Case newcase = new Case() {
                    Name = model.Name,
                    Description = model.Description,
                    FilePath = text,
                    State = 资料状态.待审核.ToString(),
                    CreateTime=DateTime.Now,
                    Organization= organization
                };
                db.Cases.Add(newcase);
                db.SaveChanges();                
            }

            return Redirect("/organization/addcase/" + model.OrganizationId);
        }

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelCase(int? id)
        {
            int orgid = 0;
            if(id.HasValue && id.Value>0)
            {
                Case cas = db.Cases.SingleOrDefault(ca => ca.Id == id);
                orgid = cas.Organization.Id;
                if(cas==null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.Cases.Remove(cas);
                db.SaveChanges();
            }
            return Redirect("/organization/addcase/" + orgid);
        }

        #endregion

        #region 政策
        /// <summary>
        /// 添加案例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddPolicy(int? id)
        {
            ViewBag.OrganizationId = id;
            if (id.HasValue && id.Value > 0)
            {
                ViewBag.OrganizationId = id;
                IQueryable<Policy> policies = db.Policies.Where(c => c.Organization.Id == id);
                return View(policies);
            }
            return View();
        }
        /// <summary>
        /// 添加案例提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddPolicy(PolicyViewModel model)
        {
            bool flag = false;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "验证失败，请重试");
                flag = true;
            }
            RequestHelper requestHelper = new RequestHelper(Request);
            string text = "";
            try
            {
                text = requestHelper.SavePdfToServer("Upload/Topup", true, "请上传文件");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                flag = true;
            }
            if (!flag)
            {
                Organization organization = db.Organizations.SingleOrDefault(or => or.Id == model.OrganizationId);
                Policy policy = new Policy()
                {
                    Name = model.Name,
                    Description = model.Description,
                    FilePath = text,
                    State = 资料状态.待审核.ToString(),
                    CreateTime = DateTime.Now,
                    Organization = organization
                };
                db.Policies.Add(policy);
                db.SaveChanges();
            }

            return Redirect("/organization/addpolicy/" + model.OrganizationId);
        }

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelPolicy(int? id)
        {
            int orgid = 0;
            if (id.HasValue && id.Value > 0)
            {
                Policy policy = db.Policies.SingleOrDefault(ca => ca.Id == id);
                orgid = policy.Organization.Id;
                if (policy == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.Policies.Remove(policy);
                db.SaveChanges();
            }
            return Redirect("/organization/addcase/" + orgid);
        }

        #endregion

        #region 绩效
        /// <summary>
        /// 添加案例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddPerformance(int? id)
        {
            ViewBag.OrganizationId = id;
            if (id.HasValue && id.Value > 0)
            {
                ViewBag.OrganizationId = id;
                IQueryable<Performance> performances = db.Performances.Where(c => c.Organization.Id == id);
                return View(performances);
            }
            return View();
        }
        /// <summary>
        /// 添加案例提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddPerformance(PerformanceViewModel model)
        {
            bool flag = false;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "验证失败，请重试");
                flag = true;
            }
            RequestHelper requestHelper = new RequestHelper(Request);
            string text = "";
            try
            {
                text = requestHelper.SavePdfToServer("Upload/Topup", true, "请上传文件");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                flag = true;
            }
            if (!flag)
            {
                Organization organization = db.Organizations.SingleOrDefault(or => or.Id == model.OrganizationId);
                Performance performance = new Performance()
                {
                    Name = model.Name,
                    Description = model.Description,
                    FilePath = text,
                    State = 资料状态.待审核.ToString(),
                    CreateTime = DateTime.Now,
                    Organization = organization
                };
                db.Performances.Add(performance);
                db.SaveChanges();
            }

            return Redirect("/organization/addperformance/" + model.OrganizationId);
        }

        /// <summary>
        /// 删除案例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelPerformance(int? id)
        {
            int orgid = 0;
            if (id.HasValue && id.Value > 0)
            {
                Performance performance = db.Performances.SingleOrDefault(ca => ca.Id == id);
                orgid = performance.Organization.Id;
                if (performance == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                db.Performances.Remove(performance);
                db.SaveChanges();
            }
            return Redirect("/organization/addperformance/" + orgid);
        }

        #endregion
    }
}