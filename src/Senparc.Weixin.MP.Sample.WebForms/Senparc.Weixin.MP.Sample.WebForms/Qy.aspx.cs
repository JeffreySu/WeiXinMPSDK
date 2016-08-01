using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.Sample.CommonService.QyMessageHandlers;
using Senparc.Weixin.QY.Entities;

namespace Senparc.Weixin.MP.Sample.WebForms
{
    public partial class Qy : System.Web.UI.Page
    {
        public static readonly string Token = "fzBsmSaI8XE1OwBh";//与微信企业账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = "9J8CQ7iF9mLtQDZrUM1loOVQ6oNDxVtBi1DBU2oaewl";//与微信企业账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string CorpId = "wx7618c0a6d9358622";//与微信企业账号后台的EncodingAESKey设置保持一致，区分大小写。

        protected void Page_Load(object sender, EventArgs e)
        {
            string msg_signature = Request["msg_signature"];
            string timestamp = Request["timestamp"];
            string nonce = Request["nonce"];
            string echostr = Request["echostr"];

            if (Request.HttpMethod == "GET")
            {
                //get method - 仅在微信后台填写URL验证时触发

                var verifyUrl = QY.Signature.VerifyURL(Token, EncodingAESKey, CorpId, msg_signature, timestamp, nonce,
                    echostr);
                if (verifyUrl != null)
                {
                    WriteContent(verifyUrl); //返回解密后的随机字符串则表示验证通过
                }
                else
                {
                    WriteContent("如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
                }

                Response.End();
            }
            else
            {
                //post method - 当有用户想公众账号发送消息时触发
                var postModel = new PostModel()
                {
                    Msg_Signature = Request.QueryString["msg_signature"],
                    Timestamp = Request.QueryString["timestamp"],
                    Nonce = Request.QueryString["nonce"],
                    //以下保密信息不会（不应该）在网络上传播，请注意
                    Token = Token,
                    EncodingAESKey = EncodingAESKey,
                    CorpId = CorpId,
                };

                var maxRecordCount = 10;
                //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
                var messageHandler = new QyCustomMessageHandler(Request.InputStream, postModel, maxRecordCount);

                if (messageHandler.RequestMessage == null)
                {
                    //验证不通过或接受信息有错误
                }

                try
                {
                    //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                    messageHandler.RequestDocument.Save(Server.MapPath("~/App_Data/Qy/" + DateTime.Now.Ticks + "_Request_" + messageHandler.RequestMessage.FromUserName + ".txt"));
                    //执行微信处理过程
                    messageHandler.Execute();
                    //测试时可开启，帮助跟踪数据
                    messageHandler.ResponseDocument.Save(Server.MapPath("~/App_Data/Qy/" + DateTime.Now.Ticks + "_Response_" + messageHandler.ResponseMessage.ToUserName + ".txt"));
                    messageHandler.FinalResponseDocument.Save(Server.MapPath("~/App_Data/Qy/" + DateTime.Now.Ticks + "_FinalResponse_" + messageHandler.ResponseMessage.ToUserName + ".txt"));

                    //自动返回加密后结果
                    WriteContent(messageHandler.FinalResponseDocument.ToString());//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可
                }
                catch (Exception ex)
                {
                    using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Qy_Error_" + DateTime.Now.Ticks + ".txt")))
                    {
                        tw.WriteLine("ExecptionMessage:" + ex.Message);
                        tw.WriteLine(ex.Source);
                        tw.WriteLine(ex.StackTrace);
                        //tw.WriteLine("InnerExecptionMessage:" + ex.InnerException.Message);

                        if (messageHandler.FinalResponseDocument != null)
                        {
                            tw.WriteLine(messageHandler.FinalResponseDocument.ToString());
                        }
                        tw.Flush();
                        tw.Close();
                    }
                }
                finally
                {
                    Response.End();
                }
            }
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }

        /// <summary>
        /// 最简单的Page_Load写法（本方法仅用于演示过程，未实际使用到）
        /// </summary>
        private void MiniProcess()
        {
            var postModel = new PostModel()
            {
                Msg_Signature = Request.QueryString["msg_signature"],
                Timestamp = Request.QueryString["timestamp"],
                Nonce = Request.QueryString["nonce"],
                //以下保密信息不会（不应该）在网络上传播，请注意
                Token = Token,
                EncodingAESKey = EncodingAESKey,
                CorpId = CorpId,
            };

            var maxRecordCount = 10;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new QyCustomMessageHandler(Request.InputStream, postModel, maxRecordCount);
            //执行微信处理过程
            messageHandler.Execute();
            //自动返回加密后结果
            WriteContent(messageHandler.FinalResponseDocument.ToString());//为了解决官方微信5.0软件换行bug暂时添加的方法，平时用下面一个方法即可

            Response.End();
        }
    }
}