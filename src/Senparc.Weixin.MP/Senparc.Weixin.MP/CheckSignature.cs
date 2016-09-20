/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    文件名：CheckSignature.cs
    文件功能描述：检测签名
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20151005
    修改描述：v13.3.1 提供带PostModel参数的方法
----------------------------------------------------------------*/

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.Entities.Request;

//using System.Web.Security;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// 签名验证类
    /// </summary>
    public class CheckSignature
    {
        /// <summary>
        /// 在网站没有提供Token（或传入为null）的情况下的默认Token，建议在网站中进行配置。
        /// </summary>
        public const string Token = "weixin";

        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="postModel">需要提供：Timestamp、Nonce、Token</param>
        /// <returns></returns>
        public static bool Check(string signature, PostModel postModel)
        {
            return Check(postModel.Timestamp, postModel.Nonce, postModel.Token);
        }

        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Check(string signature, string timestamp, string nonce, string token = null)
        {
            return signature == GetSignature(timestamp, nonce, token);
        }

        /// <summary>
        /// 返回正确的签名
        /// </summary>
        /// <param name="postModel">需要提供：Timestamp、Nonce、Token</param>
        /// <returns></returns>
        public static string GetSignature(PostModel postModel)
        {
            return GetSignature(postModel.Timestamp, postModel.Nonce, postModel.Token);
        }

        /// <summary>
        /// 返回正确的签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetSignature(string timestamp, string nonce, string token = null)
        {
            token = token ?? Token;
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            //var enText = FormsAuthentication.HashPasswordForStoringInConfigFile(arrString, "SHA1");//使用System.Web.Security程序集
            var sha1 = SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            return enText.ToString();
        }
    }
}
