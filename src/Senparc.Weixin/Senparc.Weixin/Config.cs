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
    
    文件名：Config.cs
    文件功能描述：全局设置
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20160813
    修改描述：v4.7.7 添加DefaultCacheNamespace

    修改标识：Senparc - 20171127
    修改描述：v4.18.5 添加Config.ApiMpHost属性，可以设置API域名

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Senparc.Weixin
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = 10000;

        private static bool _isDebug = false;//TODO:需要考虑分布式的情况，后期需要储存在缓存中

        /// <summary>
        /// 指定是否是Debug状态，如果是，系统会自动输出日志
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                return _isDebug;
            }
            set
            {
                _isDebug = value;

                //if (_isDebug)
                //{
                //    WeixinTrace.Open();
                //}
                //else
                //{
                //    WeixinTrace.Close();
                //}
            }
        }

        /// <summary>
        /// JavaScriptSerializer 类接受的 JSON 字符串的最大长度
        /// </summary>
        public static int MaxJsonLength = int.MaxValue;//TODO:需要考虑分布式的情况，后期需要储存在缓存中

        /// <summary>
        /// 默认缓存键的第一级命名空间，默认值：DefaultCache
        /// </summary>
        public static string DefaultCacheNamespace = "DefaultCache";//TODO:需要考虑分布式的情况，后期需要储存在缓存中,或进行全局配置

#if !NET45
        /// <summary>
        /// 默认微信配置
        /// </summary>
        public static SenparcWeixinSetting DefaultSenparcWeixinSetting { get; set; }
#endif


        /// <summary>
        /// 微信支付使用沙箱模式（默认为false）
        /// </summary>
        public static bool UseSandBoxPay { get; set; }

        /// <summary>
        /// 网站根目录绝对路径
        /// </summary>
        public static string RootDictionaryPath { get; set; }

        #region API地址（前缀）设置

        #region  公众号（小程序）、开放平台 API 的服务器地址（默认为：https://api.weixin.qq.com）

        /// <summary>
        /// 公众号（小程序）、开放平台 API 的服务器地址（默认为：https://api.weixin.qq.com）
        /// </summary>
        private static string _apiMpHost = "https://api.weixin.qq.com";
        /// <summary>
        /// 公众号（小程序）、开放平台 API 的服务器地址（默认为：https://api.weixin.qq.com）
        /// </summary>
        public static string ApiMpHost
        {
            get { return _apiMpHost; }
            set { _apiMpHost = value; }
        }

        /// <summary>
        /// 公众号（小程序）、开放平台【文件下载】 API 的服务器地址（默认为：https://api.weixin.qq.com）
        /// </summary>
        private static string _apiMpFileHost = "http://file.api.weixin.qq.com";
        /// <summary>
        /// 公众号（小程序）、开放平台【文件下载】 API 的服务器地址（默认为：http://file.api.weixin.qq.com）
        /// </summary>
        public static string ApiMpFileHost
        {
            get { return _apiMpFileHost; }
            set { _apiMpFileHost = value; }
        }
        #endregion

        #region 企业微信API的服务器地址（默认为：https://qyapi.weixin.qq.com）

        /// <summary>
        /// 企业微信API的服务器地址（默认为：https://qyapi.weixin.qq.com）
        /// </summary>
        private static string _apiWorkHost = "https://qyapi.weixin.qq.com";
        /// <summary>
        /// 企业微信API的服务器地址（默认为：https://qyapi.weixin.qq.com）
        /// </summary>
        public static string ApiWorkHost
        {
            get { return _apiWorkHost; }
            set { _apiWorkHost = value; }
        }

        #endregion

        #endregion



        /// <summary>
        /// 默认的AppId检查规则
        /// </summary>
        public static Func<string, PlatformType, bool> DefaultAppIdCheckFunc = (accessTokenOrAppId, platFormType) =>
        {
            if (platFormType == PlatformType.QY || platFormType == PlatformType.Work)
            {
                /*
                 * 企业号（企业微信）AppKey（Length=84）：wx7618c00000000222@044ZI5s6-ACxpAuOcm4md410pZ460pQUmxO9hIoMd09kRaJ1iSqhPfmg3-aBFF7q
                 * 企业号（企业微信）AccessToken（length=300）：MGelzm_P0N-41qH3PwHsNxp70rdVuB0SMEN7dE4E8eKpb0OpNQSp8jPUfgwIL_P9jcz-qGIOLbLEy3d8XQEJFfZtOLgTJqyg0rJbj6WyQJxdRVjbLnHr0-pg7oN9dD1NFI7-T7GLuJER3Pun-5cSiSmZgAegTDhXKZC8XfgjQAPPYLjZl7StBnO7dVcZStdyivZ92zq4PrDdNif9fa2p9lPSLqkur2PpDB9P7MsR8PDJWsKghEcmjB41OXohHGnqPWd5lUZaV1Y8p35BVz6PqjF-90UgAjI9IohVKVRClks
                 */

                //return accessTokenOrAppId != null && accessTokenOrAppId.Length < 256;

                /*
                 * 2017年9月26日开始，AccessToken长度有变化（长度有300、215、191等）
                 * AccessToken（Length=215）：_0evr6HbAnWCUfn1tRpbVY2uV63fDOfT-fUnpQcq6egl8bYFp3Xq45ebImXn5Aj1_nz_mFCUz9sDnoEkfy-jyXqJEc4Hty0BAo2VQTB8ogx7qkL2w1p0H2E1fKWwJrQ1285V0XhEQ0pcHMLwy9RbHuD4sHdAJ5ZkXGchNQ1eHsmseoBxucKvyAnEq9psJVLMjkU4G3ZRa0NoTBSy0g6ujg
                 */

                return accessTokenOrAppId != null && accessTokenOrAppId.Contains("@");
            }
            else
            {
                /*
                 * 公众号AppId：wxe273c3a02e09ff8c
                 * 公众号AccessToken：ga0wJ5ZmdB1Ef1gMMxmps6Uz1a9TXoutQtRqgYTbIqHfTm4Ssfoj0DjMLp1_KkG7FkaqS7m7f9rrYbqBQMBizRBQjHFG5ZIov8Wb0FBnHDq5fGpCu0S2H2j2aM8c6KDqGGEiAIAJJH
                 */
                return accessTokenOrAppId != null && accessTokenOrAppId.Length <= 32 /*wxc3c90837b0e76080*/
                ;
            }
        };
    }
}
