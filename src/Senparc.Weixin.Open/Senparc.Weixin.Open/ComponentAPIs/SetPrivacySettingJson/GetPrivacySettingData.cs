using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.ComponentAPIs.SetPrivacySettingJson
{
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
