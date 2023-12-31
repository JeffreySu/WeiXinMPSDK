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
    
    文件名：CustomInfoJson.cs
    文件功能描述：客服列表返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150306
    修改描述：增加“客服头像”

    修改标识：Senparc - 20220504
    修改描述：v16.18.2 完善客服接口，添加：kf_wx、invite_wx、invite_expire_time、invite_status 参数

----------------------------------------------------------------*/



using System.Collections.Generic;
using Senparc.Weixin.Entities;

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
		/// 客服账号
		/// </summary>
		public string kf_account { get; set; }

		/// <summary>
		/// 客服昵称
		/// </summary>
		public string kf_nick { get; set; }

		/// <summary>
		/// 客服工号
		/// </summary>
        public int kf_id { get; set; }

        /// <summary>
        /// 客服头像
        /// </summary>
        public string kf_headimgurl { get; set; }

		/// <summary>
		/// 如果客服帐号已绑定了客服人员微信号， 则此处显示微信号
		/// </summary>
		public string kf_wx { get; set; }

		/// <summary>
		/// 如果客服帐号尚未绑定微信号，但是已经发起了一个绑定邀请， 则此处显示绑定邀请的微信号
		/// </summary>
		public string invite_wx { get; set; }

		/// <summary>
		/// 如果客服帐号尚未绑定微信号，但是已经发起过一个绑定邀请， 邀请的过期时间，为unix 时间戳
		/// </summary>
		public long invite_expire_time { get; set; }

		/// <summary>
		/// 邀请的状态，有等待确认“waiting”，被拒绝“rejected”， 过期“expired”
		/// </summary>
		public string invite_status { get; set; }
    }
}