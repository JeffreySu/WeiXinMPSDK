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

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
  
    文件名：ApiContainer.cs
    文件功能描述：API操作容器
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AppStore.Api
{
    /// <summary>
    /// API操作容器（每次构造都会获取当前缓存中最新的Passport）
    /// </summary>
    public class ApiContainer
    {
        public Passport Passport { get; set; }

        public MemberApi MemberApi { get; set; }

        public ApiContainer(string appKey, string appSecret, string url = AppStoreManager.DEFAULT_URL)
        {
            var passportBag = AppStoreManager.GetPassportBag(appKey);
            if (passportBag == null || passportBag.Passport == null)
            {
                AppStoreManager.ApplyPassport(appKey, appSecret, url);
            }

            Passport = AppStoreManager.GetPassportBag(appKey).Passport;//执行SdkManager.ApplyPassport后，PassportCollection[appKey]必定存在

            MemberApi = new MemberApi(Passport);
        }
    }
}
