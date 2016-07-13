/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CustomInfoJson.cs
    文件功能描述：客服列表返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150306
    修改描述：增加“客服头像”
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
	public class CustomInfoJson : WxJsonResult
	{
		/// <summary>
		/// 客服列表
		/// </summary>
		public List<CustomInfo_Json> kf_list { get; set; }
	}

	public class CustomInfo_Json
	{
        /// <summary>
        /// 完整客服帐号，格式为：帐号前缀@公众号微信号
        /// </summary>
        public string kf_account { get; set; }

		/// <summary>
		/// 客服昵称
		/// </summary>
		public string kf_nick { get; set; }

		/// <summary>
		/// 客服编号
		/// </summary>
        public string kf_id { get; set; }

        /// <summary>
        /// 客服头像
        /// </summary>
        public string kf_headimgurl { get; set; }

        /// <summary>
        /// 如果客服帐号已绑定了客服人员微信号，则此处显示微信号
        /// </summary>
        public string kf_wx { get; set; }

        /// <summary>
        /// 如果客服帐号尚未绑定微信号，但是已经发起了一个绑定邀请，则此处显示绑定邀请的微信号
        /// </summary>
        public string invite_wx { get; set; }

        /// <summary>
        /// 如果客服帐号尚未绑定微信号，但是已经发起了一个绑定邀请，邀请的过期时间，为unix 时间戳
        /// </summary>
        public long? invite_expire_time { get; set; }

        /// <summary>
        /// 邀请的状态，有等待确认“waiting”，被拒绝“rejected”，过期“expired”
        /// </summary>
        public string invite_status { get; set; }


        /// <summary>
        /// 邀请的状态: 等待确认
        /// </summary>
        public const string InviteStatusWaiting = "waiting";
        /// <summary>
        /// 邀请的状态: 被拒绝
        /// </summary>
	    public const string InviteStatusRejected = "rejected";
        /// <summary>
        /// 邀请的状态: 过期
        /// </summary>
	    public const string InviteStatusExpired = "expired";

        /// <summary>
        /// 获取指定大小的用户头像网址
        /// </summary>
        /// <param name="size">代表正方形头像大小（目前发现有0、300数值可选，0代表640*640正方形头像）</param>
        /// <returns></returns>
        public string GetHeadImageUrl(int size = 0)
        {
            return UserInfoHelper.GetHeadImageUrlWithSize(kf_headimgurl, size);
        }

        /// <summary>
        /// 获取邀请过期时间
        /// </summary>
        /// <returns></returns>
        public DateTime? GetInviteExpireTime()
        {
            return invite_expire_time!=null
                ? DateTimeHelper.GetDateTimeFromXml(invite_expire_time.Value)
                : (DateTime?)null;
        }
    }
}