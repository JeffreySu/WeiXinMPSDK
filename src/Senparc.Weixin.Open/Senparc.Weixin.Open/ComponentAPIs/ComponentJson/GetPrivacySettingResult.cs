/*----------------------------------------------------------------
    Copyright (C) 2021 Yaofeng

    文件名：GetPrivacySettingResult.cs
    文件功能描述：查询小程序用户隐私保护指引


    创建标识：Yaofeng - 20211111

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.ComponentAPIs.SetPrivacySettingJson;
using System;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 查询小程序用户隐私保护指引
    /// </summary>
    [Serializable]
    public class GetPrivacySettingResult : WxJsonResult
    {
        /// <summary>
        /// 代码是否存在， 0 不存在， 1 存在 。如果最近没有通过commit接口上传代码，则会出现 code_exist=0的情况。
        /// </summary>
        public int code_exist { get; set; }
        /// <summary>
        /// 代码检测出来的用户信息类型（privacy_key）
        /// </summary>
        public List<string> privacy_list { get; set; }
        /// <summary>
        /// 要收集的用户信息配置
        /// </summary>
        public List<GetPrivacySettingData_SettingList> setting_list { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long update_time { get; set; }
        /// <summary>
        /// 收集方（开发者）信息配置
        /// </summary>
        public GetPrivacySettingData_OwnerSetting owner_setting { get; set; }
        /// <summary>
        /// 用户信息类型对应的中英文描述
        /// </summary>
        public GetPrivacySettingData_PrivacyDesc privacy_desc { get; set; }
    }
}