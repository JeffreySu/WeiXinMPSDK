using Senparc.Weixin.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        #region GetUrlScheme
        public ActionResult GetUrlScheme(int tickid, string ntype = "gclub")
        {
            Hashtable ht = new System.Collections.Hashtable();
            var curtoken = Senparc.Weixin.MP.Containers.AccessTokenContainer.TryGetAccessToken("当前小程序APPID", "当前小程序秘钥");
            ht.Add("data", GetGenerateScheme(curtoken));
            return Content(ht.ToString());
        }

        public GenerateSchemeResult GetGenerateScheme(string accessToken, string path = "", int timeOut = 3000)
        {
            string urlFormat = "https://api.weixin.qq.com/wxa/generatescheme?access_token=";
            var url = urlFormat + accessToken;

            GenerateSchemeParam data = new GenerateSchemeParam();
            data.jump_wxa = new jump_wxaParam()
            {
                path = path,
                query = ""
            };
            data.is_expire = false;
            data.expire_time = Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime(DateTime.Now.AddYears(10));

            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GenerateSchemeResult>(accessToken, url, data, timeOut: timeOut);
        }
        #endregion
    }

    public class GenerateSchemeParam
    {
        /// <summary>
        /// 跳转到的目标小程序信息。
        /// </summary>
        public jump_wxaParam jump_wxa { get; set; }

        /// <summary>
        /// 生成的scheme码类型，到期失效：true，永久有效：false
        /// </summary>
        public bool is_expire { get; set; } = false;
        public long expire_time { get; set; }
    }

    public class jump_wxaParam
    {
        /// <summary>
        /// 通过scheme码进入的小程序页面路径，必须是已经发布的小程序存在的页面，path为空时会跳转小程序主页。
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 通过scheme码进入小程序时的query，最大32个字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~
        /// </summary>
        public string query { get; set; }
    }

    public class GenerateSchemeResult : WxJsonResult
    {
        /// <summary>
        /// 小程序scheme码
        /// </summary>
        public string openlink { get; set; }
    }
}