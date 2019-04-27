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
 
    文件名：CheckRequestHandler.cs
    文件功能描述：对账单下载接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections;
using System.Text;
using Senparc.CO2NET.Helpers;

#if NET45
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif


namespace Senparc.Weixin.TenPay.V2
{
    public class CheckRequestHandler : ClientRequestHandler
    {
        /// <summary>
        /// 对账单下载接口
        /// </summary>
        /// <param name="httpContext"></param>
        public CheckRequestHandler(HttpContext httpContext)
            : base(httpContext)
        {

            this.SetGateUrl("http://mch.tenpay.com/cgi-bin/mchdown_real_new.cgi");
        }



        protected override void CreateSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList();
            akeys.Add("spid");
            akeys.Add("trans_time");
            akeys.Add("stamp");
            akeys.Add("cft_signtype");
            akeys.Add("mchtype");


            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.GetKey());
            string sign = EncryptHelper.GetMD5(sb.ToString(), GetCharset()).ToLower();

            this.SetParameter("sign", sign);

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
        }
    }
}
