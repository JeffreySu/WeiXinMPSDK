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
    
    文件名：RequestMessageEvent_IllegalRecord.cs
    文件功能描述：事件之小程序违规记录推送
    
    
    创建标识：mc7246 - 20211209

----------------------------------------------------------------*/


namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 事件之小程序审核失败
    /// </summary>
    public class RequestMessageEvent_IllegalRecord : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.wxa_illegal_record; }
        }

        /// <summary>
        /// 违规记录ID
        /// </summary>
        public string illegal_record_id { get; set; }

        /// <summary>
        /// 违规小程序APPID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 违规时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 违规原因
        /// </summary>
        public string illegal_reason { get; set; }

        /// <summary>
        /// 违规内容
        /// </summary>
        public string illegal_content { get; set; }

        /// <summary>
        /// 规则URL
        /// </summary>
        public string rule_url { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string rule_name { get; set; }

    }
}
