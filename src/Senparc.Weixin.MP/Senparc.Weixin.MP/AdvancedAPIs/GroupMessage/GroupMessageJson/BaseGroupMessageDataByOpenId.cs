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
    
    文件名：BaseGroupMessageDataByOpenId.cs
    文件功能描述：根据OpenId群发所需的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 2011224
    修改描述：v14.8.12 完成群发接口添加clientmsgid属性

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class BaseGroupMessageDataByOpenId
    {
        public string[] touser { get; set; }
        public string msgtype { get; set; }
        /// <summary>
        /// （非必填）开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid
        /// </summary>
        public string clientmsgid { get; set; }
    }

    public class GroupMessageByOpenId_MediaId
    {
        public string media_id { get; set; }
    }

    public class GroupMessageByOpenId_Content
    {
        public string content { get; set; }
    }

    public class GroupMessageByOpenId_Video
    {
        public string title { get; set; }
        public string media_id { get; set; }
        public string description { get; set; }
    }

    public class GroupMessageByOpenId_WxCard
    {
        public string card_id { get; set; }
    }

    public class GroupMessageByOpenId_VoiceData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_MediaId voice { get; set; }  
    }

    public class GroupMessageByOpenId_ImageData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_MediaId image { get; set; }
    }

    public class GroupMessageByOpenId_TextData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_Content text { get; set; }
    }

    public class GroupMessageByOpenId_MpNewsData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_MediaId mpnews { get; set; }
    }

    public class GroupMessageByOpenId_MpVideoData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_Video video { get; set; }
    }

    public class GroupMessageByOpenId_WxCardData : BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_WxCard wxcard { get; set; }
    }
}
