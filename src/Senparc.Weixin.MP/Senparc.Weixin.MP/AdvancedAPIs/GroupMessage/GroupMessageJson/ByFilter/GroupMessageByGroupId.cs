/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
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
