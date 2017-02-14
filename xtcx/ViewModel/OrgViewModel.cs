using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtcx.ViewModel
{
    public class OrgViewModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        [Display(Name = "机构名称"), Required(ErrorMessage = "请输入{0}")]
        public string Name{get;set;}
        /// <summary>
        /// 机构名称
        /// </summary>
        [Display(Name = "机构网址")]
        public string Url{get;set;}

        /// <summary>
        /// 机构简介
        /// </summary>
        [Display(Name = "机构简介"), Required(ErrorMessage = "请输入{0}")]
        public string Description { get; set; }
        /// <summary>
        /// 面向科学前沿
        /// </summary>
        [Display(Name = "面向科学前沿")]
        public string ScienceDesc { get; set; }
        /// <summary>
        /// 面向行业产业
        /// </summary>
        [Display(Name = "面向行业产业")]
        public string IndustryDesc { get; set; }

        /// <summary>
        /// 面向区域发展
        /// </summary>
        [Display(Name = "面向区域发展")]
        public string DomainDesc { get; set; }

        /// <summary>
        /// 面向行业产业
        /// </summary>
        [Display(Name = "面向文化传承")]
        public string CultureDesc { get; set; }

        [Display(Name = "案例数量")]
        public int CaseCount { get; set; }

        [Display(Name = "绩效数量")]
        public int PerformanceCount { get; set; }

        [Display(Name = "政策数量")]
        public int PolicyCount { get; set; }

        [Display(Name = "审核状态")]
        public string State { get; set; }
    }
}