#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：BaseGroupMessageByFilter.cs
    文件功能描述：根据筛选条件（GroupId、TagId）群发消息数据的基类
    
    
    创建标识：Senparc - 20171217

    修改标识：Senparc - 2011224
    修改描述：v14.8.12 完成群发接口添加clientmsgid属性

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    /// <summary>
    /// 根据筛选条件（GroupId、TagId）群发消息数据的基类
    /// </summary>
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

        /// <summary>
        /// （非必填）开发者侧群发msgid，长度限制64字节，如不填，则后台默认以群发范围和群发内容的摘要值做为clientmsgid
        /// </summary>
        public string clientmsgid { get; set; }
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
