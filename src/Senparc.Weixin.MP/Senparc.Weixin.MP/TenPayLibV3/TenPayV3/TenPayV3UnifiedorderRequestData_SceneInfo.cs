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
 
    文件名：TenPayV3UnifiedorderRequestData_SceneInfo.cs
    文件功能描述：TenPayV3UnifiedorderRequestData 的 SceneInfo 参数
    
    
    创建标识：Senparc - 20180223
----------------------------------------------------------------*/

//文档：https://pay.weixin.qq.com/wiki/doc/api/H5.php?chapter=9_1

using Senparc.Weixin.Helpers.Extensions;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// TenPayV3UnifiedorderRequestData 的 SceneInfo 参数（非必填）
    /// </summary>
    public class TenPayV3UnifiedorderRequestData_SceneInfo
    {
        public Store_Info store_info { get; set; }

        public TenPayV3UnifiedorderRequestData_SceneInfo()
        {
            store_info = new Store_Info();
        }

        /// <summary>
        /// 输出JSON格式：{"store_info":{"id": "门店ID","name": "名称","area_code": "编码","address": "地址" }}
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToJson();
        }
    }

    /// <summary>
    /// store_info 数据
    /// </summary>
    public class Store_Info
    {
        /// <summary>
        /// （非必填）门店id，门店唯一标识，String(32)
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// （非必填）门店名称，String(64)
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// （非必填）门店行政区划码，新县及县以上行政区划代码》：https://pay.weixin.qq.com/wiki/doc/api/download/store_adress.csv
        /// </summary>
        public string area_code { get; set; }
        /// <summary>
        /// （非必填）门店详细地址，String(128)
        /// </summary>
        public string address { get; set; }
    }

}
