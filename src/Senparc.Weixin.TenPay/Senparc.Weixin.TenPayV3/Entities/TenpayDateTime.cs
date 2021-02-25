using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Entities
{
    /// <summary>
    /// 微信支付请求接口中的提起格式，可以通过 .ToString() 方法生成标准微信支付格式的字符串
    /// </summary>
    public class TenpayDateTime
    {
        public TenpayDateTime(DateTime dateTime)
        {
            DateTime = dateTime;
        }
        public DateTime DateTime { get; }

        public override string ToString()
        {
            return DateTime.ToTenPayDateTime();
        }
    }
}
