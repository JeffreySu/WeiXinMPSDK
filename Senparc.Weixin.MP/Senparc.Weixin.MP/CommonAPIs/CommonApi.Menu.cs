using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    public partial class CommonApi
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="buttonData">菜单内容</param>
        /// <returns></returns>
        public static WxJsonResult CreateMenu(string accessToken, ButtonGroup buttonData)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonString = js.Serialize(buttonData);
            CookieContainer cookieContainer = null;// new CookieContainer();

            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);

                var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
                var result = Post.PostGetJson<WxJsonResult>(url, cookieContainer, ms);
                return result;
            }
        }

        #region GetMenu
        /// <summary>
        /// 获取单击按钮
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        private static SingleButton GetSingleButtonFromJsonObject(Dictionary<string, object> objs)
        {
            var sb = new SingleButton()
            {
                key = objs["key"] as string,
                name = objs["name"] as string,
                type = objs["type"] as string
            };
            return sb;
        }

        /// <summary>
        /// 获取当前菜单，暂时只返回基类信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetMenuResult GetMenu(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);

            var jsonString = HttpUtility.RequestUtility.HttpGet(url, Encoding.UTF8);
            var finalResult = GetMenuFromJson(jsonString);
            return finalResult;
        }

        /// <summary>
        /// 从JSON字符串获取菜单对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static GetMenuResult GetMenuFromJson(string jsonString)
        {
            var finalResult = new GetMenuResult();

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
                        if (fullButton.ContainsKey("key"))
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

        #endregion


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static WxJsonResult DeleteMenu(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);
            var result = Get.GetJson<WxJsonResult>(url);
            return result;
        }
    }
}
