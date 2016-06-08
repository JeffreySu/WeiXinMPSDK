using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 组件消息推送
    /// </summary>
    public class RequestMessageEvent_User_Scan_Product_Callback : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get
            {
                return MP.Event.scan_product_callback;
            }
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

        /// <summary>
        /// 是否使用微信提供的弹窗页面展示防伪结果
        /// </summary>
        public bool NeedAntiFake { get; set; }

        /// <summary>
        /// 商品防伪查询的结果,real 表示码为真,fake 表示码为假。
        /// </summary>
        public string CodeResul { get; set; }
    }
}
