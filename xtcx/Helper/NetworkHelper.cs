using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace xtcx.Helper
{
    public static class NetworkHelper
    {
        /// <summary>
        /// 获取客户端Ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIPv4Address()
        {
            string text = string.Empty;
            IPAddress[] hostAddresses = Dns.GetHostAddresses(NetworkHelper.GetClientIP());
            for (int i = 0; i < hostAddresses.Length; i++)
            {
                IPAddress iPAddress = hostAddresses[i];
                if (iPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    text = iPAddress.ToString();
                    break;
                }
            }
            if (text != string.Empty)
            {
                return text;
            }
            IPAddress[] addressList = Dns.GetHostEntry(NetworkHelper.GetClientIP()).AddressList;
            for (int j = 0; j < addressList.Length; j++)
            {
                IPAddress iPAddress2 = addressList[j];
                if (iPAddress2.AddressFamily.ToString() == "InterNetwork")
                {
                    text = iPAddress2.ToString();
                    break;
                }
            }
            return text;
        }
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null)
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        /// <summary>
        /// 获取是否腾讯的请求
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsTencent()
        {
            string clientIPv4Address = NetworkHelper.GetClientIPv4Address();
            return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].IndexOf("tencent") >= 0 || "180.153.202.198".Equals(clientIPv4Address) || "101.227.131.139".Equals(clientIPv4Address);
        }
    }
}