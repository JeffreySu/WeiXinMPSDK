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
    public class RequestMessageEvent_User_Scan_Product_Enter_Session : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.user_scan_product_enter_session; }
        }

        /// <summary>
        /// 商品编码标准
        /// </summary>
        public string KeyStrandard { get; set; }

        /// <summary>
        /// 商品编码内容
        /// </summary>
        public string KeyStr { get; set; }

        /// <summary>
        /// 抵用“获取商品二维码接口”时传入的extinfo为标识参数
        /// </summary>
        public string ExtInfo { get; set; }
    }
}
