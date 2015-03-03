/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CustomServiceAPI.cs
    文件功能描述：多客服接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/* 
    多客服接口聊天记录接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E5%AE%A2%E6%9C%8D%E8%81%8A%E5%A4%A9%E8%AE%B0%E5%BD%95
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Helpers;
namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 多客服接口
    /// </summary>
    public static class CustomServiceAPI
    {
        /// <summary>
        /// 获取用户聊天记录
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="startTime">查询开始时间，会自动转为UNIX时间戳</param>
        /// <param name="endTime">查询结束时间，会自动转为UNIX时间戳，每次查询不能跨日查询</param>
        /// <param name="openId">（非必须）普通用户的标识，对当前公众号唯一</param>
        /// <param name="pageSize">每页大小，每页最多拉取1000条</param>
        /// <param name="pageIndex">查询第几页，从1开始</param>
        public static GetRecordResult GetRecord(string accessToken, DateTime startTime, DateTime endTime, string openId = null, int pageSize = 10, int pageIndex = 1)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/customservice/getrecord?access_token={0}";

            //规范页码
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            else if (pageSize > 1000)
            {
                pageSize = 1000;
            }

            //组装发送消息
            var data = new
            {
                starttime = DateTimeHelper.GetWeixinDateTime(startTime),
                endtime = DateTimeHelper.GetWeixinDateTime(endTime),
                openId = openId,
                pagesize = pageSize,
                pageIndex = pageIndex
            };

            return CommonJsonSend.Send<GetRecordResult>(accessToken, urlFormat, data);
        }

        /// <summary>
		/// 获取在线客服接待信息
		/// 官方API：http://dkf.qq.com/document-3_2.html
		/// </summary>
		/// <param name="accessToken">调用接口凭证</param>
		/// <returns></returns>
		public static CustomOnlineJson GetCustomOnlineInfo(string accessToken)
		{
			var urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken);
			return GetCustomInfoResult<CustomOnlineJson>(urlFormat);
		}

		/// <summary>
		/// 获取客服基本信息
		/// 官方API：http://dkf.qq.com/document-3_1.html
		/// </summary>
		/// <param name="accessToken">调用接口凭证</param>
		/// <returns></returns>
		public static CustomInfoJson GetCustomBasicInfo(string accessToken)
		{
			var urlFormat = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token={0}", accessToken);
			return GetCustomInfoResult<CustomInfoJson>(urlFormat);
		}

		private static T GetCustomInfoResult<T>(string urlFormat)
		{
			var jsonString = HttpUtility.RequestUtility.HttpGet(urlFormat, Encoding.UTF8);
			JavaScriptSerializer js = new JavaScriptSerializer();
			return js.Deserialize<T>(jsonString);
		}
    }
}
