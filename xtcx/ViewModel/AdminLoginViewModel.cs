using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtcx.ViewModel
{
    public class AdminLoginViewModel
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "账户"), Required(ErrorMessage = "请输入{0}")]
        public string Name{get;set;}
        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password), Display(Name = "密码"), Required(ErrorMessage = "请输入{0}")]
        public string Password{get;set;}
        /// <summary>
        /// 记住我
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe{get;set;}
    }
}