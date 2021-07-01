using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    public static class TenPayDateTimeHelper
    {
        /// <summary>
        /// 输出微信 V3 指定的符合 ISO 8601 要求格式的时间字符串
        /// <para>文档：https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay2_0.shtml</para>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToTenPayDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");//).ToString("yyyy-MM-ddTHH:mm:fff");
        }
    }
}
