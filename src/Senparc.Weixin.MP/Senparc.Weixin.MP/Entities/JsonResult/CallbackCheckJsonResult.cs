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

    文件名：CallbackCheckJsonResult.cs
    文件功能描述：CallbackCheckJsonResult 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 网络通信检测动作
    /// </summary>
    public enum CallbackCheckAction
    {
        /// <summary>仅执行 DNS 检测。</summary>
        dns,

        /// <summary>仅执行 Ping 检测。</summary>
        ping,

        /// <summary>执行全部检测。</summary>
        all
    }

    /// <summary>
    /// 网络通信检测运营商
    /// </summary>
    public enum CallbackCheckOperator
    {
        /// <summary>中国电信。</summary>
        CHINANET,

        /// <summary>中国联通。</summary>
        UNICOM,

        /// <summary>中国移动。</summary>
        CAP,

        /// <summary>使用默认运营商线路。</summary>
        DEFAULT
    }

    /// <summary>
    /// 网络通信检测结果
    /// </summary>
    public class CallbackCheckJsonResult : WxJsonResult
    {
        /// <summary>DNS 检测结果。</summary>
        public CallbackCheckDnsResult[] dns { get; set; }

        /// <summary>Ping 检测结果。</summary>
        public CallbackCheckPingResult[] ping { get; set; }
    }

    /// <summary>
    /// CallbackCheckDns 接口返回结果。
    /// </summary>
    public class CallbackCheckDnsResult
    {
        /// <summary>解析得到的 IP 地址。</summary>
        public string ip { get; set; }

        /// <summary>实际使用的运营商线路。</summary>
        public string real_operator { get; set; }
    }

    /// <summary>
    /// CallbackCheckPing 接口返回结果。
    /// </summary>
    public class CallbackCheckPingResult
    {
        /// <summary>被检测的 IP 地址。</summary>
        public string ip { get; set; }

        /// <summary>发起检测的运营商线路。</summary>
        public string from_operator { get; set; }

        /// <summary>丢包率。</summary>
        public string package_loss { get; set; }

        /// <summary>网络延迟。</summary>
        public string time { get; set; }
    }
}
