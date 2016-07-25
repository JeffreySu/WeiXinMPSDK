/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Music.cs
    文件功能描述：响应回复消息 音乐类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class Music
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MusicUrl { get; set; }
        public string HQMusicUrl { get; set; }
        ///// <summary>
        ///// 缩略图的媒体id，通过上传多媒体文件，得到的id
        ///// 官方API上有，但是加入的话会出错
        ///// </summary>
        public string ThumbMediaId { get; set; }
    }
}
