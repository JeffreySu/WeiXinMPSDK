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
    
    文件名：WiFiShopGetJsonResult.cs
    文件功能描述：查询门店Wi-Fi信息的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 查询门店Wi-Fi信息的返回结果
    /// </summary>
    public class WiFiShopGetJsonResult : WxJsonResult
    {
        public ShopGet_Data data { get; set; }
        
    }

    public class ShopGet_Ssid_Password_List
    {
        /// <summary>
        /// 无线网络设备的ssid，未添加设备为空，多个ssid时显示第一个
        /// </summary>
        public string ssid { get; set; }
        public string password { get; set; }
    }

    public class ShopGet_Data
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shop_name { get; set; }
        /// <summary>
        /// 无线网络设备的ssid，未添加设备为空，多个ssid时显示第一个
        /// </summary>
        public string ssid { get; set; }
        /// <summary>
        /// 无线网络设备的ssid列表，返回数组格式
        /// </summary>
        // public List<string> ssid_list { get; set; }
        public List<string> ssid_list { get; set; }



        /// <summary>
        /// ssid和密码的列表，数组格式。当为密码型设备时，密码才有值
        /// </summary>
        public List<ShopGet_Ssid_Password_List> ssid_password_list { get; set; }


        /// <summary>
        /// 设备密码，当设备类型为密码型时返回
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 门店内设备的设备类型，0-未添加设备，4-密码型设备，31-portal型设备
        /// </summary>
        public int protocol_type { get; set; }
        /// <summary>
        /// 门店内设备总数
        /// </summary>
        public int ap_count { get; set; }
        /// <summary>
        /// 商家主页模板类型
        /// </summary>
        public int template_id { get; set; }
        /// <summary>
        /// 商家主页链接
        /// </summary>
        public string homepage_url { get; set; }
        /// <summary>
        /// 顶部常驻入口上显示的文本内容：0--欢迎光临+公众号名称；1--欢迎光临+门店名称；2--已连接+公众号名称+WiFi；3--已连接+门店名称+Wi-Fi
        /// </summary>
        public int bar_type { get; set; }
        /// <summary>
        /// 商户自己的id，与门店poi_id对应关系，建议在添加门店时候建立关联关系，具体请参考“微信门店接口”
        /// </summary>
        public string sid { get; set; }

        /// <summary>
        /// 门店ID（适用于微信卡券、微信门店业务），具体定义参考微信门店，与shop_id一一对应。
        /// </summary>
        public string poi_id { get; set; }

        /// <summary>
        /// 商家主页（bar条）跳转的小程序原始id，template_id为2时有效
        /// </summary>
        public string homepage_wxa_user_name { get; set; }
        /// <summary>
        /// 商家主页（bar条）跳转的小程序路径，template_id为2时有效
        /// </summary>
        public string homepage_wxa_path { get; set; }
        /// <summary>
        /// 连网完成页链接
        /// </summary>
        public string finishpage_url { get; set; }
        /// <summary>
        /// 完成页跳转的小程序原始id，finishpage_type为1时有效
        /// </summary>
        public string finishpage_wxa_user_name { get; set; }
        /// <summary>
        /// 完成页跳转的小程序路径，需要做urlencode，finishpage_type为1时有效
        /// </summary>
        public string finishpage_wxa_path { get; set; }
        /// <summary>
        /// 完成页跳转类型 0为H5；1为小程序
        /// </summary>
        public int finishpage_type { get; set; }

    }
}
