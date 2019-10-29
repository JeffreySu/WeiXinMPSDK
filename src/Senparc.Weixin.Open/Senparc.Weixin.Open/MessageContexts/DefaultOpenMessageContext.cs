using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.Open.MessageContexts
{
    /* Open 的上下文暂时不需要支持，和常规的不一致。 TODO：可以进行融合    —— Jeffrey 20190915 */

    ///// <summary>
    ///// 开放平台上下文消息的默认实现
    ///// </summary>
    //public class DefaultOpenMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>
    //{
    //    /// <summary>
    //    /// 获取请求消息和实体之间的映射结果
    //    /// </summary>
    //    /// <param name="requestMsgType"></param>
    //    /// <returns></returns>
    //    public override IRequestMessageBase GetRequestEntityMappingResult(RequestMsgType requestMsgType, XDocument doc = null)
    //    {
    //        throw new NotImplementedException();

    //    }

    //    /// <summary>
    //    /// 获取响应消息和实体之间的映射结果
    //    /// </summary>
    //    /// <param name="responseMsgType"></param>
    //    /// <returns></returns>
    //    public override IResponseMessageBase GetResponseEntityMappingResult(ResponseMsgType responseMsgType, XDocument doc = null)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
