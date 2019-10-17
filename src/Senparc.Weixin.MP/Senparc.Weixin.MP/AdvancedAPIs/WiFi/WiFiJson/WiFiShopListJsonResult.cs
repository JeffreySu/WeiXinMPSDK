#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：WiFiShopListJsonResult.cs
    文件功能描述：获取Wi-Fi门店列表的返回结果
    
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
   public class WiFiShopListJsonResult : WxJsonResult
    {
       /// <summary>
       /// 获取Wi-Fi门店列表的返回结果
       /// </summary>
       public ShopList_Data data { get; set; }
     }
   public class ShopList_Data
   {
       /// <summary>
       /// 总数
       /// </summary>
       public int totalcount { get; set; }
       /// <summary>
       /// 分页下标
       /// </summary>
       public int pageindex { get; set; }
       /// <summary>
       /// 分页页数
       /// </summary>
       public int pagecount { get; set; }
       public List<ShopList_Records> records { get; set; }
     } 
    /// <summary>
    /// 当前页列表数组
    /// </summary>
   public class ShopList_Records
   {
       /// <summary>
       ///  门店ID
       /// </summary>
       public string shop_id { get; set; }
       /// <summary>
       ///门店名称
       /// </summary>
       public string shop_name { get; set; }
       /// <summary>
       /// 无线网络设备的ssid，未添加设备为空，多个ssid时显示第一个
       /// </summary>
       public string ssid { get; set; }
       /// <summary>
       /// 无线网络设备的ssid列表，返回数组格式
       /// </summary>
       public List<string> ssid_list { get; set; }
       /// <summary>
       /// 门店内设备的设备类型，0-未添加设备，1-专业型设备，4-密码型设备，5-portal自助型设备，31-portal改造型设备
       /// </summary>
       public int protocol_type { get; set; }
       /// <summary>
       /// 商户自己的id，与门店poi_id对应关系，建议在添加门店时候建立关联关系，具体请参考“微信门店接口”
       /// </summary>
       public string sid { get; set; }
   }
}
