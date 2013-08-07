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
                for (int j = 0; j < 6; j++)
                {
                    var singleButton = new SingleButton();
                    subButton.sub_button.Add(singleButton);
                }
            }

            return View(reslt);
        }
    }
}
