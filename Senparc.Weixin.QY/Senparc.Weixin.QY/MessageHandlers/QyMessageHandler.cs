using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Context;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.Helpers;

namespace Senparc.Weixin.QY.MessageHandlers
{
    public interface IMessageHandler : Weixin.MessageHandlers.IMessageHandler
    {
        new IRequestMessageBase RequestMessage { get; set; }
        new IResponseMessageBase ResponseMessage { get; set; }
    }

    public class QyMessageHandler<TC> : Weixin.MessageHandlers.MessageHandler<TC>, IMessageHandler
        where TC : class ,IMessageContext, new()
    {
        /// <summary>
        /// 上下文（仅限于当前MessageHandler基类内）
        /// </summary>
        public static WeixinContext<TC> GlobalWeixinContext = new Context.WeixinContext<TC>();

        /// <summary>
        /// 全局消息上下文
        /// </summary>
        public override WeixinContext<TC> WeixinContext
        {
            get { return GlobalWeixinContext; }
        }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public override XDocument ResponseDocument
        {
            get
            {
                if (ResponseMessage == null)
                {
                    return null;
                }
                return EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase);
            }
        }

        /// <summary>
        /// 请求实体
        /// </summary>
        public new IRequestMessageBase RequestMessage
        {
            get
            {
                return base.RequestMessage as IRequestMessageBase;
            }
            set
            {
                base.RequestMessage = value;
            }
        }

        /// <summary>
        /// 响应实体
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
        /// </summary>
        public new IResponseMessageBase ResponseMessage
        {
            get
            {
                return base.ResponseMessage as IResponseMessageBase;
            }
            set
            {
                base.ResponseMessage = value;
            }
        }

        public QyMessageHandler(Stream inputStream, int maxRecordCount = 0)
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            inputStream.Seek(0, SeekOrigin.Begin);//强制调整指针位置

            //1、读取流：转成字符串


            //2、解密：获得明文字符串


            //3、转成XML及实体
            using (XmlReader xr = XmlReader.Create(inputStream))
            {
                RequestDocument = XDocument.Load(xr);
                Init(RequestDocument);
            }
        }

        public QyMessageHandler(XDocument requestDocument, int maxRecordCount = 0)
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            Init(requestDocument);
        }

     

        public override void Init(XDocument requestDocument)
        {
            throw new NotImplementedException();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
