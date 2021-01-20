/*----------------------------------------------------------------
    Copyright (C) 2021 Senparc
    
    文件名：GetExternalContactInfoBatchResult.cs
    文件功能描述：批量获取客户详情 返回结果
    
    
    创建标识：gokeiyou - 20201013
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    public class UpdateExternalContactRemarkRequest
    {
        /// <summary>
        /// 企业成员的userid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 外部联系人userid
        /// </summary>
        public string external_userid { get; set; }
        /// <summary>
        /// 此用户对外部联系人的备注，最多20个字符
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 此用户对外部联系人的描述，最多150个字符
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 此用户对外部联系人备注的所属公司名称，最多20个字符
        /// </summary>
        public string remark_company { get; set; }
        /// <summary>
        /// 此用户对外部联系人备注的手机号
        /// </summary>
        public string[] remark_mobiles { get; set; }
        /// <summary>
        /// 备注图片的mediaid
        /// </summary>
        public string remark_pic_mediaid { get; set; }
    }
}
