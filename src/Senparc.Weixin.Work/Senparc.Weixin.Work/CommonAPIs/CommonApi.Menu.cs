/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：CommonApi.Menu.cs
    文件功能描述：自定义菜单API
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
----------------------------------------------------------------*/

/*
    获取AccessToken API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E8%87%AA%E5%AE%9A%E4%B9%89%E8%8F%9C%E5%8D%95
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Entities.Menu;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.Extensions;

#if NET45
using System.Web.Script.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Senparc.Weixin.Work.CommonAPIs
{
    public partial class CommonApi
    {
        #region 同步方法

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="agentId"></param>
        /// <param name="buttonData">菜单内容</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult CreateMenu(string accessToken, int agentId, ButtonGroup buttonData, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/menu/create?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId);
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
            return CommonJsonSend.Send(null, urlFormat, buttonData, CommonJsonSendType.POST, timeOut);
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
            var finalResult = new GetMenuResult();

            try
            {
                //@"{""menu"":{""button"":[{""type"":""click"",""name"":""单击测试"",""key"":""OneClick"",""sub_button"":[]},{""name"":""二级菜单"",""sub_button"":[{""type"":""click"",""name"":""返回文本"",""key"":""SubClickRoot_Text"",""sub_button"":[]},{""type"":""click"",""name"":""返回图文"",""key"":""SubClickRoot_News"",""sub_button"":[]},{""type"":""click"",""name"":""返回音乐"",""key"":""SubClickRoot_Music"",""sub_button"":[]}]}]}}"
                object jsonResult = null;


#if NET45

#else
                jsonResult = JsonConvert.DeserializeObject<object>(jsonString);
#endif
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
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="agentId">企业应用的id，整型。可在应用的设置页面查看</param>
        /// <returns></returns>
        public static GetMenuResult GetMenu(string accessToken, int agentId)
        {
            var url = string.Format(Config.ApiWorkHost + "/cgi-bin/menu/get?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId);

            var jsonString = RequestUtility.HttpGet(url, Encoding.UTF8);
            //var finalResult = GetMenuFromJson(jsonString);

            GetMenuResult finalResult;

            try
            {
#if NET45
                JavaScriptSerializer js = new JavaScriptSerializer();
                var jsonResult = js.Deserialize<GetMenuResultFull>(jsonString);
#else
                var jsonResult = JsonConvert.DeserializeObject<GetMenuResultFull>(jsonString);
#endif
                if (jsonResult.button == null || jsonResult.button.Count == 0)
                {
                    throw new WeixinException(jsonResult.errmsg);
                }

                finalResult = GetMenuFromJsonResult(jsonResult);
            }
            catch (WeixinException ex)
            {
                finalResult = null;
            }

            return finalResult;
        }

        /// <summary>
        /// 根据微信返回的Json数据得到可用的GetMenuResult结果
        /// </summary>
        /// <param name="resultFull"></param>
        /// <returns></returns>
        public static GetMenuResult GetMenuFromJsonResult(GetMenuResultFull resultFull)
        {
            GetMenuResult result = null;
            try
            {
                //重新整理按钮信息
                ButtonGroup bg = new ButtonGroup();
                foreach (var rootButton in resultFull.button)
                {
                    if (rootButton.name == null)
                    {
                        continue;//没有设置一级菜单
                    }
                    var availableSubButton = rootButton.sub_button.Count(z => !string.IsNullOrEmpty(z.name));//可用二级菜单按钮数量
                    if (availableSubButton == 0)
                    {
                        //底部单击按钮
                        if (rootButton.type == null ||
                            (rootButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase)
                            && string.IsNullOrEmpty(rootButton.key)))
                        {
                            throw new WeixinMenuException("单击按钮的key不能为空！");
                        }

                        if (rootButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase))
                        {
                            //点击
                            bg.button.Add(new SingleClickButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
                        else if (rootButton.type.Equals("VIEW", StringComparison.OrdinalIgnoreCase))
                        {
                            //URL
                            bg.button.Add(new SingleViewButton()
                            {
                                name = rootButton.name,
                                url = rootButton.url,
                                type = rootButton.type
                            });
                        }
                        else if (rootButton.type.Equals("LOCATION_SELECT", StringComparison.OrdinalIgnoreCase))
                        {
                            //弹出地理位置选择器
                            bg.button.Add(new SingleLocationSelectButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
                        else if (rootButton.type.Equals("PIC_PHOTO_OR_ALBUM", StringComparison.OrdinalIgnoreCase))
                        {
                            //弹出拍照或者相册发图
                            bg.button.Add(new SinglePicPhotoOrAlbumButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
                        else if (rootButton.type.Equals("PIC_SYSPHOTO", StringComparison.OrdinalIgnoreCase))
                        {
                            //弹出系统拍照发图
                            bg.button.Add(new SinglePicSysphotoButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
                        else if (rootButton.type.Equals("PIC_WEIXIN", StringComparison.OrdinalIgnoreCase))
                        {
                            //弹出微信相册发图器
                            bg.button.Add(new SinglePicWeixinButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
                        else if (rootButton.type.Equals("SCANCODE_PUSH", StringComparison.OrdinalIgnoreCase))
                        {
                            //扫码推事件
                            bg.button.Add(new SingleScancodePushButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
                        else
                        {
                            //扫码推事件且弹出“消息接收中”提示框
                            bg.button.Add(new SingleScancodeWaitmsgButton()
                            {
                                name = rootButton.name,
                                key = rootButton.key,
                                type = rootButton.type
                            });
                        }
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

                            if (subSubButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase)
                                && string.IsNullOrEmpty(subSubButton.key))
                            {
                                throw new WeixinMenuException("单击按钮的key不能为空！");
                            }


                            if (subSubButton.type.Equals("CLICK", StringComparison.OrdinalIgnoreCase))
                            {
                                //点击
                                subButton.sub_button.Add(new SingleClickButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                            else if (subSubButton.type.Equals("VIEW", StringComparison.OrdinalIgnoreCase))
                            {
                                //URL
                                subButton.sub_button.Add(new SingleViewButton()
                                {
                                    name = subSubButton.name,
                                    url = subSubButton.url,
                                    type = subSubButton.type
                                });
                            }
                            else if (subSubButton.type.Equals("LOCATION_SELECT", StringComparison.OrdinalIgnoreCase))
                            {
                                //弹出地理位置选择器
                                subButton.sub_button.Add(new SingleLocationSelectButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                            else if (subSubButton.type.Equals("PIC_PHOTO_OR_ALBUM", StringComparison.OrdinalIgnoreCase))
                            {
                                //弹出拍照或者相册发图
                                subButton.sub_button.Add(new SinglePicPhotoOrAlbumButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                            else if (subSubButton.type.Equals("PIC_SYSPHOTO", StringComparison.OrdinalIgnoreCase))
                            {
                                //弹出系统拍照发图
                                subButton.sub_button.Add(new SinglePicSysphotoButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                            else if (subSubButton.type.Equals("PIC_WEIXIN", StringComparison.OrdinalIgnoreCase))
                            {
                                //弹出微信相册发图器
                                subButton.sub_button.Add(new SinglePicWeixinButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                            else if (subSubButton.type.Equals("SCANCODE_PUSH", StringComparison.OrdinalIgnoreCase))
                            {
                                //扫码推事件
                                subButton.sub_button.Add(new SingleScancodePushButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                            else
                            {
                                //扫码推事件且弹出“消息接收中”提示框
                                subButton.sub_button.Add(new SingleScancodeWaitmsgButton()
                                {
                                    name = subSubButton.name,
                                    key = subSubButton.key,
                                    type = subSubButton.type
                                });
                            }
                        }
                    }
                }

                result = new GetMenuResult()
                {
                    menu = bg
                };
            }
            catch (Exception ex)
            {
                throw new WeixinMenuException(ex.Message, ex);
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="agentId">企业应用的id，整型。可在应用的设置页面查看</param>
        /// <returns></returns>
        public static WorkJsonResult DeleteMenu(string accessToken, int agentId)
        {
            var url = string.Format(Config.ApiWorkHost + "/cgi-bin/menu/delete?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId);
            var result = Senparc.Weixin.HttpUtility.Get.GetJson<WorkJsonResult>(url);
            return result;
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】删除菜单
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="agentId">企业应用的id，整型。可在应用的设置页面查看</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteMenuAsync(string accessToken, int agentId)
        {
            var url = string.Format(Config.ApiWorkHost + "/cgi-bin/menu/delete?access_token={0}&agentid={1}", accessToken.AsUrlData(), agentId);
            var result = await Senparc.Weixin.HttpUtility.Get.GetJsonAsync<WorkJsonResult>(url);
            return result;
        }
        #endregion
#endif
    }
}
