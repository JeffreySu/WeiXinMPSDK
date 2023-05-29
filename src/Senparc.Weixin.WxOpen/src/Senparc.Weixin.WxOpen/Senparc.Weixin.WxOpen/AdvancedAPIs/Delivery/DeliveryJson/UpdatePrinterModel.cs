using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 该接口用于配置面单打印员请求参数
    /// </summary>
    public class UpdatePrinterModel
    {
        /// <summary>
        /// 打印员 openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 更新类型。bind表示绑定；unbind表示解除绑定。
        /// </summary>
        public string update_type { get; set; }
        /// <summary>
        /// 用于平台型小程序设置入驻方的打印员面单打印权限，同一打印员最多支持10个tagid，使用半角逗号分隔，中间不加空格，如填写123,456，表示该打印员可以拉取到tagid为123和456的下的单，非平台型小程序无需填写该字段
        /// </summary>
        public string tagid_list { get; set; }
    }
}
