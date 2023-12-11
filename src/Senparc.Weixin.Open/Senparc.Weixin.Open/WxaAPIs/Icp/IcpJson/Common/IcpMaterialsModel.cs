using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 其他备案材料
    /// </summary>
    public class IcpMaterialsModel
    {
        /// <summary>
        /// 互联网信息服务承诺书 media_id，最多上传1个
        /// </summary>
        public List<string> commitment_letter { get; set; }

        /// <summary>
        /// 主体更名函 media_id(非个人类型，且发生过更名时需要上传)，最多上传1个
        /// </summary>
        public List<string> business_name_change_letter { get; set; }

        /// <summary>
        /// 党建确认函 media_id，最多上传1个
        /// </summary>
        public List<string> party_building_confirmation_letter { get; set; }

        /// <summary>
        /// 承诺视频 media_id，最多上传1个
        /// </summary>
        public List<string> promise_video { get; set; }

        /// <summary>
        /// 网站备案信息真实性责任告知书 media_id，最多上传1个
        /// </summary>
        public List<string> authenticity_responsibility_letter { get; set; }
        /// <summary>
        /// 小程序备案信息真实性承诺书 media_id，最多上传1个
        /// </summary>
        public List<string> authenticity_commitment_letter { get; set; }
        /// <summary>
        /// 小程序建设方案书 media_id，最多上传1个
        /// </summary>
        public List<string> website_construction_proposal { get; set; }
        /// <summary>
        /// 主体其它附件 media_id，最多上传10个
        /// </summary>
        public List<string> subject_other_materials { get; set; }
        /// <summary>
        /// 小程序其它附件 media_id，最多上传10个
        /// </summary>
        public List<string> applets_other_materials { get; set; }
        /// <summary>
        /// 手持证件照 media_id，最多上传1个
        /// </summary>
        public List<string> holding_certificate_photo { get; set; }

    }
}
