using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxaAPIs.SecOrder
{
    /// <summary>
    /// 查询小程序是否已完成交易结算管理确认
    /// </summary>
    public class IsTradeManagementConfirmationCompletedJsonResult : WxJsonResult
    {
        public bool completed { get; set; }
    }
}
