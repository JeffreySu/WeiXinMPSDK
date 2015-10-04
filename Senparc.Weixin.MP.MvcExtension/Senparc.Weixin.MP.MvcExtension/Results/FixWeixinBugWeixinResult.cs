using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MessageHandlers;

namespace Senparc.Weixin.MP.MvcExtension
{
    public class FixWeixinBugWeixinResult : ContentResult
    {
        //private string _content;
        protected IMessageHandlerDocument _messageHandlerDocument;

        /// <summary>
        /// 这个类型只用于特殊阶段：目前IOS版本微信有换行的bug，\r\n会识别为2行
        /// </summary>
        public FixWeixinBugWeixinResult(IMessageHandlerDocument messageHandlerDocument)
        {
            _messageHandlerDocument = messageHandlerDocument;
        }

        public FixWeixinBugWeixinResult(string content)
        {
            //_content = content;
            base.Content = content;
        }


        new public string Content
        {
            get
            {
                if (base.Content != null)
                {
                    return base.Content;
                }
                else if (_messageHandlerDocument != null && _messageHandlerDocument.TextResponseMessage != null)
                {
                    if (_messageHandlerDocument.ResponseDocument != null)
                    {
                        //返回XML响应信息
                        return _messageHandlerDocument.TextResponseMessage.Replace("\r\n", "\n");
                    }
                    else
                    {
                        //返回XML响应信息或用户指定的文本内容
                        return _messageHandlerDocument.TextResponseMessage;
                    }

                }
                else
                {
                    return null;
                }
            }
            set { base.Content = value; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (base.Content == null)
            {
                //使用IMessageHandler输出
                if (_messageHandlerDocument == null)
                {
                    throw new Senparc.Weixin.Exceptions.WeixinException("执行WeixinResult时提供的MessageHandler不能为Null！", null);
                }

                if (_messageHandlerDocument.FinalResponseDocument == null)
                {
                    //throw new Senparc.Weixin.MP.WeixinException("FinalResponseDocument不能为Null！", null);
                }
                else
                {
                    context.HttpContext.Response.ClearContent();
                    context.HttpContext.Response.ContentType = "text/xml";

                    var xml = _messageHandlerDocument.FinalResponseDocument == null 
                                ? "" 
                                : _messageHandlerDocument.FinalResponseDocument
                                                         .ToString().Replace("\r\n", "\n"); //腾

                    using (MemoryStream ms = new MemoryStream())//迅
                    {//真
                        var bytes = Encoding.UTF8.GetBytes(xml);//的

                        context.HttpContext.Response.OutputStream.Write(bytes, 0, bytes.Length);//很
                    }//疼
                }
            }
        }
    }
}
