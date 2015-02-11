using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 创建卡券接口
    /// </summary>
    public static class JSSDK
    {
        /// <summary>
        /// 创建卡券
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cardType">卡券类型</param>
        /// <param name="data">创建卡券需要的数据，格式可以看CardCreateData.cs</param>
        /// <returns></returns>
        public static CardCreateResultJson CreateCard(string accessToken, CardType cardType, object data)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/create?access_token={0}", accessToken);

            CardCreateInfo cardData = null;

            return CommonJsonSend.Send<CardCreateResultJson>(null, urlFormat, cardData);
        }

        /// <summary>
        /// 获取颜色列表接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetColorsResultJson GetColors(string accessToken)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/getcolors?access_token={0}", accessToken);

            return CommonJsonSend.Send<GetColorsResultJson>(null, urlFormat, null);
        }

        /// <summary>
        /// 生成卡券二维码
        /// 获取二维码ticket 后，开发者可用ticket 换取二维码图片。换取指引参考：http://mp.weixin.qq.com/wiki/index.php?title=生成带参数的二维码
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="code">指定卡券code 码，只能被领一次。use_custom_code 字段为true 的卡券必须填写，非自定义code 不必填写。</param>
        /// <param name="openId">指定领取者的openid，只有该用户能领取。bind_openid 字段为true 的卡券必须填写，非自定义openid 不必填写。</param>
        /// <param name="expireSeconds">指定二维码的有效时间，范围是60 ~ 1800 秒。不填默认为永久有效。</param>
        /// <param name="isUniqueCode">指定下发二维码，生成的二维码随机分配一个code，领取后不可再次扫描。填写true 或false。默认false。</param>
        /// <param name="balance">红包余额，以分为单位。红包类型必填（LUCKY_MONEY），其他卡券类型不填。</param>
        /// <returns></returns>
        public static CreateQRResultJson CreateQR(string accessToken, string cardId, string code = null, string openId = null, string expireSeconds = null, bool isUniqueCode = false, string balance = null)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/qrcode/create?access_token={0}", accessToken);

            var data = new
                {
                    action_name = "QR_CARD",
                    action_info = new
                    {
                        card = new
                        {
                            card_id = cardId,
                            code = code,
                            openid = openId,
                            expire_seconds = expireSeconds,
                            is_unique_code = false,
                            balance = balance
                        }
                    }
                };

            return CommonJsonSend.Send<CreateQRResultJson>(null, urlFormat, data);
        }

        /// <summary>
        /// 卡券消耗code
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="cardId">要消耗序列号所述的card_id，创建卡券时use_custom_code 填写true 时必填。非自定义code不必填写。</param>
        /// <returns></returns>
        public static CardConsumeResultJson CardConsume(string accessToken, string code, string cardId = null)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/code/consume?access_token={0}", accessToken);

            var data = new
            {
                code = code,
                card_id = cardId
            };

            return CommonJsonSend.Send<CardConsumeResultJson>(null, urlFormat, data);
        }

        /// <summary>
        /// code 解码接口
        /// code 解码接口支持两种场景：
        /// 1.商家获取choos_card_info 后，将card_id 和encrypt_code 字段通过解码接口，获取真实code。
        /// 2.卡券内跳转外链的签名中会对code 进行加密处理，通过调用解码接口获取真实code。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="encryptCode">通过choose_card_info 获取的加密字符串</param>
        /// <returns></returns>
        public static CardDecryptResultJson CardDecrypt(string accessToken, string encryptCode)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/code/decrypt?access_token={0}", accessToken);

            var data = new
            {
                encrypt_code = encryptCode,
            };

            return CommonJsonSend.Send<CardDecryptResultJson>(null, urlFormat, data);
        }
    }
}
