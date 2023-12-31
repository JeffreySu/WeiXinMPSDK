#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GroupWelcomeTemplateRequest.cs
    文件功能描述：“添加入群欢迎语素材”接口请求信息
    

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    ///// <summary>
    ///// “添加入群欢迎语素材”接口请求信息
    ///// </summary>
    //public abstract class GroupWelcomeTemplateRequestBase
    //{
    //    public Text text { get; set; }
    //    public Image image { get; set; }
    //    public Link link { get; set; }
    //    public Miniprogram miniprogram { get; set; }
    //    public File file { get; set; }
    //    public Video video { get; set; }
    //    /// <summary>
    //    /// 授权方安装的应用agentid。仅旧的第三方多应用套件需要填此参数
    //    /// </summary>
    //    public long? agentid { get; set; }


    //    public class Text
    //    {
    //        /// <summary>
    //        /// 消息文本内容,最长为3000字节
    //        /// </summary>
    //        public string content { get; set; }
    //    }

    //    public class Image
    //    {
    //        /// <summary>
    //        /// 图片的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/90253">素材管理接口</see>获得
    //        /// </summary>
    //        public string media_id { get; set; }
    //        /// <summary>
    //        /// 图片的链接，仅可使用<see href="https://developer.work.weixin.qq.com/document/path/92135#13219">上传图片</see>接口得到的链接
    //        /// </summary>
    //        public string pic_url { get; set; }
    //    }

    //    public class Link
    //    {
    //        /// <summary>
    //        /// 图文消息标题，最长128个字节
    //        /// </summary>
    //        public string title { get; set; }
    //        /// <summary>
    //        /// 图文消息封面的url，最长2048个字节
    //        /// </summary>
    //        public string picurl { get; set; }
    //        /// <summary>
    //        /// 图文消息的描述，最多512个字节
    //        /// </summary>
    //        public string desc { get; set; }
    //        /// <summary>
    //        /// 图文消息的链接，最长2048个字节
    //        /// </summary>
    //        public string url { get; set; }
    //    }

    //    public class Miniprogram
    //    {
    //        /// <summary>
    //        /// 小程序消息标题，最多64个字节
    //        /// </summary>
    //        public string title { get; set; }
    //        /// <summary>
    //        /// 小程序消息封面的mediaid，封面图建议尺寸为520*416
    //        /// </summary>
    //        public string pic_media_id { get; set; }
    //        /// <summary>
    //        /// 小程序appid（可以在微信公众平台上查询），必须是关联到企业的小程序应用
    //        /// </summary>
    //        public string appid { get; set; }
    //        /// <summary>
    //        /// 小程序page路径
    //        /// </summary>
    //        public string page { get; set; }
    //    }

    //    public class File
    //    {
    //        /// <summary>
    //        /// 文件的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/90253">素材管理接口</see>接口获得
    //        /// </summary>
    //        public string media_id { get; set; }
    //    }

    //    public class Video
    //    {
    //        /// <summary>
    //        /// 视频的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/90253">素材管理接口</see>接口获得
    //        /// </summary>
    //        public string media_id { get; set; }
    //    }

    //}


    ///// <summary>
    ///// 
    ///// </summary>
    //public class GroupWelcomeTemplateAddRequest1 : GroupWelcomeTemplateRequestBase
    //{
    //    /// <summary>
    //    /// 是否通知成员将这条入群欢迎语应用到客户群中，0-不通知，1-通知， 不填则通知
    //    /// </summary>
    //    public int? notify { get; set; }
    //}
}
