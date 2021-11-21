#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Suzhou Senparc Network Technology Co.,Ltd.

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

    文件名：SetPrivacySettingData_OwnerSetting.cs
    文件功能描述：“配置小程序用户隐私保护指引”接口请求参数


    创建标识：Yaofeng - 20211111

----------------------------------------------------------------*/

namespace Senparc.Weixin.Open.ComponentAPIs.RequestData
{
    public class SetPrivacySettingData_OwnerSetting
    {
        /// <summary>
        /// 否   信息收集方（开发者）的邮箱地址，4种联系方式至少要填一种
        /// </summary>
        public string contact_email { get; set; }
        /// <summary>
        /// 否   信息收集方（开发者）的手机号，4种联系方式至少要填一种
        /// </summary>
        public string contact_phone { get; set; }
        /// <summary>
        /// 否   信息收集方（开发者）的qq号，4种联系方式至少要填一种
        /// </summary>
        public string contact_qq { get; set; }
        /// <summary>
        /// 否   信息收集方（开发者）的微信号，4种联系方式至少要填一种
        /// </summary>
        public string contact_weixin { get; set; }
        /// <summary>
        /// 否   如果开发者不使用微信提供的标准化用户隐私保护指引模板，也可以上传自定义的用户隐私保护指引，通过上传接口上传后可获取media_id
        /// </summary>
        public string ext_file_media_id { get; set; }
        /// <summary>
        /// 是   通知方式，指的是当开发者收集信息有变动时，通过该方式通知用户。这里服务商需要按照实际情况填写，例如通过弹窗或者公告或者其他方式。
        /// </summary>
        public string notice_method { get; set; }
        /// <summary>
        /// 否   存储期限，指的是开发者收集用户信息存储多久。如果不填则展示为【开发者承诺，除法律法规另有规定，开发者对你的信息保存期限应当为实现处理目的所必要的最短时间】，如果填请填数字+天，例如“30天”，否则会出现87072的报错。
        /// </summary>
        public string store_expire_timestamp { get; set; }

    }

    public class SetPrivacySettingData_SettingList
    {
        /// <summary>
        /// 官方的可选值参考下方说明；该字段也支持自定义
        /// </summary>
        public string privacy_key { get; set; }

        /// <summary>
        /// 请填写收集该信息的用途。例如privacy_key=Location（位置信息），那么privacy_text则填写收集位置信息的用途。无需再带上“为了”或者“用于”这些字眼，小程序端的显示格式是为了xxx，因此开发者只需要直接填写用途即可。
        /// </summary>
        public string privacy_text { get; set; }
    }

    public class SetPrivacySettingData_SettingList_PrivacyKey
    {
        public const string UserInfo = "UserInfo";
        public const string Location = "Location";
        public const string Address = "Address";
        public const string Invoice = "Invoice";
        public const string RunData = "RunData";
        public const string Record = "Record";
        public const string Album = "Album";
        public const string Camera = "Camera";
        public const string PhoneNumber = "PhoneNumber";
        public const string Contact = "Contact";
        public const string DeviceInfo = "DeviceInfo";
        public const string EXIDNumber = "EXIDNumber";
        public const string EXOrderInfo = "EXOrderInfo";
        public const string EXUserPublishContent = "EXUserPublishContent";
        public const string EXUserFollowAcct = "EXUserFollowAcct";
        public const string EXUserOpLog = "EXUserOpLog";
        public const string AlbumWriteOnly = "AlbumWriteOnly";
        public const string LicensePlate = "LicensePlate";
        public const string BlueTooth = "BlueTooth";
        public const string CalendarWriteOnly = "CalendarWriteOnly";
        public const string Email = "Email";
        public const string MessageFile = "MessageFile";
    }
}
