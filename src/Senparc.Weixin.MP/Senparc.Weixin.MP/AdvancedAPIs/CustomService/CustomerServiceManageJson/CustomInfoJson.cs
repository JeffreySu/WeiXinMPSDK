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
    
    文件名：CustomInfoJson.cs
    文件功能描述：客服列表返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150306
    修改描述：增加“客服头像”
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
	}
}