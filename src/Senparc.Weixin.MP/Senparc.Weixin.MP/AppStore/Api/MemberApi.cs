/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    文件名：MemberApi.cs
    文件功能描述：获取用户信息Api
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AppStore.Api
{
    public class MemberApi : BaseApi
    {
        public MemberApi(Passport passport)
            : base(passport)
        {
        }

        private GetMemberResult GetMemberFunc(int weixinId, string openId)
        {
            var url = _passport.ApiUrl + "GetMember";
            var formData = new Dictionary<string, string>();
            formData["token"] = _passport.Token;
            formData["openid"] = openId;
            formData["weixinId"] = weixinId.ToString();

            var result = Post.PostGetJson<GetMemberResult>(url, formData: formData);
            return result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public GetMemberResult GetMember(int weixinId, string openId)
        {
            return ApiConnection.Connection(() => GetMemberFunc(weixinId, openId)) as GetMemberResult;
        }
    }
}
