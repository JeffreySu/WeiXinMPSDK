
#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright(C) 2017 Senparc
    
    文件名：StoreResultJson.cs
    文件功能描述：门店相关接口返回结果
    
    
    创建标识：Senparc - 20150513

    修改标识：Senparc - 20170222
    修改描述：v14.3.128 完善GetStoreList_BaseInfo信息
    
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Poi
{
    /// <summary>
    /// 查询门店信息返回结果
    /// </summary>
    public class GetStoreResultJson : WxJsonResult
    {
        /// <summary>
        /// 门店信息
        /// </summary>
        public GetStore_Business business { get; set; }
    }

    public class GetStore_Business
    {
        /// <summary>
        /// 查询门店基础信息
        /// </summary>
        public GetStoreBaseInfo base_info { get; set; }
    }

    /// <summary>
    /// 查询门店基础信息
    /// </summary>
    public class GetStoreBaseInfo : StoreBaseInfo
    {
        /// <summary>
        /// 门店是否可用状态。1 表示系统错误、2 表示审核中、3 审核通过、4 审核驳回。当该字段为1、2、4 状态时，poi_id 为空
        /// </summary>
        public int available_state { get; set; }
        /// <summary>
        /// 扩展字段是否正在更新中。1 表示扩展字段正在更新中，尚未生效，不允许再次更新； 0 表示扩展字段没有在更新中或更新已生效，可以再次更新
        /// </summary>
        public int update_status { get; set; }
    }

    /// <summary>
    /// 查询门店列表返回结果
    /// </summary>
    public class GetStoreListResultJson : WxJsonResult
    {
        public List<GetStoreList_Business> business_list { get; set; }
        /// <summary>
        /// 门店总数量
        /// </summary>
        public string total_count { get; set; }
    }

    public class GetStoreList_Business
    {
        public GetStoreList_BaseInfo base_info { get; set; }
    }

    public class GetStoreList_BaseInfo
    {
        #region 数据示例
        /*
        数据示例：
为审核通过，有poi_id，全部字段；第二条为审核不通过，仅有基础字段；第三条为审核中，仅有基础字段。
{
 "errcode":0,
 "errmsg":"ok"
 "business_list":[
{"base_info":{
                "sid":"101",
                "business_name":"麦当劳",
                "branch_name":"艺苑路店",
                "address":"艺苑路11号",
                "telephone":"020-12345678",
                "categories":["美食,快餐小吃"],
                "city":"广州市",
                "province":"广东省",
                "offset_type":1,
                "longitude":115.32375,
                "latitude":25.097486,
                "photo_list":[{"photo_url":"http: ...."}],
                "introduction":"麦当劳是全球大型跨国连锁餐厅，1940 年创立于美国，在世界上大约拥有3 万间分店。主要售卖汉堡包，以及薯条、炸鸡、汽水、冰品、沙拉、水果等快餐食品",
                "recommend":"麦辣鸡腿堡套餐，麦乐鸡，全家桶",
                "special":"免费wifi，外卖服务",
                "open_time":"8:00-20:00",
                "avg_price":35,
                "poi_id":"285633617",
                "available_state":3,
                "district":"海珠区",
                "update_status":0
              }},
{"base_info":{
                "sid":"101",
                "business_name":"麦当劳",
                "branch_name":"北京路店",
                "address":"北京路12号",
                "telephone":"020-12345689",
                "categories":["美食,快餐小吃"],
                "city":"广州市",
                "province":"广东省",
                "offset_type":1,
                "longitude":115.3235,
                "latitude":25.092386,
                "photo_list":[{"photo_url":"http: ...."}],
                "introduction":"麦当劳是全球大型跨国连锁餐厅，1940 年创立于美国，在世界上大约拥有3 万间分店。主要售卖汉堡包，以及薯条、炸鸡、汽水、冰品、沙拉、水果等快餐食品",
                "recommend":"麦辣鸡腿堡套餐，麦乐鸡，全家桶",
                "special":"免费wifi，外卖服务",
                "open_time":"8:00-20:00",
                "avg_price":35,
                "poi_id":"285633618",
                "available_state":4,
                "district":"越秀区",
                "update_status":0
              }},
 {"base_info":{
                "sid":"101",
                "business_name":"麦当劳",
                "branch_name":"龙洞店",
                "address":"迎龙路122号",
                "telephone":"020-12345659",
                "categories":["美食,快餐小吃"],
                "city":"广州市",
                "province":"广东省",
                "offset_type":1,
                "longitude":115.32345,
                "latitude":25.056686,
                "photo_list":[{"photo_url":"http: ...."}],
                "introduction":"麦当劳是全球大型跨国连锁餐厅，1940 年创立于美国，在世界上大约拥有3 万间分店。主要售卖汉堡包，以及薯条、炸鸡、汽水、冰品、沙拉、水果等快餐食品",
                "recommend":"麦辣鸡腿堡套餐，麦乐鸡，全家桶",
                "special":"免费wifi，外卖服务",
                "open_time":"8:00-20:00",
                "avg_price":35,
                "poi_id":"285633619",
                "available_state":2,
                "district":"天河区",
                "update_status":0
              }},
],
       "total_count":"3",
}
*/
        #endregion

        public string sid { get; set; }
        /// <summary>
        /// 审核通过才会返回此字段
        /// </summary>
        public string poi_id { get; set; }
        /// <summary>
        /// 门店名称（仅为商户名，如：国美、麦当劳，不应包含地区、店号等信息，错误示例：北京国美）
        /// </summary>
        public string business_name { get; set; }
        /// <summary>
        /// 分店名称（不应包含地区信息、不应与门店名重复，错误示例：北京王府井店）
        /// </summary>
        public string branch_name { get; set; }
        /// <summary>
        /// 门店所在的详细街道地址（不要填写省市信息）
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 门店是否可用状态。1 表示系统错误、2 表示审核中、3 审核通过、4 审核驳回。当该字段为1、2、4 状态时，poi_id 为空
        /// </summary>
        public int? available_state { get; set; }

        /// <summary>
        /// add by ray
        /// 门店的电话（纯数字，区号、分机号均由“-”隔开）
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// add by ray
        /// 门店的类型（不同级分类用“,”隔开，如：美食，川菜，火锅。详细分类参见附件：微信门店类目表）
        /// </summary>
        public string[] categories { get; set; }

        /// <summary>
        /// add by ray
        /// 门店所在的城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// add by ray
        /// 门店所在的省份（直辖市填城市名,如：北京市）
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// add by ray
        /// 坐标类型，1 为火星坐标（目前只能选1）
        /// </summary>
        public int? offset_type { get; set; }

        /// <summary>
        /// add by ray
        /// 门店所在地理位置的经度（经纬度均为火星坐标，最好选用腾讯地图标记的坐标）
        /// </summary>
        public decimal? longitude { get; set; }

        /// <summary>
        /// add by ray
        /// 门店所在地理位置的纬度（经纬度均为火星坐标，最好选用腾讯地图标记的坐标）
        /// </summary>
        public decimal? latitude { get; set; }

        /// <summary>
        /// add by ray
        /// 图片列表，url 形式，可以有多张图片，尺寸为640*340px。必须为上一接口生成的url。图片内容不允许与门店不相关，不允许为二维码、员工合照（或模特肖像）、营业执照、无门店正门的街景、地图截图、公交地铁站牌、菜单截图等
        /// </summary>
        public PoiPhoto[] photo_list { get; set; }

        /// <summary>
        /// add by ray
        /// 商户简介，主要介绍商户信息等
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// add by ray
        /// 商户简介，主要介绍商户信息等
        /// </summary>
        public string recommend { get; set; }

        /// <summary>
        /// add by ray
        /// 特色服务，如免费wifi，免费停车，送货上门等商户能提供的特色功能或服务
        /// </summary>
        public string special { get; set; }

        /// <summary>
        /// add by ray
        /// 营业时间，24 小时制表示，用“-”连接，如 8:00-20:00
        /// </summary>
        public string open_time { get; set; }

        /// <summary>
        /// add by ray
        /// 人均价格，大于0 的整数
        /// </summary>
        public int? avg_price { get; set; }

        /// <summary>
        /// add by ray
        /// 门店所在地区
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// add by ray
        /// 更新状态
        /// </summary>
        public int? update_status { get; set; }
    }

    /// <summary>
    /// add by ray
    /// 门店图片
    /// </summary>
    public class PoiPhoto
    {
        /// <summary>
        /// 图片Url
        /// </summary>
        public string photo_url { get; set; }
    }

    /// <summary>
    /// 获取门店类目表返回结果
    /// </summary>
    public class GetCategoryResult : WxJsonResult
    {
        public List<string> category_list { get; set; } 
    }
}
