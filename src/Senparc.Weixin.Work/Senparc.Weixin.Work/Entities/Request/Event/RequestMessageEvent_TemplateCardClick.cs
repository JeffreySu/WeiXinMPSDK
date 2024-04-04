/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_TemplateCardClick.cs
    文件功能描述：模板卡片点击回调事件
    
    
    创建标识：LofyLiu - 20240315
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities
{
    public class RequestMessageEvent_TemplateCardClick : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.TEMPLATE_CARD_CLICK; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }
}
