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
