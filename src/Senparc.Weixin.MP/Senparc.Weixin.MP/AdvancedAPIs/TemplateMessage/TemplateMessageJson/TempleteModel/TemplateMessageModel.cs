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
    
    文件名：TemplateMessageModel.cs
    文件功能描述：模板消息接口需要的数据
    
    
    创建标识：RongjieAAA - 20191129

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 模板消息
    /// </summary>
    public class TemplateMessageModel
    {
        /// <summary>
        /// 是否必填：是
        /// 说明：接收者openid
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 是否必填：是
        /// 说明：模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 是否必填：否
        /// 说明：模板跳转链接（海外帐号没有跳转能力）
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 是否必填：否
        /// 说明：跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        public TemplateMessage_MiniProgram miniprogram { get; set; }


        #region 模板数据
        /// <summary>
        /// 是否必填：否
        /// 说明：开头标题数据
        /// </summary>
        public TemplateMessage_Keywords first { get; set; }
        /// <summary>
        /// 是否必填：否
        /// 说明：模板内容数据
        /// </summary>
        public List<TemplateMessage_Keywords> keywords { get; set; }
        /// <summary>
        /// 是否必填：否
        /// 说明：结尾留言数据
        /// </summary>
        public TemplateMessage_Keywords remark { get; set; }
        #endregion

    }

    /// <summary>
    /// 点击模版消息后跳转的小程序
    /// </summary>
    public class TemplateMessage_MiniProgram
    {
        /// <summary>
        /// 是否必填：是
        /// 说明：所需跳转到的小程序appid（该小程序appid必须与发模板消息的公众号是绑定关联关系，暂不支持小游戏）
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 是否必填：否
        /// 所需跳转到小程序的具体页面路径，支持带参数,（示例index?foo=bar），要求该小程序已发布，暂不支持小游戏
        /// </summary>
        public string pagepath { get; set; }
    }

    /// <summary>
    /// 模版内容
    /// </summary>
    public class TemplateMessage_Keywords
    {
        /// <summary>
        /// 是否必填：是
        /// 说明：模版内容
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 是否必填：否
        /// 说明：模板内容字体颜色，不填默认为黑色
        /// </summary>
        public string color { get; set; }
    }

}
