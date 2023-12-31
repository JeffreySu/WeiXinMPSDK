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
    
    文件名：COMMON_ContactWayResult.cs
    文件功能描述：配置客户联系「联系我」相关接口共享类
    

----------------------------------------------------------------*/

//文档：https://developer.work.weixin.qq.com/document/path/92572#%E7%BB%93%E6%9D%9F%E8%AF%AD%E5%AE%9A%E4%B9%89

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 结束语定义
    /// </summary>
    public class Conclusions
    {
        public Text text { get; set; }
        public Image image { get; set; }
        public Link link { get; set; }
        public Miniprogram miniprogram { get; set; }
    }

    public class Text
    {
        /// <summary>
        /// 消息文本内容,最长为4000字节
        /// </summary>
        public string content { get; set; }
    }

    public class Image
    {
        /// <summary>
        /// 图片的media_id
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 图片的url
        /// </summary>
        public string pic_url { get; set; }
    }

    public class Link
    {
        /// <summary>
        /// 图文消息标题，最长为128字节
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图文消息封面的url
        /// </summary>
        public string picurl { get; set; }
        /// <summary>
        /// 图文消息的描述，最长为512字节
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 图文消息的链接
        /// </summary>
        public string url { get; set; }
    }

    public class Miniprogram
    {
        /// <summary>
        /// 小程序消息标题，最长为64字节
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 小程序消息封面的mediaid，封面图建议尺寸为520*416
        /// </summary>
        public string pic_media_id { get; set; }
        /// <summary>
        /// 小程序appid，必须是关联到企业的小程序应用
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 小程序page路径
        /// </summary>
        public string page { get; set; }
    }
}
