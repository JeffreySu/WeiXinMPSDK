/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：UserSecurityJsonResults.cs
    文件功能描述：UserSecurityJsonResults 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Business.JsonResult
{
    /// <summary>
    /// 获取插件用户 OpenPID 返回结果
    /// </summary>
    public class GetPluginOpenPidJsonResult : WxJsonResult
    {
        public string openpid { get; set; }
    }

    /// <summary>
    /// 检查加密信息返回结果
    /// </summary>
    public class CheckEncryptedDataJsonResult : WxJsonResult
    {
        /// <summary>
        /// 加密信息是否有效。字段名遵循微信官方返回值。
        /// </summary>
        public bool vaild { get; set; }

        public long create_time { get; set; }
    }

    /// <summary>
    /// 获取用户数据加密密钥返回结果
    /// </summary>
    public class GetUserEncryptKeyJsonResult : WxJsonResult
    {
        public List<UserEncryptKeyInfo> key_info_list { get; set; }
    }

    /// <summary>
    /// UserEncryptKey 信息。
    /// </summary>
    public class UserEncryptKeyInfo
    {
        public string encrypt_key { get; set; }
        public int version { get; set; }
        public long expire_in { get; set; }
        public string iv { get; set; }
        public long create_time { get; set; }
    }

    /// <summary>
    /// 重置用户 SessionKey 返回结果
    /// </summary>
    public class ResetUserSessionKeyJsonResult : WxJsonResult
    {
        public string openid { get; set; }
        public string session_key { get; set; }
    }
}
