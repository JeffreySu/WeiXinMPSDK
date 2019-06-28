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
    
    文件名：BaseGroupMessageDataPreview.cs
    文件功能描述：预览群发所需的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150702
    修改描述：修改结构，可根据微信号预览群发消息
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class BaseGroupMessageDataPreview
    {
        /// <summary>
        /// OpenId
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string towxname { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }
    }

    public class GroupMessagePreview_MediaId
    {
        public string media_id { get; set; }
    }

    public class GroupMessagePreview_Content
    {
        public string content { get; set; }
    }

    public class GroupMessagePreview_WxCard
    {
        public string card_id { get; set; }
        public string card_ext { get; set; }
    }

    public class GroupMessagePreview_VoiceData : BaseGroupMessageDataPreview
    {
        public GroupMessagePreview_MediaId voice { get; set; }  
    }

    public class GroupMessagePreview_ImageData : BaseGroupMessageDataPreview
    {
        public GroupMessagePreview_MediaId image { get; set; }
    }

    public class GroupMessagePreview_TextData : BaseGroupMessageDataPreview
    {
        public GroupMessagePreview_Content text { get; set; }
    }

    public class GroupMessagePreview_MpNewsData : BaseGroupMessageDataPreview
    {
        public GroupMessagePreview_MediaId mpnews { get; set; }
    }

    public class GroupMessagePreview_MpVideoData : BaseGroupMessageDataPreview
    {
        public GroupMessagePreview_MediaId mpvideo { get; set; }
    }

    public class GroupMessagePreview_WxCardData : BaseGroupMessageDataPreview
    {
        public GroupMessagePreview_WxCard wxcard { get; set; }
    }
}
