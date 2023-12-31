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
    
    文件名：WxOpenApi.cs
    文件功能描述：微信小程序 API
    
    
    创建标识：Senparc - 20180716

    修改标识：Senparc - 20220730
    修改描述：v4.14.7 添加“获取公众号关联的小程序”接口
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.AccountAPIs;
using Senparc.Weixin.Open.WxOpenAPIs.AddCategoryJson;
using Senparc.Weixin.Open.WxOpenAPIs.CategoryListJson;
using Senparc.Weixin.Open.WxOpenAPIs.GetCategoryJson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxOpenAPIs
{
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class WxOpenApi
    {
        #region 同步方法

        #region 换绑小程序管理员接口

        /*
         *  流程
         *  步骤一：从第三方平台页面发起，并跳转至微信公众平台指定换绑页面。
         *  步骤二：小程序原管理员扫码，并填写原管理员身份证信息确认。
         *  步骤三：填写新管理员信息(姓名、身份证、手机号)，使用新管理员的微信确认。
         *  步骤四：点击提交后跳转至第三方平台页面，第三方平台回调对应 api 完成换绑流程。
         */

        /// <summary>
        /// 从第三方平台跳转至微信公众平台授权注册页面
        /// </summary>
        /// <param name="component_appid">第三方平台的appid</param>
        /// <param name="appid">公众号的 appid</param>
        /// <param name="redirect_uri">新管理员信息填写完成点击提交后，将跳转到该地址
        /// (注：Host需和第三方平台在微信开放平台上面填写的登录授权的发起页域名一致)
        /// <para>点击页面提交按钮。 跳转回第三方平台，会在上述 redirect_uri 后拼接 taskid=*</para>
        /// <para><see cref="AccountApi.ComponentRebindAdmin"/>方法</para>
        /// </param>
        public static string ComponentRebindAdmin(string component_appid, string appid, string redirect_uri)
        {
            var url =
                $"https://mp.weixin.qq.com/wxopen/componentrebindadmin?appid={appid}&component_appid={component_appid}&redirect_uri={redirect_uri.AsUrlData()}";
            return url;
        }

        #endregion

        #region 类目相关接口

        /// <summary>
        /// 获取账号可以设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        public static CategoryListJsonResult GetAllCategories(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getallcategories?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<CategoryListJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="addCategoryData">添加类目参数</param>
        /// <returns></returns>
        public static WxJsonResult AddCategory(string accessToken, List<AddCategoryData> addCategoryData)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/addcategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                categories = addCategoryData
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <returns></returns>
        public static WxJsonResult DeleteCategory(string accessToken, int first, int second)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/deletecategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second
            };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        /// <summary>
        /// 获取账号已经设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        public static GetCategoryJsonResult GetCategory(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getcategory?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<GetCategoryJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 修改类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        public static WxJsonResult ModifyCategory(string accessToken, int first, int second,
            List<KeyValuePair<string, string>> certicates)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/modifycategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second,
                certicates = certicates
            };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data);
        }

        #endregion

        #region 小程序管理

        /// <summary>
        /// 获取公众号关联的小程序
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        public static WxaMpLinkGetJsonResult WxaMpLinkGet(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxamplinkget?access_token={accessToken.AsUrlData()}";
            return CommonJsonSend.Send<WxaMpLinkGetJsonResult>(null, url, null, CommonJsonSendType.POST);
        }

        #endregion


        #endregion

        #region 异步方法

        #region 类目相关接口

        /// <summary>
        /// 获取账号可以设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        public static async Task<CategoryListJsonResult> GetAllCategoriesAsync(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getallcategories?access_token={accessToken.AsUrlData()}";
            return await CommonJsonSend.SendAsync<CategoryListJsonResult>(null, url, null,
                CommonJsonSendType.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="addCategoryData">添加类目参数</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AddCategoryAsync(string accessToken, List<AddCategoryData> addCategoryData)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/addcategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                categories = addCategoryData
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeleteCategoryAsync(string accessToken, int first, int second)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/deletecategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取账号已经设置的所有类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        public static async Task<GetCategoryJsonResult> GetCategoryAsync(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/getcategory?access_token={accessToken.AsUrlData()}";
            return await CommonJsonSend.SendAsync<GetCategoryJsonResult>(null, url, null,
                CommonJsonSendType.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 修改类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ModifyCategoryAsync(string accessToken, int first, int second,
            List<KeyValuePair<string, string>> certicates)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/modifycategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second,
                certicates = certicates
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data).ConfigureAwait(false);
        }

        #endregion

        #region 小程序管理

        /// <summary>
        /// 获取公众号关联的小程序
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <returns></returns>
        public static async Task<WxaMpLinkGetJsonResult> WxaMpLinkGetAsync(string accessToken)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/wxamplinkget?access_token={accessToken.AsUrlData()}";
            return await CommonJsonSend.SendAsync<WxaMpLinkGetJsonResult>(null, url, null, CommonJsonSendType.POST);
        }

        #endregion

        #endregion
    }
}