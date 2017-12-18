using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public abstract class BaseGroupMessageByFilter
    {
        public bool is_to_all { get; set; }
    }


    public class BaseGroupMessageDataByFilter
    {
        public BaseGroupMessageByFilter filter { get; set; }

        public string msgtype { get; set; }

        /// <summary>
        /// 群发接口新增 send_ignore_reprint 参数，开发者可以对群发接口的 send_ignore_reprint 参数进行设置，指定待群发的文章被判定为转载时，是否继续群发。
        /// 当 send_ignore_reprint 参数设置为1时，文章被判定为转载时，且原创文允许转载时，将继续进行群发操作。
        /// 当 send_ignore_reprint 参数设置为0时，文章被判定为转载时，将停止群发操作。
        /// send_ignore_reprint 默认为0。
        /// </summary>
        public int send_ignore_reprint { get; set; }
    }


    public class GroupMessageByFilter_MediaId
    {
        public string media_id { get; set; }
    }

    public class GroupMessageByFilter_Content
    {
        public string content { get; set; }
    }

    public class GroupMessageByFilter_WxCard
    {
        public string card_id { get; set; }
    }

    public class GroupMessageByFilter_VoiceData : BaseGroupMessageDataByFilter
    {
        public GroupMessageByGroupId_MediaId voice { get; set; }
    }

    public class GroupMessageByFilter_ImageData : BaseGroupMessageDataByFilter
    {
        public GroupMessageByGroupId_MediaId image { get; set; }
    }

    public class GroupMessageByFilter_TextData : BaseGroupMessageDataByFilter
    {
        public GroupMessageByGroupId_Content text { get; set; }
    }

    public class GroupMessageByFilter_MpNewsData : BaseGroupMessageDataByFilter
    {
        public GroupMessageByGroupId_MediaId mpnews { get; set; }
    }

    public class GroupMessageByFilter_MpVideoData : BaseGroupMessageDataByFilter
    {
        public GroupMessageByGroupId_MediaId mpvideo { get; set; }
    }

    public class GroupMessageByFilter_WxCardData : BaseGroupMessageDataByFilter
    {
        public GroupMessageByGroupId_WxCard wxcard { get; set; }
    }
}
