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

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class BaseGroupMessageDataByGroupId
    {
        public GroupMessageByGroupId_GroupId filter { get; set; }
        public string msgtype { get; set; }

        /// <summary>
        /// 群发接口新增 send_ignore_reprint 参数，开发者可以对群发接口的 send_ignore_reprint 参数进行设置，指定待群发的文章被判定为转载时，是否继续群发。
        /// 当 send_ignore_reprint 参数设置为1时，文章被判定为转载时，且原创文允许转载时，将继续进行群发操作。
        /// 当 send_ignore_reprint 参数设置为0时，文章被判定为转载时，将停止群发操作。
        /// send_ignore_reprint 默认为0。
        /// </summary>
        public int send_ignore_reprint { get; set; }
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
