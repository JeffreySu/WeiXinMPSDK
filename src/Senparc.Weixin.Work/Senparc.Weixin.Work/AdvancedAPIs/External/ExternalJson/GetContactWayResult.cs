/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetContactWayResult.cs
    文件功能描述：获取企业已配置的「联系我」方式 返回数据
    
    
    创建标识：Senparc - 20220918
    
----------------------------------------------------------------*/


using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 获取企业已配置的「联系我」方式 返回数据
    /// </summary>
    public class GetContactWayResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public Contact_Way contact_way { get; set; }

        public class Contact_Way
        {
            /// <summary>
            /// 新增联系方式的配置id
            /// </summary>
            public string config_id { get; set; }
            /// <summary>
            /// 联系方式类型，1-单人，2-多人
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 场景，1-在小程序中联系，2-通过二维码联系
            /// </summary>
            public int scene { get; set; }
            /// <summary>
            /// 小程序中联系按钮的样式，仅在scene为1时返回，详见附录
            /// </summary>
            public int style { get; set; }
            /// <summary>
            /// 联系方式的备注信息，用于助记
            /// </summary>
            public string remark { get; set; }
            /// <summary>
            /// 外部客户添加时是否无需验证
            /// </summary>
            public bool skip_verify { get; set; }
            /// <summary>
            /// 企业自定义的state参数，用于区分不同的添加渠道，在调用“<see href="https://developer.work.weixin.qq.com/document/path/92228#13878">获取外部联系人详情</see>”时会返回该参数值
            /// </summary>
            public string state { get; set; }
            /// <summary>
            /// 联系二维码的URL，仅在scene为2时返回
            /// </summary>
            public string qr_code { get; set; }
            /// <summary>
            /// 使用该联系方式的用户userID列表
            /// </summary>
            public string[] user { get; set; }
            /// <summary>
            /// 使用该联系方式的部门id列表
            /// </summary>
            public int[] party { get; set; }
            /// <summary>
            /// 是否临时会话模式，默认为false，true表示使用临时会话模式
            /// </summary>
            public bool is_temp { get; set; }
            /// <summary>
            /// 临时会话二维码有效期，以秒为单位
            /// </summary>
            public int expires_in { get; set; }
            /// <summary>
            /// 临时会话有效期，以秒为单位
            /// </summary>
            public int chat_expires_in { get; set; }
            /// <summary>
            /// 可进行临时会话的客户unionid
            /// </summary>
            public string unionid { get; set; }
            /// <summary>
            /// 结束语，可参考“<see cref="https://developer.work.weixin.qq.com/document/path/92228#15645/%E7%BB%93%E6%9D%9F%E8%AF%AD%E5%AE%9A%E4%B9%89">结束语定义</see>”
            /// </summary>
            public Conclusions conclusions { get; set; }
        }

    }


}
