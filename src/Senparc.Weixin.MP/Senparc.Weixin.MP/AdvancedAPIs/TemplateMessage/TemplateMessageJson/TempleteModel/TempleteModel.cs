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
    
    文件名：TempleteModel.cs
    文件功能描述：模板消息接口需要的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20170328
    修改描述：添加对小程序的支持

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 普通模板消息参数
    /// </summary>
    public class TempleteModel
    {
        /// <summary>
        /// 目标用户OpenId
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 模板消息顶部颜色（16进制），默认为#FF0000
        /// </summary>
        public string topcolor { get; set; }

        /// <summary>
        /// 模板跳转链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        public TempleteModel_MiniProgram miniprogram { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }


        public TempleteModel()
        {
            topcolor = "#FF0000";
        }
    }

    /// <summary>
    /// 小程序定义
    /// </summary>
    //[Senparc.Weixin.Helpers.JsonSetting.IgnoreValue(false)]
    public class TempleteModel_MiniProgram
    {
        /// <summary>
        /// 小程序AppId
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 路径，如：index?foo=bar
        /// </summary>
        public string pagepath { get; set; }
    }
}
