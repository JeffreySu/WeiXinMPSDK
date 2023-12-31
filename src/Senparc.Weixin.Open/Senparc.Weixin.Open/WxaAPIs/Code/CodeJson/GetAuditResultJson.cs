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
    
    文件名：GetAuditResultJson.cs
    文件功能描述：审核ID返回结果
    
    
    创建标识：Senparc - 20170726
    
    修改标识：Senparc - 20190525
    修改描述：v4.5.4.1 GetAuditStatusResultJson 改名为 GetAuditResultJson，保持全局命名唯一性

    修改标识：mc7246 - 20200318
    修改描述：v4.7.401 第三方小程序，提交审核接口更新

----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetAuditResultJson : WxJsonResult
    {

        /// <summary>
        /// 最新的审核ID，只在使用GetLatestAuditStatus接口时才有返回值
        /// </summary>
        public string auditid { get; set; }
        /// <summary>
        /// 审核状态，其中0为审核成功，1为审核失败，2为审核中，3为已撤回，4为审核延后
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 当status=1，审核被拒绝时，返回的拒绝原因
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 当 status = 1 时，会返回审核失败的小程序截图示例。用 | 分隔的 media_id 的列表，可通过获取永久素材接口拉取截图内容
        /// </summary>
        public string screenshot { get; set; }
    }
}
