using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtcx.ViewModel
{
    public class RegisterViewModel
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱"), EmailAddress, Required(ErrorMessage = "请输入{0}")]
        public string Email{get;set;}
        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password), Display(Name = "密码"), Required(ErrorMessage = "请输入{0}"), StringLength(30, ErrorMessage = "{0}长度不足{2}.", MinimumLength = 6)]
        public string Password{get;set;}
        /// <summary>
        /// 确认密码
        /// </summary>
        [Compare("Password", ErrorMessage = "两次输入密码不相符."), DataType(DataType.Password), Display(Name = "确认密码")]
        public string ConfirmPassword{get;set;}

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name ="联系地址"), Required(ErrorMessage ="请输入{0}"),StringLength(30,ErrorMessage ="{0}长度不得大于30")]
        public string Address { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        [Display(Name = "名字"), Required(ErrorMessage = "请输入{0}"), StringLength(11, ErrorMessage = "{0}长度不得大于11")]
        public string Name { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机"), Required(ErrorMessage = "请输入{0}"), StringLength(11, ErrorMessage = "{0}长度不得大于11")]
        public string Phone { get; set; }

        /// <summary>
        /// 选择身份
        /// </summary>
        [Display(Name = "身份"), Required(ErrorMessage = "请选择{0}"), StringLength(30)]
        public string MemberType { get; set; }
        
    }
}