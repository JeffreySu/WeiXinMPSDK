#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
 
    文件名：TenPayV3.PayBank.cs
    文件功能描述：微信支付V3接口：付款到银行卡
    
    
    创建标识：Senparc - 20171129

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 付款到银行卡，文档：https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_2
    /// </summary>
    public static partial class TenPayV3
    {
        #region 同步方法

        public static PayBankResult PayBank(TenPayV3PayBankRequestData dataInfo)
        {
            var urlFormat = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}mmpaysptrans/pay_bank");

            var data = dataInfo.PackageRequestHandler.ParseXML();//获取XML
            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            var resultXml = RequestUtility.HttpPost(urlFormat, null, ms);
            return new PayBankResult(resultXml);
        }

        #endregion
    }
}
