﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：UploadResultJson.cs
    文件功能描述：上传媒体文件返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150703
    修改描述：增加获取OpenId

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=OAuth2%E9%AA%8C%E8%AF%81%E6%8E%A5%E5%8F%A3
 */

using System;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.OAuth2;

namespace Senparc.Weixin.Work.AdvancedAPIs
{

    public static class OAuth2Api
    {
        #region 同步方法
        
        
        /*此接口不提供异步方法*/
        /// <summary>
        /// 企业获取code
        /// </summary>
        /// <param name="corpId">企业的CorpID</param>
        /// <param name="redirectUrl">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="state">重定向后会带上state参数，企业可以填写a-zA-Z0-9的参数值</param>
        /// <param name="responseType">返回类型，此时固定为：code</param>
        /// <param name="scope">应用授权作用域，此时固定为：snsapi_base</param>
        /// #wechat_redirect 微信终端使用此参数判断是否需要带上身份信息
        /// 员工点击后，页面将跳转至 redirect_uri/?code=CODE&state=STATE，企业可根据code参数获得员工的userid。
        /// <returns></returns>
        public static string GetCode(string corpId, string redirectUrl, string state, string responseType = "code", string scope = "snsapi_base")
        {
            var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect", corpId.AsUrlData(), redirectUrl.AsUrlData(), responseType.AsUrlData(), scope.AsUrlData(), state.AsUrlData());

            return url;
        }

        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="code">通过员工授权获取到的code，每次员工授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期</param>
        /// 权限说明：管理员须拥有agent的使用权限；agentid必须和跳转链接时所在的企业应用ID相同。
        /// <returns></returns>
        [Obsolete("请使用新方法GetUserId(string accessToken, string code)")]
        public static GetUserInfoResult GetUserId(string accessToken, string code, string agentId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}&agentid={2}", accessToken.AsUrlData(), code.AsUrlData(), agentId.AsUrlData());

            return Get.GetJson<GetUserInfoResult>(url);
        }

        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="code">通过员工授权获取到的code，每次员工授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期</param>
        /// 权限说明：管理员须拥有agent的使用权限；agentid必须和跳转链接时所在的企业应用ID相同。
        /// <returns></returns>
        public static GetUserInfoResult GetUserId(string accessToken, string code)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", accessToken.AsUrlData(), code.AsUrlData());

            return Get.GetJson<GetUserInfoResult>(url);
        }

        /// <summary>
        /// 使用user_ticket获取成员详情
        /// 官方文档：http://work.weixin.qq.com/api/doc#10028
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userTicket">成员票据</param>
        /// <returns></returns>
        public static GetUserDetailResult GetUserDetail(string accessToken,string userTicket)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserdetail?access_token={0}";

            var data = new
            {
                user_ticket = userTicket
            };

            return CommonJsonSend.Send<GetUserDetailResult>(accessToken, urlFormat, data);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        ///【异步方法】 获取成员信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="code">通过员工授权获取到的code，每次员工授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期</param>
        /// 权限说明：管理员须拥有agent的使用权限；agentid必须和跳转链接时所在的企业应用ID相同。
        /// <returns></returns>
        [Obsolete("请使用新方法GetUserId(string accessToken, string code)")]
        public static async Task<GetUserInfoResult> GetUserIdAsync(string accessToken, string code, string agentId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}&agentid={2}", accessToken.AsUrlData(), code.AsUrlData(), agentId.AsUrlData());

            return await Get.GetJsonAsync<GetUserInfoResult>(url);
        }

        /// <summary>
        /// 【异步方法】获取成员信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="code">通过员工授权获取到的code，每次员工授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期</param>
        /// 权限说明：管理员须拥有agent的使用权限；agentid必须和跳转链接时所在的企业应用ID相同。
        /// <returns></returns>
        public static async Task<GetUserInfoResult> GetUserIdAsync(string accessToken, string code)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", accessToken.AsUrlData(), code.AsUrlData());

            return await Get.GetJsonAsync<GetUserInfoResult>(url);
        }

        /// <summary>
        /// 【异步请求】使用user_ticket获取成员详情
        /// 官方文档：http://work.weixin.qq.com/api/doc#10028
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userTicket">成员票据</param>
        /// <returns></returns>
        public static async Task<GetUserDetailResult> GetUserDetailAsync(string accessToken, string userTicket)
        {
            var urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserdetail?access_token={0}";

            var data = new
            {
                user_ticket = userTicket
            };

            return await CommonJsonSend.SendAsync<GetUserDetailResult>(accessToken, urlFormat, data);
        }
        #endregion
#endif
    }
}
