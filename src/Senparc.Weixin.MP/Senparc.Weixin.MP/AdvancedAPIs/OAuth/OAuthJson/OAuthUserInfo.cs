#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：OAuthUserInfo.cs
    文件功能描述：通过OAuth的获取到的用户信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Varorbc - 20210930
    修改描述：v16.15.500 用户信息调整：不再返回用户性别及地区信息

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.MP.AdvancedAPIs.OAuth
{
    /// <summary>
    /// 通过OAuth的获取到的用户信息（snsapi_userinfo=scope）
    /// </summary>
    public class OAuthUserInfo : IUserInfo
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int subscribe { get; set; }
        /// <summary>
        /// 用户的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        [Obsolete("https://mp.weixin.qq.com/cgi-bin/announce?action=getannouncement&announce_id=11632637451RQs8y")]
        public int sex { get; set; }
        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        [Obsolete("https://mp.weixin.qq.com/cgi-bin/announce?action=getannouncement&announce_id=11632637451RQs8y")]
        public string province { get; set; }
        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        [Obsolete("https://mp.weixin.qq.com/cgi-bin/announce?action=getannouncement&announce_id=11632637451RQs8y")]
        public string city { get; set; }
        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        [Obsolete("https://mp.weixin.qq.com/cgi-bin/announce?action=getannouncement&announce_id=11632637451RQs8y")]
        public string country { get; set; }
        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// 作者注：其实这个格式称不上JSON，只是个单纯数组。
        /// </summary>
        public string[] privilege { get; set; }
        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public long subscribe_time { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：<see href="https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&amp;t=resource/res_list&amp;verify=1&amp;lang=zh_CN"/> 
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 用户所在的分组ID（兼容旧的用户分组接口）
        /// </summary>
        public string groupid { get; set; }
        /// <summary>
        /// 用户被打上的标签ID列表
        /// </summary>
        public int[] tagid_list { get; set; }
        /// <summary>
        /// 返回用户关注的渠道来源，ADD_SCENE_SEARCH 公众号搜索，ADD_SCENE_ACCOUNT_MIGRATION 公众号迁移，ADD_SCENE_PROFILE_CARD 名片分享，ADD_SCENE_QR_CODE 扫描二维码，ADD_SCENE_PROFILE_LINK 图文页内名称点击，ADD_SCENE_PROFILE_ITEM 图文页右上角菜单，ADD_SCENE_PAID 支付后关注，ADD_SCENE_WECHAT_ADVERTISEMENT 微信广告，ADD_SCENE_OTHERS 其他
        /// </summary>
        public string subscribe_scene { get; set; }
        /// <summary>
        /// 二维码扫码场景（开发者自定义）
        /// </summary>
        public string qr_scene { get; set; }
        /// <summary>
        /// 二维码扫码场景描述（开发者自定义）
        /// </summary>
        public string qr_scene_str { get; set; }
    }
}
