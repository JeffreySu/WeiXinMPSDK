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
    Copyright (C) 2024 Senparc
  
    文件名：ModifyGuideRequestData.cs
    文件功能描述：微信支付V3服务人员信息更新接口请求数据
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3服务人员信息更新接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_4.shtml </para>
    /// </summary>
    public class ModifyGuideRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="guide_id">服务人员ID <para>path服务人员在支付即服务系统中的唯一标识</para><para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para><para></para></param>
        /// <param name="name">服务人员姓名 <para>body需更新的服务人员姓名，不更新无需传入，该字段请使用微信支付平台公钥加密，加密方法详见敏感信息加密说明</para><para>特殊规则：加密前字段长度限制为64个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para><para>可为null</para></param>
        /// <param name="mobile">服务人员手机号码 <para>body需更新的服务人员手机号码，不更新无需传入，该字段请使用微信支付平台公钥加密，加密方法详见敏感信息加密说明</para><para>特殊规则：加密前字段长度限制为32个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para><para>可为null</para></param>
        /// <param name="qr_code">服务人员二维码URL <para>body需更新的服务人员二维码，不更新无需传入，企业微信商家适用，个人微信商家不可用</para><para>示例值：https://open.work.weixin.qq.com/wwopen/userQRCode?vcode=xxx</para><para>可为null</para></param>
        /// <param name="avatar">服务人员头像URL <para>body需更新的服务人员头像URL，不更新无需传入</para><para>示例值：http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0</para><para>可为null</para></param>
        /// <param name="group_qrcode">群二维码URL <para>body员工所在门店在企业微信配置的群活码的URL（可通过企业微信“获取客户群进群方式API”获取，请登录企业微信后查看API文档，若无查看权限可通过问卷提交需求）</para><para>示例值：http://p.qpic.cn/wwhead/nMl9ssowtibVGyrmvBiaibzDtp/0</para><para>可为null</para></param>
        public ModifyGuideRequestData(string guide_id, string name, string mobile, string qr_code, string avatar, string group_qrcode)
        {
            this.guide_id = guide_id;
            this.name = name;
            this.mobile = mobile;
            this.qr_code = qr_code;
            this.avatar = avatar;
            this.group_qrcode = group_qrcode;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ModifyGuideRequestData()
        {
        }

        /// <summary>
        /// 服务人员ID
        /// <para>path服务人员在支付即服务系统中的唯一标识 </para>
        /// <para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para>
        /// <para></para>
        /// </summary>
        [JsonIgnore]
        public string guide_id { get; set; }

        /// <summary>
        /// 服务人员姓名
        /// <para>body需更新的服务人员姓名，不更新无需传入，该字段请使用微信支付平台公钥加密，加密方法详见敏感信息加密说明 </para>
        /// <para>特殊规则：加密前字段长度限制为64个字节</para>
        /// <para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para>
        /// <para>可为null</para>
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 服务人员手机号码
        /// <para>body需更新的服务人员手机号码，不更新无需传入，该字段请使用微信支付平台公钥加密，加密方法详见敏感信息加密说明 </para>
        /// <para>特殊规则：加密前字段长度限制为32个字节</para>
        /// <para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para>
        /// <para>可为null</para>
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 服务人员二维码URL
        /// <para>body需更新的服务人员二维码，不更新无需传入，企业微信商家适用，个人微信商家不可用 </para>
        /// <para>示例值：https://open.work.weixin.qq.com/wwopen/userQRCode?vcode=xxx</para>
        /// <para>可为null</para>
        /// </summary>
        public string qr_code { get; set; }

        /// <summary>
        /// 服务人员头像URL
        /// <para>body需更新的服务人员头像URL，不更新无需传入 </para>
        /// <para>示例值：http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0</para>
        /// <para>可为null</para>
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 群二维码URL
        /// <para>body 员工所在门店在企业微信配置的群活码的URL（可通过企业微信“获取客户群进群方式API”获取，请登录企业微信后查看API文档，若无查看权限可通过问卷提交需求 ）</para>
        /// <para>示例值：http://p.qpic.cn/wwhead/nMl9ssowtibVGyrmvBiaibzDtp/0</para>
        /// <para>可为null</para>
        /// </summary>
        public string group_qrcode { get; set; }

    }
}
