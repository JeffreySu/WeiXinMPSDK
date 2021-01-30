using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    public static class BasePayApis
    {
        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        private static string ReurnPayApiUrl(string urlFormat)
        {
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }

        public static string JsAPi()
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}pay/unifiedorder");
            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            //throw new Exception(data.HtmlEncode());
            MemoryStream ms = new MemoryStream();
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置

            var resultXml = RequestUtility.HttpPost(CommonDI.CommonSP, urlFormat, null, ms, timeOut: timeOut);
            return new UnifiedorderResult(resultXml);
        }
    }
}
