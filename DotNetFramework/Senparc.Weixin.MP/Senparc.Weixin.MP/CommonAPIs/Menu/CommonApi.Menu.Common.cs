/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CommonApi.Menu.Common.cs
    文件功能描述：通用自定义菜单接口（公共方法）
    
    
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
        /// <summary>
        /// 从rootButtonList获取buttonGroup
        /// </summary>
        /// <param name="rootButtonList"></param>
        /// <param name="buttonGroup"></param>
        private static void GetButtonGroup(List<MenuFull_RootButton> rootButtonList, ButtonGroupBase buttonGroup)
        {
            foreach (var rootButton in rootButtonList)
            {
                if (string.IsNullOrEmpty(rootButton.name))
                {
                    continue; //没有设置一级菜单
                }
                var availableSubButton = rootButton.sub_button == null
                    ? 0
                    : rootButton.sub_button.Count(z => !string.IsNullOrEmpty(z.name)); //可用二级菜单按钮数量
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
                        buttonGroup.button.Add(new SingleClickButton()
                        {
                            name = rootButton.name,
                            key = rootButton.key,
                            type = rootButton.type
                        });
                    }
                    else if (rootButton.type.Equals("VIEW", StringComparison.OrdinalIgnoreCase))
                    {
                        //URL
                        buttonGroup.button.Add(new SingleViewButton()
                        {
                            name = rootButton.name,
                            url = rootButton.url,
                            type = rootButton.type
                        });
                    }
                    else if (rootButton.type.Equals("LOCATION_SELECT", StringComparison.OrdinalIgnoreCase))
                    {
                        //弹出地理位置选择器
                        buttonGroup.button.Add(new SingleLocationSelectButton()
                        {
                            name = rootButton.name,
                            key = rootButton.key,
                            type = rootButton.type
                        });
                    }
                    else if (rootButton.type.Equals("PIC_PHOTO_OR_ALBUM", StringComparison.OrdinalIgnoreCase))
                    {
                        //弹出拍照或者相册发图
                        buttonGroup.button.Add(new SinglePicPhotoOrAlbumButton()
                        {
                            name = rootButton.name,
                            key = rootButton.key,
                            type = rootButton.type
                        });
                    }
                    else if (rootButton.type.Equals("PIC_SYSPHOTO", StringComparison.OrdinalIgnoreCase))
                    {
                        //弹出系统拍照发图
                        buttonGroup.button.Add(new SinglePicSysphotoButton()
                        {
                            name = rootButton.name,
                            key = rootButton.key,
                            type = rootButton.type
                        });
                    }
                    else if (rootButton.type.Equals("PIC_WEIXIN", StringComparison.OrdinalIgnoreCase))
                    {
                        //弹出微信相册发图器
                        buttonGroup.button.Add(new SinglePicWeixinButton()
                        {
                            name = rootButton.name,
                            key = rootButton.key,
                            type = rootButton.type
                        });
                    }
                    else if (rootButton.type.Equals("SCANCODE_PUSH", StringComparison.OrdinalIgnoreCase))
                    {
                        //扫码推事件
                        buttonGroup.button.Add(new SingleScancodePushButton()
                        {
                            name = rootButton.name,
                            key = rootButton.key,
                            type = rootButton.type
                        });
                    }
                    else
                    {
                        //扫码推事件且弹出“消息接收中”提示框
                        buttonGroup.button.Add(new SingleScancodeWaitmsgButton()
                        {
                            name = rootButton.name,
                            key = rootButton.key,
                            type = rootButton.type
                        });
                    }
                }
                //else if (availableSubButton < 2)
                //{
                //    throw new WeixinMenuException("子菜单至少需要填写2个！");
                //}
                else
                {
                    //底部二级菜单
                    var subButton = new SubButton(rootButton.name);
                    buttonGroup.button.Add(subButton);

                    foreach (var subSubButton in rootButton.sub_button)
                    {
                        if (string.IsNullOrEmpty(subSubButton.name))
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
        }

        /// <summary>
        /// 根据微信返回的Json数据得到可用的GetMenuResult结果
        /// </summary>
        /// <param name="resultFull"></param>
        /// <param name="buttonGroupBase">ButtonGroupBase的衍生类型，可以为ButtonGroup或ConditionalButtonGroup。返回的GetMenuResult中的menu属性即为此示例。</param>
        /// <returns></returns>
        public static GetMenuResult GetMenuFromJsonResult(GetMenuResultFull resultFull, ButtonGroupBase buttonGroupBase)
        {
            GetMenuResult result = null;
            if (buttonGroupBase == null)
            {
                throw new ArgumentNullException("buttonGroupBase不可以为空！");
            }

            try
            {
                //重新整理按钮信息
                ButtonGroupBase buttonGroup = buttonGroupBase; // ?? new ButtonGroup();
                var rootButtonList = resultFull.menu.button;

                GetButtonGroup(rootButtonList, buttonGroup);//设置默认菜单
                result = new GetMenuResult(buttonGroupBase)
                {
                    menu = buttonGroup,
                    //conditionalmenu = resultFull.conditionalmenu
                };

                //设置个性化菜单列表
                if (resultFull.conditionalmenu!=null)
                {
                    var conditionalMenuList = new List<ConditionalButtonGroup>();
                    foreach (var conditionalMenu in resultFull.conditionalmenu)
                    {
                        var conditionalButtonGroup = new ConditionalButtonGroup()
                            ;
                        //fix bug 16030701  https://github.com/JeffreySu/WeiXinMPSDK/issues/169
                        conditionalButtonGroup.matchrule = conditionalMenu.matchrule;
                        conditionalButtonGroup.menuid = conditionalMenu.menuid;
                        //fix bug 16030701 end

                        GetButtonGroup(conditionalMenu.button, conditionalButtonGroup);//设置默认菜单
                        conditionalMenuList.Add(conditionalButtonGroup);
                    }
                    result.conditionalmenu = conditionalMenuList;
                }
            }
            catch (Exception ex)
            {
                throw new WeixinMenuException(ex.Message, ex);
            }
            return result;
        }
    }
}