﻿/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    修改标识：Senparc - 20160621
    修改描述：修改命名空间
              其改为Senparc.Weixin.MP.AdvancedAPIs       
----------------------------------------------------------------*/

using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class UserTagApi
    {
        public static CreateTagResult Create(string accessTokenOrAppId,string name, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/tags/create?access_token={0}";
                var data = new
                {
                    tag = new
                    {
                        name = name
                    }
                };
                return CommonJsonSend.Send<CreateTagResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        public static TagJson Get(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token={0}";
                var url = string.Format(urlFormat, accessToken);
                return HttpUtility.Get.GetJson<TagJson>(url);

            }, accessTokenOrAppId);
        }
        public  static WxJsonResult Update(string accessTokenOrAppId, int id, string name, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/tags/update?access_token={0}";
                var data = new
                {
                    tag = new
                    {
                        id = id,
                        name = name
                    }
                };
                return CommonJsonSend.Send(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        public static WxJsonResult Delete(string accessTokenOrAppId, int id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/tags/delete?access_token={0}";

                var data = new
                {
                    tag = new
                    {
                        id = id 
                    }
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        public static UserTagJsonResult Get(string accessTokenOrAppId, int tagid,string next_openid="", int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/user/tag/get?access_token={0}";
                var data = new
                {
                    tagid = tagid,
                    next_openid = next_openid
                };
                return CommonJsonSend.Send<UserTagJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }
        public static WxJsonResult BatchTagging(string accessTokenOrAppId,int tagid,List<string> openid_list,int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?access_token={0}";
                var data = new
                {
                    openid_list = openid_list,
                    tagid = tagid
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        public static WxJsonResult BatchUntagging(string accessTokenOrAppId, int tagid, List<string> openid_list, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?access_token={0}";
                var data = new
                {
                    openid_list = openid_list,
                    tagid = tagid
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        public static UserTagListResult UserTagList(string accessTokenOrAppid,string openid,int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = "https://api.weixin.qq.com/cgi-bin/tags/getidlist?access_token={0}";
                var data = new
                {
                    openid = openid
                };
                return CommonJsonSend.Send<UserTagListResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppid);
        }
    }
}
