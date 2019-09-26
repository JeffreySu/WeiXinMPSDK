/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MenuController.cs
    文件功能描述：自定义菜单设置工具Controller
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.CO2NET.HttpUtility;

namespace Senparc.Weixin.MP.CoreSample.Controllers
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
            try
            {
                if (!string.IsNullOrEmpty(IP))
                {
                    return IP;
                }

                var url =
                    "https://www.baidu.com/s?ie=utf-8&f=8&rsv_bp=0&rsv_idx=1&tn=baidu&wd=IP&rsv_pq=db4eb7d40002dd86&rsv_t=14d7uOUvNnTdrhnrUx0zdEVTPEN8XDq4aH7KkoHAEpTIXkRQkUD00KJ2p94&rqlang=cn&rsv_enter=1&rsv_sug3=2&rsv_sug1=2&rsv_sug7=100&rsv_sug2=0&inputT=875&rsv_sug4=875";

                var htmlContent = RequestUtility.HttpGet(url, cookieContainer: null);
                var result = Regex.Match(htmlContent, @"(?<=本机IP:[^\d+]*)(\d+\.\d+\.\d+\.\d+)(?=</span>)");
                if (result.Success)
                {
                    IP = result.Value;
                }
                return IP;
            }
            catch
            {
                return null;
            }
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
                return Json(result, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
            catch (ErrorJsonResultException ex)
            {
                return Json(new { error = "API 调用发生错误：{0}".FormatWith(ex.JsonResult.ToJson()) }, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
            catch (Exception ex)
            {
                return Json(new { error = "执行过程发生错误：{0}".FormatWith(ex.Message) }, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
        }

        [HttpPost]
        public ActionResult CreateMenu(string token, GetMenuResultFull resultFull, MenuMatchRule menuMatchRule)
        {
            var useAddCondidionalApi = menuMatchRule != null && !menuMatchRule.CheckAllNull();
            var apiName = string.Format("使用接口：{0}。", (useAddCondidionalApi ? "个性化菜单接口" : "普通自定义菜单接口"));
            try
            {
                if (token.IsNullOrEmpty())
                {
                    throw new WeixinException("Token不能为空！");
                }

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
                return Json(json, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = string.Format("更新失败：{0}。{1}", ex.Message, apiName) };
                return Json(json, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
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
                return Json(json, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = string.Format("更新失败：{0}。{1}", ex.Message, apiName) };
                return Json(json, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
        }

        public ActionResult GetMenu(string token)
        {
            try
            {
                var result = CommonAPIs.CommonApi.GetMenu(token);
                if (result == null)
                {
                    return Json(new { error = "菜单不存在或验证失败！" }, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
                }
                return Json(result, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
            catch (WeixinMenuException ex)
            {
                return Json(new { error = "菜单不存在或验证失败：" + ex.Message }, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
            catch (Exception ex)
            {
                return Json(new { error = "菜单不存在或验证失败：" + ex.Message }, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
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
                return Json(json, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
            catch (Exception ex)
            {
                var json = new { Success = false, Message = ex.Message };
                return Json(json, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver() });
            }
        }
    }
}
