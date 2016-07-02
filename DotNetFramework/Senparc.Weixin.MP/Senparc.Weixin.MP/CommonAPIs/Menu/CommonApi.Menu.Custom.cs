/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CommonApi.Menu.Custom.cs
    文件功能描述：通用自定义菜单接口（自定义接口）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 201503232
    修改描述：修改字符串是否为空判断方式（感谢dusdong）
 
    修改标识：Senparc - 20150703
    修改描述：改用accessTokenOrAppId参数

    修改标识：IsaacXu - 20151222
    修改描述：添加CreateMenu重写方法
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/13/43de8269be54a0a6f64413e4dfa94f39.html
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.CommonAPIs
{
    public partial class CommonApi
    {
        ///// <summary>
        ///// 特殊符号转义
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //private static string ButtonNameEncode(string name)
        //{
        //    //直接用UrlEncode不行，显示内容超长
        //    return name.Replace("&", "%26");
        //}

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId。当为AppId时，如果AccessToken错误将自动获取一次。当为null时，获取当前注册的第一个AppId。</param>
        /// <param name="buttonData">菜单内容</param>
        /// <returns></returns>
        public static WxJsonResult CreateMenu(string accessTokenOrAppId, ButtonGroup buttonData, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
             {
                 var urlFormat = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
                 ////对特殊符号进行URL转义
                 //foreach (var button in buttonData.button)
                 //{
                 //    button.name = ButtonNameEncode(button.name);//button.name.UrlEncode();
                 //    if (button is SubButton)
                 //    {
                 //        var subButtonList = button as SubButton;
                 //        foreach (var subButton in subButtonList.sub_button)
                 //        {
                 //            subButton.name = ButtonNameEncode(button.name);//button.name.UrlEncode();
                 //        }
                 //    }
                 //}
                 return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, buttonData, timeOut: timeOut);

             }, accessTokenOrAppId);
        }


        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId。当为AppId时，如果AccessToken错误将自动获取一次。当为null时，获取当前注册的第一个AppId。</param>
        /// <param name="buttonData">菜单内容</param>
        /// <returns></returns>
        public static WxJsonResult CreateMenu(string accessTokenOrAppId, object buttonData, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, buttonData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #region GetMenu
        /// <summary>
        /// 获取单击按钮
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        [Obsolete("配合GetMenuFromJson方法使用")]
        private static SingleClickButton GetSingleButtonFromJsonObject(Dictionary<string, object> objs)
        {
            var sb = new SingleClickButton()
            {
                key = objs["key"] as string,
                name = objs["name"] as string,
                type = objs["type"] as string
            };
            return sb;
        }


        /// <summary>
        /// 从JSON字符串获取菜单对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        [Obsolete("此方法通过判断GetMenuResult并结合object类型转换得到结果。结果准确。但更推荐使用GetMenuFromJsonResult方法。")]
        public static GetMenuResult GetMenuFromJson(string jsonString)
        {
            var finalResult = new GetMenuResult(new ButtonGroup());

            try
            {
                //@"{""menu"":{""button"":[{""type"":""click"",""name"":""单击测试"",""key"":""OneClick"",""sub_button"":[]},{""name"":""二级菜单"",""sub_button"":[{""type"":""click"",""name"":""返回文本"",""key"":""SubClickRoot_Text"",""sub_button"":[]},{""type"":""click"",""name"":""返回图文"",""key"":""SubClickRoot_News"",""sub_button"":[]},{""type"":""click"",""name"":""返回音乐"",""key"":""SubClickRoot_Music"",""sub_button"":[]}]}]}}"
                object jsonResult = null;

                JavaScriptSerializer js = new JavaScriptSerializer();
                jsonResult = js.Deserialize<object>(jsonString);

                var fullResult = jsonResult as Dictionary<string, object>;
                if (fullResult != null && fullResult.ContainsKey("menu"))
                {
                    //得到菜单
                    var menu = fullResult["menu"];
                    var buttons = (menu as Dictionary<string, object>)["button"] as object[];

                    foreach (var rootButton in buttons)
                    {
                        var fullButton = rootButton as Dictionary<string, object>;
                        if (fullButton.ContainsKey("key") && !string.IsNullOrEmpty(fullButton["key"] as string))
                        {
                            //按钮，无下级菜单
                            finalResult.menu.button.Add(GetSingleButtonFromJsonObject(fullButton));
                        }
                        else
                        {
                            //二级菜单
                            var subButton = new SubButton(fullButton["name"] as string);
                            finalResult.menu.button.Add(subButton);
                            foreach (var sb in fullButton["sub_button"] as object[])
                            {
                                subButton.sub_button.Add(GetSingleButtonFromJsonObject(sb as Dictionary<string, object>));
                            }
                        }
                    }
                }
                else if (fullResult != null && fullResult.ContainsKey("errmsg"))
                {
                    //菜单不存在
                    throw new ErrorJsonResultException(fullResult["errmsg"] as string, null, null);
                }
            }
            catch (ErrorJsonResultException ex)
            {
                finalResult = null;

                //如果没有惨淡会返回错误代码：46003：menu no exist
            }
            catch (Exception ex)
            {
                //其他异常
                finalResult = null;
            }
            return finalResult;
        }


        /// <summary>
        /// 获取当前菜单，如果菜单不存在，将返回null
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetMenuResult GetMenu(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken.AsUrlData());

                var jsonString = RequestUtility.HttpGet(url, Encoding.UTF8);
                //var finalResult = GetMenuFromJson(jsonString);

                GetMenuResult finalResult;
                JavaScriptSerializer js = new JavaScriptSerializer();
                try
                {
                    var jsonResult = js.Deserialize<GetMenuResultFull>(jsonString);
                    if (jsonResult.menu == null || jsonResult.menu.button.Count == 0)
                    {
                        throw new WeixinException(jsonResult.errmsg);
                    }

                    finalResult = GetMenuFromJsonResult(jsonResult, new ButtonGroup());
                }
                catch (WeixinException ex)
                {
                    finalResult = null;
                }

                return finalResult;

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static WxJsonResult DeleteMenu(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken.AsUrlData());

                return Get.GetJson<WxJsonResult>(url);

            }, accessTokenOrAppId);

        }
    }
}