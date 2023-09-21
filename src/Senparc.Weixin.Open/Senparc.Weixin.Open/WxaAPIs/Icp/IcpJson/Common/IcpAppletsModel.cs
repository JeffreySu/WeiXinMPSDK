using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 微信小程序信息
    /// </summary>
    public class IcpAppletsModel
    {
        /// <summary>
        /// 微信小程序基本信息
        /// </summary>
        public IcpAppletsBaseInfoModel base_info { get; set; }

        /// <summary>
        /// 小程序负责人信息
        /// </summary>
        public PrincipalInfoModel principal_info { get; set; }
    }

    public class IcpAppletsBaseInfoModel
    {
        /// <summary>
        /// 小程序ID，不用填写，后台自动拉取
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 小程序名称，不用填写，后台自动拉取
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 小程序服务内容类型，只能填写二级服务内容类型，最多5个，示例值：`[3, 4]`(参考：获取小程序服务类型接口)
        /// </summary>
        public List<int> service_content_types { get; set; }

        /// <summary>
        /// 前置审批项，列表中不能存在重复的前置审批类型id，如不涉及前置审批项，也需要填“以上都不涉及”
        /// </summary>
        public List<NrlxDetailModel> nrlx_details { get; set; }

        /// <summary>
        /// 小程序备注，根据需要，如实填写
        /// </summary>
        public string comment { get; set; }
    }

    /// <summary>
    /// 前置审批项，列表中不能存在重复的前置审批类型id，如不涉及前置审批项，也需要填“以上都不涉及”
    /// </summary>
    public class NrlxDetailModel
    {
        /// <summary>
        /// 前置审批类型，示例值：`2`(参考：获取前置审批项接口)
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 前置审批号，如果前置审批类型不是“以上都不涉及”，则必填，示例值：`"粤-12345号"`
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 前置审批媒体材料 media_id，如果前置审批类型不是“以上都不涉及”，则必填，示例值：`"4ahCGpd3CYkE6RpkNkUR5czt3LvG8xDnDdKAz6bBKttSfM8p4k5Rj6823HXugPwQBurgMezyib7"`
        /// </summary>
        public string media { get; set; }
    }
}
