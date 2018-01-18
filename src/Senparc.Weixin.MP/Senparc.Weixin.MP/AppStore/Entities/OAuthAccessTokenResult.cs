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
  
    文件名：OAuthAccessTokenResult.cs
    文件功能描述：获取OAuth AccessToken的结果
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Utilities.WeixinUtility;

namespace Senparc.Weixin.MP.AppStore
{
    public class OAuthAccountInfo
    {
        //public int user_id { get; set; }
        //public int user_name { get; set; }
        public int weixin_id { get; set; }
        public string weixin_name { get; set; }
    }

    /// <summary>
    /// 获取OAuth AccessToken的结果
    /// 如果错误，返回结果{"errcode":40029,"errmsg":"invalid code"}
    /// </summary>
    public class OAuthAccessTokenResult : WxJsonResult
    {
        private int _expiresIn;


        //以下看似不符合C#规范的命名方式参考微信的OAUTH
        public string access_token { get; set; }

        public int expires_in
        {
            get { return _expiresIn; }
            set
            {
                _expiresIn = value;
                ExpireTimeTicks = ApiUtility.GetExpireTime(expires_in).Ticks;
            }
        }

        public string refresh_token { get; set; }
        public OAuthAccountInfo account_info { get; set; }

        /// <summary>
        /// 过期时间Ticks
        /// </summary>
        public long ExpireTimeTicks { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime
        {
            get
            {
                return new DateTime(ExpireTimeTicks);//如果高频次读取可以使用局部变量保存
            }
        }
    }
}
