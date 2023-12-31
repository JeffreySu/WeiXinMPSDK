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
    
    文件名：GetGroupMsgListV2Result.cs
    文件功能描述：“获取企业的全部群发记录”接口返回信息
    

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// “获取企业的全部群发记录”接口返回信息
    /// </summary>
    public class GetGroupMsgListV2Result: WorkJsonResult
    {
        /// <summary>
        /// 分页游标，再下次请求时填写以获取之后分页的记录，如果已经没有更多的数据则返回空
        /// </summary>
        public string next_cursor { get; set; }
        /// <summary>
        /// 群发记录列表
        /// </summary>
        public Group_Msg_List[] group_msg_list { get; set; }
    }

    public class Group_Msg_List
    {
        /// <summary>
        /// 企业群发消息的id，可用于获取企业群发成员执行结果
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 群发消息创建者userid，<see href="https://developer.work.weixin.qq.com/document/path/93338#15836">API接口</see>创建的群发消息不返回该字段
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 群发消息创建来源。0：企业 1：个人
        /// </summary>
        public int create_type { get; set; }
        /// <summary>
        /// 消息文本内容
        /// </summary>
        public Text text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Attachment[] attachments { get; set; }


        public class Text
        {
            /// <summary>
            /// 消息文本内容，最多4000个字节
            /// </summary>
            public string content { get; set; }
        }

        public class Attachment
        {
            public string msgtype { get; set; }
            public Image image { get; set; }
            public Link link { get; set; }
            public Miniprogram miniprogram { get; set; }
            public Video video { get; set; }
            public File file { get; set; }
        }

        /// <summary>
        /// msgtype值必须是image
        /// </summary>
        public class Image
        {
            /// <summary>
            /// 图片的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/90253">素材管理接口</see>获得
            /// </summary>
            public string media_id { get; set; }
            /// <summary>
            /// 图片的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/93338#10115">获取临时素材</see>下载资源
            /// </summary>
            public string pic_url { get; set; }
        }

        /// <summary>
        /// msgtype值必须是link
        /// </summary>
        public class Link
        {
            /// <summary>
            /// 图文消息标题
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 图文消息封面的url
            /// </summary>
            public string picurl { get; set; }
            /// <summary>
            /// 图文消息的描述，最多512个字节
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 图文消息的链接
            /// </summary>
            public string url { get; set; }
        }

        /// <summary>
        /// msgtype值必须是miniprogram
        /// </summary>
        public class Miniprogram
        {
            /// <summary>
            /// 小程序消息标题，最多64个字节
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 小程序消息封面的mediaid
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

        /// <summary>
        /// msgtype值必须是video
        /// </summary>
        public class Video
        {
            /// <summary>
            /// 视频的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/93338#10115">获取临时素材</see>下载资源
            /// </summary>
            public string media_id { get; set; }
        }

        /// <summary>
        /// msgtype值必须是file
        /// </summary>
        public class File
        {
            /// <summary>
            /// 文件的media_id，可以通过<see href="https://developer.work.weixin.qq.com/document/path/93338#10115">获取临时素材</see>下载资源
            /// </summary>
            public string media_id { get; set; }
        }

    }

}
