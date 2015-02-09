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
    public interface IMessageHandlerDocument
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

        /// <summary>
        /// 最后返回的ResponseDocument。
        /// 如果是Senparc.Weixin.MP，则应当和ResponseDocument一致；如果是Senparc.Weixin.QY，则应当在ResponseDocument基础上进行加密
        /// </summary>
        XDocument FinalResponseDocument { get; }

        /// <summary>
        /// 文字返回信息。当TextResponseMessage不为null时，才获取ResponseDocument
        /// </summary>
        string TextResponseMessage { get; set; }
    }
}
