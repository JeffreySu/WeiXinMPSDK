﻿/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：GroupApi.cs
    文件功能描述：微小店分组接口
    
    
    创建标识：Senparc - 20150827
 
    修改标识;Senparc - 20160719
    修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店分组接口
    /// </summary>
    public static class GroupApi
    {
        #region 同步请求
        
       
        /// <summary>
        /// 增加分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addGroupData">增加分组需要Post的数据</param>
        /// <returns></returns>
        public static AddGroupResult AddGroup(string accessToken, AddGroupData addGroupData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/add?access_token={0}";

            return CommonJsonSend.Send<AddGroupResult>(accessToken, urlFormat, addGroupData);
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">分组Id</param>
        /// <returns></returns>
        public static WxJsonResult DeleteGroup(string accessToken, int groupId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/del?access_token={0}";

            var data = new
            {
                group_id = groupId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 修改分组属性
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="propertyModGroup">修改分组属性需要Post的数据</param>
        /// <returns></returns>
        public static WxJsonResult PropertyModGroup(string accessToken, PropertyModGroup propertyModGroup)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/propertymod?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, propertyModGroup);
        }

        /// <summary>
        /// 修改分组商品
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="productModGroup">修改分组商品需要Post的数据</param>
        /// <returns></returns>
        public static WxJsonResult ProductModGroup(string accessToken, ProductModGroup productModGroup)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/productmod?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, productModGroup);
        }

        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetAllGroup GetAllGroup(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/getall?access_token={0}";

            return CommonJsonSend.Send<GetAllGroup>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 根据分组ID获取分组信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">分组Id</param>
        /// <returns></returns>
        public static GetByIdGroup GetByIdGroup(string accessToken, int groupId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/getbyid?access_token={0}";

            var data = new
            {
                group_id = groupId
            };

            return CommonJsonSend.Send<GetByIdGroup>(accessToken, urlFormat, data);
        }
        #endregion
        #region 异步请求
        /// <summary>
        /// 【异步方法】增加分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addGroupData">增加分组需要Post的数据</param>
        /// <returns></returns>
        public static async Task<AddGroupResult> AddGroupAsync(string accessToken, AddGroupData addGroupData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/add?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddGroupResult>(accessToken, urlFormat, addGroupData);
        }

        /// <summary>
        /// 【异步方法】删除分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">分组Id</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeleteGroupAsync(string accessToken, int groupId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/del?access_token={0}";

            var data = new
            {
                group_id = groupId
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 【异步方法】修改分组属性
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="propertyModGroup">修改分组属性需要Post的数据</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> PropertyModGroupAsync(string accessToken, PropertyModGroup propertyModGroup)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/propertymod?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, propertyModGroup);
        }

        /// <summary>
        /// 【异步方法】修改分组商品
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="productModGroup">修改分组商品需要Post的数据</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ProductModGroupAsync(string accessToken, ProductModGroup productModGroup)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/productmod?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, productModGroup);
        }

        /// <summary>
        /// 【异步方法】获取所有分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<GetAllGroup> GetAllGroupAsync(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/getall?access_token={0}";

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetAllGroup>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }

        /// <summary>
        ///  【异步方法】根据分组ID获取分组信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="groupId">分组Id</param>
        /// <returns></returns>
        public static async Task<GetByIdGroup> GetByIdGroupAsync(string accessToken, int groupId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/group/getbyid?access_token={0}";

            var data = new
            {
                group_id = groupId
            };

            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetByIdGroup>(accessToken, urlFormat, data);
        }
        #endregion
    }
}
