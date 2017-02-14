using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtcx.Models
{
    public enum 会员状态
    {
        待审核,
        正常,
        锁定
    }

    public enum 机构状态
    {
        待审核,
        正常,
        未审核通过
    }

    public enum 资料状态
    {
        待审核,
        正常,
        审核未通过
    }

    public enum 会员角色
    {
        用户,
        管理员
    }
}