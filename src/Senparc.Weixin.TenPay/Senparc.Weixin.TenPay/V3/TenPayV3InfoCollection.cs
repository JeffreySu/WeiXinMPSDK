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

    文件名：TenPayV3InfoCollection.cs
    文件功能描述：微信支付V3信息集合，Key为商户号（MchId）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20180707
    修改描述：TenPayV3InfoCollection 的 Register() 的微信参数自动添加到 Config.SenparcWeixinSetting.Items 下

    修改标识：Senparc - 20180802
    修改描述：v15.2.0 SenparcWeixinSetting 添加 TenPayV3_WxOpenTenpayNotify 属性，用于设置小程序支付回调地址

    修改标识：Senparc - 20190521
    修改描述：v1.4.0 .NET Core 添加多证书注册功能

    TODO：升级为Container
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
#if NETSTANDARD2_0
using Microsoft.Extensions.DependencyInjection;
#endif
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（MchId）
    /// </summary>
    public class TenPayV3InfoCollection : Dictionary<string, TenPayV3Info>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（MchId）
        /// </summary>
        public static TenPayV3InfoCollection Data = new TenPayV3InfoCollection();

        /// <summary>
        /// 获取完整件
        /// </summary>
        /// <param name="mchId"></param>
        /// <param name="subMchId"></param>
        /// <returns></returns>
        public static string GetKey(string mchId, string subMchId)
        {
            return mchId + "_" + subMchId;
        }

        /// <summary>
        /// 获取完整件
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">ISenparcWeixinSettingForTenpayV3，也可以直接传入 SenparcWeixinSetting</param>
        /// <returns></returns>
        public static string GetKey(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3)
        {
            return GetKey(senparcWeixinSettingForTenpayV3.TenPayV3_MchId, senparcWeixinSettingForTenpayV3.TenPayV3_SubMchId);
        }

        /// <summary>
        /// 注册TenPayV3Info信息
        /// </summary>
        /// <param name="tenPayV3Info"></param>
        /// <param name="name">公众号唯一标识（或名称）</param>
        public static void Register(TenPayV3Info tenPayV3Info, string name)
        {
            var key = GetKey(tenPayV3Info.MchId, tenPayV3Info.Sub_MchId);
            Data[key] = tenPayV3Info;

            //添加到全局变量
            if (!name.IsNullOrEmpty())
            {
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_AppId = tenPayV3Info.AppId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_AppSecret = tenPayV3Info.AppSecret;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_MchId = tenPayV3Info.MchId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_Key = tenPayV3Info.Key;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_CertPath = tenPayV3Info.CertPath;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_CertSecret = tenPayV3Info.CertSecret;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_TenpayNotify = tenPayV3Info.TenPayV3Notify;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_WxOpenTenpayNotify = tenPayV3Info.TenPayV3_WxOpenNotify;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_SubMchId = tenPayV3Info.Sub_MchId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_SubAppId = tenPayV3Info.Sub_AppId;
                Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].TenPayV3_SubAppSecret = tenPayV3Info.Sub_AppSecret;
            }

            //进行证书注册
#if NETSTANDARD2_0
            try
            {
                var service = SenparcDI.GlobalServiceCollection;
                var certName = key;
                var certPassword = tenPayV3Info.CertSecret;
                var certPath = tenPayV3Info.CertPath;

                //添加注册

                //service.AddSenparcHttpClientWithCertificate(certName, certPassword, certPath, false);

                #region 测试添加证书

                //添加注册

                if (!string.IsNullOrEmpty(certPath))
                {

                    if (File.Exists(certPath))
                    {
                        try
                        {
                            var cert = new X509Certificate2(certPath, certPassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                            var checkValidationResult = false;

                            var serviceCollection = SenparcDI.GlobalServiceCollection;
                            //serviceCollection.AddHttpClient<SenparcHttpClient>(certName)
                            serviceCollection.AddHttpClient(certName)
                                    .ConfigurePrimaryHttpMessageHandler(() =>
                                    {
                                        var httpClientHandler = HttpClientHelper.GetHttpClientHandler(null, RequestUtility.SenparcHttpClientWebProxy, System.Net.DecompressionMethods.None);

                                        httpClientHandler.ClientCertificates.Add(cert);

                                        if (checkValidationResult)
                                        {
                                            httpClientHandler.ServerCertificateCustomValidationCallback = new Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>(RequestUtility.CheckValidationResult);
                                        }

                                        return httpClientHandler;
                                    });
                        }
                        catch (Exception ex)
                        {
                            Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog($"添加微信支付证书发生异常", $"certName:{certName},certPath:{certPath}");
                            Senparc.CO2NET.Trace.SenparcTrace.BaseExceptionLog(ex);
                        }
                    }
                    else
                    {
                        Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog($"已设置微信支付证书，但无法找到文件", $"certName:{certName},certPath:{certPath}");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
            finally {
                SenparcDI.ResetGlobalIServiceProvider();
            }
#endif
        }

        /// <summary>
        /// 索引 TenPayV3Info
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new TenPayV3Info this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    throw new WeixinException(string.Format("TenPayV3InfoCollection尚未注册Mch：{0}", key));
                }
                else
                {
                    return base[key];
                }
            }
            set
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// TenPayV3InfoCollection 构造函数
        /// </summary>
        public TenPayV3InfoCollection() : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
