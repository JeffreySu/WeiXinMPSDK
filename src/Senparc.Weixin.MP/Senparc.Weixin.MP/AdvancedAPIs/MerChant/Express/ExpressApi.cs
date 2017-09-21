﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
修改标识：Senparc - 20160719
修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
    /// </summary>
    public static class ExpressApi
    {
        #region 同步请求
        
      
        /// <summary>
        /// 增加邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addExpressData">增加邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static AddExpressResult AddExpress(string accessToken, AddExpressData addExpressData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/add?access_token={0}";

            return CommonJsonSend.Send<AddExpressResult>(accessToken, urlFormat, addExpressData);
        }

        /// <summary>
        /// 删除邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static WxJsonResult DeleteExpress(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/del?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 修改邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="upDateExpressData">修改邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static WxJsonResult UpDateExpress(string accessToken, UpDateExpressData upDateExpressData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/update?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, upDateExpressData);
        }

        /// <summary>
        /// 获取指定ID的邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static GetByIdExpressResult GetByIdExpress(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getbyid?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return CommonJsonSend.Send<GetByIdExpressResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取所有邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetAllExpressResult GetAllExpress(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getall?access_token={0}";

            return CommonJsonSend.Send<GetAllExpressResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
        #endregion

        #region 异步请求
        /// <summary>
        /// 【异步方法】增加邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addExpressData">增加邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static async Task<AddExpressResult> AddExpressAsync(string accessToken, AddExpressData addExpressData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/add?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddExpressResult>(accessToken, urlFormat, addExpressData);
        }

        /// <summary>
        /// 【异步方法】删除邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeleteExpressAsync(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/del?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】修改邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="upDateExpressData">修改邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UpDateExpressAsync(string accessToken, UpDateExpressData upDateExpressData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/update?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, upDateExpressData);
        }

        /// <summary>
        /// 【异步方法】获取指定ID的邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static async Task<GetByIdExpressResult> GetByIdExpressAsync(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getbyid?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetByIdExpressResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】获取所有邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<GetAllExpressResult> GetAllExpressAsync(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getall?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetAllExpressResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
        #endregion
    }
}
