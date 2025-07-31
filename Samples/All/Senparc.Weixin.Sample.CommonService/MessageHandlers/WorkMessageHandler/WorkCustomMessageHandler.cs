/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkCustomMessageHandler.cs
    文件功能描述：自定义WorkMessageHandler
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar.Entities.Request;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Sample.CommonService.WorkMessageHandlers;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace Senparc.Weixin.Sample.CommonService.WorkMessageHandlers
{
    public partial class WorkCustomMessageHandler : WorkMessageHandler<WorkCustomMessageContext>
    {
        /// <summary>
        /// 为中间件提供生成当前类的委托
        /// </summary>
        public static Func<Stream, PostModel, int, IServiceProvider, WorkCustomMessageHandler> GenerateMessageHandler =
            (stream, postModel, maxRecordCount, serviceProvider) => new WorkCustomMessageHandler(stream, postModel, maxRecordCount, serviceProvider);

        readonly ISenparcWeixinSettingForWork _workSetting;

        public WorkCustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
            _workSetting = Senparc.Weixin.Config.SenparcWeixinSetting.WorkSetting;
        }

        public override async Task<IWorkResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {

            #region 查看是否需要进入 AI 对话

            var aiResponseMessage = await this.AIChatAsync(requestMessage);
            if (aiResponseMessage != null)
            {
                return aiResponseMessage;
            }

            #endregion

            var requestHandler = await requestMessage.StartHandler()
            .Keyword("AI",()=>this.StartAIChatAsync().Result)
            .Default(async()=>{
               var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
                responseMessage.Content = "您发送了消息：" + requestMessage.Content;

                //发送一条客服消息
                var weixinSetting = Config.SenparcWeixinSetting.WorkSetting;
                var appKey = AccessTokenContainer.BuildingKey(weixinSetting.WeixinCorpId, weixinSetting.WeixinCorpSecret);
                MassApi.SendText(appKey, weixinSetting.WeixinCorpAgentId, "这是一条客服消息，对应您发送的消息：" + requestMessage.Content, OpenId);

                return responseMessage;
            });

            return requestHandler.GetResponseMessage() as IWorkResponseMessageBase;
        }

        public override async Task<IWorkResponseMessageBase> OnImageRequestAsync(RequestMessageImage requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageImage>();
            responseMessage.Image.MediaId = requestMessage.MediaId;
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您刚发送的图片如下：";
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format("位置坐标 {0} - {1}", requestMessage.Latitude, requestMessage.Longitude);
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_EnterAgentRequest(RequestMessageEvent_Enter_Agent requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "欢迎进入应用！现在时间是：" + SystemTime.Now.DateTime.ToString();
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_ChangeContactUpdateUserRequest(RequestMessageEvent_Change_Contact_User_Update requestMessage)
        {
            //发送消息
            Senparc.Weixin.Work.AdvancedAPIs.ChatApi.SendChatSimpleMessage(AccessTokenContainer.BuildingKey(_workSetting.WeixinCorpId, _workSetting.WeixinCorpSecret), "001", Work.ChatMsgType.text, $"用户信息已被修改：{requestMessage.ToJson(true)}", 1);
            //Senparc.Weixin.Work.AdvancedAPIs.ChatApi.SendChatSimpleMessage(AccessTokenContainer.BuildingKey(_workSetting), "001", Work.ChatMsgType.text, $"用户信息已被修改：{requestMessage.ToJson(true)}", 1);

            return base.OnEvent_ChangeContactUpdateUserRequest(requestMessage);
        }

        public override Work.Entities.IWorkResponseMessageBase DefaultResponseMessage(Work.Entities.IWorkRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这是一条没有找到合适回复信息的默认消息。";
            return responseMessage;
        }
    }
}
