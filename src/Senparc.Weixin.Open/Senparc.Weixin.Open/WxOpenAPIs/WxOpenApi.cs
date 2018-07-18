using Senparc.CO2NET.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.AccountAPIs;
using Senparc.Weixin.Open.WxOpenAPIs.CategoryListJson;
using Senparc.Weixin.Open.WxOpenAPIs.GetCategoryJson;

namespace Senparc.Weixin.Open.WxOpenAPIs
{
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
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        public static WxJsonResult AddCategory(string accessToken, int first, int second,
            IList<KeyValuePair<string, string>> certicates)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/addcategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second,
                certicates = certicates
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
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        public static WxJsonResult ModifyCategory(string accessToken, int first, int second,
            IList<KeyValuePair<string, string>> certicates)
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

        #endregion

#if !NET35 && !NET40

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
                CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AddCategoryAsync(string accessToken, int first, int second,
            IList<KeyValuePair<string, string>> certicates)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/addcategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second,
                certicates = certicates
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data);
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
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data);
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
                CommonJsonSendType.GET);
        }

        /// <summary>
        /// 添加类目
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="first">一级类目ID</param>
        /// <param name="second">二级类目ID</param>
        /// <param name="certicates">资质名称,资质图片</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ModifyCategoryAsync(string accessToken, int first, int second,
            IList<KeyValuePair<string, string>> certicates)
        {
            var url = $"{Config.ApiMpHost}/cgi-bin/wxopen/modifycategory?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                first = first,
                second = second,
                certicates = certicates
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data);
        }

        #endregion

        #endregion

#endif
    }
}