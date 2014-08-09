using System;
using System.Text;
using System.Web;
namespace Senparc.Weixin.MP.WeixinPayLib
{
    /// <summary>
    /// TenpayUtil 的摘要说明。
    /// 配置文件
    /// </summary>
    public class WeixinPayUtil
    {
        public static string Tenpay { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public static string PartnerId { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public static string Key { get; set; }
        /// <summary>
        /// appid
        /// </summary>
        public static string AppId { get; set; }
        /// <summary>
        /// paysignkey(非appkey) 
        /// </summary>
        public static string AppKey { get; set; }
        /// <summary>
        /// 支付完成后的回调处理页面,*替换成notify_url.asp所在路径
        /// </summary>
        public static string TenpayNotify { get; set; } // = "http://localhost/payNotifyUrl.aspx";

        /// <summary>
        /// 注册全局支付信息
        /// </summary>
        /// <param name="tenpay">商户号</param>
        /// <param name="partnerId"></param>
        /// <param name="key">密钥</param>
        /// <param name="appId">appid</param>
        /// <param name="appKey">paysignkey(非appkey) </param>
        /// <param name="tenpayNotify">支付完成后的回调处理页面,*替换成notify_url.asp所在路径</param>
        public static void Register(string tenpay, string partnerId, string key, string appId, string appKey, string tenpayNotify)
        {
            Tenpay = tenpay;
            PartnerId = partnerId;
            Key = key;
            AppId = appId;
            AppKey = appKey;
            TenpayNotify = tenpayNotify;
        }


        public WeixinPayUtil()
        {

        }

        /// <summary>
        /// 随机生成Noncestr
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            Random random = new Random();
            return MD5Util.GetMD5(random.Next(1000).ToString(), "GBK");
        }


        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = System.Web.HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;
            }
        }

        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = System.Web.HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                }


                return res;

            }
        }


        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }
        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

    }
}