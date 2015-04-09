﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ResponseMessagetTransfer_Customer_Service.cs
    文件功能描述：响应回复多客服消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
	public class ResponseMessageTransfer_Customer_Service : ResponseMessageBase, IResponseMessageBase
	{
		public ResponseMessageTransfer_Customer_Service()
		{
			TransInfo = new List<CustomerServiceAccount>();
		}

		new public virtual ResponseMsgType MsgType
		{
			get { return ResponseMsgType.Transfer_Customer_Service; }
		}

		public List<CustomerServiceAccount> TransInfo { get; set; }
	}

	public class CustomerServiceAccount
	{
		public string KfAccount { get; set; }
	}
 }