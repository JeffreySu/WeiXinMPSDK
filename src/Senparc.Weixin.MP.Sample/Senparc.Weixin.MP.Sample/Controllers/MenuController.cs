/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：MenuController.cs
    文件功能描述：自定义菜单设置工具Controller
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;
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
            GetMenuResult result = new GetMenuResult(new ButtonGroup());

            //初始化
            for (int i = 0; i < 3; i++)
            {
                var subButton = new SubButton();
                for (int j = 0; j < 5; j++)
                {
                    var singleButton = new SingleClickButton();
                    subButton.sub_button.Add(singleButton);
                }
            }

            return View(result);
        }

        public ActionResult GetToken(string appId, string appSecret)
        {
            try
            {
                //if (!AccessTokenContainer.CheckRegistered(appId))
                //{
                //    AccessTokenContainer.Register(appId, appSecret);
                //}
                var result = CommonAPIs.CommonApi.GetToken(appId, appSecret);//AccessTokenContainer.GetTokenResult(appId);

                //也可以直接一步到位：
                //var result = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                //TODO:为简化代码，这里不处理异常（如Token过期）
                return Json(new { error = "执行过程发生错误！" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateMenu(string token, GetMenuResultFull resultFull, MenuMatchRule menuMatchRule)
        {
                var useAddCondidionalApi = menuMatchRule != null && !menuMatchRule.CheckAllNull();
            var apiName = string.Format("使用接口：{0}。" , (useAddCondidionalApi ? "个性化菜单接口" : "普通自定义菜单接口"));
            try
            {
                //重新整理按钮信息
                WxJsonResult result = null;
                IButtonGroupBase buttonGroup = null;
                if (useAddCondidionalApi)
                {
                    //个性化接口
                    buttonGroup = CommonAPIs.CommonApi.GetMenuFromJsonResult(resultFull, new ConditionalButtonGroup()).menu;

                    var addConditionalButtonGroup = buttonGroup as ConditionalButtonGroup;
                    addConditionalButtonGroup.matchrule = menuMatchRule;
                    result = CommonAPIs.CommonApi.CreateMenuConditional(token, addConditionalButtonGroup);
                    apiName += string.Format("menuid：{0}。", (result as CreateMenuConditionalResult).menuid);
                }
                else
                {
                    //普通接口
                    buttonGroup = CommonAPIs.CommonApi.GetMenuFromJsonResult(resultFull,new ButtonGroup()).menu;
                    result = CommonAPIs.CommonApi.CreateMenu(token, buttonGroup);
                }

                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = "菜单更新成功。"+ apiName
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message =string.Format("更新失败：{0}。{1}",ex.Message, apiName) };
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

        public ActionResult DeleteMenu(string token)
        {
            try
            {
                var result = CommonAPIs.CommonApi.DeleteMenu(token);
                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = result.errmsg
                };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = ex.Message };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
