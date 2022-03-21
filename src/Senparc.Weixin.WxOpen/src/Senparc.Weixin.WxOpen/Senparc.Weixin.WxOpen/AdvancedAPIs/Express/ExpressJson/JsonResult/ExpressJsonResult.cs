using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class ExpressJsonResult : WxJsonResult
    {
        /// <summary>
        /// 运力返回的错误码
        /// </summary>
        public int resultcode { get; set; }

        /// <summary>
        /// 运力返回的错误描述
        /// </summary>
        public string resultmsg { get; set; }
    }
}
