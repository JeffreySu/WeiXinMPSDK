/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BaseGroupMessageDataByOpenId.cs
    文件功能描述：根据OpenId群发所需的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class BaseGroupMessageDataByOpenId
    {
        public string[] touser { get; set; }
        public string msgtype { get; set; }
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
