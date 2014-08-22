using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.MP.MvcExtension
{
    public class FixWeixinBugWeixinResult : ContentResult
    {
        //private string _content;
        protected IMessageHandler _messageHandler;

        /// <summary>
        /// 这个类型只用于特殊阶段：目前IOS版本微信有换行的bug，\r\n会识别为2行
        /// 
        /// 肯
        /// 请
        /// 微
        /// 信
        /// 团
        /// 队
        /// 加
        /// 把
        /// 劲
        /// 啊
        /// ！
        /// 
        /// </summary>
        public FixWeixinBugWeixinResult(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
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
                else if (_messageHandler != null && _messageHandler.ResponseDocument != null)
                {
                    return _messageHandler.ResponseDocument.ToString().Replace("\r\n", "\n");
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
                if (_messageHandler == null)
                {
                    throw new Senparc.Weixin.MP.WeixinException("执行WeixinResult时提供的MessageHandler不能为Null！", null);
                }

                if (_messageHandler.ResponseMessage == null)
                {
                    //throw new Senparc.Weixin.MP.WeixinException("ResponseMessage不能为Null！", null);
                }
                else
                {
                    context.HttpContext.Response.ClearContent();
                    context.HttpContext.Response.ContentType = "text/xml";

                    var xml = _messageHandler.ResponseDocument.ToString().Replace("\r\n", "\n"); //腾
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
