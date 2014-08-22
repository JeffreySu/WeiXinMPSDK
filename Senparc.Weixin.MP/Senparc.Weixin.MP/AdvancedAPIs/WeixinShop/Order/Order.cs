using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
    /// </summary>
    public static class WeixinShopOrder
    {
        /// <summary>
        /// 根据订单ID获取订单详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        public static GetByIdOrderResult GetByIdOrder(string accessToken, string orderId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/getbyid?access_token={0}";

            return CommonJsonSend.Send<GetByIdOrderResult>(accessToken, urlFormat, orderId);
        }

        /// <summary>
        /// 根据订单状态/创建时间获取订单详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="status">订单状态(不带该字段-全部状态, 2-待发货, 3-已发货, 5-已完成, 8-维权中, )</param>
        /// <param name="beginTime">订单创建时间起始时间(不带该字段则不按照时间做筛选)</param>
        /// <param name="endTime">订单创建时间终止时间(不带该字段则不按照时间做筛选)</param>
        /// <returns></returns>
        public static GetByFilterResult GetByFilterOrder(string accessToken, int status, string beginTime, string endTime)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/getbyfilter?access_token={0}";

            var data = new
                {
                    status = status,
                    begintime = beginTime,
                    endtime = endTime
                };

            return CommonJsonSend.Send<GetByFilterResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 设置订单发货信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="orderId">订单ID</param>
        /// <param name="deliveryCompany">物流公司ID</param>
        /// <param name="deliveryTrackNo">运单ID</param>
        /// <returns></returns>
        /// 物流公司    Id
        /// 邮政EMS	    Fsearch_code
        /// 申通快递	002shentong
        /// 中通速递	066zhongtong
        /// 圆通速递	056yuantong
        /// 天天快递	042tiantian
        /// 顺丰速运	003shunfeng
        /// 韵达快运	059Yunda
        /// 宅急送	    064zhaijisong
        /// 汇通快运	020huitong
        /// 易迅快递	zj001yixun
        public static WxJsonResult SetdeliveryOrder(string accessToken, string orderId, string deliveryCompany, string deliveryTrackNo)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/setdelivery?access_token={0}";

            var data = new
            {
                order_id = orderId,
                delivery_company = deliveryCompany,
                delivery_track_no = deliveryTrackNo
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public static WxJsonResult CloseOrder(string accessToken, string orderId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/close?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, orderId);
        }
    }
}
