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

    文件名：Card_BaseInfoBase.cs
    文件功能描述：基本的卡券数据，所有卡券通用。作为 Card_BaseInfo和 的基类


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20160910
    修改描述：v14.3.9 修改Card_BaseInfoBase.get_limit类型为long

    修改标识：Senparc - 20170927
    修改描述：v4.16.5 添加Card_BaseInfoBase下的sub_merchant_info属性

    修改标识：Senparc - 20180618
    修改描述：Modify_Msg_Operation modify_msg_operationg 添加  [JsonSetting.IgnoreNull] 特性

----------------------------------------------------------------*/


using Senparc.CO2NET.Helpers.Serializers;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 基本的卡券数据，所有卡券通用。作为 Card_BaseInfo和 的基类
    /// </summary>
    public class Card_BaseInfoBase
    {
        /// <summary>
        /// 子商户id，对于一个母商户公众号下唯一。
        /// 详情见创建卡券接口：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1445241432
        /// </summary>
        public Card_BaseInfoBase_SubMerchantInfo sub_merchant_info { get; set; }

        /// <summary>
        /// 卡券的商户logo，尺寸为300*300。
        /// 必填
        /// </summary>
        public string logo_url { get; set; }
        /// <summary>
        /// code 码展示类型
        /// 必填
        /// </summary>
        public Card_CodeType code_type { get; set; }
        /// <summary>
        /// 商户名字,字数上限为12 个汉字。（填写直接提供服务的商户名， 第三方商户名填写在source 字段）
        /// 必填
        /// </summary>
        public string brand_name { get; set; }
        /// <summary>
        /// 券名，字数上限为9 个汉字。(建议涵盖卡券属性、服务及金额)
        /// 必填
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 券名的副标题，字数上限为18个汉字。
        /// 非必填
        /// </summary>
        public string sub_title { get; set; }
        /// <summary>
        /// 券颜色。按色彩规范标注填写Color010-Color100
        /// 必填
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 使用提醒，字数上限为9 个汉字。（一句话描述，展示在首页，示例：请出示二维码核销卡券）
        /// 必填
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 客服电话
        /// 非必填
        /// </summary>
        public string service_phone { get; set; }
        /// <summary>
        /// 第三方来源名，例如同程旅游、格瓦拉。
        /// 非必填
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 使用说明。长文本描述，可以分行，上限为1000 个汉字。
        /// 必填
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 每人使用次数限制
        /// 非必填
        /// </summary>
        public int use_limit { get; set; }
        /// <summary>
        /// 每人最大领取次数，不填写默认等于quantity。
        /// 非必填
        /// </summary>
        public long get_limit { get; set; }
        /// <summary>
        /// 是否自定义code 码。填写true或false，不填代表默认为false。
        /// 非必填
        /// </summary>
        public bool use_custom_code { get; set; }
        /// <summary>
        /// 是否指定用户领取，填写true或false。不填代表默认为否。
        /// 非必填
        /// </summary>
        public bool bind_openid { get; set; }
        /// <summary>
        /// 领取卡券原生页面是否可分享，填写true 或false，true 代表可分享。默认可分享。
        /// 非必填
        /// </summary>
        public bool can_share { get; set; }
        /// <summary>
        /// 卡券是否可转赠，填写true 或false,true 代表可转赠。默认可转赠。
        /// 非必填
        /// </summary>
        public bool can_give_friend { get; set; }
        /// <summary>
        /// 门店位置ID。商户需在mp 平台上录入门店信息或调用批量导入门店信息接口获取门店位置ID。
        /// 非必填
        /// </summary>
        public List<string> location_id_list { get; set; }
        /// <summary>
        /// 使用日期，有效期的信息
        /// 必填
        /// </summary>
        public Card_BaseInfo_DateInfo date_info { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public Card_BaseInfo_Sku sku { get; set; }
        /// <summary>
        /// 商户自定义cell 名称
        /// 非必填
        /// </summary>
        public Card_UrlNameType url_name_type { get; set; }
        /// <summary>
        /// 商户自定义url 地址，支持卡券页内跳转,跳转页面内容需与自定义cell 名称保持一致。
        /// 非必填
        /// </summary>
        public string custom_url { get; set; }
        /// <summary>
        /// 自定义跳转外链的入口名字
        /// 非必填
        /// </summary>
        public string custom_url_name { get; set; }
        /// <summary>
        /// 显示在入口右侧的提示语
        /// 非必填
        /// </summary>
        public string custom_url_sub_title { get; set; }
        /// <summary>
        /// 营销场景的自定义入口名称
        /// 非必填
        /// </summary>
        public string promotion_url_name { get; set; }
        /// <summary>
        /// 入口跳转外链的地址链接
        /// 非必填
        /// </summary>
        public string promotion_url { get; set; }
        /// <summary>
        /// 显示在营销入口右侧的提示语
        /// 非必填
        /// </summary>
        public string promotion_url_sub_title { get; set; }
        /// <summary>
        /// 积分余额变动消息类型
        /// </summary>
        [JsonSetting.IgnoreNull]
        public Modify_Msg_Operation modify_msg_operation { get; set; }

        //public Card_BaseInfoBase()
        //{
        //    modify_msg_operation = new Modify_Msg_Operation();//为了解决提交时候modify_msg_operation=null，导致47001的错误
        //}

        //以下增加
        /// <summary>
        /// 设置本卡券支持全部门店
        /// </summary>
        public bool use_all_locations { get; set; }
        /// <summary>
        ///进入会员卡时是否推送事件，填写true或false，会员卡专用。
        /// </summary>
        public bool need_push_on_view { get; set; }
        /// <summary>
        /// 会员卡支持微信支付刷卡
        /// </summary>
        public Card_BaseInfo_member_card_PayInfo pay_info { get; set; }
    }

    public class Card_BaseInfoBase_SubMerchantInfo
    {
        public int merchant_id { get; set; }
    }

    public class Modify_Msg_Operation /*: JsonIgnoreNull//为了解决提交时候modify_msg_operation=null，导致47001的错误*/
    {
        /// <summary>
        /// 卡券类型的推荐位
        /// </summary>
        public CardCell card_cell { get; set; }
        /// <summary>
        /// 链接类型的推荐位
        /// </summary>
        public UrlCell url_cell { get; set; }

        public Modify_Msg_Operation()
        {

        }
    }

    public class CardCell
    {
        /// <summary>
        /// 推荐位展示的截止时间
        /// </summary>
        public long end_time { get; set; }
        /// <summary>
        /// 需要在运营位投放的卡券id
        /// </summary>
        public string card_id { get; set; }
    }

    public class UrlCell
    {
        /// <summary>
        /// 推荐位展示的截止时间
        /// </summary>
        public long end_time { get; set; }
        /// <summary>
        /// 文本内容
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string url { get; set; }
    }
}
