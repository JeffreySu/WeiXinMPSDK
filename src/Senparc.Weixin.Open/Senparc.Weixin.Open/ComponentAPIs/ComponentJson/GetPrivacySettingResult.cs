#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2021 Yaofeng

    文件名：GetPrivacySettingResult.cs
    文件功能描述：查询小程序用户隐私保护指引


    创建标识：Yaofeng - 20211111

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
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

    public class GetPrivacySettingData_OwnerSetting
    {
        /// <summary>
        /// 信息收集方（开发者）的邮箱
        /// </summary>
        public string contact_email { get; set; }
        /// <summary>
        /// 信息收集方（开发者）的手机号
        /// </summary>
        public string contact_phone { get; set; }
        /// <summary>
        /// 信息收集方（开发者）的qq
        /// </summary>
        public string contact_qq { get; set; }
        /// <summary>
        /// 信息收集方（开发者）的微信号
        /// </summary>
        public string contact_weixin { get; set; }
        /// <summary>
        /// 自定义 用户隐私保护指引文件的media_id
        /// </summary>
        public string ext_file_media_id { get; set; }
        /// <summary>
        /// 通知方式，指的是当开发者收集信息有变动时，通过该方式通知用户
        /// </summary>
        public string notice_method { get; set; }
        /// <summary>
        /// 存储期限，指的是开发者收集用户信息存储多久
        /// </summary>
        public string store_expire_timestamp { get; set; }

    }

    public class GetPrivacySettingData_SettingList
    {
        /// <summary>
        /// 用户信息类型的英文名称
        /// </summary>
        public string privacy_key { get; set; }

        /// <summary>
        /// 该用户信息类型的用途
        /// </summary>
        public string privacy_text { get; set; }

        /// <summary>
        /// 用户信息类型的中文名称
        /// </summary>
        public string privacy_label { get; set; }
    }

    /// <summary>
    /// 用户信息类型
    /// </summary>
    public class GetPrivacySettingData_PrivacyDesc
    {
        /// <summary>
        /// 用户信息类型
        /// </summary>
        public List<GetPrivacySettingData_PrivacyDesc_List> privacy_desc_list { get; set; }
    }

    public class GetPrivacySettingData_PrivacyDesc_List
    {
        /// <summary>
        /// 用户信息类型的中文描述
        /// </summary>
        public string privacy_desc { get; set; }

        /// <summary>
        /// 用户信息类型的英文key
        /// </summary>
        public string privacy_key { get; set; }
    }
}