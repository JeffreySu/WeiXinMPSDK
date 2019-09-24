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
    
    文件名：ShelfApi.cs
    文件功能描述：微小店货架接口
    
    
    创建标识：Senparc - 20150827
 
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店货架接口
    /// </summary>
    public static class ShelfApi
    {
        #region 同步方法
        /// <summary>
        /// 增加货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.AddShelves", true)]
        public static AddShelfResult AddShelves(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, string shelfBanner, string shelfName)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/add?access_token={0}";

            var data = new
            {
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return CommonJsonSend.Send<AddShelfResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.DeleteShelves", true)]
        public static WxJsonResult DeleteShelves(string accessToken, int shelfId)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/del?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 修改货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfId">货架Id</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.ModShelves", true)]
        public static WxJsonResult ModShelves(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, int shelfId, string shelfBanner, string shelfName)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/mod?access_token={0}";

            var data = new
            {
                shelf_id = shelfId,
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取所有货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.GetAllShelves", true)]
        public static GetAllShelfResult GetAllShelves(string accessToken)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/getall?access_token=ACCESS_TOKEN";

            return CommonJsonSend.Send<GetAllShelfResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 根据货架ID获取货架信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.GetByIdShelves", true)]
        public static GetByIdShelfResult GetByIdShelves(string accessToken, int shelfId)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/getbyid?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return CommonJsonSend.Send<GetByIdShelfResult>(accessToken, urlFormat, data);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】增加货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.AddShelvesAsync", true)]
        public static async Task<AddShelfResult> AddShelvesAsync(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, string shelfBanner, string shelfName)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/add?access_token={0}";

            var data = new
            {
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddShelfResult>(accessToken, urlFormat, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.DeleteShelvesAsync", true)]
        public static async Task<WxJsonResult> DeleteShelvesAsync(string accessToken, int shelfId)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/del?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】修改货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="m1">控件1数据</param>
        /// <param name="m2">控件2数据</param>
        /// <param name="m3">控件3数据</param>
        /// <param name="m4">控件4数据</param>
        /// <param name="m5">控件5数据</param>
        /// <param name="shelfId">货架Id</param>
        /// <param name="shelfBanner">货架招牌图片Url</param>
        /// <param name="shelfName">货架名称</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.ModShelvesAsync", true)]
        public static async Task<WxJsonResult> ModShelvesAsync(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, int shelfId, string shelfBanner, string shelfName)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/mod?access_token={0}";

            var data = new
            {
                shelf_id = shelfId,
                shelf_data = new
                {
                    module_infos = new object[]
                                {
                                    m1,
                                    m2,
                                    m3,
                                    m4,
                                    m5
                                }
                },
                shelf_banner = shelfBanner,
                shelf_name = shelfName
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取所有货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.GetAllShelvesAsync", true)]
        public static async Task<GetAllShelfResult> GetAllShelvesAsync(string accessToken)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/getall?access_token=ACCESS_TOKEN";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetAllShelfResult>(accessToken, urlFormat, null, CommonJsonSendType.GET).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】根据货架ID获取货架信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "ShelfApi.GetByIdShelvesAsync", true)]
        public static async Task<GetByIdShelfResult> GetByIdShelvesAsync(string accessToken, int shelfId)
        {
            var urlFormat = Config.ApiMpHost + "/merchant/shelf/getbyid?access_token={0}";

            var data = new
            {
                shelf_id = shelfId
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetByIdShelfResult>(accessToken, urlFormat, data).ConfigureAwait(false);
        }
        #endregion
    }
}
