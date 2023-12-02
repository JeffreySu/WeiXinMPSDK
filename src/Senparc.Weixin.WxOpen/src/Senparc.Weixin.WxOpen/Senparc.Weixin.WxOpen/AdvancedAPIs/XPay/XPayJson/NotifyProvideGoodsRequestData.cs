using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 通知已经发货完成（只能通知现金单）,正常通过xpay_goods_deliver_notify消息推送返回成功就不需要调用这个api接口。这个接口用于异常情况推送不成功时手动将单改成已发货状态
    /// </summary>
    public class NotifyProvideGoodsRequestData
    {
        /// <summary>
        /// 下单时传的单号
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 微信内部单号(与order_id二选一)
        /// </summary>
        public string wx_order_id { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
