using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson
{
    /// <summary>
    /// 绑定、解绑物流账号请求参数
    /// </summary>
    public class BindAccountModel
    {
        /// <summary>
        /// bind表示绑定，unbind表示解除绑定
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 快递公司客户编码
        /// </summary>
        public string biz_id { get; set; }
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 快递公司客户密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 备注内容（提交EMS审核需要） 格式要求： 电话：xxxxx 联系人：xxxxx 服务类型：xxxxx 发货地址：xxxx
        /// </summary>
        public string remark_content { get; set; }
    }
}
