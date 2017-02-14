using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtcx.ViewModel
{
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// 新密码
        /// </summary>
        [DataType(DataType.Password), Display(Name = "新密码"), Required(ErrorMessage = "请输入{0}"), StringLength(30, ErrorMessage = "{0}长度不足{2}.", MinimumLength = 6)]
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Compare("Password", ErrorMessage = "两次输入密码不相符."), DataType(DataType.Password), Display(Name = "确认密码")]
        public string ConfirmPassword
        {
            get;
            set;
        }
        /// <summary>
        /// 旧密码
        /// </summary>
        [DataType(DataType.Password), Display(Name = "旧密码"), Required(ErrorMessage = "请输入{0}")]
        public string OldPassword
        {
            get;
            set;
        }
    }
}