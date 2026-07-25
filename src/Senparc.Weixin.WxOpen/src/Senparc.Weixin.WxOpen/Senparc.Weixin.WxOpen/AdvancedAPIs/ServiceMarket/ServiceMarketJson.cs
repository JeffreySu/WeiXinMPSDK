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

    文件名：ServiceMarketJson.cs
    文件功能描述：ServiceMarketJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.ServiceMarket
{
    /// <summary>调用微信服务市场接口请求。</summary>
    /// <typeparam name="TData">服务提供方定义的 JSON 数据结构；也可使用 string 传入预序列化 JSON。</typeparam>
    public class ServiceMarketInvokeRequest<TData>
    {
        /// <summary>服务市场服务 ID。</summary>
        public string service { get; set; }

        /// <summary>服务提供方接口名称。</summary>
        public string api { get; set; }

        /// <summary>服务提供方定义的 JSON 格式请求数据。</summary>
        /// <remarks>官方参数表将该字段标为 string，但 HTTPS 示例直接发送 JSON 对象，因此使用泛型同时兼容两种协议形态。</remarks>
        public TData data { get; set; }

        /// <summary>调用方生成的随机字符串，用于唯一标识本次请求。</summary>
        public string client_msg_id { get; set; }

        /// <summary>是否使用异步服务；调用异步 API 时必须为 true。</summary>
        public bool? @async { get; set; }
    }

    /// <summary>调用微信服务市场接口结果。</summary>
    public class ServiceMarketInvokeJsonResult : WxJsonResult
    {
        /// <summary>同步服务的回包 JSON 字符串，或异步服务处理完成后的回包字符串。</summary>
        public string data { get; set; }

        /// <summary>异步调用的唯一请求 ID。</summary>
        public string request_id { get; set; }
    }

    /// <summary>获取服务市场异步处理结果请求。</summary>
    public class ServiceMarketRetrieveRequest
    {
        /// <summary>异步调用服务市场接口时返回的 RequestId。</summary>
        public string request_id { get; set; }
    }
}
