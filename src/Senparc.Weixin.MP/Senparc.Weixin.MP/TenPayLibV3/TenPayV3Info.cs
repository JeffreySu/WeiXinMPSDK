/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    文件名：TenPayV3Info.cs
    文件功能描述：微信支付V3基础信息储存类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付基础信息储存类
    /// </summary>
    public class TenPayV3Info
    {
        /// <summary>
        /// 第三方用户唯一凭证appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 第三方用户唯一凭证密钥，即appsecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 商户支付密钥Key。登录微信商户后台，进入栏目【账户设置】【密码安全】【API 安全】【API 密钥】
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 支付完成后的回调处理页面
        /// </summary>
        public string TenPayV3Notify { get; set; } // = "http://localhost/payNotifyUrl.aspx";

        public TenPayV3Info(string appId, string appSecret, string mchId, string key, string tenPayV3Notify)
        {
            AppId = appId;
            AppSecret = appSecret;
            MchId = mchId;
            Key = key;
            TenPayV3Notify = tenPayV3Notify;
        }
    }
}
