/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：WeixinUserInfoResult.cs
    文件功能描述：获取用户信息返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class WeixinUserInfoResult
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，拉取不到其余信息
        /// </summary>
        public int subscribe { get; set; }
        /// <summary>
        /// 普通用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 普通用户的昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 普通用户的头像链接
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 普通用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }

        /// <summary>
        ///  用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex { get; set; }

        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }

        /// <summary>
        ///  用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public long subscribe_time { get; set; }

        public DateTime GetSubscribeTime()
        {
            return DateTimeHelper.GetDateTimeFromXml(subscribe_time);
        }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string unionid { get; set; }
    }
}
