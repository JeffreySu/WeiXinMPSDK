using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            GetMenuResult reslt = new GetMenuResult();

            //初始化
            for (int i = 0; i < 3; i++)
            {
                var subButton = new SubButton();
                for (int j = 0; j < 5; j++)
                {
                    var singleButton = new SingleButton();
                    subButton.sub_button.Add(singleButton);
                }
            }

            return View(reslt);
        }

        public ActionResult GetToken(string appId, string appSecret)
        {
            try
            {
                var result = CommonAPIs.CommonApi.GetToken(appId, appSecret);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //TODO:为简化代码，这里不处理异常（如Token过期）
                return Json(new { error = "执行过程发生错误！" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetMenu(string token)
        {
            var result = CommonAPIs.CommonApi.GetMenu(token);
            if (result == null)
            {
                return Json(new { error = "没有菜单信息！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
