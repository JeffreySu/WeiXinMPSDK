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
  
    文件名：RegisterGuideRequestData.cs
    文件功能描述：微信支付V3服务人员注册接口请求数据
    
    
    创建标识：Senparc - 20210924
    
----------------------------------------------------------------*/


using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3服务人员注册接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_1.shtml </para>
    /// </summary>
    public class RegisterGuideRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public RegisterGuideRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="corpid">企业ID <para>body商户的企业微信唯一标识</para><para>示例值：1234567890</para></param>
        /// <param name="store_id">门店ID <para>body门店在微信支付商户平台的唯一标识（查找路径：登录商户平台—>营销中心—>门店管理，若无门店则需先创建门店）</para><para>示例值：12345678</para></param>
        /// <param name="userid">企业微信的员工ID <para>body员工在商户企业微信通讯录使用的唯一标识（企业微信的员工信息可通过接口从企业微信通讯录获取，具体请参考企业微信的API文档）</para><para>示例值：robert</para></param>
        /// <param name="name">企业微信的员工姓名 <para>body员工在商户企业微信通讯录上的姓名,需使用微信支付平台公钥加密该字段需进行加密处理，加密方法详见敏感信息加密说明。</para><para>特殊规则：加密前字段长度限制为64个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para><para></para></param>
        /// <param name="mobile">手机号码 <para>员工在商户企业微信通讯录上设置的手机号码，使用微信支付平台公钥加密该字段需进行加密处理，加密方法详见敏感信息加密说明。</para><para>特殊规则：加密前字段长度限制为32个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para><para></para></param>
        /// <param name="qr_code">员工个人二维码 <para>body员工在商户企业微信通讯录上的二维码串</para><para>示例值：https://open.work.weixin.qq.com/wwopen/userQRCode?vcode=xxx</para></param>
        /// <param name="avatar">头像URL <para>body员工在商户企业微信通讯录上头像的URL示例值：http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0</para></param>
        /// <param name="group_qrcode">群二维码URL <para>body员工所在门店在企业微信配置的群活码的URL（可通过企业微信“获取客户群进群方式API”获取，请登录企业微信后查看API文档，若无查看权限可通过问卷提交需求）示例值：http://p.qpic.cn/wwhead/nMl9ssowtibVGyrmvBiaibzDtp/0</para><para>可为null</para></param>
        public RegisterGuideRequestData(string corpid, int store_id, string userid, string name, string mobile, string qr_code, string avatar, string group_qrcode)
        {
            this.corpid = corpid;
            this.store_id = store_id;
            this.userid = userid;
            this.name = name;
            this.mobile = mobile;
            this.qr_code = qr_code;
            this.avatar = avatar;
            this.group_qrcode = group_qrcode;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">服务人员所属商户的商户ID</param>
        /// <param name="corpid">企业ID <para>body商户的企业微信唯一标识</para><para>示例值：1234567890</para></param>
        /// <param name="store_id">门店ID <para>body门店在微信支付商户平台的唯一标识（查找路径：登录商户平台—>营销中心—>门店管理，若无门店则需先创建门店）</para><para>示例值：12345678</para></param>
        /// <param name="userid">企业微信的员工ID <para>body员工在商户企业微信通讯录使用的唯一标识（企业微信的员工信息可通过接口从企业微信通讯录获取，具体请参考企业微信的API文档）</para><para>示例值：robert</para></param>
        /// <param name="name">企业微信的员工姓名 <para>body员工在商户企业微信通讯录上的姓名,需使用微信支付平台公钥加密该字段需进行加密处理，加密方法详见敏感信息加密说明。</para><para>特殊规则：加密前字段长度限制为64个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para><para></para></param>
        /// <param name="mobile">手机号码 <para>员工在商户企业微信通讯录上设置的手机号码，使用微信支付平台公钥加密该字段需进行加密处理，加密方法详见敏感信息加密说明。</para><para>特殊规则：加密前字段长度限制为32个字节</para><para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==</para><para></para></param>
        /// <param name="qr_code">员工个人二维码 <para>body员工在商户企业微信通讯录上的二维码串</para><para>示例值：https://open.work.weixin.qq.com/wwopen/userQRCode?vcode=xxx</para></param>
        /// <param name="avatar">头像URL <para>body员工在商户企业微信通讯录上头像的URL示例值：http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0</para></param>
        /// <param name="group_qrcode">群二维码URL <para>body员工所在门店在企业微信配置的群活码的URL（可通过企业微信“获取客户群进群方式API”获取，请登录企业微信后查看API文档，若无查看权限可通过问卷提交需求）示例值：http://p.qpic.cn/wwhead/nMl9ssowtibVGyrmvBiaibzDtp/0</para><para>可为null</para></param>
        public RegisterGuideRequestData(string sub_mchid, string corpid, int store_id, string userid, string name, string mobile, string qr_code, string avatar, string group_qrcode)
        {
            this.sub_mchid = sub_mchid;
            this.corpid = corpid;
            this.store_id = store_id;
            this.userid = userid;
            this.name = name;
            this.mobile = mobile;
            this.qr_code = qr_code;
            this.avatar = avatar;
            this.group_qrcode = group_qrcode;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 企业ID
        /// <para>body 商户的企业微信唯一标识</para>
        /// <para>示例值：1234567890</para>
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 门店ID
        /// <para>body 门店在微信支付商户平台的唯一标识（查找路径：登录商户平台—>营销中心—>门店管理，若无门店则需先创建门店）</para>
        /// <para>示例值：12345678</para>
        /// </summary>
        public int store_id { get; set; }

        /// <summary>
        /// 企业微信的员工ID
        /// <para>body 员工在商户企业微信通讯录使用的唯一标识（企业微信的员工信息可通过接口从企业微信通讯录获取，具体请参考企业微信的API文档 ）</para>
        /// <para>示例值：robert</para>
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 企业微信的员工姓名
        /// <para>body 员工在商户企业微信通讯录上的姓名,需使用微信支付平台公钥加密该字段需进行加密处理，加密方法详见敏感信息加密说明。</para>
        /// <para>特殊规则：加密前字段长度限制为64个字节</para>
        /// <para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== </para>
        /// <para></para>
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 手机号码
        /// <para>员工在商户企业微信通讯录上设置的手机号码，使用微信支付平台公钥加密 该字段需进行加密处理，加密方法详见敏感信息加密说明。</para>
        /// <para>特殊规则：加密前字段长度限制为32个字节</para>
        /// <para>示例值：pVd1HJ6v/69bDnuC4EL5Kz4jBHLiCa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== </para>
        /// <para></para>
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 员工个人二维码
        /// <para>body 员工在商户企业微信通讯录上的二维码串</para>
        /// <para>示例值：https://open.work.weixin.qq.com/wwopen/userQRCode?vcode=xxx</para>
        /// </summary>
        public string qr_code { get; set; }

        /// <summary>
        /// 头像URL
        /// <para>body 员工在商户企业微信通讯录上头像的URL 示例值：http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0</para>
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 群二维码URL
        /// <para>body 员工所在门店在企业微信配置的群活码的URL（可通过企业微信“获取客户群进群方式API”获取，请登录企业微信后查看API文档，若无查看权限可通过问卷提交需求 ） 示例值：http://p.qpic.cn/wwhead/nMl9ssowtibVGyrmvBiaibzDtp/0</para>
        /// <para>可为null</para>
        /// </summary>
        public string group_qrcode { get; set; }

    }




}
