using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.MessageHandlers
{
    /// <summary>
    /// 为IMessageHandler单独提供XDocument类型的属性接口（主要是ResponseDocument）。
    /// 分离这个接口的目的是为了在MvcExtension中对IMessageHandler解耦，使用IMessageHandlerDocument接口直接操作XML。
    /// </summary>
    public interface  IMessageHandlerDocument
    {
        /// <summary>
        /// 在构造函数中转换得到原始XML数据
        /// </summary>
        XDocument RequestDocument { get; set; }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        XDocument ResponseDocument { get; }
    }
}
