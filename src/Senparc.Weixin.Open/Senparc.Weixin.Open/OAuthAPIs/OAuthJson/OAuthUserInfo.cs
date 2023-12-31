/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：OAuthUserInfo.cs
    文件功能描述：通过OAuth的获取到的用户信息
    
    
    创建标识：Senparc - 20150712
    
    修改标识：Varorbc - 20210930
    修改描述：v16.15.500 用户信息调整：不再返回用户性别及地区信息

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.Open.OAuthAPIs
{
    /// <summary>
    /// 通过OAuth的获取到的用户信息（snsapi_userinfo=scope）
    /// </summary>
    [Serializable]
    public class OAuthUserInfo : IUserInfo
    {
        public string openid { get; set; }
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
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// 作者注：其实这个格式称不上JSON，只是个单纯数组。
        /// </summary>
        public string[] privilege { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&lang=zh_CN
        /// </summary>
        public string unionid { get; set; }
    }
}
