/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：LinkerCorpData.cs
    文件功能描述：互联企业消息推送接口数据
     
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mass
{
    #region 文本消息

    /// <summary>
    /// 文本消息数据
    /// </summary>
    public class SendTextData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Text text { get; set; }
        public int safe { get; set; }
    }

    public class Text
    {
        public string content { get; set; }
    }

    #endregion

    #region 图片消息

    /// <summary>
    /// 图片消息
    /// </summary>
    public class SendImageData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Image image { get; set; }
        public int safe { get; set; }
    }

    public class Image
    {
        public string media_id { get; set; }
    }


    #endregion

    #region 语音消息

    /// <summary>
    /// 语音消息
    /// </summary>
    public class SendVoiceData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Voice voice { get; set; }
    }

    public class Voice
    {
        public string media_id { get; set; }
    }

    #endregion

    #region 视频消息

    /// <summary>
    /// 视频消息
    /// </summary>
    public class SendVideoData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Video video { get; set; }
        public int safe { get; set; }
    }

    public class Video
    {
        public string media_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }


    #endregion

    #region 文件消息

    /// <summary>
    /// 文件消息
    /// </summary>
    public class SendFileData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public File file { get; set; }
        public int safe { get; set; }
    }

    public class File
    {
        public string media_id { get; set; }
    }


    #endregion

    #region 文本卡片消息

    /// <summary>
    /// 文本卡片消息
    /// </summary>
    public class SendTextCardData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Textcard textcard { get; set; }
    }

    public class Textcard
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string btntxt { get; set; }
    }


    #endregion

    #region 图文消息

    /// <summary>
    /// 图文消息
    /// </summary>
    public class SendNewsData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public News news { get; set; }
    }

    public class News
    {
        public LinkedArticle[] articles { get; set; }
    }

    public class LinkedArticle
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string picurl { get; set; }
        public string btntxt { get; set; }
    }


    #endregion

    #region 图文消息（mpnews）

    /// <summary>
    /// 图文消息（mpnews）
    /// </summary>
    public class SendMpNewsData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public int toall { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Mpnews mpnews { get; set; }
        public int safe { get; set; }
    }

    public class Mpnews
    {
        public MpArticle[] articles { get; set; }
    }

    public class MpArticle
    {
        public string title { get; set; }
        public string thumb_media_id { get; set; }
        public string author { get; set; }
        public string content_source_url { get; set; }
        public string content { get; set; }
        public string digest { get; set; }
    }


    #endregion

    #region 小程序通知消息

    /// <summary>
    /// 小程序通知消息
    /// </summary>
    public class SendMiniNoticeData
    {
        public string[] touser { get; set; }
        public string[] toparty { get; set; }
        public string[] totag { get; set; }
        public string msgtype { get; set; }
        public Miniprogram_Notice miniprogram_notice { get; set; }
    }

    public class Miniprogram_Notice
    {
        public string appid { get; set; }
        public string page { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool emphasis_first_item { get; set; }
        public Content_Item[] content_item { get; set; }
    }

    public class Content_Item
    {
        public string key { get; set; }
        public string value { get; set; }
    }


    #endregion

    #region 任务卡片消息
    public class Taskcard_Notice
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string task_id { get; set; }
        public TaskcardBtn[] btn { get; set; }
    }

    public class TaskcardBtn
    {
        public string key { get; set; }
        public string name { get; set; }
        public string replace_name { get; set; }
        public string color { get; set; }
        public bool is_bold { get; set; }
    }
    #endregion
}

