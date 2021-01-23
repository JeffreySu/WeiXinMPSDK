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
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToTenPayDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("szzz");//).ToString("yyyy-MM-ddTHH:mm:fff");
        }
    }
}
