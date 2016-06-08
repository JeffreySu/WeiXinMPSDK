using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Scan_Product_Async : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override MP.Event Event
        {
            get
            {
                return MP.Event.user_scan_product_async;
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
        /// 用户的实时地理位置
        /// </summary>
        public string RegionCode { get; set; }
    }
}
