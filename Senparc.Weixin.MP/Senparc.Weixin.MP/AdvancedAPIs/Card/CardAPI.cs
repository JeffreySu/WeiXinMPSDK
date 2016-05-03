/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：CardApi.cs
    文件功能描述：卡券高级功能API


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150212
    修改描述：整理接口
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：Senparc - 20150512
    修改描述：门店接口过期处理

    修改标识：Senparc - 20150717
    修改描述：增加获取用户已领取卡券、修改库存接口

    修改标识：hello2008zj - 20160422
    修改描述：修改CreateQR接口，匹配最新的文档
----------------------------------------------------------------*/

/*
   API地址：http://mp.weixin.qq.com/wiki/9/d8a5f3b102915f30516d79b44fe665ed.html
   PDF下载：https://mp.weixin.qq.com/zh_CN/htmledition/comm_htmledition/res/cardticket/wx_card_document.zip
*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.Card;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 卡券接口
    /// </summary>
    public static class CardApi
    {
        /// <summary>
        /// 创建卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardInfo">创建卡券需要的数据，格式可以看CardCreateData.cs</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardCreateResultJson CreateCard(string accessTokenOrAppId, BaseCardInfo cardInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/create?access_token={0}", accessToken.AsUrlData());

                CardCreateInfo cardData = null;
                CardType cardType = cardInfo.GetCardType();

                switch (cardType)
                {
                    case CardType.GENERAL_COUPON:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_GeneralCoupon()
                            {
                                card_type = cardType.ToString(),
                                general_coupon = cardInfo as Card_GeneralCouponData
                            }
                        };
                        break;
                    case CardType.GROUPON:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_Groupon()
                            {
                                card_type = cardType.ToString(),
                                groupon = cardInfo as Card_GrouponData
                            }
                        };
                        break;
                    case CardType.GIFT:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_Gift()
                            {
                                card_type = cardType.ToString(),
                                gift = cardInfo as Card_GiftData
                            }
                        };
                        break;
                    case CardType.CASH:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_Cash()
                            {
                                card_type = cardType.ToString(),
                                cash = cardInfo as Card_CashData
                            }
                        };
                        break;
                    case CardType.DISCOUNT:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_DisCount()
                            {
                                card_type = cardType.ToString(),
                                discount = cardInfo as Card_DisCountData
                            }
                        };
                        break;
                    case CardType.MEMBER_CARD:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_MemberCard()
                            {
                                card_type = cardType.ToString(),
                                member_card = cardInfo as Card_MemberCardData
                            }
                        };
                        break;
                    case CardType.SCENIC_TICKET:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_ScenicTicket()
                            {
                                card_type = cardType.ToString(),
                                scenic_ticket = cardInfo as Card_ScenicTicketData
                            }
                        };
                        break;
                    case CardType.MOVIE_TICKET:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_MovieTicket()
                            {
                                card_type = cardType.ToString(),

                                movie_ticket = cardInfo as Card_MovieTicketData
                            }
                        };
                        break;
                    case CardType.BOARDING_PASS:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_BoardingPass()
                            {
                                card_type = cardType.ToString(),
                                boarding_pass = cardInfo as Card_BoardingPassData
                            }
                        };
                        break;
                    case CardType.LUCKY_MONEY:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_LuckyMoney()
                            {
                                card_type = cardType.ToString(),
                                lucky_money = cardInfo as Card_LuckyMoneyData
                            }
                        };
                        break;
                    case CardType.MEETING_TICKET:
                        cardData = new CardCreateInfo()
                        {
                            card = new Card_MeetingTicket()
                            {
                                card_type = cardType.ToString(),
                                meeting_ticket = cardInfo as Card_MeetingTicketData
                            }
                        };
                        break;
                    default:
                        break;
                }

                var jsonSetting = new JsonSetting(true, null,
                    new List<Type>()
                    {
                        //typeof (Modify_Msg_Operation),
                        //typeof (CardCreateInfo),
                        typeof (Card_BaseInfoBase)//过滤Modify_Msg_Operation主要起作用的是这个
                    });

                var result = CommonJsonSend.Send<CardCreateResultJson>(null, urlFormat, cardData, timeOut: timeOut,
                   //针对特殊字段的null值进行过滤
                   jsonSetting: jsonSetting);
                return result;

            }, accessTokenOrAppId);
        }

        ///// <summary>
        ///// 此接口已取消，微信直接提供了十四种色值供选择，详见：http://mp.weixin.qq.com/wiki/8/b7e310e7943f7763450eced91fa793b0.html#.E5.8D.A1.E5.88.B8.E5.9F.BA.E7.A1.80.E4.BF.A1.E6.81.AF.E5.AD.97.E6.AE.B5.EF.BC.88.E9.87.8D.E8.A6.81.EF.BC.89
        ///// 获取颜色列表接口
        ///// </summary>
        ///// <param name="accessTokenOrAppId"></param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>


        /// <summary>
        /// 生成卡券二维码
        /// 获取二维码ticket 后，开发者可用ticket 换取二维码图片。换取指引参考：http://mp.weixin.qq.com/wiki/index.php?title=生成带参数的二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="code">指定卡券code 码，只能被领一次。use_custom_code 字段为true 的卡券必须填写，非自定义code 不必填写。</param>
        /// <param name="openId">指定领取者的openid，只有该用户能领取。bind_openid 字段为true 的卡券必须填写，非自定义openid 不必填写。</param>
        /// <param name="expireSeconds">指定二维码的有效时间，范围是60 ~ 1800 秒。不填默认为永久有效。</param>
        /// <param name="isUniqueCode">指定下发二维码，生成的二维码随机分配一个code，领取后不可再次扫描。填写true 或false。默认false。</param>
        /// <param name="balance">红包余额，以分为单位。红包类型必填（LUCKY_MONEY），其他卡券类型不填。</param>
        /// <param name="outer_id">自定义应用场景ID（v13.7.3起支持）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateQRResultJson CreateQR(string accessTokenOrAppId, string cardId, string code = null,
            string openId = null, string expireSeconds = null,
            bool isUniqueCode = false, string balance = null, string outer_id = null,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/qrcode/create?access_token={0}", accessToken.AsUrlData());

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

                //var jsonSettingne = new JsonSetting(true);

                var jsonSetting = new JsonSetting(true, null,
                                      new List<Type>()
                                      {
                                            //typeof (Modify_Msg_Operation),
                                            //typeof (CardCreateInfo),
                                            data.action_info.card.GetType()//过滤Modify_Msg_Operation主要起作用的是这个
                                      });

                return CommonJsonSend.Send<CreateQRResultJson>(null, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建货架
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ShelfCreateResultJson ShelfCreate(string accessTokenOrAppId, ShelfCreateData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/landingpage/create?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<ShelfCreateResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 导入code
        ///
        /// 新创建卡券情况
        /// 如果开发者打算新创建一张支持导入code模式的卡券，不同于以往的创建方式，建议开发者采用以下流程创建预存code模式卡券，否则会报错。
        /// 步骤一：创建预存模式卡券，将库存quantity初始值设置为0，并填入Deposit_Mode字段；
        /// 步骤二：待卡券通过审核后，调用导入code接口并核查code；
        /// 步骤三：调用修改库存接口，须令卡券库存小于或等于导入code的数目。（为了避免混乱建议设置为相等）
        ///
        /// 注： 1）单次调用接口传入code的数量上限为100个。
        /// 2）每一个 code 均不能为空串。
        /// 3）导入结束后系统会自动判断提供方设置库存与实际导入code的量是否一致。
        /// 4）导入失败支持重复导入，提示成功为止。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">需要进行导入code的卡券ID</param>
        /// <param name="codeList">需导入微信卡券后台的自定义code，上限为100个。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult CodeDeposit(string accessTokenOrAppId, string cardId, string[] codeList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("http://api.weixin.qq.com/card/code/deposit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    code = codeList
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询导入code数目
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">进行导入code的卡券ID。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetDepositCountResultJson GetDepositCount(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("http://api.weixin.qq.com/card/code/getdepositcount?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                };

                return CommonJsonSend.Send<GetDepositCountResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 核查code
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">进行导入code的卡券ID。</param>
        /// <param name="codeList">已经微信卡券后台的自定义code，上限为100个。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CheckCodeResultJson CheckCode(string accessTokenOrAppId, string cardId, string[] codeList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("http://api.weixin.qq.com/card/code/checkcode?access_token={0}", accessToken);

                var data = new
                {
                    card_id = cardId,
                    code = codeList
                };

                return CommonJsonSend.Send<CheckCodeResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 图文消息群发卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetHtmlResult GetHtml(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/mpnews/gethtml?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                };

                return CommonJsonSend.Send<GetHtmlResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 卡券消耗code
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="cardId">要消耗序列号所述的card_id，创建卡券时use_custom_code 填写true 时必填。非自定义code不必填写。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardConsumeResultJson CardConsume(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/code/consume?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId
                };

                return CommonJsonSend.Send<CardConsumeResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// code 解码接口
        /// code 解码接口支持两种场景：
        /// 1.商家获取choos_card_info 后，将card_id 和encrypt_code 字段通过解码接口，获取真实code。
        /// 2.卡券内跳转外链的签名中会对code 进行加密处理，通过调用解码接口获取真实code。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="encryptCode">通过choose_card_info 获取的加密字符串</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardDecryptResultJson CardDecrypt(string accessTokenOrAppId, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/code/decrypt?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    encrypt_code = encryptCode,
                };

                return CommonJsonSend.Send<CardDecryptResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardDeleteResultJson CardDelete(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/delete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId
                };

                return CommonJsonSend.Send<CardDeleteResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询code接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code"></param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardGetResultJson CardGet(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/code/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId
                };

                return CommonJsonSend.Send<CardGetResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 批量查询卡列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="offset">查询卡列表的起始偏移量，从0 开始，即offset: 5 是指从从列表里的第六个开始读取。</param>
        /// <param name="count">需要查询的卡片的数量（数量最大50）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardBatchGetResultJson CardBatchGet(string accessTokenOrAppId, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/batchget?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    offset = offset,
                    count = count
                };

                return CommonJsonSend.Send<CardBatchGetResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询卡券详情
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardDetailGetResultJson CardDetailGet(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId
                };

                return CommonJsonSend.Send<CardDetailGetResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更改code
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">卡券的code 编码</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="newCode">新的卡券code 编码</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CardChangeCode(string accessTokenOrAppId, string code, string cardId, string newCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/code/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    new_code = newCode
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置卡券失效接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">需要设置为失效的code</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 的卡券不填。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CardUnavailable(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/code/unavailable?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更改卡券信息接口
        /// 支持更新部分通用字段及特殊卡券（会员卡、飞机票、电影票、红包）中特定字段的信息。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardType">卡券种类，会员卡、飞机票、电影票、红包中的一种</param>
        /// <param name="data">创建卡券需要的数据，格式可以看CardUpdateData.cs</param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CardUpdate(string accessTokenOrAppId, CardType cardType, object data, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/update?access_token={0}", accessToken.AsUrlData());

                BaseCardUpdateInfo cardData = null;

                switch (cardType)
                {
                    case CardType.MEMBER_CARD:
                        cardData = new CardUpdate_MemberCard()
                        {
                            card_id = cardId,
                            member_card = data as Card_MemberCardUpdateData
                        };
                        break;
                    case CardType.BOARDING_PASS:
                        cardData = new CardUpdate_BoardingPass()
                        {
                            card_id = cardId,
                            boarding_pass = data as Card_BoardingPassData
                        };
                        break;
                    case CardType.MOVIE_TICKET:
                        cardData = new CardUpdate_MovieTicket()
                        {
                            card_id = cardId,
                            movie_ticket = data as Card_MovieTicketData
                        };
                        break;
                    case CardType.SCENIC_TICKET:
                        cardData = new CardUpdate_ScenicTicket()
                        {
                            card_id = cardId,
                            scenic_ticket = data as Card_ScenicTicketData
                        };
                        break;
                    default:
                        break;
                }

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, cardData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置测试用户白名单
        /// 由于卡券有审核要求，为方便公众号调试，可以设置一些测试帐号，这些帐号可以领取未通过审核的卡券，体验整个流程。
        ///注：同时支持“openid”、“username”两种字段设置白名单，总数上限为10 个。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openIds">测试的openid 列表</param>
        /// <param name="userNames">测试的微信号列表</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult AuthoritySet(string accessTokenOrAppId, string[] openIds, string[] userNames, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/testwhitelist/set?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    openid = openIds,
                    username = userNames
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        ///  <summary>
        ///  激活/绑定会员卡
        ///  </summary>
        ///  <param name="accessTokenOrAppId"></param>
        ///  <param name="membershipNumber">必填，会员卡编号，作为序列号显示在用户的卡包里。</param>
        ///  <param name="code">创建会员卡时获取的code</param>
        ///  <param name="activateEndTime">激活后的有效截至时间。若不填写默认以创建时的 data_info 为准。Unix时间戳格式。</param>
        ///  <param name="initBonus">初始积分，不填为0</param>
        ///  <param name="initBalance">初始余额，不填为0</param>
        ///  <param name="initCustomFieldValue1">创建时字段custom_field1定义类型的初始值，限制为4个汉字，12字节。</param>
        ///  <param name="initCustomFieldValue2">创建时字段custom_field2定义类型的初始值，限制为4个汉字，12字节。</param>
        ///  <param name="initCustomFieldValue3">创建时字段custom_field3定义类型的初始值，限制为4个汉字，12字节。</param>
        ///  <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="cardId"></param>
        /// <param name="activateBeginTime">激活后的有效起始时间。若不填写默认以创建时的 data_info 为准。Unix时间戳格式。</param>
        ///  <returns></returns>
        public static WxJsonResult MemberCardActivate(string accessTokenOrAppId, string membershipNumber, string code, string cardId, string activateBeginTime = null, string activateEndTime = null, string initBonus = null,
            string initBalance = null, string initCustomFieldValue1 = null, string initCustomFieldValue2 = null, string initCustomFieldValue3 = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/membercard/activate?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    init_bonus = initBonus,
                    init_balance = initBalance,
                    membership_number = membershipNumber,
                    code = code,
                    card_id = cardId,
                    activate_begin_time = activateBeginTime,
                    activate_end_time = activateEndTime,
                    init_custom_field_value1 = initCustomFieldValue1,
                    init_custom_field_value2 = initCustomFieldValue2,
                    init_custom_field_value3 = initCustomFieldValue3,
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置开卡字段接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ActivateUserFormSet(string accessTokenOrAppId, ActivateUserFormSetData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/membercard/activateuserform/set?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 拉取会员信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">CardID</param>
        /// <param name="code">Code</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UserinfoGetResult UserinfoGet(string accessTokenOrAppId, string cardId, string code, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/membercard/userinfo/get?access_token={0}", accessToken);

                return CommonJsonSend.Send<UserinfoGetResult>(null, urlFormat, new { card_id = cardId, code = code }, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 设置跟随推荐接口
        /// 有 使用消息配置卡券（cardCellData） 和 使用消息配置URL（urlCellData） 两种方式
        /// 注意：cardCellData和urlCellData必须也只能选择一个，不可同时为空
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="cardCellData">使用消息配置卡券数据</param>
        /// <param name="urlCellData">使用消息配置URL数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult RecommendSet(string accessTokenOrAppId, string cardId, CardCell cardCellData = null, UrlCell urlCellData = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    member_card = new
                    {
                        base_info = new
                        {
                            modify_msg_operation = new
                            {
                                card_cell = cardCellData,
                                url_cell = urlCellData
                            }
                        }
                    }
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置微信买单接口
        /// 注意：在调用买单接口之前，请开发者务必确认是否已经开通了微信支付以及对相应的cardid设置了门店，否则会报错
        /// 错误码，0为正常；43008为商户没有开通微信支付权限
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="isOpen">是否开启买单功能，填true/false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult PayCellSet(string accessTokenOrAppId, string cardId, bool isOpen, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/paycell/set?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    is_open = isOpen
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新会员信息
        /// </summary>
        ///  post数据：
        /// 可以传入积分、余额的差值
        /// {
        ///  "code": "12312313",
        ///  "card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
        ///  "record_bonus": "消费30元，获得3积分",
        ///  "add_bonus": 3,//可以传入积分增减的差值
        ///  "add_balance": -3000,//可以传入余额本次增减的差值
        ///  "record_balance": "购买焦糖玛琪朵一杯，扣除金额30元。",
        ///  "custom_field_value1": "xxxxx",
        /// }
        /// 或者直接传入积分、余额的全量值
        ///
        /// {
        ///  "code": "12312313",
        ///  "card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
        ///  "record_bonus": "消费30元，获得3积分",
        ///  "bonus": 3000,//可以传入第三方系统记录的积分全量值
        ///  "balance": 3000,//可以传入第三方系统记录的余额全量值
        ///  "record_balance": "购买焦糖玛琪朵一杯，扣除金额30元。",
        ///  "custom_field_value1": "xxxxx",
        /// }
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">卡券Code码。</param>
        /// <param name="cardId">卡券ID。</param>
        /// <param name="addBonus">需要变更的积分，扣除积分用“-“表示。</param>
        /// <param name="addBalance">需要变更的余额，扣除金额用“-”表示。单位为分。</param>
        /// <param name="bonus">需要设置的积分全量值，传入的数值会直接显示，如果同时传入add_bonus和bonus,则前者无效。</param>
        /// <param name="balance">需要设置的余额全量值，传入的数值会直接显示，如果同时传入add_balance和balance,则前者无效。</param>
        /// <param name="recordBonus">商家自定义积分消耗记录，不超过14个汉字。</param>
        /// <param name="recordBalance">商家自定义金额消耗记录，不超过14个汉字。</param>
        /// <param name="customFieldValue1">创建时字段custom_field1定义类型的最新数值，限制为4个汉字，12字节。</param>
        /// <param name="customFieldValue2">创建时字段custom_field2定义类型的最新数值，限制为4个汉字，12字节。</param>
        /// <param name="customFieldValue3">创建时字段custom_field3定义类型的最新数值，限制为4个汉字，12字节。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UpdateUserResult UpdateUser(string accessTokenOrAppId, string code, string cardId, int addBonus, int addBalance,
            int? bonus = null, int? balance = null, string recordBonus = null, string recordBalance = null, string customFieldValue1 = null,
            string customFieldValue2 = null, string customFieldValue3 = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/membercard/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    add_bonus = addBonus,
                    bonus = bonus,
                    record_bonus = recordBonus,
                    add_balance = addBalance,
                    balance = balance,
                    record_balance = recordBalance,
                    custom_field_value1 = customFieldValue1,
                    custom_field_value2 = customFieldValue2,
                    custom_field_value3 = customFieldValue3,
                };

                return CommonJsonSend.Send<UpdateUserResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 会员卡交易
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="cardId">要消耗序列号所述的card_id。自定义code 的会员卡必填</param>
        /// <param name="recordBonus">商家自定义积分消耗记录，不超过14 个汉字</param>
        /// <param name="addBonus">需要变更的积分，扣除积分用“-“表</param>
        /// <param name="addBalance">需要变更的余额，扣除金额用“-”表示。单位为分</param>
        /// <param name="recordBalance">商家自定义金额消耗记录，不超过14 个汉字</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MemberCardDeal MemberCardDeal(string accessTokenOrAppId, string code, string cardId, string recordBonus, decimal addBonus, decimal addBalance, string recordBalance, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/membercard/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    record_bonus = recordBonus,
                    add_bonus = addBonus,
                    add_balance = addBalance,
                    record_balance = recordBalance,
                };

                return CommonJsonSend.Send<MemberCardDeal>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新电影票
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">电影票的序列号</param>
        /// <param name="cardId">电影票card_id。自定义code 的电影票为必填，非自定义code 的电影票不必填。</param>
        /// <param name="ticketClass">电影票的类别，如2D、3D</param>
        /// <param name="showTime">电影放映时间对应的时间戳</param>
        /// <param name="duration">放映时长，填写整数</param>
        /// <param name="screeningRoom">该场电影的影厅信息</param>
        /// <param name="seatNumbers">座位号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult MovieCardUpdate(string accessTokenOrAppId, string code, string cardId, string ticketClass, string showTime, int duration, string screeningRoom, string[] seatNumbers, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/movieticket/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    ticket_class = ticketClass,
                    show_time = showTime,
                    duration = duration,
                    screening_room = screeningRoom,
                    seat_number = seatNumbers
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 飞机票在线选座
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">飞机票的序列</param>
        /// <param name="cardId">需办理值机的机票card_id。自定义code 的飞机票为必</param>
        /// <param name="passengerName">乘客姓名，上限为15 个汉字</param>
        /// <param name="classType">舱等，如头等舱等，上限为5 个汉字</param>
        /// <param name="seat">乘客座位号</param>
        /// <param name="etktBnr">电子客票号，上限为14 个数字</param>
        /// <param name="qrcodeData">二维码数据。乘客用于值机的二维码字符串，微信会通过此数据为用户生成值机用的二维码</param>
        /// <param name="isCancel">是否取消值机。填写true 或false。true 代表取消，如填写true 上述字段（如calss 等）均不做判断，机票返回未值机状态，乘客可重新值机。默认填写false</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult BoardingPassCheckIn(string accessTokenOrAppId, string code, string cardId, string passengerName, string classType, string seat, string etktBnr, string qrcodeData, bool isCancel = false, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/boardingpass/checkin?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    passenger_name = passengerName,
                    @class = classType,
                    seat = seat,
                    etkt_bnr = etktBnr,
                    qrcode_data = qrcodeData,
                    is_cancel = isCancel
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新红包金额
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">红包的序列号</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 可不填。</param>
        /// <param name="balance">红包余额</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult UpdateUserBalance(string accessTokenOrAppId, string code, string cardId, decimal balance, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/luckymoney/updateuserbalance?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    balance = balance
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新会议门票接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="code">用户的门票唯一序列号</param>
        /// <param name="cardId">要更新门票序列号所述的card_id ， 生成券时use_custom_code 填写true 时必填。</param>
        /// <param name="zone">区域</param>
        /// <param name="entrance">入口</param>
        /// <param name="seatNumber">座位号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult UpdateMeetingTicket(string accessTokenOrAppId, string code, string cardId = null, string zone = null, string entrance = null, string seatNumber = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/meetingticket/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    zone = zone,
                    entrance = entrance,
                    seat_number = seatNumber
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取用户已领取卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId">需要查询的用户openid</param>
        /// <param name="cardId">卡券ID。不填写时默认查询当前appid下的卡券。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetCardListResult GetCardList(string accessTokenOrAppId, string openId, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/user/getcardlist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    openid = openId,
                    card_id = cardId,
                };

                return CommonJsonSend.Send<GetCardListResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改库存接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="increaseStockValue">增加多少库存，支持不填或填0</param>
        /// <param name="reduceStockValue">减少多少库存，可以不填或填0</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ModifyStock(string accessTokenOrAppId, string cardId, int increaseStockValue = 0, int reduceStockValue = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/card/modifystock?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    increase_stock_value = increaseStockValue,
                    reduce_stock_value = reduceStockValue
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #region 门店接口已过期

        ///// <summary>
        ///// 批量导入门店信息
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="data">门店数据</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>
        //public static StoreResultJson StoreBatchAdd(string accessToken, StoreLocationData data, int timeOut = Config.TIME_OUT)
        //{
        //    var urlFormat = string.Format("https://api.weixin.qq.com/card/location/batchadd?access_token={0}", accessToken.AsUrlData());

        //    return CommonJsonSend.Send<StoreResultJson>(null, urlFormat, data, timeOut: timeOut);
        //}

        ///// <summary>
        ///// 拉取门店列表
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="offset">偏移量，0 开始</param>
        ///// <param name="count">拉取数量</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>
        //public static StoreGetResultJson BatchGet(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        //{
        //    var urlFormat = string.Format("https://api.weixin.qq.com/card/location/batchget?access_token={0}", accessToken.AsUrlData());

        //    var data = new
        //    {
        //        offset = offset,
        //        count = count
        //    };

        //    return CommonJsonSend.Send<StoreGetResultJson>(null, urlFormat, data, timeOut: timeOut);
        //}

        ///// <summary>
        ///// 上传LOGO
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="file">文件路径</param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static Card_UploadLogoResultJson UploadLogo(string accessToken, string file, int timeOut = Config.TIME_OUT)
        //{
        //    var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}", accessToken.AsUrlData());
        //    var fileDictionary = new Dictionary<string, string>();
        //    fileDictionary["media"] = file;
        //    return HttpUtility.Post.PostFileGetJson<Card_UploadLogoResultJson>(url, null, fileDictionary, null, timeOut: timeOut);
        //}

        #endregion
    }
}