using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.NetCore3.Controllers.WxOpen
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        #region GetUrlScheme
        public async Task<ActionResult> GetUrlScheme(int tickid, string ntype = "gclub")
        {
            if (!HttpContext.Request.IsLocal())
            {
                return Content("此接口为内部接口，请在服务器本地调用！");
            }

            Hashtable ht = new System.Collections.Hashtable();
            var weixinAppId = Senparc.Weixin.Config.SenparcWeixinSetting.MpSetting.WeixinAppId;
            var jumpWxa = new Weixin.WxOpen.AdvancedAPIs.UrlScheme.GenerateSchemeJumpWxa("", null);
            var schmeResult = await Senparc.Weixin.WxOpen.AdvancedAPIs.UrlSchemeApi.GenerateSchemeAsync(weixinAppId, jumpWxa, false, null);
            ht.Add("data", schmeResult);

            return Content(ht.ToString());
        }

        //public GenerateSchemeResult GetGenerateScheme(string accessToken, string path = "", int timeOut = 3000)
        //{
        //    string urlFormat = "https://api.weixin.qq.com/wxa/generatescheme?access_token=";
        //    var url = urlFormat + accessToken;

        //    GenerateSchemeParam data = new GenerateSchemeParam();
        //    data.jump_wxa = new jump_wxaParam()
        //    {
        //        path = path,
        //        query = ""
        //    };
        //    data.is_expire = false;
        //    data.expire_time = Senparc.CO2NET.Helpers.DateTimeHelper.GetUnixDateTime(DateTime.Now.AddYears(10));

        //    return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GenerateSchemeResult>(accessToken, url, data, timeOut: timeOut);
        //}
        #endregion
    }

    //public class GenerateSchemeParam
    //{
    //    /// <summary>
    //    /// 跳转到的目标小程序信息。
    //    /// </summary>
    //    public jump_wxaParam jump_wxa { get; set; }

    //    /// <summary>
    //    /// 生成的scheme码类型，到期失效：true，永久有效：false
    //    /// </summary>
    //    public bool is_expire { get; set; } = false;
    //    public long expire_time { get; set; }
    //}

    //public class jump_wxaParam
    //{
    //    /// <summary>
    //    /// 通过scheme码进入的小程序页面路径，必须是已经发布的小程序存在的页面，path为空时会跳转小程序主页。
    //    /// </summary>
    //    public string path { get; set; }
    //    /// <summary>
    //    /// 通过scheme码进入小程序时的query，最大32个字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~
    //    /// </summary>
    //    public string query { get; set; }
    //}

    //public class GenerateSchemeResult : WxJsonResult
    //{
    //    /// <summary>
    //    /// 小程序scheme码
    //    /// </summary>
    //    public string openlink { get; set; }
    //}
}