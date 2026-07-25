#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：TenPayJsonSerializer.cs
    文件功能描述：提供微信支付请求的 System.Text.Json 源生成序列化上下文


    创建标识：Senparc - 20260720

    修改标识：Senparc - 20260723
    修改描述：v1.19.0 新增支付请求源生成序列化上下文，提升裁剪与 Native AOT 兼容性

    修改标识：Senparc - 20260724
    修改描述：v1.19.0 增强微信支付请求模型的 Native AOT 序列化兼容能力

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Senparc.Weixin.TenPay.V3
{
    [JsonSourceGenerationOptions(
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        GenerationMode = JsonSourceGenerationMode.Metadata)]
    [JsonSerializable(typeof(TenpayV3ProfitShareingRequestData_ReceiverInfo[]))]
    [JsonSerializable(typeof(TenpayV3ProfitShareingAddReceiverRequestData_ReceiverInfo))]
    [JsonSerializable(typeof(TenpayV3ProfitShareing_ReceiverInfo))]
    [JsonSerializable(typeof(StoreInfoJson))]
    [JsonSerializable(typeof(H5InfoIosJson))]
    [JsonSerializable(typeof(H5InfoAndroidJson))]
    [JsonSerializable(typeof(H5InfoWapJson))]
    internal partial class TenPayJsonSerializerContext : JsonSerializerContext
    {
    }

    internal static class TenPayJsonSerializer
    {
        internal static string Serialize<T>(T value)
        {
            var jsonTypeInfo = TenPayJsonSerializerContext.Default.GetTypeInfo(typeof(T));
            if (jsonTypeInfo == null)
            {
                throw new NotSupportedException($"未生成类型 {typeof(T).FullName} 的 JSON 元数据。");
            }

            return JsonSerializer.Serialize(value, jsonTypeInfo);
        }

        internal static string SerializeCustomH5Info(IH5_Info value)
        {
            if (!JsonSerializer.IsReflectionEnabledByDefault)
            {
                throw new NotSupportedException(
                    $"Native AOT 不支持未注册的 H5 场景类型：{value.GetType().FullName}。" +
                    "请使用 H5_Info_IOS、H5_Info_Android 或 H5_Info_WAP。");
            }

#pragma warning disable IL2026
#pragma warning disable IL3050
            return JsonSerializer.Serialize(
                new Dictionary<string, object> { ["h5_info"] = value },
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });
#pragma warning restore IL3050
#pragma warning restore IL2026
        }
    }

    internal sealed class StoreInfoJson
    {
        public Store_Info store_info { get; set; }
    }

    internal sealed class H5InfoIosJson
    {
        public H5_Info_IOS h5_info { get; set; }
    }

    internal sealed class H5InfoAndroidJson
    {
        public H5_Info_Android h5_info { get; set; }
    }

    internal sealed class H5InfoWapJson
    {
        public H5_Info_WAP h5_info { get; set; }
    }
}
