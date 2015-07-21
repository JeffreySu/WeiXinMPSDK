using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.Entities;
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
        /// <param name="accessToken"></param>
        /// <param name="addPostageTemplateData">增加邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static AddPostageTemplateResult AddPostageTemplate(string accessToken, AddPostageTemplateData addPostageTemplateData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/add?access_token={0}";

            return CommonJsonSend.Send<AddPostageTemplateResult>(accessToken, urlFormat, addPostageTemplateData);
        }

        /// <summary>
        /// 删除邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static WxJsonResult DeletePostageTemplate(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/del?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, templateId);
        }

        /// <summary>
        /// 修改邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="upDatePostageTemplateData">修改邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static WxJsonResult UpDatePostageTemplate(string accessToken, UpDatePostageTemplateData upDatePostageTemplateData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/update?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, upDatePostageTemplateData);
        }

        /// <summary>
        /// 获取指定ID的邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static GetByIdPostageTemplateResult GetByIdPostageTemplate(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getbyid?access_token={0}";

            return CommonJsonSend.Send<GetByIdPostageTemplateResult>(accessToken, urlFormat, templateId);
        }

        /// <summary>
        /// 获取所有邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetAllPostageTemplateResult GetAllPostageTemplate(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getall?access_token={0}";

            return CommonJsonSend.Send<GetAllPostageTemplateResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
    }
}
