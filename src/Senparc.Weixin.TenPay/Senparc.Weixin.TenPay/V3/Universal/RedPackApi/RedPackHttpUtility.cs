/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RedPackHttpUtility.cs
    文件功能描述：微信支付红包请求的资源安全 HTTP 辅助类


    创建标识：Senparc - 20260719

    修改标识：Senparc - 20260718
    修改描述：v1.18.3 新增红包请求的证书、响应与流资源安全封装

    修改标识：Senparc - 20260725
    修改描述：v1.19.0 增加红包证书请求的异步执行与取消传播

----------------------------------------------------------------*/

#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
#if !NET462
using System.Net.Http;
#endif

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 旧红包接口的证书请求公共实现。集中管理请求、响应和流的释放。
    /// </summary>
    internal static class RedPackHttpUtility
    {
        internal static XmlDocument PostXml(string url, string data, X509Certificate2 certificate)
        {
            var document = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();

#if NET462
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ClientCertificates.Add(certificate);
            request.Method = "post";

            var postData = Encoding.UTF8.GetBytes(data);
            request.ContentLength = postData.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                document.LoadXml(reader.ReadToEnd());
            }
#else
            using (var handler = new HttpClientHandler())
            {
                handler.ClientCertificates.Add(certificate);
                using (var client = new HttpClient(handler))
                using (var content = new StringContent(data, Encoding.UTF8, "application/xml"))
                using (var response = client.PostAsync(url, content).ConfigureAwait(false).GetAwaiter().GetResult())
                using (var responseStream = response.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    document.Load(responseStream);
                }
            }
#endif

            return document;
        }

        internal static async Task<XmlDocument> PostXmlAsync(
            string url,
            string data,
            X509Certificate2 certificate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var document = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();

#if NET462
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ClientCertificates.Add(certificate);
            request.Method = "post";

            var postData = Encoding.UTF8.GetBytes(data);
            request.ContentLength = postData.Length;
            using (cancellationToken.Register(request.Abort))
            {
                using (var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
                {
                    await requestStream.WriteAsync(postData, 0, postData.Length, cancellationToken).ConfigureAwait(false);
                }

                using (var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false))
                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    document.LoadXml(await reader.ReadToEndAsync().ConfigureAwait(false));
                }
            }
#else
            using (var handler = new HttpClientHandler())
            {
                handler.ClientCertificates.Add(certificate);
                using (var client = new HttpClient(handler))
                using (var content = new StringContent(data, Encoding.UTF8, "application/xml"))
                using (var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false))
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    document.Load(responseStream);
                }
            }
#endif

            return document;
        }
    }
}
