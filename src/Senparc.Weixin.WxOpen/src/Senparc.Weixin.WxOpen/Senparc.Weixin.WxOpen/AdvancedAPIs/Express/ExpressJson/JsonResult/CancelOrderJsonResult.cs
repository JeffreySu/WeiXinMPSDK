using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class CancelOrderJsonResult : ExpressJsonResult
    {
        /// <summary>
        /// 扣除的违约金(单位：元)，精确到分
        /// </summary>
        public decimal deduct_fee { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string desc { get; set; }
    }
}
