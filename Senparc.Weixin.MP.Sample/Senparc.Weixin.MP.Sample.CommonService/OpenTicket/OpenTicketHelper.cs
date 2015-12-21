using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;

namespace Senparc.Weixin.MP.Sample.CommonService.OpenTicket
{
    /// <summary>
    /// OpenTicket即ComponentVerifyTicket
    /// </summary>
    public class OpenTicketHelper
    {
        public static string GetOpenTicket(string componentAppId)
        {
            //实际开发过程不一定要用文件记录，也可以用数据库。
            var openTicketPath = Server.GetMapPath("~/App_Data/OpenTicket");
            string openTicket = null;
            var filePath = Path.Combine(openTicketPath, string.Format("{0}.txt", componentAppId));
            if (File.Exists(filePath))
            {
                using (TextReader tr = new StreamReader(filePath))
                {
                    openTicket = tr.ReadToEnd();
                }
            }
            else
            {
                throw new WeixinException("OpenTicket不存在！");
            }

            //其他逻辑

            return openTicket;
        }
    }
}
