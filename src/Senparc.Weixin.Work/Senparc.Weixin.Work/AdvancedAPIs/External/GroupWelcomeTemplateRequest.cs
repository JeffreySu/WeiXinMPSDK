/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GroupWelcomeTemplateBaseRequest.cs
    文件功能描述：入群欢迎语 请求参数
    
    
    创建标识：Senparc - 20210807

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 入群欢迎语 基础请求参数
    /// </summary>
    public class GroupWelcomeTemplateBaseRequest
    {
        public Text text { get; set; }
        public GroupWelcomeTemplateImage image { get; set; }
        public Link link { get; set; }
        public Miniprogram miniprogram { get; set; }

        /// <summary>
        /// 授权方安装的应用agentid。仅旧的第三方多应用套件需要填此参数
        /// </summary>
        public string agentid { get; set; }
    }
    /// <summary>
    /// 入群欢迎语模板编辑 请求参数
    /// </summary>
    public class GroupWelcomeTemplateEditRequest : GroupWelcomeTemplateBaseRequest
    {

        /// <summary>
        /// 欢迎语素材id
        /// </summary>
        public string template_id { get; set; }
    }
    /// <summary>
    /// 入群欢迎语模板添加 请求参数
    /// </summary>
    public class GroupWelcomeTemplateAddRequest : GroupWelcomeTemplateBaseRequest
    {
        /// <summary>
        /// 是否通知成员将这条入群欢迎语应用到客户群中，0-不通知，1-通知， 不填则通知
        /// </summary>
        public short notify { get; set; }
    }

    /// <summary>
    /// 图片内容
    /// </summary>
    public class GroupWelcomeTemplateImage
    {
        /// <summary>
        /// 图片的media_id 必填
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 图片的链接，仅可使用上传图片接口得到的链接
        /// </summary>

        public string pic_url { get; set; }
    }

    public class GroupWelcomeTemplateAddResult : WorkJsonResult
    {
        /// <summary>
        /// 欢迎语素材id
        /// </summary>
        public string template_id { get; set; }
    }


    public class GroupWelcomeTemplateGetResult : WorkJsonResult
    {

        public Text text { get; set; }
        public GroupWelcomeTemplateImage image { get; set; }
        public Link link { get; set; }
        public Miniprogram miniprogram { get; set; }
    }
}
