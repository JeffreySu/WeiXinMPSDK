#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：PatchBusifavorStockInformationRequestData.cs
    文件功能描述：修改商家券基本信息接口请求数据
    
    
    创建标识：Senparc - 20210914
    
    修改标识：Senparc - 20230106
    修改描述：v0.6.8.2 更新 ModifyBusifavorStockInformationRequestData 参数，删除 stock_id

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 修改商家券基本信息接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_12.shtml </para>
    /// </summary>
    public class ModifyBusifavorStockInformationRequestData
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="custom_entrance">自定义入口 <para>body卡详情页面，可选择多种入口引导用户</para><para>可为null</para></param>
        /// <param name="stock_name">商家券批次名称 <para>body批次名称，字数上限为21个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：8月1日活动券</para><para>可为null</para></param>
        /// <param name="comment">批次备注 <para>body仅配置商户可见，用于自定义信息。字数上限为20个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：活动使用</para><para>可为null</para></param>
        /// <param name="goods_name">适用商品范围 <para>body用来描述批次在哪些商品可用，会显示在微信卡包中。字数上限为15个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：xxx商品使用</para><para>可为null</para></param>
        /// <param name="out_request_no">商户请求单号 <para>body商户修改批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性。</para><para>示例值：6122352020010133287985742</para></param>
        /// <param name="display_pattern_info">样式信息 <para>body创建批次时的样式信息。</para><para>可为null</para></param>
        /// <param name="coupon_use_rule">核销规则 <para>body券核销相关规则</para><para>可为null</para></param>
        /// <param name="stock_send_rule">发放规则 <para>body券发放相关规则</para><para>可为null</para></param>
        /// <param name="notify_config">事件通知配置 <para>body事件回调通知商户的配置</para><para>可为null</para></param>
        public ModifyBusifavorStockInformationRequestData(Custom_Entrance custom_entrance, string stock_name, string comment, string goods_name, string out_request_no, Display_Pattern_Info display_pattern_info, Coupon_Use_Rule coupon_use_rule, Stock_Send_Rule stock_send_rule, Notify_Config notify_config)
        {
            this.custom_entrance = custom_entrance;
            this.stock_name = stock_name;
            this.comment = comment;
            this.goods_name = goods_name;
            this.out_request_no = out_request_no;
            this.display_pattern_info = display_pattern_info;
            this.coupon_use_rule = coupon_use_rule;
            this.stock_send_rule = stock_send_rule;
            this.notify_config = notify_config;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ModifyBusifavorStockInformationRequestData()
        {
        }


        /// <summary>
        /// 自定义入口
        /// <para>body卡详情页面，可选择多种入口引导用户 </para>
        /// <para>可为null</para>
        /// </summary>
        public Custom_Entrance custom_entrance { get; set; }

        /// <summary>
        /// 商家券批次名称
        /// <para>body批次名称，字数上限为21个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
        /// <para>示例值：8月1日活动券</para>
        /// <para>可为null</para>
        /// </summary>
        public string stock_name { get; set; }

        /// <summary>
        /// 批次备注
        /// <para>body仅配置商户可见，用于自定义信息。字数上限为20个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
        /// <para>示例值：活动使用</para>
        /// <para>可为null</para>
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 适用商品范围
        /// <para>body用来描述批次在哪些商品可用，会显示在微信卡包中。字数上限为15个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
        /// <para>示例值：xxx商品使用</para>
        /// <para>可为null</para>
        /// </summary>
        public string goods_name { get; set; }

        /// <summary>
        /// 商户请求单号
        /// <para>body商户修改批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性。 </para>
        /// <para>示例值：6122352020010133287985742</para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 样式信息
        /// <para>body创建批次时的样式信息。</para>
        /// <para>可为null</para>
        /// </summary>
        public Display_Pattern_Info display_pattern_info { get; set; }

        /// <summary>
        /// 核销规则
        /// <para>body券核销相关规则 </para>
        /// <para>可为null</para>
        /// </summary>
        public Coupon_Use_Rule coupon_use_rule { get; set; }

        /// <summary>
        /// 发放规则
        /// <para>body券发放相关规则 </para>
        /// <para>可为null</para>
        /// </summary>
        public Stock_Send_Rule stock_send_rule { get; set; }

        /// <summary>
        /// 事件通知配置
        /// <para>body事件回调通知商户的配置 </para>
        /// <para>可为null</para>
        /// </summary>
        public Notify_Config notify_config { get; set; }

        #region 子数据类型
        public class Custom_Entrance
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="mini_programs_info">小程序入口 <para>需要小程序APPID、path、入口文案、引导文案。如果需要跳转小程序，APPID、path、入口文案为必填，引导文案非必填。appid要与归属商户号有绑定关系</para><para>可为null</para></param>
            /// <param name="appid">商户公众号appid <para>可配置商户公众号，从券详情可跳转至公众号</para><para>示例值：wx324345hgfhfghfg</para><para>可为null</para></param>
            /// <param name="hall_id">营销馆id <para>填写微信支付营销馆的馆id，用户自定义字段。营销馆需在商户平台创建。</para><para>示例值：233455656</para><para>可为null</para></param>
            /// <param name="code_display_mode">code展示模式 <para>枚举值：NOT_SHOW：不展示codeBARCODE：一维码QRCODE：二维码</para><para>示例值：BARCODE</para><para>可为null</para></param>
            public Custom_Entrance(Mini_Programs_Info mini_programs_info, string appid, string hall_id, string code_display_mode)
            {
                this.mini_programs_info = mini_programs_info;
                this.appid = appid;
                this.hall_id = hall_id;
                this.code_display_mode = code_display_mode;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Custom_Entrance()
            {
            }

            /// <summary>
            /// 小程序入口
            /// <para>需要小程序APPID、path、入口文案、引导文案。如果需要跳转小程序，APPID、path、入口文案为必填，引导文案非必填。appid要与归属商户号有绑定关系 </para>
            /// <para>可为null</para>
            /// </summary>
            public Mini_Programs_Info mini_programs_info { get; set; }

            /// <summary>
            /// 商户公众号appid
            /// <para>可配置商户公众号，从券详情可跳转至公众号 </para>
            /// <para>示例值：wx324345hgfhfghfg</para>
            /// <para>可为null</para>
            /// </summary>
            public string appid { get; set; }

            /// <summary>
            /// 营销馆id
            /// <para>填写微信支付营销馆的馆id，用户自定义字段。 营销馆需在商户平台 创建。 </para>
            /// <para>示例值：233455656 </para>
            /// <para>可为null</para>
            /// </summary>
            public string hall_id { get; set; }

            /// <summary>
            /// code展示模式
            /// <para>枚举值： NOT_SHOW：不展示 code BARCODE：一维码 QRCODE：二维码 </para>
            /// <para>示例值：BARCODE</para>
            /// <para>可为null</para>
            /// </summary>
            public string code_display_mode { get; set; }

            #region 子数据类型
            public class Mini_Programs_Info
            {

                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="mini_programs_appid">商家小程序appid <para>需要小程序appid与归属商户号有绑定关系</para><para>示例值：wx234545656765876</para></param>
                /// <param name="mini_programs_path">商家小程序path <para>商家小程序path</para><para>示例值：/path/index/index</para></param>
                /// <param name="entrance_words">入口文案 <para>入口文案，字数上限为5个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：欢迎选购</para></param>
                /// <param name="guiding_words">引导文案 <para>小程序入口引导文案，字数上限为6个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：获取更多优惠</para><para>可为null</para></param>
                public Mini_Programs_Info(string mini_programs_appid, string mini_programs_path, string entrance_words, string guiding_words)
                {
                    this.mini_programs_appid = mini_programs_appid;
                    this.mini_programs_path = mini_programs_path;
                    this.entrance_words = entrance_words;
                    this.guiding_words = guiding_words;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Mini_Programs_Info()
                {
                }

                /// <summary>
                /// 商家小程序appid
                /// <para>需要小程序appid与归属商户号有绑定关系 </para>
                /// <para>示例值：wx234545656765876</para>
                /// </summary>
                public string mini_programs_appid { get; set; }

                /// <summary>
                /// 商家小程序path
                /// <para>商家小程序path </para>
                /// <para>示例值：/path/index/index</para>
                /// </summary>
                public string mini_programs_path { get; set; }

                /// <summary>
                /// 入口文案
                /// <para>入口文案，字数上限为5个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
                /// <para>示例值：欢迎选购</para>
                /// </summary>
                public string entrance_words { get; set; }

                /// <summary>
                /// 引导文案
                /// <para>小程序入口引导文案，字数上限为6个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
                /// <para>示例值：获取更多优惠</para>
                /// <para>可为null</para>
                /// </summary>
                public string guiding_words { get; set; }

            }


            #endregion
        }

        public class Display_Pattern_Info
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="description">使用须知 <para>用于说明详细的活动规则，会展示在代金券详情页。</para><para>示例值：xxx门店可用</para><para>可为null</para></param>
            /// <param name="merchant_logo_url">商户logo <para>商户logo的URL地址，可通过《图片上传API》获得图片URL地址。1、商户logo大小需为120像素*120像素。2、支持JPG/JPEG/PNG格式，且图片小于1M。</para><para>示例值：https://xxx</para><para>可为null</para></param>
            /// <param name="merchant_name">商户名称 <para>商户名称，字数上限为16个，一个中文汉字/英文字母/数字均占用一个字数。</para><para>示例值：微信支付</para><para>可为null</para></param>
            /// <param name="background_color">背景颜色 <para>代金券的背景颜色，可设置10种颜色，色值请参考卡券背景颜色图。颜色取值为颜色图中的颜色名称。</para><para>示例值：Color020</para><para>可为null</para></param>
            /// <param name="coupon_image_url">券详情图片 <para>券详情图片，850像素*350像素，且图片大小不超过2M，支持JPG/PNG格式，可通过《图片上传API》获得图片URL地址。</para><para>示例值：https://qpic.cn/xxx</para><para>可为null</para></param>
            public Display_Pattern_Info(string description, string merchant_logo_url, string merchant_name, string background_color, string coupon_image_url)
            {
                this.description = description;
                this.merchant_logo_url = merchant_logo_url;
                this.merchant_name = merchant_name;
                this.background_color = background_color;
                this.coupon_image_url = coupon_image_url;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Display_Pattern_Info()
            {
            }

            /// <summary>
            /// 使用须知
            /// <para>用于说明详细的活动规则，会展示在代金券详情页。</para>
            /// <para>示例值：xxx门店可用</para>
            /// <para>可为null</para>
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 商户logo
            /// <para>商户logo的URL地址， 可通过《图片上传API》获得图片URL地址。 1、商户logo大小需为120像素*120像素。 2、支持JPG/JPEG/PNG格式，且图片小于1M。 </para>
            /// <para>示例值：https://xxx</para>
            /// <para>可为null</para>
            /// </summary>
            public string merchant_logo_url { get; set; }

            /// <summary>
            /// 商户名称
            /// <para>商户名称，字数上限为16个，一个中文汉字/英文字母/数字均占用一个字数。 </para>
            /// <para>示例值：微信支付</para>
            /// <para>可为null</para>
            /// </summary>
            public string merchant_name { get; set; }

            /// <summary>
            /// 背景颜色
            /// <para>代金券的背景颜色，可设置10种颜色，色值请参考卡券背景颜色图。颜色取值为颜色图中的颜色名称。</para>
            /// <para>示例值：Color020</para>
            /// <para>可为null</para>
            /// </summary>
            public string background_color { get; set; }

            /// <summary>
            /// 券详情图片
            /// <para>券详情图片，850像素*350像素，且图片大小不超过2M，支持JPG/PNG格式，可通过《图片上传API》获得图片URL地址。 </para>
            /// <para>示例值：https://qpic.cn/xxx</para>
            /// <para>可为null</para>
            /// </summary>
            public string coupon_image_url { get; set; }

        }

        public class Coupon_Use_Rule
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="use_method">核销方式 <para>核销方式,枚举值：OFF_LINE：线下滴码核销MINI_PROGRAMS：小程序核销SELF_CONSUME：用户自主核销PAYMENT_CODE：微信支付付款码核销</para><para>示例值：OFF_LINE</para><para>可为null</para></param>
            /// <param name="mini_programs_appid">小程序appid <para>核销方式为线上小程序核销才有效</para><para>示例值：wx23232232323</para><para>可为null</para></param>
            /// <param name="mini_programs_path">小程序path <para>核销方式为线上小程序核销才有效</para><para>示例值：/path/index/index</para><para>可为null</para></param>
            public Coupon_Use_Rule(string use_method, string mini_programs_appid, string mini_programs_path)
            {
                this.use_method = use_method;
                this.mini_programs_appid = mini_programs_appid;
                this.mini_programs_path = mini_programs_path;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Coupon_Use_Rule()
            {
            }

            /// <summary>
            /// 核销方式
            /// <para> 核销方式,枚举值： OFF_LINE：线下滴码核销 MINI_PROGRAMS：小程序核销 SELF_CONSUME：用户自主核销  PAYMENT_CODE：微信支付付款码核销 </para>
            /// <para>示例值：OFF_LINE</para>
            /// <para>可为null</para>
            /// </summary>
            public string use_method { get; set; }

            /// <summary>
            /// 小程序appid
            /// <para>核销方式为线上小程序核销才有效 </para>
            /// <para>示例值：wx23232232323</para>
            /// <para>可为null</para>
            /// </summary>
            public string mini_programs_appid { get; set; }

            /// <summary>
            /// 小程序path
            /// <para>核销方式为线上小程序核销才有效 </para>
            /// <para>示例值：/path/index/index</para>
            /// <para>可为null</para>
            /// </summary>
            public string mini_programs_path { get; set; }

        }

        public class Stock_Send_Rule
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="prevent_api_abuse">可疑账号拦截 <para>不填默认否，枚举值：true：是false：否</para><para>示例值：false</para><para>可为null</para></param>
            public Stock_Send_Rule(bool prevent_api_abuse)
            {
                this.prevent_api_abuse = prevent_api_abuse;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Stock_Send_Rule()
            {
            }

            /// <summary>
            /// 可疑账号拦截
            /// <para>不填默认否，枚举值： true：是 false：否 </para>
            /// <para>示例值：false</para>
            /// <para>可为null</para>
            /// </summary>
            public bool prevent_api_abuse { get; set; }

        }

        public class Notify_Config
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="notify_appid">事件通知appid <para>用于回调通知时，计算返回操作用户的openid（诸如领券用户），支持小程序or公众号的APPID；如该字段不填写，则回调通知中涉及到用户身份信息的openid与unionid都将为空。</para><para>示例值：wx23232232323</para><para>可为null</para></param>
            public Notify_Config(string notify_appid)
            {
                this.notify_appid = notify_appid;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Notify_Config()
            {
            }

            /// <summary>
            /// 事件通知appid
            /// <para>用于回调通知时，计算返回操作用户的openid（诸如领券用户），支持小程序or公众号的APPID；如该字段不填写，则回调通知中涉及到用户身份信息的openid与unionid都将为空。 </para>
            /// <para>示例值：wx23232232323</para>
            /// <para>可为null</para>
            /// </summary>
            public string notify_appid { get; set; }

        }
        #endregion
    }
}
