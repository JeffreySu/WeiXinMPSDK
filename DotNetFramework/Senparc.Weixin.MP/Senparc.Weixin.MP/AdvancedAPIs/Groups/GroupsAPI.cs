/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：GroupsAPI.cs
    文件功能描述：用户组接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/0/56d992c605a97245eb7e617854b169fc.html
*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.Groups;
using Senparc.Weixin.MP.CommonAPIs;

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateGroupResult Create(string accessTokenOrAppId, string name, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";
                var data = new
                {
                    group = new
                    {
                        name = name
                    }
                };
                return CommonJsonSend.Send<CreateGroupResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <returns></returns>
        public static GroupsJson Get(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";
                var url = string.Format(urlFormat, accessToken.AsUrlData());
                return HttpUtility.Get.GetJson<GroupsJson>(url);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetGroupIdResult GetId(string accessTokenOrAppId, string openId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}";
                var data = new { openid = openId };
                return CommonJsonSend.Send<GetGroupIdResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="id"></param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Update(string accessTokenOrAppId, int id, string name, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
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
                return CommonJsonSend.Send(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="toGroupId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult MemberUpdate(string accessTokenOrAppId, string openId, int toGroupId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";
                var data = new
                {
                    openid = openId,
                    to_groupid = toGroupId
                };
                return CommonJsonSend.Send(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 批量移动用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="toGroupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="openIds">用户唯一标识符openid的列表（size不能超过50）</param>
        /// <returns></returns>
        public static WxJsonResult BatchUpdate(string accessTokenOrAppId, int toGroupId, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate?access_token={0}";

                var data = new
                {
                    openid_list = openIds,
                    to_groupid = toGroupId
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="groupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Delete(string accessTokenOrAppId, int groupId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}";

                var data = new
                {
                    group = new
                    {
                        id = groupId
                    }
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
    }
}