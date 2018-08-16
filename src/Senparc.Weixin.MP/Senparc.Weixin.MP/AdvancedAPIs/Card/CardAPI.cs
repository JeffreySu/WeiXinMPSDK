#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

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

    修改标识：Senparc - 20160520
    修改描述：增加开通券点账户接口，对优惠券批价，查询券点余额接口，确认兑换库存接口，查询订单详情接口，查询券点流水详情接口，Mark(占用)Code接口，
              拉取卡券概况数据接口，获取免费券数据接口，拉取会员卡数据接口，设置自助核销接口，创建子商户接口，
              卡券开放类目查询接口，拉取单个子商户信息接口，拉取子商户列表接口，更新子商户接口，拉取单个子商户信息接口 ，
              批量拉取子商户信息接口，母商户资质申请接口，母商户资质审核查询接口，子商户资质申请接口，子商户资质审核查询接口
    
    修改标识：hello2008zj - 20160422
    修改描述：修改CreateQR接口，匹配最新的文档
 
    修改标识：Senparc - 20160718
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170527
    修改描述：v14.4.10 CardApi.CardBatchGet()方法增加statusList参数

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20170810
    修改描述：v14.5.11 更新CardApi.CardBatchGet()方法的statusList参数传值

    修改标识：Senparc - 20170810
    修改描述：v14.8.14 CardApi.UpdateUser() 方法参数中重新加添 add_bonus 和 add_balance 两个参数

    修改标识：Senparc - 20180526
    修改描述：v14.8.14 CardApi.UpdateUser() 方法参数中重新加添 add_bonus 和 add_balance 两个参数

----------------------------------------------------------------*/

/*
   API地址：http://mp.weixin.qq.com/wiki/9/d8a5f3b102915f30516d79b44fe665ed.html
   PDF下载：https://mp.weixin.qq.com/zh_CN/htmledition/comm_htmledition/res/cardticket/wx_card_document.zip
*/


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.Card;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.HttpUtility;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers.Serializers;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 卡券接口
    /// </summary>
    public static class CardApi
    {
        #region 同步方法
        /// <summary>
        /// 创建卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardInfo">创建卡券需要的数据，格式可以看CardCreateData.cs</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardCreateResultJson CreateCard(string accessTokenOrAppId, BaseCardInfo cardInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/create?access_token={0}", accessToken.AsUrlData());

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
        ///// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>

        /// <summary>
        /// 开通券点账户接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PayActiveResultJson PayActive(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/activate?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<PayActiveResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 对优惠券批价
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///<param name="cardId">需要来配置库存的card_id</param>
        /// <param name="quantity">本次需要兑换的库存数目</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetpayPriceResultJson GetpayPrice(string accessTokenOrAppId, string cardId, int quantity, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getpayprice?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    quantity = quantity
                };

                return CommonJsonSend.Send<GetpayPriceResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 查询券点余额接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetCoinsInfoResultJson GetCoinsInfo(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getcoinsinfo?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetCoinsInfoResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///确认兑换库存接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">需要来兑换库存的card_id</param>
        /// <param name="quantity">本次需要兑换的库存数目</param>
        /// <param name="orderId">仅可以使用上面得到的订单号，保证批价有效性</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult PayConfirm(string accessTokenOrAppId, string cardId, int quantity, string orderId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/confirm?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    quantity = quantity,
                    order_id = orderId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///充值券点接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="coinCount">需要充值的券点数目，1点=1元</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PayRechargeResultJson PayRecharge(string accessTokenOrAppId, int coinCount, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/recharge?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    coin_count = coinCount

                };

                return CommonJsonSend.Send<PayRechargeResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///查询订单详情接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="orderId">上一步中获得的订单号，作为一次交易的唯一凭证</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PayGetOrderResultJson PayGetOrder(string accessTokenOrAppId, int orderId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getorder?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    order_id = orderId

                };

                return CommonJsonSend.Send<PayGetOrderResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///查询券点流水详情接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">分批查询的起点，默认为0</param>
        /// <param name="count">分批查询的数量</param>
        /// <param name="orderType">所要拉取的订单类型ORDER_TYPE_SYS_ADD 平台赠送 ORDER_TYPE_WXPAY 充值 ORDER_TYPE_REFUND 库存回退券点 ORDER_TYPE_REDUCE 券点兑换库存 ORDER_TYPE_SYS_REDUCE 平台扣减</param>
        /// <param name="norFilter">反选，不要拉取的订单</param>
        /// <param name="sortInfo">对结果排序</param>
        /// <param name="beginTime">批量查询订单的起始事件，为时间戳，默认1周前</param>
        ///  <param name="endTime">批量查询订单的结束事件，为时间戳，默认为当前时间</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetOrderListResultJson GetOrderList(string accessTokenOrAppId, int offset, int count, string orderType, NorFilter norFilter, SortInfo sortInfo, int beginTime, int endTime, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getorderlist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    offset = offset,
                    count = count,
                    order_type = orderType,
                    nor_filter = norFilter,
                    sort_info = sortInfo,
                    begin_time = beginTime,
                    end_time = endTime

                };

                return CommonJsonSend.Send<GetOrderListResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 生成卡券二维码
        /// 获取二维码ticket 后，开发者可用ticket 换取二维码图片。换取指引参考：http://mp.weixin.qq.com/wiki/index.php?title=生成带参数的二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="code">指定卡券code 码，只能被领一次。use_custom_code 字段为true 的卡券必须填写，非自定义code 不必填写。</param>
        /// <param name="openId">指定领取者的openid，只有该用户能领取。bind_openid 字段为true 的卡券必须填写，非自定义openid 不必填写。</param>
        /// <param name="expireSeconds">指定二维码的有效时间，范围是60 ~ 1800 秒。不填默认为永久有效。</param>
        /// <param name="isUniqueCode">指定下发二维码，生成的二维码随机分配一个code，领取后不可再次扫描。填写true 或false。默认false。</param>
        /// <param name="outer_id">自定义应用场景ID（v13.7.3起支持）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateQRResultJson CreateQR(string accessTokenOrAppId, string cardId, string code = null,
            string openId = null, string expireSeconds = null,
            bool isUniqueCode = false, string outer_id = null,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/qrcode/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    action_name = "QR_CARD",
                    expire_seconds = expireSeconds,
                    action_info = new
                    {
                        card = new
                        {
                            card_id = cardId,
                            code = code,
                            openid = openId,
                            //expire_seconds = expireSeconds,
                            is_unique_code = false,
                            //balance = balance,
                            outer_id = outer_id
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
        /// 创建发行多个卡券的二维码
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="cardIds"></param>
        /// <param name="code"></param>
        /// <param name="openId"></param>
        /// <param name="expireSeconds"></param>
        /// <param name="isUniqueCode"></param>
        /// <param name="outer_id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CreateQRResultJson CreateMultipleCardQR(string accessTokenOrAppId,
                                                                string[] cardIds,
                                                                string code = null,
                                                                string openId = null,
                                                                string expireSeconds = null,
                                                                bool isUniqueCode = false,
                                                                string outer_id = null,
                                                                int timeOut = Config.TIME_OUT
            )
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = string.Format(Config.ApiMpHost+"/card/qrcode/create?access_token={0}",
                                                    accessToken.AsUrlData());

                List<QR_CARD_INFO> cardlist = new List<QR_CARD_INFO>();
                foreach (string cardid in cardIds)
                {
                    cardlist.Add(new QR_CARD_INFO()
                    {
                        card_id = cardid,
                        openid = openId,
                        is_unique_code = isUniqueCode,
                        outer_id = outer_id
                    });
                }

                var data = new
                {
                    action_name = "QR_MULTIPLE_CARD",
                    expire_seconds = expireSeconds,
                    action_info = new
                    {
                        multiple_card = new
                        {
                            card_list = cardlist
                        }
                    }
                };
                //var jsonSettingne = new JsonSetting(true);

                JsonSetting jsonSetting = new JsonSetting(true,
                                                            null,
                                                            new List<Type>()
                {
                //typeof (Modify_Msg_Operation),
                //typeof (CardCreateInfo),
                data.action_info.multiple_card.GetType() //过滤Modify_Msg_Operation主要起作用的是这个
                                                            });

                return CommonJsonSend.Send<CreateQRResultJson>(null,
                                                                urlFormat,
                                                                data,
                                                                timeOut:timeOut,
                                                                jsonSetting: jsonSetting);
            },
                                                    accessTokenOrAppId);
        }

            /// <summary>
            /// 创建货架
            /// </summary>
            /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
            /// <param name="data"></param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public static ShelfCreateResultJson ShelfCreate(string accessTokenOrAppId, ShelfCreateData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/landingpage/create?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">需要进行导入code的卡券ID</param>
        /// <param name="codeList">需导入微信卡券后台的自定义code，上限为100个。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult CodeDeposit(string accessTokenOrAppId, string cardId, string[] codeList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/deposit?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">进行导入code的卡券ID。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetDepositCountResultJson GetDepositCount(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/getdepositcount?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">进行导入code的卡券ID。</param>
        /// <param name="codeList">已经微信卡券后台的自定义code，上限为100个。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CheckCodeResultJson CheckCode(string accessTokenOrAppId, string cardId, string[] codeList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/checkcode?access_token={0}", accessToken);

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetHtmlResultJson GetHtml(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/mpnews/gethtml?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                };

                return CommonJsonSend.Send<GetHtmlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// Mark(占用)Code接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">卡券的code码。</param>
        /// <param name="cardId">卡券的ID。</param>
        /// <param name="openId">用券用户的openid。</param>
        /// <param name="isMark">是否要mark（占用）这个code，填写true或者false，表示占用或解除占用。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult CodeMark(string accessTokenOrAppId, string code, string cardId, string openId, string isMark, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/mark?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    openid = openId,
                    is_mark = isMark
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 卡券消耗code
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="cardId">要消耗序列号所述的card_id，创建卡券时use_custom_code 填写true 时必填。非自定义code不必填写。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardConsumeResultJson CardConsume(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/consume?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="encryptCode">通过choose_card_info 获取的加密字符串</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardDecryptResultJson CardDecrypt(string accessTokenOrAppId, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/decrypt?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardDeleteResultJson CardDelete(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/delete?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code"></param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardGetResultJson CardGet(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/get?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">查询卡列表的起始偏移量，从0 开始，即offset: 5 是指从从列表里的第六个开始读取。</param>
        /// <param name="count">需要查询的卡片的数量（数量最大50）</param>
        /// <param name="statusList">状态列表</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardBatchGetResultJson CardBatchGet(string accessTokenOrAppId, int offset, int count, List<string> statusList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/batchget?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    offset = offset,
                    count = count,
                    status_list = statusList
                };

                return CommonJsonSend.Send<CardBatchGetResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询卡券详情
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CardDetailGetResultJson CardDetailGet(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/get?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">卡券的code 编码</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="newCode">新的卡券code 编码</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CardChangeCode(string accessTokenOrAppId, string code, string cardId, string newCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/update?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">需要设置为失效的code</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 的卡券不填。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CardUnavailable(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/unavailable?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 拉取卡券概况数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">查询数据的起始时间。</param>
        /// <param name="endDate">查询数据的截至时间。</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public static GetCardBizuinInfoResultJson GetCardBizuinInfo(string accessTokenOrAppId, string beginDate, string endDate, int condSource, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/datacube/getcardbizuininfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    cond_source = condSource

                };

                return CommonJsonSend.Send<GetCardBizuinInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取免费券数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">查询数据的起始时间。</param>
        /// <param name="endDate">查询数据的截至时间。</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <param name="cardId">卡券ID。填写后，指定拉出该卡券的相关数据。</param>
        /// <returns></returns>
        public static GetCardInfoResultJson GetCardInfo(string accessTokenOrAppId, string beginDate, string endDate, int condSource, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/datacube/getcardcardinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    cond_source = condSource,
                    card_id = cardId

                };

                return CommonJsonSend.Send<GetCardInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 拉取会员卡数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">查询数据的起始时间。</param>
        /// <param name="endDate">查询数据的截至时间。</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public static GetCardMemberCardInfoResultJson GetCardMemberCardInfo(string accessTokenOrAppId, string beginDate, string endDate, int condSource, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/datacube/getcardmembercardinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    cond_source = condSource


                };

                return CommonJsonSend.Send<GetCardMemberCardInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 更改卡券信息接口
        /// 支持更新部分通用字段及特殊卡券（会员卡、飞机票、电影票、红包）中特定字段的信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardType">卡券种类，会员卡、飞机票、电影票、红包中的一种</param>
        /// <param name="data">创建卡券需要的数据，格式可以看CardUpdateData.cs</param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult CardUpdate(string accessTokenOrAppId, CardType cardType, object data, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/update?access_token={0}", accessToken.AsUrlData());

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

                JsonSetting jsonSetting = new JsonSetting()
                {
                    TypesToIgnoreNull = new List<Type>() { typeof(BaseUpdateInfo), typeof(BaseCardUpdateInfo) }
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, cardData, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置测试用户白名单
        /// 由于卡券有审核要求，为方便公众号调试，可以设置一些测试帐号，这些帐号可以领取未通过审核的卡券，体验整个流程。
        ///注：同时支持“openid”、“username”两种字段设置白名单，总数上限为10 个。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openIds">测试的openid 列表</param>
        /// <param name="userNames">测试的微信号列表</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult AuthoritySet(string accessTokenOrAppId, string[] openIds, string[] userNames, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/testwhitelist/set?access_token={0}", accessToken.AsUrlData());

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
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
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
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/activate?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ActivateUserFormSet(string accessTokenOrAppId, ActivateUserFormSetData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/activateuserform/set?access_token={0}", accessToken.AsUrlData());
                JsonSetting jsonSetting = new JsonSetting()
                {
                    TypesToIgnoreNull = new List<Type>() { typeof(ActivateUserFormSetData), typeof(BaseForm) }
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 拉取会员信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">CardID</param>
        /// <param name="code">Code</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UserinfoGetResult UserinfoGet(string accessTokenOrAppId, string cardId, string code, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/userinfo/get?access_token={0}", accessToken);

                return CommonJsonSend.Send<UserinfoGetResult>(null, urlFormat, new { card_id = cardId, code = code }, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 设置跟随推荐接口
        /// 有 使用消息配置卡券（cardCellData） 和 使用消息配置URL（urlCellData） 两种方式
        /// 注意：cardCellData和urlCellData必须也只能选择一个，不可同时为空
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="cardCellData">使用消息配置卡券数据</param>
        /// <param name="urlCellData">使用消息配置URL数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult RecommendSet(string accessTokenOrAppId, string cardId, CardCell cardCellData = null, UrlCell urlCellData = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    member_card = new
                    {
                        modify_msg_operation = new
                        {
                            card_cell = cardCellData,
                            url_cell = urlCellData
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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="isOpen">是否开启买单功能，填true/false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult PayCellSet(string accessTokenOrAppId, string cardId, bool isOpen, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/paycell/set?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    is_open = isOpen
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 设置自助核销接口
        /// 注意：设置自助核销的card_id必须已经配置了门店，否则会报错。
        /// 错误码，0为正常；43008为商户没有开通微信支付权限
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="isOpen">是否开启自助核销功能，填true/false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SelfConsumecellSet(string accessTokenOrAppId, string cardId, bool isOpen, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/selfconsumecell/set?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    is_open = isOpen
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

            ///  <summary>
            ///  更新会员信息
            ///  </summary>
            ///   post数据：
            ///  可以传入积分、余额的差值
            ///  {
            ///   "code": "12312313",
            ///   "card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
            ///   "record_bonus": "消费30元，获得3积分",
            ///   "add_bonus": 3,//可以传入积分增减的差值
            ///   "add_balance": -3000,//可以传入余额本次增减的差值
            ///   "record_balance": "购买焦糖玛琪朵一杯，扣除金额30元。",
            ///   "custom_field_value1": "xxxxx",
            ///  }
            ///  或者直接传入积分、余额的全量值
            /// 
            ///  {
            ///   "code": "12312313",
            ///   "card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
            ///   "record_bonus": "消费30元，获得3积分",
            ///   "bonus": 3000,//可以传入第三方系统记录的积分全量值
            ///   "balance": 3000,//可以传入第三方系统记录的余额全量值
            ///   "record_balance": "购买焦糖玛琪朵一杯，扣除金额30元。",
            ///   "custom_field_value1": "xxxxx",
            ///  }
            ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
            ///  <param name="code">卡券Code码。</param>
            ///  <param name="cardId">卡券ID。</param>
            ///  <param name="addBonus">需要变更的积分，扣除积分用“-“表示。</param>
            ///  <param name="addBalance">需要变更的余额，扣除金额用“-”表示。单位为分。</param>
            /// <param name="backgroundPicUrl">用户卡片的背景图片</param>
            /// <param name="bonus">需要设置的积分全量值，传入的数值会直接显示，如果同时传入add_bonus和bonus,则前者无效。</param>
            ///  <param name="balance">需要设置的余额全量值，传入的数值会直接显示，如果同时传入add_balance和balance,则前者无效。</param>
            ///  <param name="recordBonus">商家自定义积分消耗记录，不超过14个汉字。</param>
            ///  <param name="recordBalance">商家自定义金额消耗记录，不超过14个汉字。</param>
            ///  <param name="customFieldValue1">创建时字段custom_field1定义类型的最新数值，限制为4个汉字，12字节。</param>
            ///  <param name="customFieldValue2">创建时字段custom_field2定义类型的最新数值，限制为4个汉字，12字节。</param>
            ///  <param name="customFieldValue3">创建时字段custom_field3定义类型的最新数值，限制为4个汉字，12字节。</param>
            ///   <param name="membershipNumber">会员号，wiki文档没有，经测算可以用，用于会员号设置错误后重新设置</param>
            ///  <param name="timeOut"></param>
            ///  <returns></returns>
            public static UpdateUserResultJson UpdateUser(string accessTokenOrAppId, string code, string cardId, int addBonus, int addBalance, string backgroundPicUrl = null,
            int? bonus = null, int? balance = null, string recordBonus = null, string recordBalance = null, string customFieldValue1 = null,
            string customFieldValue2 = null, string customFieldValue3 = null,string membershipNumber = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    background_pic_url = backgroundPicUrl,
                    add_bonus = addBonus,//之前注释掉，现还原    ——Jeffrey Su 2018.1.21
                    bonus = bonus,
                    record_bonus = recordBonus,
                    add_balance = addBalance,//之前注释掉，现还原    ——Jeffrey Su 2018.1.21
                    balance = balance,
                    record_balance = recordBalance,
                    custom_field_value1 = customFieldValue1,
                    custom_field_value2 = customFieldValue2,
                    custom_field_value3 = customFieldValue3,
                    membership_number = membershipNumber
                };

                JsonSetting jsonSetting = new JsonSetting()
                {
                    TypesToIgnoreNull = new List<Type>() { data.GetType() }
                };

                return CommonJsonSend.Send<UpdateUserResultJson>(null, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

            /// <summary>
            /// 会员卡交易
            /// </summary>
            /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
            /// <param name="code">要消耗的序列号</param>
            /// <param name="cardId">要消耗序列号所述的card_id。自定义code 的会员卡必填</param>
            /// <param name="recordBonus">商家自定义积分消耗记录，不超过14 个汉字</param>
            /// <param name="addBonus">需要变更的积分，扣除积分用“-“表</param>
            /// <param name="addBalance">需要变更的余额，扣除金额用“-”表示。单位为分</param>
            /// <param name="recordBalance">商家自定义金额消耗记录，不超过14 个汉字</param>
            /// <param name="isNotifyBonus">积分变动时是否触发系统模板消息，默认为true</param>
            /// <param name="isNotifyBalance">余额变动时是否触发系统模板消息，默认为true</param>
            /// <param name="isNotifyCustomField1">自定义group1变动时是否触发系统模板消息，默认为false。</param>
            /// <param name="timeOut">代理请求超时时间（毫秒）</param>
            /// <returns></returns>
            public static MemberCardDealResultJson MemberCardDeal(string accessTokenOrAppId, string code, string cardId, string recordBonus, decimal addBonus, decimal addBalance, string recordBalance,bool isNotifyBonus=true,bool isNotifyBalance=true,bool isNotifyCustomField1=false,int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    record_bonus = recordBonus,
                    add_bonus = addBonus,
                    add_balance = addBalance,
                    record_balance = recordBalance,
                    // notify_optional如果没有显式设置，接口默认为false。
                    notify_optional = new  
                    {
                        is_notify_bonus = isNotifyBonus,
                        is_notify_balance=isNotifyBalance,
                        is_notify_custom_field1=isNotifyCustomField1
                    }
                };

                return CommonJsonSend.Send<MemberCardDealResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新电影票
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
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
                var urlFormat = string.Format(Config.ApiMpHost + "/card/movieticket/updateuser?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
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
                var urlFormat = string.Format(Config.ApiMpHost + "/card/boardingpass/checkin?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">红包的序列号</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 可不填。</param>
        /// <param name="balance">红包余额</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult UpdateUserBalance(string accessTokenOrAppId, string code, string cardId, decimal balance, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/luckymoney/updateuserbalance?access_token={0}", accessToken.AsUrlData());

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
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
                var urlFormat = string.Format(Config.ApiMpHost + "/card/meetingticket/updateuser?access_token={0}", accessToken.AsUrlData());

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
        ///  创建子商户接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="info">json结构</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SubmerChantSubmitJsonResult SubmerChantSubmit(string accessTokenOrAppId, InfoList info, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/submit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    info = info
                };

                return CommonJsonSend.Send<SubmerChantSubmitJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 卡券开放类目查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static GetApplyProtocolJsonResult GetApplyProtocol(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/card/getapplyprotocol?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<GetApplyProtocolJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);

        }
        /// <summary>
        ///拉取单个子商户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appid"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static GetCardMerchantJsonResult GetCardMerchant(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/get_card_merchant?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    appid = appid
                };
                return CommonJsonSend.Send<GetCardMerchantJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }
        /// <summary>
        ///拉取子商户列表接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="nextGet">获取子商户列表，注意最开始时为空。每次拉取20个子商户，下次拉取时填入返回数据中该字段的值，该值无实际意义。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static BatchGetCardMerchantJsonResult BatchGetCardMerchant(string accessTokenOrAppId, string nextGet, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/batchget_card_merchant?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    next_get = nextGet
                };
                return CommonJsonSend.Send<BatchGetCardMerchantJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }

        /// <summary>
        ///  更新子商户接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="info">json结构</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SubmerChantSubmitJsonResult SubmerChantUpdate(string accessTokenOrAppId, InfoList info, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    info = info
                };

                return CommonJsonSend.Send<SubmerChantSubmitJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///  拉取单个子商户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="merchantId">子商户id，一个母商户公众号下唯一。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SubmerChantSubmitJsonResult SubmerChantGet(string accessTokenOrAppId, string merchantId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    merchant_id = merchantId
                };

                return CommonJsonSend.Send<SubmerChantSubmitJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///  批量拉取子商户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="beginId">起始的子商户id，一个母商户公众号下唯一</param>
        /// <param name="limit">拉取的子商户的个数，最大值为100</param>
        /// <param name="status">json结构</param>
        /// <param name="timeOut">子商户审核状态，填入后，只会拉出当前状态的子商户</param>
        /// <returns></returns>
        public static SubmerChantBatchGetJsonResult SubmerChantBatchGet(string accessTokenOrAppId, string beginId, int limit, string status, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/batchget?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_id = beginId,
                    limit = limit,
                    status = status
                };

                return CommonJsonSend.Send<SubmerChantBatchGetJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///  母商户资质申请接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="registerCapital">注册资本，数字，单位：分</param>
        /// <param name="businessLicenseMediaid">营业执照扫描件的media_id</param>
        /// <param name="taxRegistRationCertificateMediaid">税务登记证扫描件的media_id</param>
        /// <param name="lastQuarterTaxListingMediaid">上个季度纳税清单扫描件media_id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult AgentQualification(string accessTokenOrAppId, string registerCapital, string businessLicenseMediaid, string taxRegistRationCertificateMediaid, string lastQuarterTaxListingMediaid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/component/upload_card_agent_qualification?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    register_capital = registerCapital,
                    business_license_media_id = businessLicenseMediaid,
                    tax_registration_certificate_media_id = taxRegistRationCertificateMediaid,
                    last_quarter_tax_listing_media_id = lastQuarterTaxListingMediaid
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 母商户资质审核查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static CheckQualificationJsonResult CheckAgentQualification(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/check_card_agent_qualification?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<CheckQualificationJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);

        }
        /// <summary>
        ///  子商户资质申请接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appid">子商户公众号的appid</param>
        /// <param name="name">子商户商户名，用于显示在卡券券面</param>
        /// <param name="logoMediaid">子商户logo，用于显示在子商户卡券的券面</param>
        /// <param name="businessLicenseMediaid">营业执照或个体工商户执照扫描件的media_id</param>
        ///  <param name="operatorIdCardMediaid">当子商户为个体工商户且无公章时，授权函须签名，并额外提交该个体工商户经营者身份证扫描件的media_id</param>
        /// <param name="agreementFileMediaid">子商户与第三方签署的代理授权函的media_id</param>
        ///  <param name="primaryCategoryId">一级类目id</param>
        /// <param name="secondaryCategoryId">二级类目id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult MerchantQualification(string accessTokenOrAppId, string appid, string name, string logoMediaid, string businessLicenseMediaid, string operatorIdCardMediaid, string agreementFileMediaid, string primaryCategoryId, string secondaryCategoryId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/component/upload_card_merchant_qualification?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    appid = appid,
                    name = name,
                    logo_meida_id = logoMediaid,
                    business_license_media_id = businessLicenseMediaid,
                    operator_id_card_media_id = operatorIdCardMediaid,
                    agreement_file_media_id = agreementFileMediaid,
                    primary_category_id = primaryCategoryId,
                    secondary_category_id = secondaryCategoryId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 子商户资质审核查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appid"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static CheckQualificationJsonResult CheckMerchantQualification(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/check_card_merchant_qualification?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    appid = appid
                };
                return CommonJsonSend.Send<CheckQualificationJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }

        /// <summary>
        /// 获取用户已领取卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">需要查询的用户openid</param>
        /// <param name="cardId">卡券ID。不填写时默认查询当前appid下的卡券。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetCardListResultJson GetCardList(string accessTokenOrAppId, string openId, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/user/getcardlist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    openid = openId,
                    card_id = cardId,
                };

                return CommonJsonSend.Send<GetCardListResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改库存接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="increaseStockValue">增加多少库存，支持不填或填0</param>
        /// <param name="reduceStockValue">减少多少库存，可以不填或填0</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ModifyStock(string accessTokenOrAppId, string cardId, int increaseStockValue = 0, int reduceStockValue = 0, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/modifystock?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    increase_stock_value = increaseStockValue,
                    reduce_stock_value = reduceStockValue
                };

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】创建卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardInfo">创建卡券需要的数据，格式可以看CardCreateData.cs</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CardCreateResultJson> CreateCardAsync(string accessTokenOrAppId, BaseCardInfo cardInfo, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/create?access_token={0}", accessToken.AsUrlData());

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

                var result = Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CardCreateResultJson>(null, urlFormat, cardData, timeOut: timeOut,
                    //针对特殊字段的null值进行过滤
                    jsonSetting: jsonSetting);
                return await result;

            }, accessTokenOrAppId);
        }

        ///// <summary>
        ///// 此接口已取消，微信直接提供了十四种色值供选择，详见：http://mp.weixin.qq.com/wiki/8/b7e310e7943f7763450eced91fa793b0.html#.E5.8D.A1.E5.88.B8.E5.9F.BA.E7.A1.80.E4.BF.A1.E6.81.AF.E5.AD.97.E6.AE.B5.EF.BC.88.E9.87.8D.E8.A6.81.EF.BC.89
        ///// 获取颜色列表接口
        ///// </summary>
        ///// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>

        /// <summary>
        /// 【异步方法】开通券点账户接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<PayActiveResultJson> PayActiveAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/activate?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<PayActiveResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】对优惠券批价
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///<param name="cardId">需要来配置库存的card_id</param>
        /// <param name="quantity">本次需要兑换的库存数目</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetpayPriceResultJson> GetpayPriceAsync(string accessTokenOrAppId, string cardId, int quantity, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getpayprice?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    quantity = quantity
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetpayPriceResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】查询券点余额接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetCoinsInfoResultJson> GetCoinsInfoAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getcoinsinfo?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCoinsInfoResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】确认兑换库存接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">需要来兑换库存的card_id</param>
        /// <param name="quantity">本次需要兑换的库存数目</param>
        /// <param name="orderId">仅可以使用上面得到的订单号，保证批价有效性</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> PayConfirmAsync(string accessTokenOrAppId, string cardId, int quantity, string orderId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/confirm?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    card_id = cardId,
                    quantity = quantity,
                    order_id = orderId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】充值券点接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="coinCount">需要充值的券点数目，1点=1元</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<PayRechargeResultJson> PayRechargeAsync(string accessTokenOrAppId, int coinCount, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/recharge?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    coin_count = coinCount

                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<PayRechargeResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】查询订单详情接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="orderId">上一步中获得的订单号，作为一次交易的唯一凭证</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<PayGetOrderResultJson> PayGetOrderAsync(string accessTokenOrAppId, int orderId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getorder?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    order_id = orderId

                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<PayGetOrderResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】查询券点流水详情接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">分批查询的起点，默认为0</param>
        /// <param name="count">分批查询的数量</param>
        /// <param name="orderType">所要拉取的订单类型ORDER_TYPE_SYS_ADD 平台赠送 ORDER_TYPE_WXPAY 充值 ORDER_TYPE_REFUND 库存回退券点 ORDER_TYPE_REDUCE 券点兑换库存 ORDER_TYPE_SYS_REDUCE 平台扣减</param>
        /// <param name="norFilter">反选，不要拉取的订单</param>
        /// <param name="sortInfo">对结果排序</param>
        /// <param name="beginTime">批量查询订单的起始事件，为时间戳，默认1周前</param>
        ///  <param name="endTime">批量查询订单的结束事件，为时间戳，默认为当前时间</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetOrderListResultJson> GetOrderListAsync(string accessTokenOrAppId, int offset, int count, string orderType, NorFilter norFilter, SortInfo sortInfo, int beginTime, int endTime, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/pay/getorderlist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    offset = offset,
                    count = count,
                    order_type = orderType,
                    nor_filter = norFilter,
                    sort_info = sortInfo,
                    begin_time = beginTime,
                    end_time = endTime

                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetOrderListResultJson>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】生成卡券二维码
        /// 获取二维码ticket 后，开发者可用ticket 换取二维码图片。换取指引参考：http://mp.weixin.qq.com/wiki/index.php?title=生成带参数的二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="code">指定卡券code 码，只能被领一次。use_custom_code 字段为true 的卡券必须填写，非自定义code 不必填写。</param>
        /// <param name="openId">指定领取者的openid，只有该用户能领取。bind_openid 字段为true 的卡券必须填写，非自定义openid 不必填写。</param>
        /// <param name="expireSeconds">指定二维码的有效时间，范围是60 ~ 1800 秒。不填默认为永久有效。</param>
        /// <param name="isUniqueCode">指定下发二维码，生成的二维码随机分配一个code，领取后不可再次扫描。填写true 或false。默认false。</param>
        /// <param name="outer_id">自定义应用场景ID（v13.7.3起支持）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CreateQRResultJson> CreateQRAsync(string accessTokenOrAppId, string cardId, string code = null,
            string openId = null, string expireSeconds = null,
            bool isUniqueCode = false, string outer_id = null,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/qrcode/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    action_name = "QR_CARD",
                    expire_seconds = expireSeconds,
                    action_info = new
                    {
                        card = new
                        {
                            card_id = cardId,
                            code = code,
                            openid = openId,
                            //expire_seconds = expireSeconds,
                            is_unique_code = false,
                            //balance = balance,
                            outer_id = outer_id
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

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CreateQRResultJson>(null, urlFormat, data, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】创建货架
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ShelfCreateResultJson> ShelfCreateAsync(string accessTokenOrAppId, ShelfCreateData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/landingpage/create?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<ShelfCreateResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】导入code
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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">需要进行导入code的卡券ID</param>
        /// <param name="codeList">需导入微信卡券后台的自定义code，上限为100个。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CodeDepositAsync(string accessTokenOrAppId, string cardId, string[] codeList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/deposit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    code = codeList
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询导入code数目
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">进行导入code的卡券ID。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetDepositCountResultJson> GetDepositCountAsync(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/getdepositcount?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetDepositCountResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】核查code
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">进行导入code的卡券ID。</param>
        /// <param name="codeList">已经微信卡券后台的自定义code，上限为100个。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CheckCodeResultJson> CheckCodeAsync(string accessTokenOrAppId, string cardId, string[] codeList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/checkcode?access_token={0}", accessToken);

                var data = new
                {
                    card_id = cardId,
                    code = codeList
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CheckCodeResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】图文消息群发卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetHtmlResultJson> GetHtmlAsync(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/mpnews/gethtml?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetHtmlResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】Mark(占用)Code接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">卡券的code码。</param>
        /// <param name="cardId">卡券的ID。</param>
        /// <param name="openId">用券用户的openid。</param>
        /// <param name="isMark">是否要mark（占用）这个code，填写true或者false，表示占用或解除占用。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CodeMarkAsync(string accessTokenOrAppId, string code, string cardId, string openId, string isMark, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/mark?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    openid = openId,
                    is_mark = isMark
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】卡券消耗code
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="cardId">要消耗序列号所述的card_id，创建卡券时use_custom_code 填写true 时必填。非自定义code不必填写。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CardConsumeResultJson> CardConsumeAsync(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/consume?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CardConsumeResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】code 解码接口
        /// code 解码接口支持两种场景：
        /// 1.商家获取choos_card_info 后，将card_id 和encrypt_code 字段通过解码接口，获取真实code。
        /// 2.卡券内跳转外链的签名中会对code 进行加密处理，通过调用解码接口获取真实code。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="encryptCode">通过choose_card_info 获取的加密字符串</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CardDecryptResultJson> CardDecryptAsync(string accessTokenOrAppId, string encryptCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/decrypt?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    encrypt_code = encryptCode,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CardDecryptResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】删除卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CardDeleteResultJson> CardDeleteAsync(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/delete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CardDeleteResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询code接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code"></param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CardGetResultJson> CardGetAsync(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CardGetResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】批量查询卡列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">查询卡列表的起始偏移量，从0 开始，即offset: 5 是指从从列表里的第六个开始读取。</param>
        /// <param name="count">需要查询的卡片的数量（数量最大50）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CardBatchGetResultJson> CardBatchGetAsync(string accessTokenOrAppId, int offset, int count, List<string> statusList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/batchget?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    offset = offset,
                    count = count,
                    status_list = statusList
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CardBatchGetResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询卡券详情
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CardDetailGetResultJson> CardDetailGetAsync(string accessTokenOrAppId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CardDetailGetResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】更改code
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">卡券的code 编码</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="newCode">新的卡券code 编码</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CardChangeCodeAsync(string accessTokenOrAppId, string code, string cardId, string newCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    new_code = newCode
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】设置卡券失效接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">需要设置为失效的code</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 的卡券不填。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CardUnavailableAsync(string accessTokenOrAppId, string code, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/code/unavailable?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】拉取卡券概况数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">查询数据的起始时间。</param>
        /// <param name="endDate">查询数据的截至时间。</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public static async Task<GetCardBizuinInfoResultJson> GetCardBizuinInfoAsync(string accessTokenOrAppId, string beginDate, string endDate, int condSource, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/datacube/getcardbizuininfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    cond_source = condSource

                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCardBizuinInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取免费券数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">查询数据的起始时间。</param>
        /// <param name="endDate">查询数据的截至时间。</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <param name="cardId">卡券ID。填写后，指定拉出该卡券的相关数据。</param>
        /// <returns></returns>
        public static async Task<GetCardInfoResultJson> GetCardInfoAsync(string accessTokenOrAppId, string beginDate, string endDate, int condSource, string cardId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/datacube/getcardcardinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    cond_source = condSource,
                    card_id = cardId

                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCardInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】拉取会员卡数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">查询数据的起始时间。</param>
        /// <param name="endDate">查询数据的截至时间。</param>
        /// <param name="condSource">卡券来源，0为公众平台创建的卡券数据、1是API创建的卡券数据</param>
        /// <returns></returns>
        public static async Task<GetCardMemberCardInfoResultJson> GetCardMemberCardInfoAsync(string accessTokenOrAppId, string beginDate, string endDate, int condSource, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/datacube/getcardmembercardinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    cond_source = condSource


                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCardMemberCardInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】更改卡券信息接口
        /// 支持更新部分通用字段及特殊卡券（会员卡、飞机票、电影票、红包）中特定字段的信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardType">卡券种类，会员卡、飞机票、电影票、红包中的一种</param>
        /// <param name="data">创建卡券需要的数据，格式可以看CardUpdateData.cs</param>
        /// <param name="cardId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CardUpdateAsync(string accessTokenOrAppId, CardType cardType, object data, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/update?access_token={0}", accessToken.AsUrlData());

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

                JsonSetting jsonSetting = new JsonSetting()
                {
                    TypesToIgnoreNull = new List<Type>() { typeof(BaseUpdateInfo), typeof(BaseCardUpdateInfo) }
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, cardData, timeOut: timeOut, jsonSetting: jsonSetting);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】设置测试用户白名单
        /// 由于卡券有审核要求，为方便公众号调试，可以设置一些测试帐号，这些帐号可以领取未通过审核的卡券，体验整个流程。
        ///注：同时支持“openid”、“username”两种字段设置白名单，总数上限为10 个。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openIds">测试的openid 列表</param>
        /// <param name="userNames">测试的微信号列表</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AuthoritySetAsync(string accessTokenOrAppId, string[] openIds, string[] userNames, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/testwhitelist/set?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    openid = openIds,
                    username = userNames
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        ///  <summary>
        ///  【异步方法】激活/绑定会员卡
        ///  </summary>
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
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
        public static async Task<WxJsonResult> MemberCardActivateAsync(string accessTokenOrAppId, string membershipNumber, string code, string cardId, string activateBeginTime = null, string activateEndTime = null, string initBonus = null,
            string initBalance = null, string initCustomFieldValue1 = null, string initCustomFieldValue2 = null, string initCustomFieldValue3 = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/activate?access_token={0}", accessToken.AsUrlData());

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

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】设置开卡字段接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ActivateUserFormSetAsync(string accessTokenOrAppId, ActivateUserFormSetData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/activateuserform/set?access_token={0}", accessToken.AsUrlData());

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】拉取会员信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">CardID</param>
        /// <param name="code">Code</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UserinfoGetResult> UserinfoGetAsync(string accessTokenOrAppId, string cardId, string code, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/userinfo/get?access_token={0}", accessToken);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UserinfoGetResult>(null, urlFormat, new { card_id = cardId, code = code }, timeOut: timeOut);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】设置跟随推荐接口
        /// 有 使用消息配置卡券（cardCellData） 和 使用消息配置URL（urlCellData） 两种方式
        /// 注意：cardCellData和urlCellData必须也只能选择一个，不可同时为空
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="cardCellData">使用消息配置卡券数据</param>
        /// <param name="urlCellData">使用消息配置URL数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> RecommendSetAsync(string accessTokenOrAppId, string cardId, CardCell cardCellData = null, UrlCell urlCellData = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    member_card = new
                    {
                        modify_msg_operation = new
                        {
                            card_cell = cardCellData,
                            url_cell = urlCellData
                        }
                    }
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】设置微信买单接口
        /// 注意：在调用买单接口之前，请开发者务必确认是否已经开通了微信支付以及对相应的cardid设置了门店，否则会报错
        /// 错误码，0为正常；43008为商户没有开通微信支付权限
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="isOpen">是否开启买单功能，填true/false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> PayCellSetAsync(string accessTokenOrAppId, string cardId, bool isOpen, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/paycell/set?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    is_open = isOpen
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】设置自助核销接口
        /// 注意：设置自助核销的card_id必须已经配置了门店，否则会报错。
        /// 错误码，0为正常；43008为商户没有开通微信支付权限
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="isOpen">是否开启自助核销功能，填true/false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SelfConsumecellSetAsync(string accessTokenOrAppId, string cardId, bool isOpen, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/selfconsumecell/set?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    is_open = isOpen
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        ///  <summary>
        /// 【异步方法】 更新会员信息
        ///  </summary>
        ///   post数据：
        ///  可以传入积分、余额的差值
        ///  {
        ///   "code": "12312313",
        ///   "card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
        ///   "record_bonus": "消费30元，获得3积分",
        ///   "add_bonus": 3,//可以传入积分增减的差值
        ///   "add_balance": -3000,//可以传入余额本次增减的差值
        ///   "record_balance": "购买焦糖玛琪朵一杯，扣除金额30元。",
        ///   "custom_field_value1": "xxxxx",
        ///  }
        ///  或者直接传入积分、余额的全量值
        /// 
        ///  {
        ///   "code": "12312313",
        ///   "card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
        ///   "record_bonus": "消费30元，获得3积分",
        ///   "bonus": 3000,//可以传入第三方系统记录的积分全量值
        ///   "balance": 3000,//可以传入第三方系统记录的余额全量值
        ///   "record_balance": "购买焦糖玛琪朵一杯，扣除金额30元。",
        ///   "custom_field_value1": "xxxxx",
        ///  }
        ///  <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        ///  <param name="code">卡券Code码。</param>
        ///  <param name="cardId">卡券ID。</param>
        ///  <param name="addBonus">需要变更的积分，扣除积分用“-“表示。</param>
        ///  <param name="addBalance">需要变更的余额，扣除金额用“-”表示。单位为分。</param>
        /// <param name="backgroundPicUrl">用户卡片的背景图片</param>
        /// <param name="bonus">需要设置的积分全量值，传入的数值会直接显示，如果同时传入add_bonus和bonus,则前者无效。</param>
        ///  <param name="balance">需要设置的余额全量值，传入的数值会直接显示，如果同时传入add_balance和balance,则前者无效。</param>
        ///  <param name="recordBonus">商家自定义积分消耗记录，不超过14个汉字。</param>
        ///  <param name="recordBalance">商家自定义金额消耗记录，不超过14个汉字。</param>
        ///  <param name="customFieldValue1">创建时字段custom_field1定义类型的最新数值，限制为4个汉字，12字节。</param>
        ///  <param name="customFieldValue2">创建时字段custom_field2定义类型的最新数值，限制为4个汉字，12字节。</param>
        ///  <param name="customFieldValue3">创建时字段custom_field3定义类型的最新数值，限制为4个汉字，12字节。</param>
        ///  <param name="timeOut"></param>
        ///  <returns></returns>
        public static async Task<UpdateUserResultJson> UpdateUserAsync(string accessTokenOrAppId, string code, string cardId, int addBonus, int addBalance, string backgroundPicUrl = null,
            int? bonus = null, int? balance = null, string recordBonus = null, string recordBalance = null, string customFieldValue1 = null,
            string customFieldValue2 = null, string customFieldValue3 = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    background_pic_url = backgroundPicUrl,
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

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UpdateUserResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】会员卡交易
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="cardId">要消耗序列号所述的card_id。自定义code 的会员卡必填</param>
        /// <param name="recordBonus">商家自定义积分消耗记录，不超过14 个汉字</param>
        /// <param name="addBonus">需要变更的积分，扣除积分用“-“表</param>
        /// <param name="addBalance">需要变更的余额，扣除金额用“-”表示。单位为分</param>
        /// <param name="recordBalance">商家自定义金额消耗记录，不超过14 个汉字</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<MemberCardDealResultJson> MemberCardDealAsync(string accessTokenOrAppId, string code, string cardId, string recordBonus, decimal addBonus, decimal addBalance, string recordBalance, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/membercard/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    record_bonus = recordBonus,
                    add_bonus = addBonus,
                    add_balance = addBalance,
                    record_balance = recordBalance,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MemberCardDealResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】更新电影票
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">电影票的序列号</param>
        /// <param name="cardId">电影票card_id。自定义code 的电影票为必填，非自定义code 的电影票不必填。</param>
        /// <param name="ticketClass">电影票的类别，如2D、3D</param>
        /// <param name="showTime">电影放映时间对应的时间戳</param>
        /// <param name="duration">放映时长，填写整数</param>
        /// <param name="screeningRoom">该场电影的影厅信息</param>
        /// <param name="seatNumbers">座位号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> MovieCardUpdateAsync(string accessTokenOrAppId, string code, string cardId, string ticketClass, string showTime, int duration, string screeningRoom, string[] seatNumbers, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/movieticket/updateuser?access_token={0}", accessToken.AsUrlData());

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

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】飞机票在线选座
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
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
        public static async Task<WxJsonResult> BoardingPassCheckInAsync(string accessTokenOrAppId, string code, string cardId, string passengerName, string classType, string seat, string etktBnr, string qrcodeData, bool isCancel = false, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/boardingpass/checkin?access_token={0}", accessToken.AsUrlData());

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

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】更新红包金额
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">红包的序列号</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 可不填。</param>
        /// <param name="balance">红包余额</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UpdateUserBalanceAsync(string accessTokenOrAppId, string code, string cardId, decimal balance, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/luckymoney/updateuserbalance?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    balance = balance
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】更新会议门票接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="code">用户的门票唯一序列号</param>
        /// <param name="cardId">要更新门票序列号所述的card_id ， 生成券时use_custom_code 填写true 时必填。</param>
        /// <param name="zone">区域</param>
        /// <param name="entrance">入口</param>
        /// <param name="seatNumber">座位号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UpdateMeetingTicketAsync(string accessTokenOrAppId, string code, string cardId = null, string zone = null, string entrance = null, string seatNumber = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/meetingticket/updateuser?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    code = code,
                    card_id = cardId,
                    zone = zone,
                    entrance = entrance,
                    seat_number = seatNumber
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///  【异步方法】创建子商户接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="info">json结构</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<SubmerChantSubmitJsonResult> SubmerChantSubmitAsync(string accessTokenOrAppId, InfoList info, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/submit?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    info = info
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SubmerChantSubmitJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】卡券开放类目查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static async Task<GetApplyProtocolJsonResult> GetApplyProtocolAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/card/getapplyprotocol?access_token={0}", accessToken.AsUrlData());
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetApplyProtocolJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);

        }
        /// <summary>
        ///【异步方法】拉取单个子商户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appid"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static async Task<GetCardMerchantJsonResult> GetCardMerchantAsync(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/get_card_merchant?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    appid = appid
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCardMerchantJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }
        /// <summary>
        ///【异步方法】拉取子商户列表接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="nextGet">获取子商户列表，注意最开始时为空。每次拉取20个子商户，下次拉取时填入返回数据中该字段的值，该值无实际意义。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static async Task<BatchGetCardMerchantJsonResult> BatchGetCardMerchantAsync(string accessTokenOrAppId, string nextGet, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/batchget_card_merchant?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    next_get = nextGet
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<BatchGetCardMerchantJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }

        /// <summary>
        /// 【异步方法】 更新子商户接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="info">json结构</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<SubmerChantSubmitJsonResult> SubmerChantUpdateAsync(string accessTokenOrAppId, InfoList info, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    info = info
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SubmerChantSubmitJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///  【异步方法】拉取单个子商户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="merchantId">子商户id，一个母商户公众号下唯一。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<SubmerChantSubmitJsonResult> SubmerChantGetAsync(string accessTokenOrAppId, string merchantId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/get?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    merchant_id = merchantId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SubmerChantSubmitJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///  【异步方法】批量拉取子商户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="beginId">起始的子商户id，一个母商户公众号下唯一</param>
        /// <param name="limit">拉取的子商户的个数，最大值为100</param>
        /// <param name="status">json结构</param>
        /// <param name="timeOut">子商户审核状态，填入后，只会拉出当前状态的子商户</param>
        /// <returns></returns>
        public static async Task<SubmerChantBatchGetJsonResult> SubmerChantBatchGetAsync(string accessTokenOrAppId, string beginId, int limit, string status, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/submerchant/batchget?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin_id = beginId,
                    limit = limit,
                    status = status
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SubmerChantBatchGetJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///  【异步方法】母商户资质申请接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="registerCapital">注册资本，数字，单位：分</param>
        /// <param name="businessLicenseMediaid">营业执照扫描件的media_id</param>
        /// <param name="taxRegistRationCertificateMediaid">税务登记证扫描件的media_id</param>
        /// <param name="lastQuarterTaxListingMediaid">上个季度纳税清单扫描件media_id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AgentQualificationAsync(string accessTokenOrAppId, string registerCapital, string businessLicenseMediaid, string taxRegistRationCertificateMediaid, string lastQuarterTaxListingMediaid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/component/upload_card_agent_qualification?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    register_capital = registerCapital,
                    business_license_media_id = businessLicenseMediaid,
                    tax_registration_certificate_media_id = taxRegistRationCertificateMediaid,
                    last_quarter_tax_listing_media_id = lastQuarterTaxListingMediaid
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】母商户资质审核查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static async Task<CheckQualificationJsonResult> CheckAgentQualificationAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/check_card_agent_qualification?access_token={0}", accessToken.AsUrlData());
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CheckQualificationJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);

        }
        /// <summary>
        ///  【异步方法】子商户资质申请接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appid">子商户公众号的appid</param>
        /// <param name="name">子商户商户名，用于显示在卡券券面</param>
        /// <param name="logoMediaid">子商户logo，用于显示在子商户卡券的券面</param>
        /// <param name="businessLicenseMediaid">营业执照或个体工商户执照扫描件的media_id</param>
        ///  <param name="operatorIdCardMediaid">当子商户为个体工商户且无公章时，授权函须签名，并额外提交该个体工商户经营者身份证扫描件的media_id</param>
        /// <param name="agreementFileMediaid">子商户与第三方签署的代理授权函的media_id</param>
        ///  <param name="primaryCategoryId">一级类目id</param>
        /// <param name="secondaryCategoryId">二级类目id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> MerchantQualificationAsync(string accessTokenOrAppId, string appid, string name, string logoMediaid, string businessLicenseMediaid, string operatorIdCardMediaid, string agreementFileMediaid, string primaryCategoryId, string secondaryCategoryId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/cgi-bin/component/upload_card_merchant_qualification?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    appid = appid,
                    name = name,
                    logo_meida_id = logoMediaid,
                    business_license_media_id = businessLicenseMediaid,
                    operator_id_card_media_id = operatorIdCardMediaid,
                    agreement_file_media_id = agreementFileMediaid,
                    primary_category_id = primaryCategoryId,
                    secondary_category_id = secondaryCategoryId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】子商户资质审核查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appid"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>

        public static async Task<CheckQualificationJsonResult> CheckMerchantQualificationAsync(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/check_card_merchant_qualification?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    appid = appid
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CheckQualificationJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);

        }

        /// <summary>
        /// 【异步方法】获取用户已领取卡券
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">需要查询的用户openid</param>
        /// <param name="cardId">卡券ID。不填写时默认查询当前appid下的卡券。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetCardListResultJson> GetCardListAsync(string accessTokenOrAppId, string openId, string cardId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/user/getcardlist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    openid = openId,
                    card_id = cardId,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCardListResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】修改库存接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="increaseStockValue">增加多少库存，支持不填或填0</param>
        /// <param name="reduceStockValue">减少多少库存，可以不填或填0</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ModifyStockAsync(string accessTokenOrAppId, string cardId, int increaseStockValue = 0, int reduceStockValue = 0, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/modifystock?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    card_id = cardId,
                    increase_stock_value = increaseStockValue,
                    reduce_stock_value = reduceStockValue
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

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
        //    var urlFormat = string.Format(Config.ApiMpHost + "/card/location/batchadd?access_token={0}", accessToken.AsUrlData());

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
        //    var urlFormat = string.Format(Config.ApiMpHost + "/card/location/batchget?access_token={0}", accessToken.AsUrlData());

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
        //    var url = string.Format(Config.ApiMpHost + "/cgi-bin/media/uploadimg?access_token={0}", accessToken.AsUrlData());
        //    var fileDictionary = new Dictionary<string, string>();
        //    fileDictionary["media"] = file;
        //    return HttpUtility.Post.PostFileGetJson<Card_UploadLogoResultJson>(url, null, fileDictionary, null, timeOut: timeOut);
        //}

        #endregion
#endif
    }
}