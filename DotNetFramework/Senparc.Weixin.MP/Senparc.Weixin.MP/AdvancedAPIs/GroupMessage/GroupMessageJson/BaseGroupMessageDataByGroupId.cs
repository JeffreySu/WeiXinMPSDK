/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BaseGroupMessageDataByGroupId.cs
    文件功能描述：根据GroupId群发所需的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_GroupId filter { get; set; }
        public string msgtype { get; set; }
    }

    public class GroupMessageByGroupId_GroupId
    {
        public string group_id { get; set; }
        public bool is_to_all { get; set; }
    }

    public class GroupMessageByGroupId_MediaId
    {
        public string media_id { get; set; }
    }

    public class GroupMessageByGroupId_Content
    {
        public string content { get; set; }
    }

    public class GroupMessageByGroupId_WxCard
    {
        public string card_id { get; set; }
    }

    public class GroupMessageByGroupId_VoiceData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId voice { get; set; }  
    }

    public class GroupMessageByGroupId_ImageData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId image { get; set; }
    }

    public class GroupMessageByGroupId_TextData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_Content text { get; set; }
    }

    public class GroupMessageByGroupId_MpNewsData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId mpnews { get; set; }
    }

    public class GroupMessageByGroupId_MpVideoData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_MediaId mpvideo { get; set; }
    }

    public class GroupMessageByGroupId_WxCardData : BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_WxCard wxcard { get; set; }
    }
}
