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
  
    文件名：ApiConnection.cs
    文件功能描述：API链接
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MP.AppStore.Api
{
    public class ApiConnection
    {
        private Passport _passport;
        public ApiConnection(Passport passport)
        {
            if (passport == null)
            {
                throw new WeixinException("Passport不可以为NULL！");
            }
            _passport = passport;
        }

        /// <summary>
        /// 自动更新Passport的链接方法
        /// </summary>
        /// <param name="apiFunc"></param>
        /// <returns></returns>
        public IAppResult<T> Connection<T>(Func<IAppResult<T>> apiFunc) where T : IAppResultData
        {
            var result = apiFunc();
            if (result.Result == AppResultKind.账户验证失败)
            {
                //更新Passport
                AppStoreManager.ApplyPassport(_passport.AppKey, _passport.Secret, _passport.ApiUrl);
                result = apiFunc();
            }
            return result;
        }
    }
}
