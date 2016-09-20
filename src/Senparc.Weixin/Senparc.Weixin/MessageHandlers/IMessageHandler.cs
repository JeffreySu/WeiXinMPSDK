/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestUtility.cs
    文件功能描述：微信请求集中处理接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MessageHandlers
{
    public interface IMessageHandler<TRequest, TResponse> : IMessageHandlerDocument
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
        /// <summary>
        /// 发送者用户名（OpenId）
        /// </summary>
        string WeixinOpenId { get; }

        /// <summary>
        /// 取消执行Execute()方法。一般在OnExecuting()中用于临时阻止执行Execute()。
        /// 默认为False。
        /// 如果在执行OnExecuting()执行前设为True，则所有OnExecuting()、Execute()、OnExecuted()代码都不会被执行。
        /// 如果在执行OnExecuting()执行过程中设为True，则后续Execute()及OnExecuted()代码不会被执行。
        /// 建议在设为True的时候，给ResponseMessage赋值，以返回友好信息。
        /// </summary>
        bool CancelExcute { get; set; }

        /// <summary>
        /// 请求实体
        /// </summary>
        TRequest RequestMessage { get; set; }
        /// <summary>
        /// 响应实体
        /// 只有当执行Execute()方法后才可能有值
        /// </summary>
        TResponse ResponseMessage { get; set; }

        /// <summary>
        /// 是否使用了MessageAgent代理
        /// </summary>
        bool UsedMessageAgent { get; set; }

        /// <summary>
        /// 忽略重复发送的同一条消息（通常因为微信服务器没有收到及时的响应）
        /// </summary>
        bool OmitRepeatedMessage { get; set; }


        /// <summary>
        /// 执行微信请求前触发
        /// </summary>
        void OnExecuting();

        /// <summary>
        /// 执行微信请求
        /// </summary>
        void Execute();

        /// <summary>
        /// 执行微信请求后触发
        /// </summary>
        void OnExecuted();
    }
}
