/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：MenuController.cs
    文件功能描述：自定义菜单设置工具Controller
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class MenuController : BaseController
    {
        #region 获取IP
        private static string IP { get; set; }
        
        /// <summary>
        /// 获得当前服务器外网IP
        /// </summary>
        private string GetIP()
        {
            if (!string.IsNullOrEmpty(IP))
            {
                return IP;
            }

            Process cmd = new Process();
            cmd.StartInfo.FileName = "ipconfig.exe";//设置程序名 
            cmd.StartInfo.Arguments = "/all"; //参数 
                                              //重定向标准输出 
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;//不显示窗口（控制台程序是黑屏） 
                                                //cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//暂时不明白什么意思 
                                                /* 
                                                收集一下 有备无患 
                                                关于:ProcessWindowStyle.Hidden隐藏后如何再显示？ 
                                                hwndWin32Host = Win32Native.FindWindow(null, win32Exinfo.windowsName); 
                                                Win32Native.ShowWindow(hwndWin32Host, 1); //先FindWindow找到窗口后再ShowWindow 
                                                */
            cmd.Start();
            string info = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
            cmd.Close();
            return info;
        }
        #endregion


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

            //获取服务器外网IP
            ViewData["IP"] = GetIP() ?? "使用CMD命令ping sdk.weixin.senparc.com";

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
            catch (Exception ex)
            {
                //TODO:为简化代码，这里不处理异常（如Token过期）
                return Json(new { error = "执行过程发生错误！" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateMenu(string token, GetMenuResultFull resultFull, MenuMatchRule menuMatchRule)
        {
            var useAddCondidionalApi = menuMatchRule != null && !menuMatchRule.CheckAllNull();
            var apiName = string.Format("使用接口：{0}。", (useAddCondidionalApi ? "个性化菜单接口" : "普通自定义菜单接口"));
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
                    buttonGroup = CommonAPIs.CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
                    result = CommonAPIs.CommonApi.CreateMenu(token, buttonGroup);
                }

                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = "菜单更新成功。" + apiName
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = string.Format("更新失败：{0}。{1}", ex.Message, apiName) };
                return Json(json);
            }
        }

        [HttpPost]
        public ActionResult CreateMenuFromJson(string token, string fullJson)
        {
            //TODO:根据"conditionalmenu"判断自定义菜单

            var apiName = "使用JSON更新";
            try
            {
                GetMenuResultFull resultFull = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMenuResultFull>(fullJson);

                //重新整理按钮信息
                WxJsonResult result = null;
                IButtonGroupBase buttonGroup = null;

                buttonGroup = CommonAPIs.CommonApi.GetMenuFromJsonResult(resultFull, new ButtonGroup()).menu;
                result = CommonAPIs.CommonApi.CreateMenu(token, buttonGroup);

                var json = new
                {
                    Success = result.errmsg == "ok",
                    Message = "菜单更新成功。" + apiName
                };
                return Json(json);
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = string.Format("更新失败：{0}。{1}", ex.Message, apiName) };
                return Json(json);
            }
        }

        public ActionResult GetMenu(string token)
        {
            try
            {
                var result = CommonAPIs.CommonApi.GetMenu(token);
                if (result == null)
                {
                    return Json(new { error = "菜单不存在或验证失败！" }, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (WeixinMenuException ex)
            {
                return Json(new { error = "菜单不存在或验证失败：" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "菜单不存在或验证失败：" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
