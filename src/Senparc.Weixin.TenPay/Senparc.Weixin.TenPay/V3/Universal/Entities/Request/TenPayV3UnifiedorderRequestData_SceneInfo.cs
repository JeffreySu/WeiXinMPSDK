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
 
    文件名：TenPayV3UnifiedorderRequestData_SceneInfo.cs
    文件功能描述：TenPayV3UnifiedorderRequestData 的 SceneInfo 参数
    
    
    创建标识：Senparc - 20180223

    修改标识：Senparc - 20180223
    修改描述：v14.10.9 TenPayV3UnifiedorderRequestData_SceneInfo 支持新H5支付的场景参数
                       - https://github.com/JeffreySu/WeiXinMPSDK/issues/1111

    ----------------------------------------------------------------*/

//统一支付文档：https://pay.weixin.qq.com/wiki/doc/api/H5.php?chapter=9_1 
//H5统一支付文档：https://pay.weixin.qq.com/wiki/doc/api/H5.php?chapter=9_20&index=1

using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// TenPayV3UnifiedorderRequestData 的 SceneInfo 参数（非必填）
    /// </summary>
    public class TenPayV3UnifiedorderRequestData_SceneInfo
    {
        /// <summary>
        /// 统一支付接口信息（H5支付请留空）
        /// </summary>
        public Store_Info store_info { get; set; }

        /// <summary>
        /// H5统一支付接口信息（非H5支付请留空）
        /// </summary>
        public IH5_Info h5_info { get; set; }


        ///// <summary>
        ///// TenPayV3UnifiedorderRequestData_SceneInfo 构造函数
        ///// </summary>
        //public TenPayV3UnifiedorderRequestData_SceneInfo()
        //{
        //    store_info = new Store_Info();
        //}

        /// <summary>
        /// TenPayV3UnifiedorderRequestData_SceneInfo 构造函数
        /// </summary>
        /// <param name="isH5Pay">是否为H5支付</param>
        /// <param name="h5Info">当isH5Pay为true时填写，可以使用TenPayV3UnifiedorderRequestData_SceneInfo.GetH5InfoInstance&lt;T&gt;()方法获得</param>
        public TenPayV3UnifiedorderRequestData_SceneInfo(bool isH5Pay, IH5_Info h5Info=null)
        {
            if (!isH5Pay)
            {
                store_info = new Store_Info(); 
            }
            else
            {
                h5_info = h5Info;
            }
        }

        /// <summary>
        /// 获取 IH5_Info 接口示例，可用类型：H5_Info_IOS，H5_Info_Android，H5_Info_WAP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">场景类型，如：IOS，Android，Wap</param>
        /// <returns></returns>
        public static T GetH5InfoInstance<T>(string type) where T : IH5_Info, new()
        {
            var t = new T();
            t.type = type;
            return t;
        }

        /// <summary>
        /// <para>常规输出JSON格式：{"store_info":{"id": "门店ID","name": "名称","area_code": "编码","address": "地址" }}</para>
        /// <para>H5支付JSON格式：{"h5_info": {"type":"IOS","app_name": "王者荣耀","bundle_id": "com.tencent.wzryIOS"}}</para>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (store_info != null)
            {
                return new { store_info = store_info }.ToJson();
            }

            if (h5_info != null)
            {
                return new { h5_info = h5_info }.ToJson();
            }

            return "";
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


    /// <summary>
    /// H5支付信息
    /// </summary>
    public interface IH5_Info
    {
        /// <summary>
        /// 场景类型
        /// </summary>
        string type { get; set; }
    }

    /// <summary>
    /// H5支付-IOS移动应用
    /// </summary>
    public class H5_Info_IOS : IH5_Info
    {
        /// <summary>
        /// 场景类型，如IOS
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 场景类型
        /// </summary>
        public string app_name { get; set; }
        /// <summary>
        /// bundle_id
        /// </summary>
        public string bundle_id { get; set; }
    }

    /// <summary>
    /// H5支付-安卓移动应用
    /// </summary>
    public class H5_Info_Android : IH5_Info
    {
        /// <summary>
        /// 场景类型，如Android
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 应用名
        /// </summary>
        public string app_name { get; set; }
        /// <summary>
        /// 包名
        /// </summary>
        public string package_name { get; set; }
    }

    /// <summary>
    /// H5支付-WAP网站应用
    /// </summary>
    public class H5_Info_WAP : IH5_Info
    {
        /// <summary>
        /// 场景类型，如Wap
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// WAP网站URL地址
        /// </summary>
        public string wap_url { get; set; }
        /// <summary>
        /// WAP 网站名
        /// </summary>
        public string wap_name { get; set; }
    }

}
