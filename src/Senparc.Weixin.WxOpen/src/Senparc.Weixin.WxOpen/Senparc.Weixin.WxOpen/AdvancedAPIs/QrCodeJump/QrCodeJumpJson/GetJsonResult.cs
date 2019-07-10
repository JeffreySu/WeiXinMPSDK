using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.QrCodeJump
{
    /// <summary>
    /// 获取已设置的普通链接二维码规则结果
    /// </summary>
    public class GetJsonResult : WxJsonResult
    {
        /// <summary>
        /// 是否已经打开二维码跳转链接设置
        /// </summary>
        public uint qrcodejump_open { get; set; }

        /// <summary>
        /// 本月还可发布的次数
        /// </summary>
        public uint qrcodejump_pub_quota { get; set; }

        /// <summary>
        /// 二维码规则数量
        /// </summary>
        public uint list_size { get; set; }

        public List<Rule> rule_list { get; set; }
    }

    /// <summary>
    /// 二维码规则详情
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// 二维码规则
        /// </summary>
        public string prefix { get; set; }

        /// <summary>
        /// 是否独占符合二维码前缀匹配规则的所有子规 1 为不占用，2 为占用; 
        /// </summary>
        public uint permit_sub_rule { get; set; }

        /// <summary>
        /// 小程序功能页面
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 测试范围1开发版（配置只对开发者生效）,2体验版（配置对管理员、体验者生效),3正式版（配置对开发者、管理员和体验者生效）
        /// </summary>
        public uint open_version { get; set; }

        /// <summary>
        /// 测试链接（选填）可填写不多于 5 个用于测试的二维码完整链接，此链接必须符合已填写的二维码规则。
        /// </summary>
        public string[] debug_url { get; set; }

        /// <summary>
        /// 发布标志位，1 表示未发布，2 表示已发布
        /// </summary>
        public uint state { get; set; }
    }
}
