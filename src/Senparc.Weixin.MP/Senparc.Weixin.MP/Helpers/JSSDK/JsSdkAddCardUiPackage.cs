using Senparc.Weixin.MP.Containers;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;

namespace Senparc.Weixin.MP.Helpers.JSSDK
{
    /// <summary>
    /// jsSDK 卡券AddCard api调用的参数
    /// </summary>
    public class JsSdkAddCardUiPackage
    {
        /// <summary>
        /// 微信AppId
        /// </summary>
        public string AppId
        {
            get; set;
        }

        /// <summary>
        /// 卡ID
        /// </summary>
        public string CardId
        {
            get; set;
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp
        {
            get; set;
        }

        /// <summary>
        /// 随机码
        /// </summary>
        public string NonceStr
        {
            get; set;
        }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature
        {
            get; set;
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string Code
        {
            get; set;
        }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string OpenId
        {
            get; set;
        }
        /// <summary>
        /// 创建UI参数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="cardId"></param>
        /// <param name="code"></param>
        /// <param name="openId"></param>
        public JsSdkAddCardUiPackage(string appId,
                                     string appSecret,
                                     string cardId,
                                     string code = "",
                                     string openId = "")
        {
            AppId = appId;
            CardId = cardId;
            Timestamp = JSSDKHelper.GetTimestamp();
            NonceStr = JSSDKHelper.GetNoncestr();
            string ticket = WxCardApiTicketContainer.TryGetWxCardApiTicket(appId, appSecret);
            Signature = JSSDKHelper.GetcardExtSign(ticket, Timestamp, cardId, NonceStr);
            Code = code;
            OpenId = openId;
        }

        /// <summary>
        /// 生成卡cardExt的json字符串。
        /// </summary>
        /// <returns></returns>
        public string GetCardExtJsonString()
        {
            var cardExt = new
            {
                timestamp = Timestamp,
                signature = Signature,
                nonce_str = string.IsNullOrEmpty(NonceStr) ? null : NonceStr,
                openid = string.IsNullOrEmpty(OpenId) ? null : OpenId,
                code = string.IsNullOrEmpty(Code) ? null : Code
            };

            JsonSetting jsonSetting = new JsonSetting(true);
            return SerializerHelper.GetJsonString(cardExt, jsonSetting);
        }
    }
}
