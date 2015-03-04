/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GroupsAPI.cs
    文件功能描述：用户组接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/0/56d992c605a97245eb7e617854b169fc.html
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 用户组接口
    /// </summary>
    public static class GroupsApi
    {

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <returns></returns>
        public static CreateGroupResult Create(string accessToken, string name)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";
            var data = new
            {
                group = new
                {
                    name = name
                }
            };
            return CommonJsonSend.Send<CreateGroupResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GroupsJson Get(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";
            var url = string.Format(urlFormat, accessToken);
            return HttpUtility.Get.GetJson<GroupsJson>(url);
        }

        /// <summary>
        /// 获取用户分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static GetGroupIdResult GetId(string accessToken, string openId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}";
            var data = new { openid = openId };
            return CommonJsonSend.Send<GetGroupIdResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="id"></param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <returns></returns>
        public static WxJsonResult Update(string accessToken, int id, string name)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}";
            var data = new
            {
                group = new
                {
                    id = id,
                    name = name
                }
            };
            return CommonJsonSend.Send(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="toGroupId"></param>
        /// <returns></returns>
        public static WxJsonResult MemberUpdate(string accessToken, string openId, int toGroupId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";
            var data = new
            {
                openid = openId,
                to_groupid = toGroupId
            };
            return CommonJsonSend.Send(accessToken, urlFormat, data);
        }
    }
}
