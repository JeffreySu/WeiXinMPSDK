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

    文件名：RedPacketCoverJson.cs
    文件功能描述：RedPacketCoverJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.RedPacketCover
{
    /// <summary>获取指定用户红包封面领取链接请求。</summary>
    public class RedPacketCoverUrlRequest
    {
        /// <summary>可领取红包封面的用户 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>红包封面开放平台生成的发放 CToken；生成时必须指定当前小程序 AppId。</summary>
        public string ctoken { get; set; }
    }

    /// <summary>红包封面领取链接数据。</summary>
    public class RedPacketCoverUrlData
    {
        /// <summary>指定用户可领取红包封面的带鉴权链接。</summary>
        public string url { get; set; }
    }

    /// <summary>获取红包封面领取链接结果。</summary>
    public class RedPacketCoverUrlJsonResult : WxJsonResult
    {
        /// <summary>红包封面领取链接数据。</summary>
        public RedPacketCoverUrlData data { get; set; }
    }
}
