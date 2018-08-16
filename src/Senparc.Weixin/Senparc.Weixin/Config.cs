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

    修改标识：Senparc - 20180622
    修改描述：v5.0.2.1 修复 IsDebug 逻辑判断错误

    修改标识：Senparc - 20180717
    修改描述：v5.1.2 Config.SenparcWeixinSetting 提供默认实例


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
    /// Senparc.Weixin 全局设置
    /// </summary>
    public static class Config
    {
        //private static bool _isDebug = false;

        /// <summary>
        /// <para>指定是否是Debug状态，如果是，系统会自动输出日志。</para>
        /// <para>如果 CO2NET.Config.IsDebug 为 true，则此参数也会为 true，否则以此参数为准。</para>
        /// </summary>
        public static bool IsDebug
        {
            get { return CO2NET.Config.IsDebug || SenparcWeixinSetting.IsDebug; }
            set { SenparcWeixinSetting.IsDebug = value; }
        }

        /// <summary>
        /// 默认微信配置
        /// </summary>
        [Obsolete("请使用 SenparcWeixinSetting")]
        public static SenparcWeixinSetting DefaultSenparcWeixinSetting { get { return SenparcWeixinSetting; } set { SenparcWeixinSetting = value; } }

        /// <summary>
        /// <para>微信全局配置</para>
        /// <para>注意：在程序运行过程中修改 SenparcWeixinSetting.Items 中的微信配置值，并不能修改 Container 中的对应信息（如AppSecret），</para>
        /// <para>如果需要修改微信信息（如AppSecret）应该使用 xxContainer.Register() 修改，这里的值也会随之更新。</para>
        /// </summary>
        public static SenparcWeixinSetting SenparcWeixinSetting { get; set; }


        /// <summary>
        /// 微信支付使用沙箱模式（默认为false）
        /// </summary>
        public static bool UseSandBoxPay { get; set; }

        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = CO2NET.Config.TIME_OUT;

        /// <summary>
        /// 网站根目录绝对路径
        /// </summary>
        public static string RootDictionaryPath
        {
            get { return CO2NET.Config.RootDictionaryPath; }
            set { CO2NET.Config.RootDictionaryPath = value; }
        }

        /// <summary>
        /// 默认缓存键的第一级命名空间，默认值：DefaultCache
        /// </summary>
        public static string DefaultCacheNamespace
        {
            get { return CO2NET.Config.DefaultCacheNamespace; }
            set { CO2NET.Config.DefaultCacheNamespace = value; }
        }

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

        static Config()
        {
            SenparcWeixinSetting = new SenparcWeixinSetting();//提供默认实例
        }
    }
}
