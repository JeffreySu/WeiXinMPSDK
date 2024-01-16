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

    文件名：RequestMessageEvent_PublishJobFinish.cs
    文件功能描述：公众号文章事件推送发布结果


    创建标识：IcedMango - 20240116 增加公众号文章事件推送发布结果实体

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities
{
    // 示例：
    // 推送的XML结构成功时示例：
    // <xml> 
    //   <ToUserName><![CDATA[gh_4d00ed8d6399]]></ToUserName>  
    //   <FromUserName><![CDATA[oV5CrjpxgaGXNHIQigzNlgLTnwic]]></FromUserName>  
    //   <CreateTime>1481013459</CreateTime>
    //   <MsgType><![CDATA[event]]></MsgType>
    //   <Event><![CDATA[PUBLISHJOBFINISH]]></Event>
    //   <PublishEventInfo>
    //     <publish_id>2247503051</publish_id>
    //     <publish_status>0</publish_status>
    //     <article_id><![CDATA[b5O2OUs25HBxRceL7hfReg-U9QGeq9zQjiDvyWP4Hq4]]></article_id>
    //     <article_detail>
    //       <count>1</count>
    //       <item>
    //         <idx>1</idx>
    //         <article_url><![CDATA[ARTICLE_URL]]></article_url>
    //       </item>
    //     </article_detail>
    //   </PublishEventInfo>
    // </xml>

    // 原创审核不通过时示例：
    // <xml> 
    //   <ToUserName><![CDATA[gh_4d00ed8d6399]]></ToUserName>  
    //   <FromUserName><![CDATA[oV5CrjpxgaGXNHIQigzNlgLTnwic]]></FromUserName>  
    //   <CreateTime>1481013459</CreateTime>
    //   <MsgType><![CDATA[event]]></MsgType>
    //   <Event><![CDATA[PUBLISHJOBFINISH]]></Event>
    //   <PublishEventInfo>
    //     <publish_id>2247503051</publish_id>
    //     <publish_status>2</publish_status>
    //     <fail_idx>1</fail_idx>
    //     <fail_idx>2</fail_idx>
    //   </PublishEventInfo>
    // </xml>

    /// <summary>
    /// 事件推送发布结果
    /// 文档地址: https://developers.weixin.qq.com/doc/offiaccount/Publish/Callback_on_finish.html
    /// 由于发布任务提交后，发布任务可能在一定时间后才完成，因此，发布接口调用时，仅会给出发布任务是否提交成功的提示，若发布任务提交成功，则在发布任务结束时，会向开发者在公众平台填写的开发者URL（callback URL）推送事件。
    /// </summary>
    public class RequestMessageEvent_PublishJobFinish : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.PUBLISHJOBFINISH; }
        }

        /// <summary>
        ///  PublishEventInfo
        /// </summary>
        public Publish_Event_Info PublishEventInfo { get; set; }


        public RequestMessageEvent_PublishJobFinish()
        {
            PublishEventInfo = new Publish_Event_Info();
        }
    }
}