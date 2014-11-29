using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class BaseGroupMessageDataByOpenId
    {
        public GroupMessageByOpenId_GroupId filter { get; set; }
        public string msgtype { get; set; }
    }

    public class GroupMessageByOpenId_GroupId
    {
        public string[] touser { get; set; }
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
}
