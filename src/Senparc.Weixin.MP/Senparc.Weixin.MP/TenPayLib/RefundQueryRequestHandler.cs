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

using System;

#if NET35 || NET40 || NET45 || NET461
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif

namespace Senparc.Weixin.MP.TenPayLib
{
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V2 中的对应方法")]
    public class RefundQueryRequestHandler : ClientRequestHandler
    {
        /// <summary>
        /// 退款明细查询接口
        /// </summary>
        /// <param name="httpContext"></param>
        public RefundQueryRequestHandler(HttpContext httpContext)
            : base(httpContext)
        {
            this.SetGateUrl("https://gw.tenpay.com/gateway/normalrefundquery.xml");
        }
    }
}
