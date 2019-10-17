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

    文件名：EntityBase.cs
    文件功能描述：EntityBase


    创建标识：Senparc - 20170131
    创建描述：v4.10.0 添加TemplateMessageBase作为所有模板消息数据实体基类

----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Entities.TemplateMessage
{
    /// <summary>
    /// 模板消息数据基础类接口
    /// </summary>
    public interface ITemplateMessageBase
    {
        /// <summary>
        /// Url，为null时会自动忽略
        /// </summary>
        string TemplateId { get; set; }
        /// <summary>
        /// Url，为null时会自动忽略
        /// </summary>
        string Url { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        string TemplateName { get; set; }
    }

    /// <summary>
    /// 模板消息数据基础类
    /// </summary>
    public abstract class TemplateMessageBase : ITemplateMessageBase
    {
        /// <summary>
        /// 每个公众号都不同的templateId
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// Url，为null时会自动忽略
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        public TemplateMessageBase(string templateId, string url, string templateName)
        {
            TemplateId = templateId;
            Url = url;
            TemplateName = templateName;
        }
    }
}
