using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections;

namespace Senparc.Weixin.MP.TenPayLib
{
    public class CheckRequestHandler : ClientRequestHandler
    {
        /// <summary>
        /// 对账单下载接口
        /// </summary>
        /// <param name="httpContext"></param>
        public CheckRequestHandler(HttpContext httpContext)
            : base(httpContext)
        {

            this.SetGateUrl("http://mch.tenpay.com/cgi-bin/mchdown_real_new.cgi");
        }



        protected override void CreateSign()
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList();
            akeys.Add("spid");
            akeys.Add("trans_time");
            akeys.Add("stamp");
            akeys.Add("cft_signtype");
            akeys.Add("mchtype");


            foreach (string k in akeys)
            {
                string v = (string)Parameters[k];
                if (null != v && "".CompareTo(v) != 0
                    && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append("key=" + this.GetKey());
            string sign = MD5Util.GetMD5(sb.ToString(), GetCharset()).ToLower();

            this.SetParameter("sign", sign);

            //debug信息
            this.SetDebugInfo(sb.ToString() + " => sign:" + sign);
        }
    }
}
