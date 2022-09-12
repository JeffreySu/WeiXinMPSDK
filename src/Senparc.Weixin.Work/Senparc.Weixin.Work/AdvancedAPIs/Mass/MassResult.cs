#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2022 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright(C) 2021 Senparc
    
    文件名：MassResult.cs
    文件功能描述：发送消息接口返回参数
    
    
    创建标识：Senparc - 20161204

    修改标识：Senparc - 20220912
    修改描述：v3.15.8 添加“按钮交互型”，“投票选择型”和“多项选择型”的模板卡片消息


----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mass
{
    /// <summary>
    /// 发送消息返回结果
    /// 如果对应用或收件人、部门、标签任何一个无权限，则本次发送失败；如果收件人、部门或标签不存在，发送仍然执行，但返回无效的部分。
    /// </summary>
    public class MassResult : WorkJsonResult
    {
        /// <summary>
        /// 不合法的userid，不区分大小写，统一转为小写
        /// </summary>
        public string invaliduser { get; set; }
        /// <summary>
        /// 不合法的partyid
        /// </summary>
        public string invalidparty { get; set; }
        /// <summary>
        /// 不合法的标签id
        /// </summary>
        public string invalidtag { get; set; }
        /// <summary>
        /// 没有基础接口许可(包含已过期)的userid
        /// </summary>
        public string unlicenseduser { get; set; }
        /// <summary>
        /// 消息id，用于<see href="https://developer.work.weixin.qq.com/document/path/90236#31947">撤回应用消息</see>
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 仅消息类型为“按钮交互型”，“投票选择型”和“多项选择型”的模板卡片消息返回，应用可使用response_code调用<see href="https://developer.work.weixin.qq.com/document/path/90236#32086">更新模版卡片消息</see>接口，24小时内有效，且只能使用一次
        /// </summary>
        public string response_code { get; set; }

    }


    public class SendMiniProgramNoticeData
    {
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }
        public string msgtype { get; set; }
        public Miniprogram_Notice miniprogram_notice { get; set; }
        /// <summary>
        /// 表示是否开启重复消息检查，0表示否，1表示是，默认0
        /// </summary>
        public int enableDuplicateCheck { get; set; }
        /// <summary>
        /// 表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时
        /// </summary>
        public int duplicateCheckInterval { get; set; } = 1800;
    }

    public class SendTaskCardNoticeData
    {
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Taskcard_Notice taskcard { get; set; }
        /// <summary>
        /// 表示是否开启重复消息检查，0表示否，1表示是，默认0
        /// </summary>
        public int enableDuplicateCheck { get; set; }
        /// <summary>
        /// 表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时
        /// </summary>
        public int duplicateCheckInterval { get; set; } = 1800;
    }

    /// <summary>
    /// 更新任务卡片消息状态 输入参数
    /// </summary>
    public class UpdateTaskCardData
    {
        public string[] userids { get; set; }
        public int agentid { get; set; }
        public string task_id { get; set; }
        public string clicked_key { get; set; }

    }

    /// <summary>
    /// 更新任务卡片消息状态 返回结果
    /// </summary>
    public class UpdateTaskCardResultJson : WorkJsonResult
    {
        /// <summary>
        /// 不区分大小写，返回的列表都统一转为小写
        /// </summary>
        public string[] invaliduser { get; set; }
    }
}
