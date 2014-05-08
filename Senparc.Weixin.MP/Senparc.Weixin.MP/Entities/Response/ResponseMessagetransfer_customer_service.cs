using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
	public class ResponseMessagetransfer_customer_service : ResponseMessageBase, IResponseMessageBase
	{
		new public virtual ResponseMsgType MsgType
		{
			get { return ResponseMsgType.transfer_customer_service; }
		}
	}
}