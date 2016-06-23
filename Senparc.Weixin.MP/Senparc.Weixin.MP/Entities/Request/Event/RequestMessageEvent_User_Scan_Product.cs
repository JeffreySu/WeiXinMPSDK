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
    public class RequestMessageEvent_User_Scan_Product : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.user_scan_product; }
        }

        public string KeyStandard { get; set; }

        public string KeyStr { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public int Sex { get; set; }

        public int Scene { get; set; }

        public string ExtInfo { get; set; }
    }
}
