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
    
    文件名：BaseGroupMessageDataByGroupId.cs
    文件功能描述：根据GroupId群发所需的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20170402
    修改描述：v14.3.140 添加BaseGroupMessageDataByGroupId.send_ignore_reprint属性

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    /// <summary>
    /// 根据GroupId群发筛选
    /// </summary>
    public class GroupMessageByGroupId : BaseGroupMessageByFilter
    {
        public string group_id { get; set; }
    }

    #region 已废弃

    [Obsolete("请使用GroupMessageByGroupId")]
    public class GroupMessageByGroupId_GroupId : BaseGroupMessageByFilter
    {
        public string group_id { get; set; }
    }

    /// <summary>
    /// 根据GroupId群发消息筛选
    /// </summary>
    [Obsolete("请使用BaseGroupMessageDataByFilter")]
    public class BaseGroupMessageDataByGroupId : BaseGroupMessageDataByFilter
    {

    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_MediaId
    {
        public string media_id { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_Content
    {
        public string content { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_WxCard
    {
        public string card_id { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_VoiceData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId voice { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_ImageData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId image { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_TextData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_Content text { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_MpNewsData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId mpnews { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_MpVideoData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId mpvideo { get; set; }
    }

    [Obsolete("请使用GroupMessageByFilter_MediaId")]
    public class GroupMessageByGroupId_WxCardData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_WxCard wxcard { get; set; }
    }

    #endregion

}
