using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.QA.Helpers
{
    public class JSSDKHelper
    {
        public JSSDKHelper()
        {
            Parameters = new Hashtable();
        }

        protected Hashtable Parameters;

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue"></param>
        private void SetParameter(string parameter, string parameterValue)
        {
            if (parameter != null && parameter != "")
            {
                if (Parameters.Contains(parameter))
                {
                    Parameters.Remove(parameter);
                }

                Parameters.Add(parameter, parameterValue);
            }
        }

        private void ClearParameter()
        {
            Parameters.Clear();
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            Random random = new Random();
            return MD5UtilHelper.GetMD5(random.Next(1000).ToString(), "GBK");
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <returns></returns>
        private string CreateSha1()
        {
            StringBuilder sb = new StringBuilder();
            ArrayList akeys = new ArrayList(Parameters.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];

                if (sb.Length == 0)
                {
                    sb.Append(k + "=" + v);
                }
                else
                {
                    sb.Append("&" + k + "=" + v);
                }
            }
            return SHA1UtilHelper.GetSha1(sb.ToString()).ToString().ToLower();
        }

        /// <summary>
        /// 获取JS-SDK权限验证的签名Signature
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetSignature(string ticket, string noncestr, string timestamp, string url)
        {
            //清空Parameters
            ClearParameter();

            SetParameter("jsapi_ticket", ticket);
            SetParameter("noncestr", noncestr);
            SetParameter("timestamp", timestamp);
            SetParameter("url", url);

            return CreateSha1();
        }
    }
}
