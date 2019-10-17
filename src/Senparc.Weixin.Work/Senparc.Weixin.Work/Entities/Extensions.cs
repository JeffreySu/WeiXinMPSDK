/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：Extensions.cs
    文件功能描述：将RequestMessageEventBase转换成RequestMessageText类型，其中Content = requestMessage.EventKey
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/


namespace Senparc.Weixin.Work.Entities
{
    public static class Extensions
    {
        /// <summary>
        /// 将RequestMessageEventBase转换成RequestMessageText类型，其中Content = requestMessage.EventKey
        /// </summary>
        /// <param name="requestMessageEvent"></param>
        /// <returns></returns>
        public static RequestMessageText ConvertToRequestMessageText(this IRequestMessageEventBase requestMessageEvent)
        {
            var requestMessage = requestMessageEvent;
            var requestMessageText = new RequestMessageText()
            {
                FromUserName = requestMessage.FromUserName,
                ToUserName = requestMessage.ToUserName,
                CreateTime = requestMessage.CreateTime,
                MsgId = requestMessage.MsgId
            };

            //判断是否具有EventKey属性
            if (requestMessageEvent is IRequestMessageEventKey)
            {
                requestMessageText.Content = (requestMessageEvent as IRequestMessageEventKey).EventKey;
            }
            else
            {
                requestMessageText.Content = "";
            }

            return requestMessageText;
        }
    }
}
