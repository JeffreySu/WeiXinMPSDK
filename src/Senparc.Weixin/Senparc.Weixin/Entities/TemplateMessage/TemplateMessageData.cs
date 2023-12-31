/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：TemplateMessageData.cs
    文件功能描述：模板消息数据


    创建标识：Senparc - 20191014
    创建描述：v6.6.102 添加给模板消息同意使用的类

----------------------------------------------------------------*/


using System.Collections.Generic;

namespace Senparc.Weixin.Entities.TemplateMessage
{
    /// <summary>
    /// 模板消息数据，JSON 格式形如 { "key1": { "value": any }, "key2": { "value": any } }
    /// </summary>
    public class TemplateMessageData : Dictionary<string, TemplateMessageDataValue>
    {
        public TemplateMessageData()
           : base()
        {
        }

        public TemplateMessageData(int capacity)
            : base(capacity)
        {
        }

        public TemplateMessageData(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        public TemplateMessageData(int capacity, IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {

        }

        public TemplateMessageData(IDictionary<string, TemplateMessageDataValue> dictionary)
            : base(dictionary)
        {
        }

        public TemplateMessageData(IDictionary<string, TemplateMessageDataValue> dictionary, IEqualityComparer<string> comparer)
            : base(dictionary, comparer)
        {

        }
    }

    /// <summary>
    /// 模板内容，JSON 格式形如: { "value": any }
    /// </summary>
    public class TemplateMessageDataValue
    {
        public object value { get; set; }

        public TemplateMessageDataValue(object _value)
        {
            value = _value;
        }
    }
}
