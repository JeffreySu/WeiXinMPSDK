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
    
    文件名：GetGroupMsgListV2Request.cs
    文件功能描述：“获取企业的全部群发记录”接口请求信息
    

----------------------------------------------------------------*/

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// “获取企业的全部群发记录”接口请求信息
    /// </summary>
    public class GetGroupMsgListV2Request
    {
        /// <summary>
        /// 群发任务的类型，默认为single，表示发送给客户，group表示发送给客户群
        /// </summary>
        public string chat_type { get; set; }
        /// <summary>
        /// 群发任务记录开始时间
        /// <para>提示：可以使用 <see cref="Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime"/> 进行转换</para>
        /// </summary>
        public long start_time { get; set; }
        /// <summary>
        /// 群发任务记录结束时间
        /// <para>提示：可以使用 <see cref="Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime"/> 进行转换</para>
        /// </summary>
        public long end_time { get; set; }
        /// <summary>
        /// 群发任务创建人企业账号id
        /// </summary>
        public string? creator { get; set; }
        /// <summary>
        /// 创建人类型。0：企业发表 1：个人发表 2：所有，包括个人创建以及企业创建，默认情况下为所有类型
        /// </summary>
        public int? filter_type { get; set; }
        /// <summary>
        /// 返回的最大记录数，整型，最大值100，默认值50，超过最大值时取默认值
        /// </summary>
        public int? limit { get; set; }
        /// <summary>
        /// 用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填
        /// </summary>
        public string? cursor { get; set; }

        
    }

}
