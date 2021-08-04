#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
    文件名：JsApiRequestData.cs
    文件功能描述：JsApi请求数据类
    
    
    创建标识：Senparc - 20210804
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class JsApiRequestData
    {
        public TenpayDateTime time_expire { get; set; }
        public Amount amount { get; set; }
        public string mchid { get; set; }
        public string description { get; set; }
        public string notify_url { get; set; }
        public Payer payer { get; set; }
        public string out_trade_no { get; set; }
        public string goods_tag { get; set; }
        public string appid { get; set; }
        public string attach { get; set; }
        public Detail detail { get; set; }
        public Scene_Info scene_info { get; set; }

        public JsApiRequestData(TenpayDateTime time_expire, Amount amount, string mchid, string description, string notify_url, Payer payer, string out_trade_no, string goods_tag, string appid, string attach, Detail detail, Scene_Info scene_info)
        {
            this.time_expire = time_expire;
            this.amount = amount;
            this.mchid = mchid;
            this.description = description;
            this.notify_url = notify_url;
            this.payer = payer;
            this.out_trade_no = out_trade_no;
            this.goods_tag = goods_tag;
            this.appid = appid;
            this.attach = attach;
            this.detail = detail;
            this.scene_info = scene_info;
        }
    }

    public class Amount
    {
        public int total { get; set; }
        public string currency { get; set; }
    }

    public class Payer
    {
        public string openid { get; set; }
    }

    public class Detail
    {
        public string invoice_id { get; set; }
        public Goods_Detail[] goods_detail { get; set; }
        public int cost_price { get; set; }
    }

    public class Goods_Detail
    {
        public string goods_name { get; set; }
        public string wechatpay_goods_id { get; set; }
        public int quantity { get; set; }
        public string merchant_goods_id { get; set; }
        public int unit_price { get; set; }
    }

    public class Scene_Info
    {
        public Store_Info store_info { get; set; }
        public string device_id { get; set; }
        public string payer_client_ip { get; set; }
    }

    public class Store_Info
    {
        public string address { get; set; }
        public string area_code { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

}
