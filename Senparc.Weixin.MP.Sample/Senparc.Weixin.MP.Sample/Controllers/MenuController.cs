using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class MenuController : BaseController
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

        [HttpPost]
        public ActionResult CreateMenu(string token, MenuPost menuPost)
        {
            try
            {
                //重新整理按钮信息
                ButtonGroup bg = new ButtonGroup();
                foreach (var rootButton in menuPost.menu.button)
                {
                    if (rootButton.name == null)
                    {
                        continue;//没有设置一级菜单
                    }

                    if (rootButton.sub_button.Count==0)
                    {
                        //底部单击按钮
                        if (string.IsNullOrEmpty(rootButton.key))
                        {
                            throw new Exception("单击按钮的key不能为空！");
                        }

                        bg.button.Add(new SingleButton()
                                          {
                                              name = rootButton.name,
                                              key = rootButton.key,
                                              type = rootButton.type//目前只有click
                                          });
                    }
                    else if(rootButton.sub_button.Count<2)
                    {
                        throw new Exception("子菜单至少需要填写2个！");
                    }
                    else
                    {
                        //底部二级菜单
                        var subButton = new SubButton(rootButton.name);
                        bg.button.Add(subButton);

                        foreach (var subSubButton in rootButton.sub_button)
                        {
                            if (subSubButton.name == null)
                            {
                                continue; //没有设置菜单
                            }

                            if (string.IsNullOrEmpty(subSubButton.key))
                            {
                                throw new Exception("单击按钮的key不能为空！");
                            }

                            subButton.sub_button.Add(new SingleButton()
                                                         {
                                                             name = subSubButton.name,
                                                             key = subSubButton.key,
                                                             type = subSubButton.type
                                                             //目前只有click                                                       });
                                                         });
                        }
                    }
                }

                var result = CommonAPIs.CommonApi.CreateMenu(token, bg);
                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = result.errmsg
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = ex.Message };
                return Json(json);
            }
        }

        public ActionResult GetMenu(string token)
        {
            var result = CommonAPIs.CommonApi.GetMenu(token);
            if (result == null)
            {
                return Json(new { error = "菜单不存在或验证失败！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
