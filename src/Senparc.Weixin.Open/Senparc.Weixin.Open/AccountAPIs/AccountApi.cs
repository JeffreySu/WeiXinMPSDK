using Senparc.CO2NET.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.AccountAPIs.AccountBasicInfoJson;
using Senparc.Weixin.Open.AccountAPIs.FastRegisterJson;
using Senparc.Weixin.Open.MpAPIs.Open;
using Senparc.NeuChar;

namespace Senparc.Weixin.Open.AccountAPIs
{
    /// <summary>
    /// 小程序信息设置
    /// <para>https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=21528465979XX32V&token=&lang=zh_CN</para>
    /// <para>包含 复用公众号主体快速注册小程序</para>
    /// <para>https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=21521706765hLoMO&token=&lang=zh_CN</para>
    /// </summary>
    public class AccountApi
    {
        #region 同步方法

        #region 快速注册小程序 https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=21521706765hLoMO&token=&lang=zh_CN

        /// <summary>
        /// 从第三方平台跳转至微信公众平台授权注册页面
        /// </summary>
        /// <param name="componentAppId">第三方平台的appid</param>
        /// <param name="appid">公众号的 appid</param>
        /// <param name="copy_wx_verify">是否复用公众号的资质进行微信认证(1:申请复用资质进行微信 认证 0:不申请)</param>
        /// <param name="redirect_uri">
        /// 用户扫码授权后，MP 扫码页面将跳转到该地址(注:Host 需和第三方平台在微信开放平台上面填写的登 录授权的发起页域名一致)
        /// 公众号管理员扫码后在手机端完成授权确认。跳转回第三方平台，会在上述 redirect_uri后拼接 ticket=*
        /// </param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.FastRegisterAuth", true)]
        public static string FastRegisterAuth(string componentAppId, string appid, bool copy_wx_verify,
            string redirect_uri)
        {
            var url = $"https://mp.weixin.qq.com/cgi-bin/fastregisterauth";
            var oAuthUrl =
                $"{url}?appid={appid}&component_appid={componentAppId}&copy_wx_verify={(copy_wx_verify ? 1 : 0)}&redirect_uri={redirect_uri.AsUrlData()}";
            return oAuthUrl;
        }

        /// <summary>
        /// 跳转至第三方平台，第三方平台调用快速注册API完成注册
        /// </summary>
        /// <param name="accessToken">使用公众号appid换取authorizer_access_token</param>
        /// <param name="ticket">公众号扫码授权的凭证(公众平台扫码页面回跳到第三方平台时携带)</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.FastRegister", true)]
        public static FastRegisterJsonResult FastRegister(string accessToken, string ticket)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/fastregister?access_token={accessToken.AsUrlData()}";
            var data = new {ticket = ticket};
            return CommonJsonSend.Send<FastRegisterJsonResult>(null, url, data);
        }

        #endregion

        #region 小程序信息设置相关接口

        /// <summary>
        /// 获取帐号基本信息
        /// </summary>
        /// <param name="accessToken">
        /// 小程序的access_token
        /// <para>新创建小程序appid及authorization_code换取authorizer_refresh_token进而得到authorizer_access_token。</para>
        /// </param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.GetAccountBasicInfo", true)]
        public static AccountBasicInfoJsonResult GetAccountBasicInfo(string accessToken)
        {
            var url =
                $"{Config.ApiMpHost}/cgi-bin/account/getaccountbasicinfo?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<AccountBasicInfoJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <para>图片格式只支持：BMP、JPEG、JPG、GIF、PNG，大小不超过2M
        /// 注：实际头像始终为正方形</para>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="head_img_media_id">头像素材media_id</param>
        /// <param name="x1">裁剪框左上角x坐标（取值范围：[0, 1]）</param>
        /// <param name="y1">裁剪框左上角y坐标（取值范围：[0, 1]）</param>
        /// <param name="x2">裁剪框右下角x坐标（取值范围：[0, 1]）</param>
        /// <param name="y2">裁剪框右下角y坐标（取值范围：[0, 1]）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.ModifyHeadImage", true)]
        public static WxJsonResult ModifyHeadImage(string accessToken, string head_img_media_id, float x1, float y1,
            float x2, float y2)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/modifyheadimage?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                head_img_media_id = head_img_media_id,
                x1 = x1,
                y1 = y1,
                x2 = x2,
                y2 = y2
            };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        /// <summary>
        /// 修改功能介绍
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="signature">功能介绍</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.ModifySignature", true)]
        public static WxJsonResult ModifySignature(string accessToken, string signature)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/modifysignature?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                signature = signature
            };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        #endregion

        #region 换绑小程序管理员接口

        /// <summary>
        /// 跳转至第三方平台，第三方平台调用快速注册API完成管理员换绑。
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="taskid">换绑管理员任务序列号(公众平台最终点击提交回跳到第三方平台时携带)
        /// <para><see cref="Senparc.Weixin.Open.WxOpenAPIs.WxOpenApi.ComponentRebindAdmin"/></para></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.ComponentRebindAdmin", true)]
        public static WxJsonResult ComponentRebindAdmin(string accessToken, string taskid)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/componentrebindadmin?access_token={accessToken.AsUrlData()}";
            var data = new {taskid = taskid};
            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        #endregion

        #endregion


        #region 异步方法

        #region 快速注册小程序

        /// <summary>
        /// 跳转至第三方平台，第三方平台调用快速注册API完成注册
        /// </summary>
        /// <param name="accessToken">使用公众号appid换取authorizer_access_token</param>
        /// <param name="ticket">公众号扫码授权的凭证(公众平台扫码页面回跳到第三方平台时携带)</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.FastRegisterAsync", true)]
        public static async Task<FastRegisterJsonResult> FastRegisterAsync(string accessToken, string ticket)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/fastregister?access_token={accessToken.AsUrlData()}";
            var data = new { ticket = ticket };
            return await CommonJsonSend.SendAsync<FastRegisterJsonResult>(null, url, data).ConfigureAwait(false);
        }

        #endregion

        #region 小程序信息设置相关接口

        /// <summary>
        /// 获取帐号基本信息
        /// </summary>
        /// <param name="accessToken">新创建小程序appid及authorization_code换取authorizer_refresh_token进而得到authorizer_access_token。</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.GetAccountBasicInfoAsync", true)]
        public static async Task<AccountBasicInfoJsonResult> GetAccountBasicInfoAsync(string accessToken)
        {
            var url =
                $"{Config.ApiMpHost}/cgi-bin/account/getaccountbasicinfo?access_token={accessToken.AsUrlData()}";
            return await CommonJsonSend.SendAsync<AccountBasicInfoJsonResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <para>图片格式只支持：BMP、JPEG、JPG、GIF、PNG，大小不超过2M
        /// 注：实际头像始终为正方形</para>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="head_img_media_id">头像素材media_id</param>
        /// <param name="x1">裁剪框左上角x坐标（取值范围：[0, 1]）</param>
        /// <param name="y1">裁剪框左上角y坐标（取值范围：[0, 1]）</param>
        /// <param name="x2">裁剪框右下角x坐标（取值范围：[0, 1]）</param>
        /// <param name="y2">裁剪框右下角y坐标（取值范围：[0, 1]）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.ModifyHeadImageAsync", true)]
        public static async Task<WxJsonResult> ModifyHeadImageAsync(string accessToken, string head_img_media_id, float x1, float y1,
            float x2, float y2)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/modifyheadimage?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                head_img_media_id = head_img_media_id,
                x1 = x1,
                y1 = y1,
                x2 = x2,
                y2 = y2
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 修改功能介绍
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="signature">功能介绍</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.ModifySignatureAsync", true)]
        public static async Task<WxJsonResult> ModifySignatureAsync(string accessToken, string signature)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/modifysignature?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                signature = signature
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        #endregion

        #region 换绑小程序管理员接口

        /// <summary>
        /// 跳转至第三方平台，第三方平台调用快速注册API完成管理员换绑。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="taskid">换绑管理员任务序列号(公众平台最终点击提交回跳到第三方平台时携带)
        /// <para><see cref="Senparc.Weixin.Open.WxOpenAPIs.WxOpenApi.ComponentRebindAdmin"/></para></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "AccountApi.ComponentRebindAdminAsync", true)]
        public static async Task<WxJsonResult> ComponentRebindAdminAsync(string accessToken, string taskid)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/account/componentrebindadmin?access_token={accessToken.AsUrlData()}";
            var data = new { taskid = taskid };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}