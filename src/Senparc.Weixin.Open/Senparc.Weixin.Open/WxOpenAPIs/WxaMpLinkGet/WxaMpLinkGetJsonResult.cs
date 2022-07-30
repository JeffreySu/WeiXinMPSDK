using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxOpenAPIs
{
    public class WxaMpLinkGetJsonResult : WxJsonResult
    {
        public Wxopens wxopens { get; set; }
    }

    public class Wxopens
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        /// <summary>
        /// status：关联状态
        /// <para>1：已关联</para>
        /// <para>2：等待小程序管理员确认中</para>
        /// <para>3：小程序管理员拒绝关联</para>
        /// <para>12：等待公众号管理员确认中</para>
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 小程序 gh_id
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 小程序 appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// （官方文档无说明）例：SOURCE_NORMAL
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 是否在公众号管理页展示中
        /// </summary>
        public int selected { get; set; }
        /// <summary>
        /// 是否展示在附近的小程序中
        /// </summary>
        public int nearby_display_status { get; set; }
        /// <summary>
        /// 是否已经发布
        /// </summary>
        public int released { get; set; }
        /// <summary>
        /// 头像 url
        /// </summary>
        public string headimg_url { get; set; }
        /// <summary>
        /// 微信认证及支付信息
        /// </summary>
        public Func_Infos[] func_infos { get; set; }
        /// <summary>
        /// （官方文档无说明）例：1
        /// </summary>
        public int copy_verify_status { get; set; }
        /// <summary>
        /// 小程序邮箱
        /// </summary>
        public string email { get; set; }
    }

    /// <summary>
    /// 微信认证及支付信息
    /// </summary>
    public class Func_Infos
    {
        /// <summary>
        /// 0 表示未开通，1 表示开通
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// （官方文档无说明）例：1、2
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// “微信认证”“微信支付”等字符串信息
        /// </summary>
        public string name { get; set; }
    }

}
