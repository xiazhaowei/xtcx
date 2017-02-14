using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtcx.ViewModel
{
    public class MemberViewModel
    {
        /// <summary>
        /// 机构名称
        /// </summary>
        [Display(Name = "您的姓名"), Required(ErrorMessage = "请输入{0}")]
        public string Name{get;set;}

        [Display(Name = "邮箱"), Required(ErrorMessage = "请输入{0}")]
        public string Email { get; set; }

        [Display(Name = "联系方式"), Required(ErrorMessage = "请输入{0}")]
        public string Phone { get; set; }

        [Display(Name = "详细地址"), Required(ErrorMessage = "请输入{0}")]
        public string Address { get; set; }

        [Display(Name = "工作单位"), Required(ErrorMessage = "请输入{0}")]
        public string WorkingPlace { get; set; }
    }
}