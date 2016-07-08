/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
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

   }
}
