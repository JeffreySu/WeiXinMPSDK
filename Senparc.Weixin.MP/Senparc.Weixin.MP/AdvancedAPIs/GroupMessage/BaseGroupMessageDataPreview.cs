using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class BaseGroupMessageDataPreview
    {
        public string touser { get; set; }
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
}
