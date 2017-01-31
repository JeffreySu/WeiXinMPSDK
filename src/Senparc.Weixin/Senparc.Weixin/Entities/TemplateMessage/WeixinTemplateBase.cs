/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：EntityBase.cs
    文件功能描述：EntityBase


    创建标识：Senparc - 20170131
    创建描述：v4.10.0 添加TemplateMessageBase作为所有模板消息数据实体基类

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Entities.TemplateMessage
{
    /// <summary>
    /// 模板消息数据基础类接口
    /// </summary>
    public interface ITemplateMessageBase
    {
        string TemplateId { get; set; }
        string Url { get; set; }
        string TemplateName { get; set; }
    }

    /// <summary>
    /// 模板消息数据基础类
    /// </summary>
    public class TemplateMessageBase : ITemplateMessageBase
    {
        public string TemplateId { get; set; }
        public string Url { get; set; }
        public string TemplateName { get; set; }

        public TemplateMessageBase(string templateId,string url, string templateName)
        {
            TemplateId = templateId;
            Url = url;
            TemplateName = templateName;
        }
    }
}
