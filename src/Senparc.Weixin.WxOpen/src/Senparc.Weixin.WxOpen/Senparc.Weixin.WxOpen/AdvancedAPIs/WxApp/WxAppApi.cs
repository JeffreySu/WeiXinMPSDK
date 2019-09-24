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
    
    文件名：WxAppApi.cs
    文件功能描述：小程序WxApp目录下面的接口
    
    
    创建标识：Senparc - 20170103
    
    修改标识：Senparc - 20170129
    修改描述：v1.0.1 完善CreateWxaQrCode方法

    修改标识：Senparc - 20170726
    修改描述：完成接口开放平台-代码管理及小程序码获取

    修改标识：Senparc - 20180106
    修改描述：完成接口-附近的小程序API

    修改标识：Senparc - 20190615
    修改描述：修复附近的小程序添加地点

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
//using Senparc.Weixin.Helpers;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using System.Collections.Generic;
using Senparc.NeuChar;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /* 
    tip：通过该接口，仅能生成已发布的小程序的二维码。
    tip：可以在开发者工具预览时生成开发版的带参二维码。
    tip：带参二维码只有 10000 个，请谨慎调用。
    */

    /// <summary>
    /// WxApp接口
    /// </summary>
    public static class WxAppApi
    {
        #region 同步方法

        /// <summary>
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCode", true)]
        public static WxJsonResult GetWxaCode(string accessTokenOrAppId, Stream stream, string path,
            int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { path = path, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                Post.Download(url, SerializerHelper.GetJsonString(data, jsonSetting), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCode", true)]
        public static WxJsonResult GetWxaCode(string accessTokenOrAppId, string filePath, string path, int width = 430,
            bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.GetWxaCode(accessTokenOrAppId, ms, path, width, auto_color, lineColor, isHyaline, timeOut);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取小程序码，适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCodeUnlimit", true)]
        public static WxJsonResult GetWxaCodeUnlimit(string accessTokenOrAppId, Stream stream, string scene,
            string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false,
            int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacodeunlimit?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { scene = scene, page = page, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                Post.Download(url, SerializerHelper.GetJsonString(data, jsonSetting), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序码，适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCodeUnlimit", true)]
        public static WxJsonResult GetWxaCodeUnlimit(string accessTokenOrAppId, string filePath, string scene, string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.GetWxaCodeUnlimit(accessTokenOrAppId, ms, scene, page, width, auto_color, lineColor, isHyaline, timeOut);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存二维码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.CreateWxQrCode", true)]
        public static WxJsonResult CreateWxQrCode(string accessTokenOrAppId, Stream stream, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                Post.Download(url, SerializerHelper.GetJsonString(data), stream);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.CreateWxQrCode", true)]
        public static WxJsonResult CreateWxQrCode(string accessTokenOrAppId, string filePath, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = WxAppApi.CreateWxQrCode(accessTokenOrAppId, ms, path, width);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    ms.CopyTo(fs);
                    fs.Flush();
                }
                return result;
            }
        }

        /// <summary>
        /// session_key 合法性校验
        /// https://mp.weixin.qq.com/debug/wxagame/dev/tutorial/http-signature.html
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">用户唯一标识符</param>
        /// <param name="sessionKey">用户登录态签名</param>
        /// <param name="buffer">托管数据，类型为字符串，长度不超过1000字节（官方文档没有提供说明，可留空）</param>
        /// <param name="sigMethod">用户登录态签名的哈希方法，默认为hmac_sha256</param>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.CheckSession", true)]
        public static WxJsonResult CheckSession(string accessTokenOrAppId, string openId, string sessionKey, string buffer, string sigMethod = "hmac_sha256")
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/checksession?access_token={0}&signature={1}&openid={2}&sig_method={3}";
                var signature = EncryptHelper.GetHmacSha256("", sessionKey);
                var url = urlFormat.FormatWith(accessToken, signature, openId, sigMethod);

                return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 添加地点
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pic_list">门店图片，最多9张，最少1张，上传门店图片如门店外景、环境设施、商品服务等，图片将展示在微信客户端的门店页。图片链接通过文档https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738729中的《上传图文消息内的图片获取URL》接口获取。必填，文件格式为bmp、png、jpeg、jpg或gif，大小不超过5M pic_list是字符串，内容是一个json！</param>
        /// <param name="service_infos">服务标签列表 选填，需要填写服务标签ID、APPID、对应服务落地页的path路径，详细字段格式见下方示例</param>
        /// <param name="store_name">门店名字 必填，门店名称需按照所选地理位置自动拉取腾讯地图门店名称，不可修改，如需修改请重新选择地图地点或重新创建地点</param>
        /// <param name="hour">营业时间，格式11:11-12:12 必填</param>
        /// <param name="credential">资质号 必填, 15位营业执照注册号或9位组织机构代码</param>
        /// <param name="address">地址 必填</param>
        /// <param name="company_name">主体名字 必填</param>
        /// <param name="qualification_list">证明材料 必填 如果company_name和该小程序主体不一致，需要填qualification_list，详细规则见附近的小程序使用指南-如何证明门店的经营主体跟公众号或小程序帐号主体相关http://kf.qq.com/faq/170401MbUnim17040122m2qY.html</param>
        /// <param name="kf_info">客服信息 选填，可自定义服务头像与昵称，具体填写字段见下方示例kf_info pic_list是字符串，内容是一个json！</param>
        /// <param name="poi_id">如果创建新的门店，poi_id字段为空 如果更新门店，poi_id参数则填对应门店的poi_id 选填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.AddNearbyPoi", true)]
        public static AddNearbyPoiJsonResult AddNearbyPoi(string accessTokenOrAppId, string pic_list, string service_infos, string store_name,string hour, string credential, string address, string company_name, string qualification_list="", string kf_info="", string poi_id="", int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/addnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new {
                    is_comm_nearby = "1", //必填,写死为"1"
                    pic_list = pic_list,
                    service_infos = service_infos,
                    store_name = store_name,
                    hour = hour,
                    credential = credential,
                    address = address,
                    company_name = company_name,
                    qualification_list = qualification_list,
                    kf_info = kf_info,
                    poi_id = poi_id
                };

                return CommonJsonSend.Send<AddNearbyPoiJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查看地点列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="page">起始页id（从1开始计数）</param>
        /// <param name="page_rows">每页展示个数（最多1000个）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetNearbyPoiList", true)]
        public static GetNearbyPoiListJsonResult GetNearbyPoiList(string accessTokenOrAppId, int page = 1, int page_rows = 10, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getnearbypoilist?access_token={0}&page={1}&page_rows={2}";
                string url = string.Format(urlFormat, accessToken, page, page_rows);

                //var data = new { page = page, page_rows = page_rows };

                return CommonJsonSend.Send<GetNearbyPoiListJsonResult>(accessToken, url, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除地点
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.DelNearbyPoi", true)]
        public static WxJsonResult DelNearbyPoi(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/delnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 展示/取消展示附近小程序
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="status">0：取消展示；1：展示</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.SetNearbyPoiShowStatus", true)]
        public static WxJsonResult SetNearbyPoiShowStatus(string accessTokenOrAppId, string poi_id, int status, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/setnearbypoishowstatus?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id, status = status };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 检查一段文本是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/msgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="content">要检测的文本内容，长度不超过 500KB，编码格式为utf-8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.MsgSecCheck", true)]
        public static WxJsonResult MsgSecCheck(string accessTokenOrAppId, string content, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/msg_sec_check?access_token={0}";
                string url = string.Format(urlFormat, accessToken);
                var data = new { content = content };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 校验一张图片是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/imgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">文件完整物理路径<para>格式支持PNG、JPEG、JPG、GIF，图片尺寸不超过 750px * 1334px</para></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.ImgSecCheck", true)]
        public static WxJsonResult ImgSecCheck(string accessTokenOrAppId, string filePath, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/img_sec_check?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);
                var fileDic = new Dictionary<string, string>();
                fileDic["media"] = filePath;
                return CO2NET.HttpUtility.Post.PostFileGetJson<WxJsonResult>(url, fileDictionary: fileDic, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 向插件开发者发起使用插件的申请
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/applyPlugin.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pluginAppid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.ApplyPlugin", true)]
        public static WxJsonResult ApplyPlugin(string accessTokenOrAppId, string pluginAppid, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/plugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "apply",
                    plugin_appid = pluginAppid
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取当前所有插件使用方（供插件开发者调用）
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/getPluginDevApplyList.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="page"></param>
        /// <param name="num"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.DevPlugin", true)]
        public static DevPluginResultJson DevPlugin(string accessTokenOrAppId, int page, int num, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/devplugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "dev_apply_list",
                    page,
                    num
                };

                return CommonJsonSend.Send<DevPluginResultJson>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改插件使用申请的状态（供插件开发者调用）
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/setDevPluginApplyStatus.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="action"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.SetDevPluginApplyStatus", true)]
        public static WxJsonResult SetDevPluginApplyStatus(string accessTokenOrAppId, string action, string appId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/devplugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action,
                    appid = appId
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询已添加的插件
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/getPluginList.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetPluginList", true)]
        public static GetPluginListResultJson GetPluginList(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/plugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "list"
                };

                return CommonJsonSend.Send<GetPluginListResultJson>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除已添加的插件
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/unbindPlugin.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.UnbindPlugin", true)]
        public static WxJsonResult UnbindPlugin(string accessTokenOrAppId, string appId, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/plugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "unbind",
                    plugin_appid = appId
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCodeAsync", true)]
        public static async Task<WxJsonResult> GetWxaCodeAsync(string accessTokenOrAppId, string filePath, string path,
            int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.GetWxaCodeAsync(accessTokenOrAppId, ms, path, width, auto_color, lineColor, isHyaline, timeOut).ConfigureAwait(false);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(fs).ConfigureAwait(false);
                    await fs.FlushAsync().ConfigureAwait(false);
                }
                return result;
            }
        }


        /// <summary>
        /// 【异步方法】获取小程序码，适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCodeUnlimitAsync", true)]
        public static async Task<WxJsonResult> GetWxaCodeUnlimitAsync(string accessTokenOrAppId, Stream stream,
            string scene, string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false,
            int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacodeunlimit?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { scene = scene, page = page, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                await Post.DownloadAsync(url, SerializerHelper.GetJsonString(data, jsonSetting), stream).ConfigureAwait(false);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取小程序码，适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）</param>
        /// <param name="page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCodeUnlimitAsync", true)]
        public static async Task<WxJsonResult> GetWxaCodeUnlimitAsync(string accessTokenOrAppId, string filePath,
            string scene, string page, int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false,
            int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.GetWxaCodeUnlimitAsync(accessTokenOrAppId, ms, scene, page, width, auto_color, lineColor, isHyaline, timeOut).ConfigureAwait(false);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(fs).ConfigureAwait(false);
                    await fs.FlushAsync().ConfigureAwait(false);
                }
                return result;
            }
        }

        /// <summary>
        /// 【异步方法】获取小程序页面的小程序码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存小程序码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">小程序码的宽度</param>
        /// <param name="auto_color">自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调</param>
        /// <param name="lineColor">auth_color 为 false 时生效，使用 rgb 设置颜色 例如 {"r":"xxx","g":"xxx","b":"xxx"}</param>
        /// <param name="isHyaline">是否需要透明底色， is_hyaline 为true时，生成透明底色的小程序码，默认为 false</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetWxaCodeAsync", true)]
        public static async Task<WxJsonResult> GetWxaCodeAsync(string accessTokenOrAppId, Stream stream, string path,
            int width = 430, bool auto_color = false, LineColor lineColor = null, bool isHyaline = false, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getwxacode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                if (auto_color && lineColor == null)
                {
                    lineColor = new LineColor();//提供默认值
                }

                var data = new { path = path, width = width, line_color = lineColor, is_hyaline = isHyaline };
                JsonSetting jsonSetting = new JsonSetting(true);
                await CO2NET.HttpUtility.Post.DownloadAsync(url, SerializerHelper.GetJsonString(data, jsonSetting), stream).ConfigureAwait(false);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="stream">储存二维码的流</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1,注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.CreateWxQrCodeAsync", true)]
        public static async Task<WxJsonResult> CreateWxQrCodeAsync(string accessTokenOrAppId, Stream stream,
            string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/wxaapp/createwxaqrcode?access_token={0}";
                var url = string.Format(urlFormat, accessToken);

                var data = new { path = path, width = width };
                await Post.DownloadAsync(url, SerializerHelper.GetJsonString(data), stream).ConfigureAwait(false);

                return new WxJsonResult()
                {
                    errcode = ReturnCode.请求成功
                };
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取小程序页面二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">储存图片的物理路径</param>
        /// <param name="path">不能为空，最大长度 128 字节（如：pages/index?query=1。注：pages/index 需要在 app.json 的 pages 中定义）</param>
        /// <param name="width">二维码的宽度</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.CreateWxQrCodeAsync", true)]
        public static async Task<WxJsonResult> CreateWxQrCodeAsync(string accessTokenOrAppId, string filePath, string path, int width = 430, int timeOut = Config.TIME_OUT)
        {
            using (var ms = new MemoryStream())
            {
                var result = await WxAppApi.CreateWxQrCodeAsync(accessTokenOrAppId, ms, path, width).ConfigureAwait(false);
                ms.Seek(0, SeekOrigin.Begin);
                //储存图片
                File.Delete(filePath);
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(fs).ConfigureAwait(false);
                    await fs.FlushAsync().ConfigureAwait(false);
                }
                return result;
            }
        }


        /// <summary>
        /// 【异步方法】session_key 合法性校验
        /// https://mp.weixin.qq.com/debug/wxagame/dev/tutorial/http-signature.html
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">用户唯一标识符</param>
        /// <param name="sessionKey">用户登录态签名</param>
        /// <param name="buffer">托管数据，类型为字符串，长度不超过1000字节（官方文档没有提供说明，可留空）</param>
        /// <param name="sigMethod">用户登录态签名的哈希方法，默认为hmac_sha256</param>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.CheckSessionAsync", true)]
        public static async Task<WxJsonResult> CheckSessionAsync(string accessTokenOrAppId, string openId, string sessionKey, string buffer, string sigMethod = "hmac_sha256")
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/checksession?access_token={0}&signature={1}&openid={2}&sig_method={3}";
                var signature = EncryptHelper.GetHmacSha256("", sessionKey);
                var url = urlFormat.FormatWith(accessToken, signature, openId, sigMethod);

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】添加地点
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pic_list">门店图片，最多9张，最少1张，上传门店图片如门店外景、环境设施、商品服务等，图片将展示在微信客户端的门店页。图片链接通过文档https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1444738729中的《上传图文消息内的图片获取URL》接口获取。必填，文件格式为bmp、png、jpeg、jpg或gif，大小不超过5M pic_list是字符串，内容是一个json！</param>
        /// <param name="service_infos">服务标签列表 选填，需要填写服务标签ID、APPID、对应服务落地页的path路径，详细字段格式见下方示例</param>
        /// <param name="store_name">门店名字 必填，门店名称需按照所选地理位置自动拉取腾讯地图门店名称，不可修改，如需修改请重新选择地图地点或重新创建地点</param>
        /// <param name="hour">营业时间，格式11:11-12:12 必填</param>
        /// <param name="credential">资质号 必填, 15位营业执照注册号或9位组织机构代码</param>
        /// <param name="address">地址 必填</param>
        /// <param name="company_name">主体名字 必填</param>
        /// <param name="qualification_list">证明材料 必填 如果company_name和该小程序主体不一致，需要填qualification_list，详细规则见附近的小程序使用指南-如何证明门店的经营主体跟公众号或小程序帐号主体相关http://kf.qq.com/faq/170401MbUnim17040122m2qY.html</param>
        /// <param name="kf_info">客服信息 选填，可自定义服务头像与昵称，具体填写字段见下方示例kf_info pic_list是字符串，内容是一个json！</param>
        /// <param name="poi_id">如果创建新的门店，poi_id字段为空 如果更新门店，poi_id参数则填对应门店的poi_id 选填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.AddNearbyPoiAsync", true)]
        public static async Task<AddNearbyPoiJsonResult> AddNearbyPoiAsync(string accessTokenOrAppId, string pic_list, string service_infos, string store_name, string hour, string credential, string address, string company_name, string qualification_list = "", string kf_info = "", string poi_id = "", int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/addnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new
                {
                    is_comm_nearby = "1", //必填,写死为"1"
                    pic_list = pic_list,
                    service_infos = service_infos,
                    store_name = store_name,
                    hour = hour,
                    credential = credential,
                    address = address,
                    company_name = company_name,
                    qualification_list = qualification_list,
                    kf_info = kf_info,
                    poi_id = poi_id
                };

                return CommonJsonSend.SendAsync<AddNearbyPoiJsonResult>(accessToken, url, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查看地点列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="page">起始页id（从1开始计数）</param>
        /// <param name="page_rows">每页展示个数（最多1000个）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetNearbyPoiListAsync", true)]
        public static async Task<GetNearbyPoiListJsonResult> GetNearbyPoiListAsync(string accessTokenOrAppId, int page = 1, int page_rows = 10, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/getnearbypoilist?access_token={0}&page={1}&page_rows={2}";
                string url = string.Format(urlFormat, accessToken, page, page_rows);

                //var data = new { page = page, page_rows = page_rows };

                return await CommonJsonSend.SendAsync<GetNearbyPoiListJsonResult>(accessToken, url, null, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除地点
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.DelNearbyPoiAsync", true)]
        public static async Task<WxJsonResult> DelNearbyPoiAsync(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/delnearbypoi?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】展示/取消展示附近小程序
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">附近地点ID</param>
        /// <param name="status">0：取消展示；1：展示</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.SetNearbyPoiListAsync", true)]
        public static async Task<WxJsonResult> SetNearbyPoiListAsync(string accessTokenOrAppId, string poi_id, int status, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/setnearbypoishowstatus?access_token={0}";
                string url = string.Format(urlFormat, accessToken);

                var data = new { poi_id = poi_id, status = status };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 检查一段文本是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/msgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="content">要检测的文本内容，长度不超过 500KB，编码格式为utf-8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.MsgSecCheckAsync", true)]
        public static async Task<WxJsonResult> MsgSecCheckAsync(string accessTokenOrAppId, string content, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/msg_sec_check?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);
                var data = new { content = content };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】校验一张图片是否含有违法违规内容
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/imgSecCheck.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="filePath">文件完整物理路径<para>格式支持PNG、JPEG、JPG、GIF，图片尺寸不超过 750px * 1334px</para></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.ImgSecCheckAsync", true)]
        public static async Task<WxJsonResult> ImgSecCheckAsync(string accessTokenOrAppId, string filePath, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/img_sec_check?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);
                var fileDic = new Dictionary<string, string>();
                fileDic["media"] = filePath;
                return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<WxJsonResult>(url, fileDictionary: fileDic, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】向插件开发者发起使用插件的申请
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/applyPlugin.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pluginAppid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.ApplyPluginAsync", true)]
        public static async Task<WxJsonResult> ApplyPluginAsync(string accessTokenOrAppId, string pluginAppid, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/plugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "apply",
                    plugin_appid = pluginAppid
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取当前所有插件使用方（供插件开发者调用）
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/getPluginDevApplyList.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="page"></param>
        /// <param name="num"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.DevPluginAsync", true)]
        public static async Task<DevPluginResultJson> DevPluginAsync(string accessTokenOrAppId, int page, int num, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/devplugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "dev_apply_list",
                    page,
                    num
                };

                return await CommonJsonSend.SendAsync<DevPluginResultJson>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】修改插件使用申请的状态（供插件开发者调用）
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/setDevPluginApplyStatus.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="action"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.SetDevPluginApplyStatusAsync", true)]
        public static async Task<WxJsonResult> SetDevPluginApplyStatusAsync(string accessTokenOrAppId, string action, string appId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/devplugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action,
                    appid = appId
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询已添加的插件
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/getPluginList.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.GetPluginListAsync", true)]
        public static async Task<GetPluginListResultJson> GetPluginListAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/plugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "list"
                };

                return await CommonJsonSend.SendAsync<GetPluginListResultJson>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除已添加的插件
        /// <para>https://developers.weixin.qq.com/miniprogram/dev/api/open-api/plugin-management/unbindPlugin.html</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="appId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "WxAppApi.UnbindPluginAsync", true)]
        public static async Task<WxJsonResult> UnbindPluginAsync(string accessTokenOrAppId, string appId, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/plugin?access_token={0}";
                var url = urlFormat.FormatWith(accessToken);

                var data = new
                {
                    action = "unbind",
                    plugin_appid = appId
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, url, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion
    }
}