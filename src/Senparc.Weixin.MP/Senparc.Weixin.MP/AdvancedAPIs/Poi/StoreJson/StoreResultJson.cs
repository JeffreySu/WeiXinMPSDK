
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

        public string telephone { get; set; }
        public List<string> categories { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public int offset_type { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public List<GetStoreList_BaseInfo_PhotoList> photo_list { get; set; }
        public string introduction { get; set; }
        public string recommend { get; set; }
        public string special { get; set; }
        public string open_time { get; set; }
        public int avg_price { get; set; }
        /// <summary>
        /// 门店是否可用状态。1 表示系统错误、2 表示审核中、3 审核通过、4 审核驳回。当该字段为1、2、4 状态时，poi_id 为空
        /// </summary>
        public int available_state { get; set; }
        public string district { get; set; }
        public int update_status { get; set; }
    }

    /// <summary>
    /// 获取门店类目表返回结果
    /// </summary>
    public class GetCategoryResult : WxJsonResult
    {
        public List<string> category_list { get; set; } 
    }

    public class GetStoreList_BaseInfo_PhotoList
    {
        public string photo_url { get; set; }
    }

}
