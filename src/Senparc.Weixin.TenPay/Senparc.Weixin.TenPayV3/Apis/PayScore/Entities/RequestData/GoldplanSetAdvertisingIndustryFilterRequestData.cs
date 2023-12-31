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
  
    文件名：GoldplanSetAdvertisingIndustryFilterRequestData.cs
    文件功能描述：微信支付V3同业过滤标签管理请求数据
    
    
    创建标识：Senparc - 20230602
    
----------------------------------------------------------------*/


using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3同业过滤标签管理请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter8_5_3.shtml </para>
    /// </summary>
    public class GoldplanSetAdvertisingIndustryFilterRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public GoldplanSetAdvertisingIndustryFilterRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">特约商户号  <para>开通或关闭点金计划的特约商户商户号，由微信支付生成并下发。</para><para>示例值：1234567890</para></param>
        /// <param name="advertising_industry_filters">同业过滤标签值 <para>特约商户同业过滤的同业过滤标签值，同业过滤标签最少传一个，最多三个。如已设置同业过滤标签，再次请求传入，视为新增，将覆盖原有同业标签配置</para></param>
        public GoldplanSetAdvertisingIndustryFilterRequestData(string sub_mchid, List<string> advertising_industry_filters)
        { 
            this.sub_mchid = sub_mchid;
            this.advertising_industry_filters = advertising_industry_filters;
        }

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 同业过滤标签值
        /// <para>特约商户同业过滤的同业过滤标签值，同业过滤标签最少传一个，最多三个。如已设置同业过滤标签，再次请求传入，视为新增，将覆盖原有同业标签配置</para>
        /// <para>枚举值：</para>
        /// <para>
        /// E_COMMERCE：综合电商平台
        /// LOVE_MARRIAGE：婚恋
        /// POTOGRAPHY：摄影摄像机构及服务
        /// EDUCATION：教育
        /// FINANCE：金融
        /// TOURISM：旅游
        /// SKINCARE：护肤彩妆
        /// FOOD：食品
        /// SPORT：运动户外
        /// JEWELRY_WATCH：珠宝钟表
        /// HEALTHCARE：医疗健康
        /// BUSSINESS：商务服务
        /// PARENTING：亲子
        /// CATERING：餐饮美食
        /// RETAIL：零售百货
        /// SERVICES：生活服务
        /// LAW：法律服务
        /// ESTATE：房地产
        /// TRANSPORTATION：交通运输
        /// ENERGY_SAVING：节能环保
        /// SECURITY：安全安保
        /// BUILDING_MATERIAL：家居装修建材
        /// COMMUNICATION：通讯及IT服务
        /// MERCHANDISE：日用百货
        /// ASSOCIATION：机构协会
        /// COMMUNITY：网络资讯及社区
        /// ONLINE_AVR：在线视听与阅读
        /// WE_MEDIA：自媒体
        /// CAR：汽车
        /// SOFTWARE：软件工具
        /// GAME：游戏
        /// CLOTHING：服饰鞋帽箱包
        /// INDUSTY：工业工程
        /// AGRICULTURE：农林牧渔
        /// PUBLISHING_MEDIA：出版传媒
        /// HOME_DIGITAL：数码家电
        /// </para>
        /// <para>注：若配置完成后需清空标签的，可登陆商户平台手动清空（路径：商户平台->服务商功能->点金计划->特约商户管理->同业过滤标签）</para>
        /// <para>示例值：SOFTWARE,SECURITY,LOVE_MARRIAGE</para>
        /// </summary>
        public List<string> advertising_industry_filters { get; set; }

    }
}
