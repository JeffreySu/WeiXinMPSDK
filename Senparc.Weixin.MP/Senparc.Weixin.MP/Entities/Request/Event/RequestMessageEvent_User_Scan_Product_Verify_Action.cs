/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_Scan.cs
    文件功能描述：事件之二维码扫描（关注微信）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之二维码扫描（关注微信）
    /// </summary>
    public class RequestMessageEvent_User_Scan_Product_Verify_Action : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.user_scan_product; }
        }

        /// <summary>
        /// 商品编码标准
        /// </summary>
        public string KeyStandard { get; set; }

        /// <summary>
        /// 商品编码内容
        /// </summary>
        public string KeyStr { get; set; }

        /// <summary>
        /// 审核结果，verrify_ok通过，verify_not_pass未通过
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 未通过原因
        /// </summary>
        public string ResonMsg { get; set; }

    }
}
