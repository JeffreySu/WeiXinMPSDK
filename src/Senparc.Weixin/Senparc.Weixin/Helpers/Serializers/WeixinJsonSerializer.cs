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

    文件名：WeixinJsonSerializer.cs
    文件功能描述：提供微信 JSON 类型的 System.Text.Json 源生成序列化能力


    创建标识：Senparc - 20260720

    修改标识：Senparc - 20260723
    修改描述：v6.25.0 新增内置 JSON 源生成上下文和裁剪安全的序列化帮助方法

----------------------------------------------------------------*/

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.Helpers.Serializers
{
    /// <summary>
    /// Senparc.Weixin 内置 JSON 类型的源生成上下文。
    /// </summary>
    [JsonSourceGenerationOptions(
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        GenerationMode = JsonSourceGenerationMode.Metadata,
        PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(WxJsonResult))]
    public partial class WeixinJsonSerializerContext : JsonSerializerContext
    {
    }

    /// <summary>
    /// 支持裁剪及 Native AOT 的 JSON 序列化帮助类。
    /// </summary>
    public static class WeixinJsonSerializer
    {
        /// <summary>
        /// 使用调用方提供的源生成元数据序列化对象。
        /// </summary>
        public static string Serialize<T>(T value, JsonTypeInfo<T> jsonTypeInfo)
        {
            if (jsonTypeInfo == null)
            {
                throw new ArgumentNullException(nameof(jsonTypeInfo));
            }

            return JsonSerializer.Serialize(value, jsonTypeInfo);
        }

        /// <summary>
        /// 使用调用方提供的源生成元数据反序列化对象。
        /// </summary>
        public static T Deserialize<T>(string json, JsonTypeInfo<T> jsonTypeInfo)
        {
            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            if (jsonTypeInfo == null)
            {
                throw new ArgumentNullException(nameof(jsonTypeInfo));
            }

            var result = JsonSerializer.Deserialize(json, jsonTypeInfo);
            if (result == null)
            {
                throw new WeixinException($"JSON 反序列化结果为空，目标类型：{typeof(T).FullName}");
            }

            return result;
        }

        /// <summary>
        /// 使用 SDK 内置源生成元数据反序列化微信错误结果。
        /// </summary>
        public static WxJsonResult DeserializeWxJsonResult(string json)
        {
            return Deserialize(json, WeixinJsonSerializerContext.Default.WxJsonResult);
        }
    }
}
