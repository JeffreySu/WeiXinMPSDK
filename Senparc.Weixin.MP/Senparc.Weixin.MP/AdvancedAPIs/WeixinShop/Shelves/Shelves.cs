﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
    /// </summary>
    public static class WeixinShopShelves
    {
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
        public static AddShelvesResult AddShelves(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5, string shelfBanner, string shelfName)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/add?access_token={0}";

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
            
            return CommonJsonSend.Send<AddShelvesResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        public static WxJsonResult DeleteShelves(string accessToken, int shelfId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/del?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, shelfId);
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
        public static WxJsonResult ModShelves(string accessToken, M1 m1, M2 m2, M3 m3, M4 m4, M5 m5,int shelfId, string shelfBanner, string shelfName)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/mod?access_token={0}";

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
        public static GetAllShelvesResult GetAllShelves(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/getall?access_token=ACCESS_TOKEN";

            return CommonJsonSend.Send<GetAllShelvesResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 根据货架ID获取货架信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="shelfId">货架Id</param>
        /// <returns></returns>
        public static GetByIdShelvesResult GetByIdShelves(string accessToken, int shelfId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/shelf/getbyid?access_token={0}";

            return CommonJsonSend.Send<GetByIdShelvesResult>(accessToken, urlFormat, shelfId);
        }
    }
}
