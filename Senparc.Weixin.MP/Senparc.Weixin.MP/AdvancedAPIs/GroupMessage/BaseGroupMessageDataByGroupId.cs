using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_GroupId filter { get; set; }
        public string msgtype { get; set; }
    }

    public class GroupMessageByGroupId_GroupId
    {
        public string group_id { get; set; }
    }

    public class GroupMessageByGroupId_MediaId
    {
        public string media_id { get; set; }
    }

    public class GroupMessageByGroupId_Content
    {
        public string content { get; set; }
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
}
