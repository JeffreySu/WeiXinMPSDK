﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
    
    文件名：ResponseMessagetTransfer_Customer_Service.cs
    文件功能描述：响应回复多客服消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20180924
    修改描述：从 Senparc.Weixi.MP 移植并修改

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 响应回复多客服消息
    /// </summary>
	public class ResponseMessageTransfer_Customer_Service : ResponseMessageBase, IResponseMessageTransfer_Customer_Service
    {
		public ResponseMessageTransfer_Customer_Service()
		{
		}

		public override ResponseMsgType MsgType
		{
			get { return ResponseMsgType.Transfer_Customer_Service; }
		}

	}
 }