/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：CustomMessageHandler.cs
    文件功能描述：微信公众号自定义MessageHandler


    创建标识：Senparc - 20150312

    修改标识：Senparc - 20171027
    修改描述：v14.8.3 添加OnUnknownTypeRequest()方法Demo

    修改标识：Senparc - 20191002
    修改描述：v16.9.102 提供 MessageHandler 中间件

----------------------------------------------------------------*/

//DPBMARK_FILE MP
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Entities.Request;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageContexts;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.Sample.MP
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<DefaultMpMessageContext>  /*如果不需要自定义，可以直接使用：MessageHandler<DefaultMpMessageContext> */
    {
        /*
         * 重要提示：v1.5起，MessageHandler提供了一个DefaultResponseMessage的抽象方法，
         * DefaultResponseMessage必须在子类中重写，用于返回没有处理过的消息类型（也可以用于默认消息，如帮助信息等）；
         * 其中所有原OnXX的抽象方法已经都改为虚方法，可以不必每个都重写。若不重写，默认返回DefaultResponseMessage方法中的结果。
         */


        private string appId = Config.SenparcWeixinSetting.MpSetting.WeixinAppId;
        private string appSecret = Config.SenparcWeixinSetting.MpSetting.WeixinAppSecret;

        /// <summary>
        /// 为中间件提供生成当前类的委托
        /// </summary>
        public static Func<Stream, PostModel, int, IServiceProvider, CustomMessageHandler> GenerateMessageHandler = (stream, postModel, maxRecordCount, serviceProvider)
                         => new CustomMessageHandler(stream, postModel, maxRecordCount, false /* 是否只允许处理加密消息，以提高安全性 */, serviceProvider: serviceProvider);

        /// <summary>
        /// 自定义 MessageHandler
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="inputStream"></param>
        /// <param name="postModel"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="onlyAllowEncryptMessage"></param>
        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider: serviceProvider)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalGlobalMessageContext.ExpireMinutes = 3。
            GlobalMessageContext.ExpireMinutes = 3;

            OnlyAllowEncryptMessage = true;//是否只允许接收加密消息，默认为 false
        }


        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {
            var defaultResponseMessage = base.CreateResponseMessage<ResponseMessageText>();

            // 下方 RequestHandler 为消息关键字提供了便捷的处理方式，当然您也可以使用传统的 if...else... 对 requestMessage.Content 进行判断
            var requestHandler = await requestMessage.StartHandler()
                //关键字不区分大小写，按照顺序匹配成功后将不再运行下面的逻辑
                .Keyword("关键字1", () =>
                {
                    defaultResponseMessage.Content = @"收到关键字1";
                    return defaultResponseMessage;
                }).
                //匹配任一关键字
                Keywords(new[] { "关键字2", "关键字3" }, () =>
                {
                    defaultResponseMessage.Content = @"收到“关键字2”或“关键字3”";
                    return defaultResponseMessage;
                })
                .Keyword("OPENID", () =>
                {
                    var openId = requestMessage.FromUserName;//获取OpenId
                    var userInfo = Weixin.MP.AdvancedAPIs.UserApi.Info(appId, openId, Language.zh_CN);

                    defaultResponseMessage.Content = string.Format(
                        "您的OpenID为：{0}\r\n昵称：{1}\r\n性别：{2}\r\n地区（国家/省/市）：{3}/{4}/{5}\r\n关注时间：{6}\r\n关注状态：{7}\r\n\r\n说明：从2021年12月27日起，公众号无法直接获取用户昵称、性别、地区等信息，如需获取相关信息，需要使用<a href=\"https://sdk.weixin.senparc.com/oauth2?returnUrl=%2FOAuth2%2FTestReturnUrl\">OAuth 2.0 接口</a>。",
                        requestMessage.FromUserName, userInfo.nickname, (WeixinSex)userInfo.sex, userInfo.country, userInfo.province, userInfo.city, DateTimeHelper.GetDateTimeFromXml(userInfo.subscribe_time), userInfo.subscribe);
                    return defaultResponseMessage;
                })
                .Keyword("MUTE", () => //不回复任何消息
                {
                    //方案一：
                    return new SuccessResponseMessage();

                    //方案二：
                    var muteResponseMessage = base.CreateResponseMessage<ResponseMessageNoResponse>();
                    return muteResponseMessage;

                    //方案三：
                    base.TextResponseMessage = "success";
                    return null;

                    //方案四：
                    return null;//在 Action 中结合使用 return new FixWeixinBugWeixinResult(messageHandler);
                })
                //选择菜单，关键字：101（微信服务器端最终格式：id="s:101",content="满意"）——需要对应配置
                .SelectMenuKeyword("101", () =>
                {
                    defaultResponseMessage.Content = $"感谢您的评价（{requestMessage.Content}）！我们会一如既往为提高企业和开发者生产力而努力！";
                    return defaultResponseMessage;
                })
                //正则表达式
                .Regex(@"^\d+#\d+$", () =>
                {
                    defaultResponseMessage.Content = string.Format("您输入了：{0}，符合正则表达式：^\\d+#\\d+$", requestMessage.Content);
                    return defaultResponseMessage;
                })
                //当 Default 使用异步方法时，需要写在最后一个，且 requestMessage.StartHandler() 前需要使用 await 等待异步方法执行；
                //当 Default 使用同步方法，不一定要在最后一个,并且不需要使用 await
                .Default(async () =>
                {
                    defaultResponseMessage.Content = $"您刚才发送了文字信息：{requestMessage.Content}";
                    return defaultResponseMessage;
                });

            return requestHandler.GetResponseMessage() as IResponseMessageBase;
        }

        /// <summary>
        /// 处理位置请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnLocationRequestAsync(RequestMessageLocation requestMessage)
        {
            var locationService = new LocationService();
            var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            return responseMessage;
        }

        public override async Task<IResponseMessageBase> OnShortVideoRequestAsync(RequestMessageShortVideo requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您刚才发送的是小视频";
            return responseMessage;
        }

        /// <summary>
        /// 处理图片请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnImageRequestAsync(RequestMessageImage requestMessage)
        {
            //一隔一返回News或Image格式
            if (base.GlobalMessageContext.GetMessageContext(requestMessage).RequestMessages.Count() % 2 == 0)
            {
                var responseMessage = CreateResponseMessage<ResponseMessageNews>();

                responseMessage.Articles.Add(new Article()
                {
                    Title = "您刚才发送了图片信息",
                    Description = "您发送的图片将会显示在边上",
                    PicUrl = requestMessage.PicUrl,
                    Url = "https://sdk.weixin.senparc.com"
                });
                responseMessage.Articles.Add(new Article()
                {
                    Title = "第二条",
                    Description = "第二条带连接的内容",
                    PicUrl = requestMessage.PicUrl,
                    Url = "https://sdk.weixin.senparc.com"
                });

                return responseMessage;
            }
            else
            {
                var responseMessage = CreateResponseMessage<ResponseMessageImage>();
                responseMessage.Image.MediaId = requestMessage.MediaId;
                return responseMessage;
            }
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnVoiceRequestAsync(RequestMessageVoice requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageMusic>();
            //上传缩略图
            //var accessToken = Containers.AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var uploadResult = Weixin.MP.AdvancedAPIs.MediaApi.UploadTemporaryMedia(appId, UploadMediaFileType.image,
                                                         ServerUtility.ContentRootMapPath("~/Images/Logo.jpg"));

            //设置音乐信息
            responseMessage.Music.Title = "天籁之音";
            responseMessage.Music.Description = "播放您上传的语音";
            responseMessage.Music.MusicUrl = "https://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            responseMessage.Music.HQMusicUrl = "https://sdk.weixin.senparc.com/Media/GetVoice?mediaId=" + requestMessage.MediaId;
            responseMessage.Music.ThumbMediaId = uploadResult.media_id;

            //推送一条客服消息
            try
            {
                CustomApi.SendText(appId, OpenId, "本次上传的音频MediaId：" + requestMessage.MediaId);
            }
            catch
            {
            }

            return responseMessage;
        }
        /// <summary>
        /// 处理视频请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnVideoRequestAsync(RequestMessageVideo requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了一条视频信息，ID：" + requestMessage.MediaId;

            #region 上传素材并推送到客户端

            _ = Task.Factory.StartNew(async () =>
               {
                   //上传素材
                   var dir = ServerUtility.ContentRootMapPath("~/App_Data/TempVideo/");
                   var file = await MediaApi.GetAsync(appId, requestMessage.MediaId, dir);
                   var uploadResult = await MediaApi.UploadTemporaryMediaAsync(appId, UploadMediaFileType.video, file, 50000);
                   await CustomApi.SendVideoAsync(appId, base.OpenId, uploadResult.media_id, "这是您刚才发送的视频", "这是一条视频消息");
               }).ContinueWith(async task =>
               {
                   if (task.Exception != null)
                   {
                       WeixinTrace.Log("OnVideoRequest()储存Video过程发生错误：", task.Exception.Message);

                       var msg = string.Format("上传素材出错：{0}\r\n{1}",
                                  task.Exception.Message,
                                  task.Exception.InnerException != null
                                      ? task.Exception.InnerException.Message
                                      : null);
                       await CustomApi.SendTextAsync(appId, base.OpenId, msg);
                   }
               });

            #endregion

            return responseMessage;
        }


        /// <summary>
        /// 处理链接消息请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnLinkRequestAsync(RequestMessageLink requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content = string.Format(@"您发送了一条连接信息：
Title：{0}
Description:{1}
Url:{2}", requestMessage.Title, requestMessage.Description, requestMessage.Url);
            return responseMessage;
        }

        public override async Task<IResponseMessageBase> OnFileRequestAsync(RequestMessageFile requestMessage)
        {
            var responseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format(@"您发送了一个文件：
文件名：{0}
说明:{1}
大小：{2}
MD5:{3}", requestMessage.Title, requestMessage.Description, requestMessage.FileTotalLen, requestMessage.FileMd5);
            return responseMessage;
        }

        /// <summary>
        /// 处理事件请求（这个方法一般不用重写，这里仅作为示例出现。除非需要在判断具体Event类型以外对Event信息进行统一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override async Task<IResponseMessageBase> OnEventRequestAsync(IRequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = await base.OnEventRequestAsync(requestMessage);//对于Event下属分类的重写方法，见：CustomerMessageHandler_Events.cs
                                                                                      //TODO: 对Event信息进行统一操作
            return eventResponseMessage;
        }

        public override async Task<IResponseMessageBase> DefaultResponseMessageAsync(IRequestMessageBase requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            /* 所有没有被处理的消息会默认返回这里的结果，
            * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
            * 只需要在这里统一发出委托请求，如：
            * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
            * return responseMessage;
            */

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"这条消息来自DefaultResponseMessage。\r\n您收到这条消息，表明该公众号没有对【{requestMessage.MsgType}】类型做处理。";
            return responseMessage;
        }


        public override async Task<IResponseMessageBase> OnUnknownTypeRequestAsync(RequestMessageUnknownType requestMessage)
        {
            /*
             * 此方法用于应急处理SDK没有提供的消息类型，
             * 原始XML可以通过requestMessage.RequestDocument（或this.RequestDocument）获取到。
             * 如果不重写此方法，遇到未知的请求类型将会抛出异常（v14.8.3 之前的版本就是这么做的）
             */
            var msgType = Senparc.NeuChar.Helpers.MsgTypeHelper.GetRequestMsgTypeString(requestMessage.RequestDocument);
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "未知消息类型：" + msgType;

            WeixinTrace.SendCustomLog("未知请求消息类型", requestMessage.RequestDocument.ToString());//记录到日志中

            return responseMessage;
        }

        #region 重写执行过程

        public override async Task OnExecutingAsync(CancellationToken cancellationToken)
        {
            //演示：MessageContext.StorageData

            var currentMessageContext = await base.GetUnsafeMessageContext();//为了在分布式缓存下提高读写效率，使用此方法，如果需要获取实时数据，应该使用 base.GetCurrentMessageContext()
            if (currentMessageContext.StorageData == null || !(currentMessageContext.StorageData is int))
            {
                currentMessageContext.StorageData = (int)0;
                //await GlobalMessageContext.UpdateMessageContextAsync(currentMessageContext);//储存到缓存
            }
            await base.OnExecutingAsync(cancellationToken);
        }

        public override async Task OnExecutedAsync(CancellationToken cancellationToken)
        {
            //演示：MessageContext.StorageData

            var currentMessageContext = await base.GetUnsafeMessageContext();//为了在分布式缓存下提高读写效率，使用此方法，如果需要获取实时数据，应该使用 base.GetCurrentMessageContext()
            currentMessageContext.StorageData = ((int)currentMessageContext.StorageData) + 1;
            GlobalMessageContext.UpdateMessageContext(currentMessageContext);//储存到缓存
            await base.OnExecutedAsync(cancellationToken);
        }

        #endregion
    }
}
