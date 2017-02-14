using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace xtcx.Helper
{
    public class RequestHelper
    {
        private HttpRequestBase request;

        public RequestHelper(HttpRequestBase request)
        {
            this.request = request;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="targetFolder"></param>
        /// <param name="isRequire"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public string SaveImageToServer(string targetFolder, bool isRequire = true, string errorMessage = "请上传汇款凭证截图文件")
        {
            if (isRequire && (request.Files == null || request.Files.Count == 0 || string.IsNullOrWhiteSpace(request.Files[0].FileName)))
            {
                throw new Exception(errorMessage);
            }
            if (request.Files.Count <= 0 || !(request.Files[0].FileName != "") || request.Files[0].FileName == null)
            {
                return "";
            }
            string name = new FileInfo(this.request.Files[0].FileName).Name;
            if (!IsImageFile(name))
            {
                throw new Exception("请选择正确文件格式!");
            }
            string text = DateTime.Now.Ticks + name.Substring(name.LastIndexOf("."));
            string filename = Path.Combine(HttpContext.Current.Server.MapPath("~/" + targetFolder), text);
            request.Files[0].SaveAs(filename);
            return "/" + targetFolder + "/" + text;
        }

        public string SavePdfToServer(string targetFolder, bool isRequire = true, string errorMessage = "请上传文件")
        {
            if (isRequire && (request.Files == null || request.Files.Count == 0 || string.IsNullOrWhiteSpace(request.Files[0].FileName)))
            {
                throw new Exception(errorMessage);
            }
            if (request.Files.Count <= 0 || !(request.Files[0].FileName != "") || request.Files[0].FileName == null)
            {
                return "";
            }
            string name = new FileInfo(this.request.Files[0].FileName).Name;
            if (!IsPdfFile(name))
            {
                throw new Exception("请选择正确文件格式!");
            }
            string text = DateTime.Now.Ticks + name.Substring(name.LastIndexOf("."));
            string filename = Path.Combine(HttpContext.Current.Server.MapPath("~/" + targetFolder), text);
            request.Files[0].SaveAs(filename);
            return "/" + targetFolder + "/" + text;
        }
        /// <summary>
        /// 移除特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 判定是否图片文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsImageFile(string fileName)
        {
            string text = Path.GetExtension(fileName);
            string[] array = new string[]
            {
                "jpg",
                "bmp",
                "gif",
                "png",
                "jpeg"
            };
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            text = text.ToLower().Trim(new char[]
            {
                '.'
            });
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string b = array2[i];
                if (text == b)
                {
                    return true;
                }
            }
            return false;
        }


        private bool IsPdfFile(string fileName)
        {
            string text = Path.GetExtension(fileName);
            string[] array = new string[]
            {
                "pdf"
            };
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            text = text.ToLower().Trim(new char[]
            {
                '.'
            });
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string b = array2[i];
                if (text == b)
                {
                    return true;
                }
            }
            return false;
        }
    }
}