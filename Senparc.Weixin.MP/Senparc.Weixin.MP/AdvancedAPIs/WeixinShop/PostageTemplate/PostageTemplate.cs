using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
    /// </summary>
    public static class WeixinShopPostageTemplate
    {
        /// <summary>
        /// 增加邮费模板
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="addPostageTemplateData">增加邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static AddPostageTemplateResult AddPostageTemplate(string appId, AddPostageTemplateData addPostageTemplateData)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/express/add?access_token={0}";

            return CommonJsonSend.Send<AddPostageTemplateResult>(accessToken, urlFormat, addPostageTemplateData);
        }

        /// <summary>
        /// 删除邮费模板
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static DeletePostageTemplateResult DeletePostageTemplate(string appId, int templateId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/express/del?access_token={0}";

            return CommonJsonSend.Send<DeletePostageTemplateResult>(accessToken, urlFormat, templateId);
        }

        /// <summary>
        /// 修改邮费模板
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="upDatePostageTemplateData">修改邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static UpdatePostageTemplateResult UpDatePostageTemplate(string appId, UpDatePostageTemplateData upDatePostageTemplateData)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/express/update?access_token={0}";

            return CommonJsonSend.Send<UpdatePostageTemplateResult>(accessToken, urlFormat, upDatePostageTemplateData);
        }

        /// <summary>
        /// 获取指定ID的邮费模板
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static GetByIdPostageTemplateResult GetByIdPostageTemplate(string appId, int templateId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/express/getbyid?access_token={0}";

            return CommonJsonSend.Send<GetByIdPostageTemplateResult>(accessToken, urlFormat, templateId);
        }

        /// <summary>
        /// 获取所有邮费模板
        /// </summary>
        /// <param name="appId">公众平台账户的AppId</param>
        /// <returns></returns>
        public static GetAllPostageTemplateResult GetAllPostageTemplate(string appId)
        {
            var accessToken = AccessTokenContainer.GetToken(appId);

            var urlFormat = "https://api.weixin.qq.com/merchant/express/getall?access_token={0}";

            return CommonJsonSend.Send<GetAllPostageTemplateResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
    }
}
