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
    
    文件名：GetGroupMsgListV2Result.cs
    文件功能描述：“获取企业群发成员执行结果”接口返回结果
    

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// “获取企业群发成员执行结果”接口返回结果
    /// </summary>
    public class GetGroupMsgSendResultResult : WorkJsonResult
    {
        /// <summary>
        /// 分页游标，再下次请求时填写以获取之后分页的记录，如果已经没有更多的数据则返回空
        /// </summary>
        public string next_cursor { get; set; }
        /// <summary>
        /// 群成员发送结果列表
        /// </summary>
        public Send_List[] send_list { get; set; }
    }

    public class Send_List
    {
        /// <summary>
        /// 外部联系人userid，群发消息到企业的客户群不返回该字段
        /// </summary>
        public string external_userid { get; set; }
        /// <summary>
        /// 外部客户群id，群发消息到客户不返回该字段
        /// </summary>
        public string chat_id { get; set; }
        /// <summary>
        /// 企业服务人员的userid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 发送状态：0-未发送 1-已发送 2-因客户不是好友导致发送失败 3-因客户已经收到其他群发消息导致发送失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 发送时间，发送状态为1时返回
        /// </summary>
        public int send_time { get; set; }
    }

}
