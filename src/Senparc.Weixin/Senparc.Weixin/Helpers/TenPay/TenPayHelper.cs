#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：TenPayHelper.cs
    文件功能描述：微信支付帮助类
    
    创建标识：Senparc - 20200222

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using System.IO;
using System.Text.RegularExpressions;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// 微信支付帮助类
    /// </summary>
    public static class TenPayHelper
    {
        /// <summary>
        /// 获取微信支付（V3）注册 TenPayV3InfoCollection 以及 Cert HrrpClient 时提供的 Key 或 Name
        /// </summary>
        /// <param name="mchId"></param>
        /// <param name="subMchId"></param>
        /// <returns></returns>
        public static string GetRegisterKey(string mchId, string subMchId)
        {
            return mchId + "_" + subMchId;
        }

        /// <summary>
        /// 获取微信支付（V3）注册 TenPayV3InfoCollection 以及 Cert HrrpClient 时提供的 Key 或 Name
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        /// <returns></returns>
        public static string GetRegisterKey(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3)
        {
            return GetRegisterKey(senparcWeixinSettingForTenpayV3.TenPayV3_MchId, senparcWeixinSettingForTenpayV3.TenPayV3_SubMchId);
        }

        /// <summary>
        /// 尝试从文件获取正确格式的私钥
        /// </summary>
        /// <returns></returns>
        public static string TryGetPrivateKeyFromFile(ref string tenPayV3_PrivateKey)
        {
            if (tenPayV3_PrivateKey != null && tenPayV3_PrivateKey.Length < 100 && tenPayV3_PrivateKey.StartsWith("~/"))
            {
                //虚拟路径
                //尝试读取文件
                var filePath = CO2NET.Utilities.ServerUtility.ContentRootMapPath(tenPayV3_PrivateKey);
                if (!File.Exists(filePath))
                {
                    Senparc.Weixin.WeixinTrace.BaseExceptionLog(new WeixinException("TenPayV3_PrivateKey 证书文件不存在！" + filePath));
                }

                var fileContent = File.ReadAllText(filePath);
                Regex regex = new Regex(@"(--([^\r\n])+--[\r\n]{0,1})|[\r\n]");
                var privateKey = regex.Replace(fileContent, "");
                tenPayV3_PrivateKey = privateKey;
            }
            return tenPayV3_PrivateKey;
        }
    }
}
