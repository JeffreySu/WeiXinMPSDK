using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Business.JsonResult
{
    /// <summary>
    /// code换取用户手机号 返回信息
    /// </summary>
    public class GetUserPhoneNumberJsonResult : WxJsonResult
    {
        public Phone_Info phone_info { get; set; }
    }

    public class Phone_Info
    {
        public string phoneNumber { get; set; }
        public string purePhoneNumber { get; set; }
        public int countryCode { get; set; }
        public Watermark watermark { get; set; }
    }

    public class Watermark
    {
        public int timestamp { get; set; }
        public string appid { get; set; }
    }
}
