using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    public class GetAllAccountJsonResult:WxJsonResult
    {
        public int count { get; set; }
        public List<GetAllAccountList> list { get; set; }

    }
    /// <summary>
    /// 账号信息
    /// </summary>
    public class GetAllAccountList
    {
        /// <summary>
        /// 快递公司客户编码
        /// </summary>
        public string biz_id { get; set; }
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 账号绑定时间(时间戳)
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 账号更新时间(时间戳)
        /// </summary>
        public long update_time { get; set;}
        /// <summary>
        /// 绑定状态
        /// </summary>
        public int status_code { get; set; }
        /// <summary>
        /// 账号别名
        /// </summary>
        public string alias { get; set; }
        /// <summary>
        /// 账号绑定失败的错误信息（EMS审核结果）
        /// </summary>
        public string remark_wrong_msg { get; set; }
        /// <summary>
        /// 账号绑定时的备注内容（提交EMS审核需要）
        /// </summary>
        public string remark_content { get; set; }
        /// <summary>
        /// 电子面单余额
        /// </summary>
        public int quota_num { get; set; }
        /// <summary>
        /// 电子面单余额更新时间(时间戳)
        /// </summary>
        public long quota_update_time { get; set; }

        public List<ServiceType> service_type { get; set;}
    }
}
