using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    public class AddStoreJsonData
    {
        /// <summary>
        /// 如果是从门店管理迁移门店到门店小程序，则需要填该字段
        /// </summary>
        public string poi_id { get; set; }
        /// <summary>
        /// 从腾讯地图换取的位置点id， 即search_map_poi接口返回的sosomap_poi_uid字段 必填
        /// </summary>
        public string map_poi_id { get; set; }
        /// <summary>
        /// 门店图片，可传多张图片 pic_list 字段是一个 json 必填
        /// <para>{\"list\":[\"http://mmbiz.qpic.cn/mmbiz_fmt=jpeg\"]}</para>
        /// </summary>
        public string pic_list { get; set; }
        /// <summary>
        /// 联系电话 必填
        /// </summary>
        public string contract_phone { get; set; }
        /// <summary>
        /// 营业时间，格式11:11-12:12 必填
        /// </summary>
        public string hour { get; set; }
        /// <summary>
        /// 经营资质证件号 必填
        /// </summary>
        public string credential { get; set; }
        /// <summary>
        /// 主体名字 临时素材mediaid 如果复用公众号主体，则company_name为空 如果不复用公众号主体，则company_name为具体的主体名字
        /// <para>选填</para>
        /// </summary>
        public string company_name { get; set; }
        /// <summary>
        /// 相关证明材料 临时素材mediaid 不复用公众号主体时，才需要填 支持0~5个mediaid，例如mediaid1
        /// <para>选填</para>
        /// </summary>
        public string qualification_list { get; set; }
        /// <summary>
        /// 卡券id，如果不需要添加卡券，该参数可为空 目前仅开放支持会员卡、买单和刷卡支付券，不支持自定义code，需要先去公众平台卡券后台创建cardid
        /// </summary>
        public string card_id { get; set; }
    }


    public class AddStoreJsonResult : WxJsonResult
    {
        public AuditId data { get; set; }
    }

    public class AuditId
    {
        /// <summary>
        /// 审核单id
        /// </summary>
        public long audit_id { get; set; }
    }
    
    public class AuditResultId : AuditId
    {
        /// <summary>
        /// 是否需要审核(1表示需要，0表示不需要)
        /// </summary>
        public int has_audit_id { get; set; }
    }

    public class UpdateStoreJsonResult : WxJsonResult
    {
        public AuditResultId data { get; set; }
    }

    public class UpdateStoreData
    {
        /// <summary>
        /// 为门店小程序添加门店，审核成功后返回的门店id 必填
        /// </summary>
        public string poi_id { get; set; }
        /// <summary>
        /// 营业时间，格式11:11-12:12 必填
        /// </summary>
        public string hour { get; set; }
        /// <summary>
        /// 门店图片，可传多张图片 pic_list 字段是一个 json 必填
        /// <para>{\"list\":[\"http://mmbiz.qpic.cn/mmbiz_fmt=jpeg\"]}</para>
        /// </summary>
        public string pic_list { get; set; }
        /// <summary>
        /// 联系电话 必填
        /// </summary>
        public string contract_phone { get; set; }
        /// <summary>
        /// 卡券id，如果不想修改的话，设置为空
        /// </summary>
        public string card_id { get; set; }
    }
}
