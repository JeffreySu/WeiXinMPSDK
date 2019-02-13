using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.AccountAPIs.AccountBasicInfoJson
{
    /*
{
    "errcode": 0,
    "errmsg": "ok",
"appid": "wxdc685123d955453",
    "account_type": 2,
"principal_type": 1,
"principal_name": "深圳市腾讯计算机系统有限公司",
    "realname_status": 1,
    "wx_verify_info": {
        "qualification_verify": 1,
        "naming_verify": 1,
        "annual_review": 1,
        "annual_review_begin_time": 1550490981,
        "annual_review_end_time": 1558266981,
    }
    "signature_info": {
        "signature": "功能介绍",
        "modify_used_count": 1,
        "modify_quota": 5,
    }
"head_image_info": {
        "head_image_url": "http://mmbiz.qpic.cn/mmbiz/a5icZrUmbV8p5jb6RZ8aYfjfS2AVle8URwBt8QIu6XbGewB9wiaWYWkPwq4R7pfdsFibuLkic16UcxDSNYtB8HnC1Q/0",
        "modify_used_count": 3,
        "modify_quota": 5,
    }
}

     */

    /// <summary>
    /// 小程序信息
    /// </summary>
    public class AccountBasicInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 帐号appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 帐号类型（1：订阅号，2：服务号，3：小程序）
        /// </summary>
        public AccountType account_type { get; set; }

        /// <summary>
        /// 主体类型（1：企业）
        /// </summary>
        public PrincipalType principal_type { get; set; }

        /// <summary>
        /// 主体名称
        /// </summary>
        public string principal_name { get; set; }

        /// <summary>
        /// 实名验证状态
        /// <para>（1：实名验证成功，2：实名验证中，3：实名验证失败）
        /// 调用接口1.1创建帐号时，realname_status会初始化为2对于注册方式为微信认证的帐号，资质认证成功时，realname_status会更新为1
        /// 注意！！！当realname_status不为1时，帐号只允许调用本文档内的以下API：（即无权限调用其他API）
        /// 微信认证相关接口（参考2.x） 帐号设置相关接口（参考3.x）</para>
        /// </summary>
        public RealNameStatus realname_status { get; set; }

        /// <summary>
        /// （微信认证信息）
        /// </summary>
        public WxVerifyInfo wx_verify_info { get; set; }

        /// <summary>
        /// （功能介绍信息）
        /// </summary>
        public SignatureInfo signature_info { get; set; }

        /// <summary>
        /// 头像信息
        /// </summary>
        public HeadImageInfo head_image_info { get; set; }
    }

    /// <summary>
    /// 微信认证信息
    /// </summary>
    public class WxVerifyInfo
    {
        /// <summary>
        /// 是否资质认证 （1：是，0：否）若是，拥有微信认证相关的权限。
        /// </summary>
        public int qualification_verify { get; set; }
        /// <summary>
        /// 是否名称认证 （1：是，0：否） 对于公众号（订阅号、服务号），是名称认证，微信客户端才会有微信认证打勾的标识
        /// </summary>
        public int naming_verify { get; set; }
        /// <summary>
        /// 是否需要年审 （1：是，0：否）（qualification_verify = true时才有该字段）
        /// </summary>
        public int annual_review { get; set; }
        /// <summary>
        /// 年审开始时间 时间戳（qualification_verify = true时才有该字段）
        /// </summary>
        public long annual_review_begin_time { get; set; }
        /// <summary>
        /// 年审截止时间 时间戳（qualification_verify = true时才有该字段）
        /// </summary>
        public long annual_review_end_time { get; set; }
    }

    /// <summary>
    /// 功能介绍信息
    /// </summary>
    public class SignatureInfo
    {
        /// <summary>
        /// 功能介绍
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 功能介绍已使用修改次数（本月）
        /// </summary>
        public int modify_used_count { get; set; }

        /// <summary>
        /// 功能介绍修改次数总额度（本月）
        /// </summary>
        public int modify_quota { get; set; }
    }

    /// <summary>
    /// 头像信息
    /// </summary>
    public class HeadImageInfo
    {
        /// <summary>
        /// 头像url
        /// </summary>
        public string head_image_url { get; set; }

        /// <summary>
        /// 头像已使用修改次数（本月）
        /// </summary>
        public int modify_used_count { get; set; }

        /// <summary>
        /// 头像修改次数总额度（本月）
        /// </summary>
        public int modify_quota { get; set; }
    }
}
