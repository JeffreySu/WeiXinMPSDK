/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：AddContactWayRequest.cs
    文件功能描述：配置客户联系「联系我」方式 请求数据
    
    
    创建标识：Senparc - 20210316
    
    修改标识：Senparc - 20220918
    修改描述：v3.15.9 将 Conclusions 类放到 COMMON_ContactWayResult.cs 中公用

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 配置客户联系「联系我」方式 请求数据
    /// <para>注意，每个联系方式最多配置100个使用成员（包含部门展开后的成员）</para>
    /// <para>当设置为临时会话模式时（即is_temp为true），联系人仅支持配置为单人，暂不支持多人</para>
    /// <para>使用unionid需要调用方（企业或服务商）的企业微信“客户联系”中已绑定微信开发者账户</para>
    /// </summary>
    public class AddContactWayRequest
    {
        /// <summary>
        /// 联系方式类型,1-单人, 2-多人（必须）
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 场景，1-在小程序中联系，2-通过二维码联系（必须）
        /// </summary>
        public int scene { get; set; }
        /// <summary>
        /// 在小程序中联系时使用的控件样式，详见附表：https://work.weixin.qq.com/api/doc/90000/90135/92572
        /// </summary>
        public int style { get; set; }
        /// <summary>
        /// 联系方式的备注信息，用于助记，不超过30个字符
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 外部客户添加时是否无需验证，默认为true
        /// </summary>
        public bool? skip_verify { get; set; }
        /// <summary>
        /// 企业自定义的state参数，用于区分不同的添加渠道，在调用“获取外部联系人详情(https://work.weixin.qq.com/api/doc/90000/90135/92114)”时会返回该参数值，不超过30个字符
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 使用该联系方式的用户userID列表，在type为1时为必填，且只能有一个
        /// </summary>
        public string[] user { get; set; }
        /// <summary>
        /// 使用该联系方式的部门id列表，只在type为2时有效
        /// </summary>
        public int[] party { get; set; }
        /// <summary>
        /// 是否临时会话模式，true表示使用临时会话模式，默认为false
        /// </summary>
        public bool? is_temp { get; set; }
        /// <summary>
        /// 临时会话二维码有效期，以秒为单位。该参数仅在is_temp为true时有效，默认7天
        /// </summary>
        public int? expires_in { get; set; }
        /// <summary>
        /// 临时会话有效期，以秒为单位。该参数仅在is_temp为true时有效，默认为添加好友后24小时
        /// </summary>
        public int? chat_expires_in { get; set; }
        /// <summary>
        /// 可进行临时会话的客户unionid，该参数仅在is_temp为true时有效，如不指定则不进行限制
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 结束语，会话结束时自动发送给客户，可参考“结束语定义”，仅在is_temp为true时有效
        /// </summary>
        public Conclusions conclusions { get; set; }
    }

}
