using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetAppealRecordsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 申诉记录列表
        /// </summary>
        List<MaterialInfo> records { get; set; }
    }

    public class MaterialInfo
    {
        /// <summary>
        /// 申诉单id
        /// </summary>
        public string appeal_record_id { get; set; }

        /// <summary>
        /// 申诉时间
        /// </summary>
        public string appeal_time { get; set; }

        /// <summary>
        /// 申诉次数
        /// </summary>
        public int appeal_count { get; set; }

        /// <summary>
        /// 申诉来源（0--用户，1--服务商）
        /// </summary>
        public int appeal_from { get; set; }

        /// <summary>
        /// 申诉状态，1正在处理，2申诉通过，3申诉不通过，4申诉已撤销
        /// </summary>
        public int appeal_status { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public string audit_time { get; set; }

        /// <summary>
        /// 审核结果理由
        /// </summary>
        public string audit_reason { get; set; }

        /// <summary>
        /// 处罚原因描述
        /// </summary>
        public string punish_description { get; set; }

        /// <summary>
        /// 违规材料和申诉材料
        /// </summary>
        public List<Material> materials { get; set; }
    }

    public class Material
    {
        /// <summary>
        /// 违规材料
        /// </summary>
        public Illegal_Material illegal_material { get; set; }

        /// <summary>
        /// 申诉材料（针对违规材料提供的资料
        /// </summary>
        public Appeal_Material appeal_material { get; set; }
    }

    public class Illegal_Material
    {
        /// <summary>
        /// 违规内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 违规链接
        /// </summary>
        public string content_url { get; set; }
    }

    public class Appeal_Material
    {
        /// <summary>
        /// 申诉理由
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 证明材料列表(可以通过“获取临时素材”接口下载对应的材料）
        /// </summary>
        public List<string> proof_material_ids { get; set; }
    }
}
