#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：RecordJson.cs
    文件功能描述：客服记录消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 客服记录消息
    /// </summary>
    public class RecordJson 
    {
        /// <summary>
        /// 客服账号
        /// </summary>
        public string worker { get; set; }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 操作ID（会话状态）
        /// </summary>
        public Opercode opercode { get; set; }
        /// <summary>
        /// 操作时间，UNIX时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 聊天记录
        /// </summary>
        public string text { get; set; }
    }

    /// <summary>
    /// 操作ID(会化状态）定义
    /// </summary>
    public enum Opercode
    {
        创建未接入会话 = 1000,
        接入会话 = 1001,
        主动发起会话 = 1002,
        转接会话 = 1003,
        关闭会话 = 1004,
        抢接会话 = 1005,
        公众号收到消息 = 2001,
        客服发送消息 = 2002,
        客服收到消息 = 2003
    }
}
