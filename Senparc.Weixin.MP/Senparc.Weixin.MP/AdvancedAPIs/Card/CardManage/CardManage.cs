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
    /// 卡券管理接口
    /// </summary>
    public static class CardManage
    {
        /// <summary>
        /// 删除卡券
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cardId">卡券ID</param>
        /// <returns></returns>
        public static CardDeleteResultJson CardDelete(string accessToken, string cardId)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/delete?access_token={0}", accessToken);

            var data = new
                {
                    card_id = cardId
                };

            return CommonJsonSend.Send<CardDeleteResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 查询code接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static CardGetResultJson CardGet(string accessToken, string code, string cardId = null)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/code/get?access_token={0}", accessToken);

            var data = new
            {
                code = code,
                card_id = cardId
            };

            return CommonJsonSend.Send<CardGetResultJson>(accessToken, urlFormat, data); 
        }

        /// <summary>
        /// 批量查询卡列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="offset">查询卡列表的起始偏移量，从0 开始，即offset: 5 是指从从列表里的第六个开始读取。</param>
        /// <param name="count">需要查询的卡片的数量（数量最大50）</param>
        /// <returns></returns>
        public static CardBatchGetResultJson CardBatchGet(string accessToken, int offset, int count)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/batchget?access_token={0}", accessToken);

            var data = new
            {
                offset = offset,
                count = count
            };

            return CommonJsonSend.Send<CardBatchGetResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 查询卡券详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static CardDetailGetResultJson CardDetailGet(string accessToken, string cardId)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/get?access_token={0}", accessToken);

            var data = new
            {
                card_id = cardId
            };

            return CommonJsonSend.Send<CardDetailGetResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 更改code
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code">卡券的code 编码</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="newCode">新的卡券code 编码</param>
        /// <returns></returns>
        public static WxJsonResult CardChangeCode(string accessToken, string code, string cardId, string newCode)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/code/update?access_token={0}", accessToken);

            var data = new
            {
                code = code,
                card_id = cardId,
                new_code = newCode
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 设置卡券失效接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code">需要设置为失效的code</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 的卡券不填。</param>
        /// <returns></returns>
        public static WxJsonResult CardUnavailable(string accessToken, string code, string cardId = null)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/code/unavailable?access_token={0}", accessToken);

            var data = new
            {
                code = code,
                card_id = cardId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 更改卡券信息接口
        /// 支持更新部分通用字段及特殊卡券（会员卡、飞机票、电影票、红包）中特定字段的信息。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cardType">卡券种类，会员卡、飞机票、电影票、红包中的一种</param>
        /// <param name="data">创建卡券需要的数据，格式可以看CardUpdateData.cs</param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static WxJsonResult CardUpdate(string accessToken,CardType cardType, object data, string cardId = null)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/update?access_token={0}", accessToken);

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

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, cardData);
        }

        /// <summary>
        /// 设置测试用户白名单
        /// 由于卡券有审核要求，为方便公众号调试，可以设置一些测试帐号，这些帐号可以领取未通过审核的卡券，体验整个流程。
        ///注：同时支持“openid”、“username”两种字段设置白名单，总数上限为10 个。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openIds">测试的openid 列表</param>
        /// <param name="userNames">测试的微信号列表</param>
        /// <returns></returns>
        public static WxJsonResult AuthoritySet(string accessToken, string[] openIds, string[] userNames)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/testwhitelist/set?access_token={0}", accessToken);

            var data = new
            {
                openid = openIds,
                username = userNames
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 激活/绑定会员卡
        /// 参数【initBonus、initBalance】和【bonus、balance】取其一，不可同时传入
        /// post数据：
        /// {
        ///"init_bonus": 100,
        ///"init_balance": 200,
        ///"membership_number": "AAA00000001",
        ///"code": "12312313",
        ///"card_id": "xxxx_card_id"
        ///}
        ///或
        ///{
        ///"bonus": “www.xxxx.com”,
        ///"balance": “www.xxxx.com”,
        ///"membership_number": "AAA00000001",
        ///"code": "12312313",
        ///"card_id": "xxxx_card_id"
        ///}
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="membershipNumber">必填，会员卡编号，作为序列号显示在用户的卡包里。</param>
        /// <param name="code">创建会员卡时获取的code</param>
        /// <param name="cardId">卡券ID。自定义code 的会员卡必填card_id，非自定义code 的会员卡不必填</param>
        /// <param name="initBonus">初始积分，不填为0</param>
        /// <param name="initBalance">初始余额，不填为0</param>
        /// <param name="bonus">积分查询，仅用于init_bonus 无法同步的情况填写，调转外链查询积分</param>
        /// <param name="balance">余额查询，仅用于init_balance 无法同步的情况填写，调转外链查询积分</param>
        /// <returns></returns>
        public static WxJsonResult MemberCardActivate(string accessToken, string membershipNumber, string code, string cardId, int initBonus, int initBalance, string bonus = null, string balance = null)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/testwhitelist/set?access_token={0}", accessToken);

            var data = new
            {
                init_bonus = initBonus,
                init_balance = initBalance,
                bonus = bonus,
                balance = balance,
                membership_number = membershipNumber,
                code = code,
                card_id = cardId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 会员卡交易
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="cardId">要消耗序列号所述的card_id。自定义code 的会员卡必填</param>
        /// <param name="recordBonus">商家自定义积分消耗记录，不超过14 个汉字</param>
        /// <param name="addBonus">需要变更的积分，扣除积分用“-“表</param>
        /// <param name="addBalance">需要变更的余额，扣除金额用“-”表示。单位为分</param>
        /// <param name="recordBalance">商家自定义金额消耗记录，不超过14 个汉字</param>
        /// <returns></returns>
        public static MemberCardDeal MemberCardDeal(string accessToken, string code, string cardId, string recordBonus, decimal addBonus, decimal addBalance, string recordBalance)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/membercard/updateuser?access_token={0}", accessToken);

            var data = new
            {
                code = code,
                card_id = cardId,
                record_bonus = recordBonus,
                add_bonus = addBonus,
                add_balance = addBalance,
                record_balance = recordBalance,
            };

            return CommonJsonSend.Send<MemberCardDeal>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 更新电影票
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code">电影票的序列号</param>
        /// <param name="cardId">电影票card_id。自定义code 的电影票为必填，非自定义code 的电影票不必填。</param>
        /// <param name="ticketClass">电影票的类别，如2D、3D</param>
        /// <param name="showTime">电影放映时间对应的时间戳</param>
        /// <param name="duration">放映时长，填写整数</param>
        /// <param name="screeningRoom">该场电影的影厅信息</param>
        /// <param name="seatNumbers">座位号</param>
        /// <returns></returns>
        public static WxJsonResult MovieCardUpdate(string accessToken, string code, string cardId, string ticketClass, string showTime, int duration, string screeningRoom, string[] seatNumbers)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/movieticket/updateuser?access_token={0}", accessToken);

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

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 飞机票在线选座
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code">飞机票的序列</param>
        /// <param name="cardId">需办理值机的机票card_id。自定义code 的飞机票为必</param>
        /// <param name="passengerName">乘客姓名，上限为15 个汉字</param>
        /// <param name="classType">舱等，如头等舱等，上限为5 个汉字</param>
        /// <param name="seat">乘客座位号</param>
        /// <param name="etktBnr">电子客票号，上限为14 个数字</param>
        /// <param name="qrcodeData">二维码数据。乘客用于值机的二维码字符串，微信会通过此数据为用户生成值机用的二维码</param>
        /// <param name="isCancel">是否取消值机。填写true 或false。true 代表取消，如填写true 上述字段（如calss 等）均不做判断，机票返回未值机状态，乘客可重新值机。默认填写false</param>
        /// <returns></returns>
        public static WxJsonResult BoardingPassCheckIn(string accessToken, string code, string cardId, string passengerName, string classType, string seat, string etktBnr, string qrcodeData, bool isCancel = false)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/boardingpass/checkin?access_token={0}", accessToken);

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

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 更新红包金额
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="code">红包的序列号</param>
        /// <param name="cardId">自定义code 的卡券必填。非自定义code 可不填。</param>
        /// <param name="balance">红包余额</param>
        /// <returns></returns>
        public static WxJsonResult BoardingPassCheckIn(string accessToken, string code, string cardId, decimal balance)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/luckymoney/updateuserbalance?access_token={0}", accessToken);

            var data = new
            {
                code = code,
                card_id = cardId,
                balance = balance
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }
    }
}
