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
    
    文件名：RequestMessageEvent_MassSendJobFinish.cs
    文件功能描述：事件之推送群发结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using System;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities
{
    //示例：
    //<xml>
    //  <ToUserName><![CDATA[gh_4d00ed8d6399]]></ToUserName>
    //  <FromUserName><![CDATA[oV5CrjpxgaGXNHIQigzNlgLTnwic]]></FromUserName>
    //  <CreateTime>1481013459</CreateTime>
    //  <MsgType><![CDATA[event]]></MsgType>
    //  <Event><![CDATA[MASSSENDJOBFINISH]]></Event>
    //  <MsgID>1000001625</MsgID>
    //  <Status><![CDATA[err(30003)]]></Status>
    //  <TotalCount>0</TotalCount>
    //  <FilterCount>0</FilterCount>
    //  <SentCount>0</SentCount>
    //  <ErrorCount>0</ErrorCount>
    //  <CopyrightCheckResult>
    //    <Count>2</Count>
    //    <ResultList>
    //      <item>
    //        <ArticleIdx>1</ArticleIdx>
    //        <UserDeclareState>0</UserDeclareState>
    //        <AuditState>2</AuditState>
    //        <OriginalArticleUrl><![CDATA[Url_1]]></OriginalArticleUrl>
    //        <OriginalArticleType>1</OriginalArticleType>
    //        <CanReprint>1</CanReprint>
    //        <NeedReplaceContent>1</NeedReplaceContent>
    //        <NeedShowReprintSource>1</NeedShowReprintSource>
    //      </item>
    //      <item>
    //        <ArticleIdx>2</ArticleIdx>
    //        <UserDeclareState>0</UserDeclareState>
    //        <AuditState>2</AuditState>
    //        <OriginalArticleUrl><![CDATA[Url_2]]></OriginalArticleUrl>
    //        <OriginalArticleType>1</OriginalArticleType>
    //        <CanReprint>1</CanReprint>
    //        <NeedReplaceContent>1</NeedReplaceContent>
    //        <NeedShowReprintSource>1</NeedShowReprintSource>
    //      </item>
    //    </ResultList>
    //    <CheckState>2</CheckState>
    //  </CopyrightCheckResult>
    //</xml>

    /// <summary>
    /// 事件推送群发结果。
    /// 
    /// 由于群发任务提交后，群发任务可能在一定时间后才完成，因此，群发接口调用时，仅会给出群发任务是否提交成功的提示，若群发任务提交成功，则在群发任务结束时，会向开发者在公众平台填写的开发者URL（callback URL）推送事件。
    /// </summary>
    public class RequestMessageEvent_MassSendJobFinish : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.MASSSENDJOBFINISH; }
        }

        /// <summary>
        /// 群发的结构，为“send success”或“send fail”或“err(num)”。当send success时，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败。err(num)是审核失败的具体原因，可能的情况如下：
        /// err(10001), //涉嫌广告 err(20001), //涉嫌政治 err(20004), //涉嫌社会 err(20002), //涉嫌色情 err(20006), //涉嫌违法犯罪 err(20008), //涉嫌欺诈 err(20013), //涉嫌版权 err(22000), //涉嫌互推(互相宣传) err(21000), //涉嫌其他
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// group_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 过滤（过滤是指，有些用户在微信设置不接收该公众号的消息）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public int FilterCount { get; set; }

        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public int SentCount { get; set; }

        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 群发的消息ID
        /// </summary>
        public long MsgID { get; set; }

        [Obsolete("请使用MsgID")]
        public new long MsgId { get; set; }

        /// <summary>
        /// CopyrightCheckResult
        /// </summary>
        public CopyrightCheckResult CopyrightCheckResult { get; set; }

        public RequestMessageEvent_MassSendJobFinish()
        {
            CopyrightCheckResult = new CopyrightCheckResult();
        }
    }

    //public class CopyrightCheckResult
    //{
    //    /// <summary>
    //    /// 消息数量
    //    /// </summary>
    //    public int Count { get; set; }
    //    /// <summary>
    //    /// 各个单图文校验结果
    //    /// </summary>
    //    public List<CopyrightCheckResult_ResultList> ResultList { get; set; }
    //    /// <summary>
    //    /// 整体校验结果 1-未被判为转载，可以群发，2-被判为转载，可以群发，3-被判为转载，不能群发
    //    /// </summary>
    //    public int CheckState { get; set; }

    //    public CopyrightCheckResult()
    //    {
    //        ResultList = new List<CopyrightCheckResult_ResultList>();
    //    }
    //}

    ///// <summary>
    ///// 单图文校验结果
    ///// </summary>
    //public class CopyrightCheckResult_ResultList
    //{
    //    public CopyrightCheckResult_ResultList_Item item { get; set; }

    //    public CopyrightCheckResult_ResultList()
    //    {
    //        item = new CopyrightCheckResult_ResultList_Item();
    //    }
    //}

    ///// <summary>
    ///// 单图文校验结果
    ///// </summary>
    //public class CopyrightCheckResult_ResultList_Item
    //{
    //    /// <summary>
    //    /// 群发文章的序号，从1开始
    //    /// </summary>
    //    public int ArticleIdx { get; set; }
    //    /// <summary>
    //    /// 用户声明文章的状态
    //    /// </summary>
    //    public int UserDeclareState { get; set; }
    //    /// <summary>
    //    /// 系统校验的状态
    //    /// </summary>
    //    public int AuditState { get; set; }
    //    /// <summary>
    //    /// 相似原创文的url
    //    /// </summary>
    //    public string OriginalArticleUrl { get; set; }
    //    /// <summary>
    //    /// 相似原创文的类型
    //    /// </summary>
    //    public int OriginalArticleType { get; set; }
    //    /// <summary>
    //    /// 是否能转载
    //    /// </summary>
    //    public int CanReprint { get; set; }
    //    /// <summary>
    //    /// 是否需要替换成原创文内容
    //    /// </summary>
    //    public int NeedReplaceContent { get; set; }
    //    /// <summary>
    //    /// 是否需要注明转载来源
    //    /// </summary>
    //    public int NeedShowReprintSource { get; set; }
    //}
}