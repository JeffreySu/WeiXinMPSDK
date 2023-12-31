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
    
    文件名：CodeApi.cs
    文件功能描述：代码管理
    
    
    创建标识：Senparc - 20170726

    修改标识：Senparc - 20190511
    修改描述：v14.5.3 添加 QrCode_ActionName.QR_STR_SCENE

    修改标识：Senparc - 20190525
    修改描述：v4.5.4.1 GetAuditStatusResultJson 改名为 GetAuditResultJson，保持全局命名唯一性

    修改标识：Senparc - 20190529
    修改描述：v4.7.101 添加“开放平台-代码管理-加急审核”接口：CodeApi.SpeedupAudit()

    修改标识：Senparc - 20191030
    修改描述：v4.7.102.1 修改 GetAuditStatus() 方法 auditid 参数类型（int -> long)

    修改标识：mc7246 - 20200318
    修改描述：v4.7.401 第三方小程序，提交审核接口更新

    修改标识：mc7246 - 20210930
    修改描述：v4.11.500 添加第三方代码提审资料上传接口

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.CO2NET.HttpUtility;
using System.IO;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.Code;

namespace Senparc.Weixin.Open.WxaAPIs
{
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class CodeApi
    {
        #region 同步方法
        /// <summary>
        /// 为授权的小程序帐号上传小程序代码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="template_id">代码库中的代码模版ID</param>
        /// <param name="ext_json">第三方自定义的配置</param>
        /// <param name="user_version">代码版本号</param>
        /// <param name="user_desc">代码描述</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson Commit(string accessToken, int template_id, string ext_json, string user_version, string user_desc, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/commit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id,
                ext_json = ext_json,
                user_version = user_version,
                user_desc = user_desc
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取小程序的体验二维码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CodeApi.GetQRCode", true, ApiRequestMethod = CO2NET.WebApi.ApiRequestMethod.Get)]
        public static CodeResultJson GetQRCode(string accessToken, Stream stream, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_qrcode?access_token={0}", accessToken.AsUrlData());

            Get.Download(CommonDI.CommonSP, url, stream);
            return new CodeResultJson()
            {
                errcode = ReturnCode.请求成功
            };
            //CommonJsonSend.Send<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取授权小程序帐号的可选类目
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetCategoryResultJson GetCategory(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_category?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetCategoryResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序的第三方提交代码的页面配置
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetPageResultJson GetPage(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_page?access_token={0}", accessToken.AsUrlData());


            return CommonJsonSend.Send<GetPageResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 将第三方提交的代码包提交审核
        /// 注意：只有上个版本被驳回，才能使用 feedback_info、feedback_stuff 这两个字段，否则忽略处理。
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="item_list">审核项列表（选填，至多填写 5 项）</param>
        /// <param name="preview_info">预览信息（小程序页面截图和操作录屏）</param>
        /// <param name="version_desc">小程序版本说明和功能解释</param>
        /// <param name="feedback_info">反馈内容，至多 200 字</param>
        /// <param name="feedback_stuff">用 | 分割的 media_id 列表，至多 5 张图片, 可以通过新增临时素材接口上传而得到</param>
        /// <param name="ugc_declare">用户生成内容场景（UGC）信息安全声明</param>
        /// <param name="privacy_api_not_use">用于声明是否不使用“代码中检测出但是未配置的隐私相关接口”</param>
        /// <param name="order_path">订单中心path</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetAuditResultJson SubmitAudit(string accessToken, List<SubmitAuditPageInfo> item_list, SubmitAuditPreviewInfo preview_info = null, string version_desc = "", string feedback_info = "", string feedback_stuff = "", SubmitAuditUgcDeclareInfo ugc_declare = null, bool privacy_api_not_use = false, string order_path = null, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/submit_audit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                item_list = item_list,
                preview_info = preview_info,
                version_desc = version_desc,
                feedback_info = feedback_info,
                feedback_stuff = feedback_stuff,
                ugc_declare = ugc_declare,
                privacy_api_not_use = privacy_api_not_use,
                order_path = order_path
            };

            return CommonJsonSend.Send<GetAuditResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询某个指定版本的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="auditid">提交审核时获得的审核id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetAuditResultJson GetAuditStatus(string accessToken, long auditid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_auditstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                auditid = auditid
            };

            return CommonJsonSend.Send<GetAuditResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询最新一次提交的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetAuditResultJson GetLatestAuditStatus(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_latest_auditstatus?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetAuditResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 小程序审核撤回
        /// 注意：单个帐号每天审核撤回次数最多不超过 1 次，一个月不超过 10 次
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson UndoCodeAudit(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/undocodeaudit?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 发布已通过审核的小程序
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson Release(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/release?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 修改小程序线上代码的可见状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="action">设置可访问状态，发布后默认可访问，close为不可见，open为可见</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson ChangeVisitStatus(string accessToken, ChangVisitStatusAction action, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/change_visitstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                action = action.ToString()
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 小程序版本回退（仅供第三方代小程序调用）
        /// <para>文档：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/code/get_history_version.html</para>
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="appVersion"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson RevertCodeRelease(string accessToken, string appVersion, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/revertcoderelease?access_token={0}&app_version={1}", accessToken.AsUrlData(),appVersion.AsUrlData());

            return CommonJsonSend.Send<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 查询当前设置的最低基础库版本及各版本用户占比（仅供第三方代小程序调用）
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWeappSupportVersionResultJson GetWeappSupportVersion(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/wxopen/getweappsupportversion?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetWeappSupportVersionResultJson>(null, url, null, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 设置最低基础库版本（仅供第三方代小程序调用）
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="version">版本</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson SetWeappSupportVersion(string accessToken, string version, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/wxopen/setweappsupportversion?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                version = version.ToString()
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 小程序分阶段发布接口
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="gray_percentage">灰度的百分比，1到100的整数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson GrayRelease(string accessToken, int gray_percentage, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/grayrelease?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                gray_percentage = gray_percentage
            };

            return CommonJsonSend.Send<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 小程序取消分阶段发布
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CodeResultJson RevertGrayRelease(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/revertgrayrelease?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }
        /// <summary>
        /// 小程序查询当前分阶段发布详情
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGrayReleasePlanResultJson GetGrayReleasePlan(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/getgrayreleaseplan?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetGrayReleasePlanResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 查询服务商的当月提审限额（quota）和加急次数
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryQuotaResultJson QueryQuota(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/queryquota?access_token={0}", accessToken.AsUrlData());

            object data = new { };

            return CommonJsonSend.Send<QueryQuotaResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        /// <summary>
        /// 加急审核申请
        /// 有加急次数的第三方可以通过该接口，对已经提审的小程序进行加急操作，加急后的小程序预计2-12小时内审完。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="auditid">审核单ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SpeedupAudit(string accessToken, int auditid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/speedupaudit?access_token={0}", accessToken.AsUrlData());

            object data = new
            {
                auditid = auditid
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 提审素材上传接口
        /// 文档 https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/code/submit_audit.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadMediaResult UploadMedia(string accessToken, string file, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/uploadmedia?access_token={0}", accessToken.AsUrlData());

            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return Post.PostFileGetJson<UploadMediaResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut);
        }


        #endregion


        #region 异步方法
        /// <summary>
        /// 为授权的小程序帐号上传小程序代码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="template_id">代码库中的代码模版ID</param>
        /// <param name="ext_json">第三方自定义的配置</param>
        /// <param name="user_version">代码版本号</param>
        /// <param name="user_desc">代码描述</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> CommitAsync(string accessToken, int template_id, string ext_json, string user_version, string user_desc, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/commit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id,
                ext_json = ext_json,
                user_version = user_version,
                user_desc = user_desc
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取小程序的体验二维码
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        [NcApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "CodeApi.GetQRCodeAsync", true, ApiRequestMethod = CO2NET.WebApi.ApiRequestMethod.Get)]
        public static async Task<CodeResultJson> GetQRCodeAsync(string accessToken, Stream stream, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_qrcode?access_token={0}", accessToken.AsUrlData());

            await Get.DownloadAsync(CommonDI.CommonSP, url, stream).ConfigureAwait(false);
            return new CodeResultJson()
            {
                errcode = ReturnCode.请求成功
            };
            //CommonJsonSend.Send<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取授权小程序帐号的可选类目
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetCategoryResultJson> GetCategoryAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_category?access_token={0}", accessToken.AsUrlData());


            return await CommonJsonSend.SendAsync<GetCategoryResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取小程序的第三方提交代码的页面配置
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetPageResultJson> GetPageAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_page?access_token={0}", accessToken.AsUrlData());


            return await CommonJsonSend.SendAsync<GetPageResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 将第三方提交的代码包提交审核
        /// 注意：只有上个版本被驳回，才能使用 feedback_info、feedback_stuff 这两个字段，否则忽略处理。
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="item_list">审核项列表（选填，至多填写 5 项）</param>
        /// <param name="preview_info">预览信息（小程序页面截图和操作录屏）</param>
        /// <param name="version_desc">小程序版本说明和功能解释</param>
        /// <param name="feedback_info">反馈内容，至多 200 字</param>
        /// <param name="feedback_stuff">用 | 分割的 media_id 列表，至多 5 张图片, 可以通过新增临时素材接口上传而得到</param>
        /// <param name="ugc_declare">用户生成内容场景（UGC）信息安全声明</param>
        /// <param name="privacy_api_not_use">用于声明是否不使用“代码中检测出但是未配置的隐私相关接口”</param>
        /// <param name="order_path">订单中心path</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetAuditResultJson> SubmitAuditAsync(string accessToken, List<SubmitAuditPageInfo> item_list, SubmitAuditPreviewInfo preview_info = null, string version_desc = "", string feedback_info = "", string feedback_stuff = "", SubmitAuditUgcDeclareInfo ugc_declare = null, bool privacy_api_not_use = false, string order_path = null, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/submit_audit?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                item_list = item_list,
                preview_info = preview_info,
                version_desc = version_desc,
                feedback_info = feedback_info,
                feedback_stuff = feedback_stuff,
                ugc_declare = ugc_declare,
                privacy_api_not_use = privacy_api_not_use,
                order_path = order_path
            };

            return await CommonJsonSend.SendAsync<GetAuditResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }


        /// <summary>
        /// 查询某个指定版本的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="auditid">提交审核时获得的审核id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetAuditResultJson> GetAuditStatusAsync(string accessToken, long auditid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_auditstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                auditid = auditid
            };

            return await CommonJsonSend.SendAsync<GetAuditResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询最新一次提交的审核状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetAuditResultJson> GetLatestAuditStatusAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_latest_auditstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {

            };

            return await CommonJsonSend.SendAsync<GetAuditResultJson>(null, url, data, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 小程序审核撤回
        /// 注意：单个帐号每天审核撤回次数最多不超过 1 次，一个月不超过 10 次
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> UndoCodeAuditAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/undocodeaudit?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 发布已通过审核的小程序
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> ReleaseAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/release?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 修改小程序线上代码的可见状态
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="action">设置可访问状态，发布后默认可访问，close为不可见，open为可见</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> ChangeVisitStatusAsync(string accessToken, ChangVisitStatusAction action, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/change_visitstatus?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                action = action.ToString()
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 小程序版本回退（仅供第三方代小程序调用）
        /// <para>文档：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/code/get_history_version.html</para>
        /// <para>文档：</para>
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> RevertCodeReleaseAsync(string accessToken, string appVersion, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/revertcoderelease?access_token={0}&app_version={1}", accessToken.AsUrlData(), appVersion.AsUrlData());

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 查询当前设置的最低基础库版本及各版本用户占比（仅供第三方代小程序调用）
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetWeappSupportVersionResultJson> GetWeappSupportVersionAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/wxopen/getweappsupportversion?access_token={0}", accessToken.AsUrlData());

            return await CommonJsonSend.SendAsync<GetWeappSupportVersionResultJson>(null, url, null, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 设置最低基础库版本（仅供第三方代小程序调用）
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="version">版本</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> SetWeappSupportVersionAsync(string accessToken, string version, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/wxopen/setweappsupportversion?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                version = version.ToString()
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 小程序分阶段发布接口
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="gray_percentage">灰度的百分比，1到100的整数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> GrayReleaseAsync(string accessToken, int gray_percentage, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/grayrelease?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                gray_percentage = gray_percentage
            };

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 小程序取消分阶段发布
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CodeResultJson> RevertGrayReleaseAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/revertgrayrelease?access_token={0}", accessToken.AsUrlData());

            return await CommonJsonSend.SendAsync<CodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 小程序查询当前分阶段发布详情
        /// </summary>
        /// <param name="accessToken">从第三方平台获取到的该小程序授权</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGrayReleasePlanResultJson> GetGrayReleasePlanAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/getgrayreleaseplan?access_token={0}", accessToken.AsUrlData());

            return await CommonJsonSend.SendAsync<GetGrayReleasePlanResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 查询服务商的当月提审限额（quota）和加急次数
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryQuotaResultJson> QueryQuotaAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/queryquota?access_token={0}", accessToken.AsUrlData());

            object data = new { };

            return await CommonJsonSend.SendAsync<QueryQuotaResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }
        /// <summary>
        /// 加急审核申请
        /// 有加急次数的第三方可以通过该接口，对已经提审的小程序进行加急操作，加急后的小程序预计2-12小时内审完。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="auditid">审核单ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SpeedupAuditAsync(string accessToken, int auditid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/speedupaudit?access_token={0}", accessToken.AsUrlData());

            object data = new
            {
                auditid = auditid
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 提审素材上传接口
        /// 文档 https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/code/submit_audit.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="file"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UploadMediaResult> UploadMediaAsync(string accessToken, string file, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/uploadmedia?access_token={0}", accessToken.AsUrlData());

            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return await Post.PostFileGetJsonAsync<UploadMediaResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);
        }

        #endregion
    }
}
