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
 
    文件名：SM3WithSM2Signer.cs

    
    创建标识：MartyZane - 20240530

    修改标识：MartyZane - 20240530
    修改描述：验证国密算法SM3WithSM2，修改返回标识为SM2-WITH-SM3

----------------------------------------------------------------*/

using Org.BouncyCastle.Crypto.Parameters;
using Senparc.Weixin.TenPayV3.Helpers;
using System;

namespace Client.TenPayHttpClient.Signer
{
    public class SM3WithSM2Signer : ISigner
    {
        /// <summary>
        /// 返回同微信中Http请求头的Authrization的认证类型
        /// </summary>
        /// <returns></returns>
        public string GetAlgorithm()
        {
            return "SM2-WITH-SM3";
        }

        public string Sign(string message, string privateKey = null)
        {
            byte[] keyData = Convert.FromBase64String(privateKey);

            ECPrivateKeyParameters eCPrivateKeyParameters = SMPemHelper.LoadPrivateKeyToParameters(keyData);

            byte[] signBytes = GmHelper.SignSm3WithSm2(eCPrivateKeyParameters, message);

            return Convert.ToBase64String(signBytes);
        }
    }
}
