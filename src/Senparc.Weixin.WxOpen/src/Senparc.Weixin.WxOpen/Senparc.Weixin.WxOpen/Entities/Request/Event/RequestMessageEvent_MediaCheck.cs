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
    Copyright (C) 2022 Senparc
    
    文件名：RequestMessageEvent_MediaCheck.cs
    文件功能描述：内容安全回调：wxa_media_check 推送结果
    
    
    创建标识：Senparc - 20220806

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 内容安全回调：wxa_media_check 推送结果
    /// </summary>
    public class RequestMessageEvent_MediaCheck : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.wxa_media_check; }
        }

        public string appid { get; set; }
        public string trace_id { get; set; }
        public int version { get; set; }
        public Detail[] detail { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public string suggest { get; set; }
        public int label { get; set; }
    }

    public class Detail
    {
        public string strategy { get; set; }
        public int errcode { get; set; }
        public string suggest { get; set; }
        public int label { get; set; }
        public int prob { get; set; }
    }

}
