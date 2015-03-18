using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var result = HttpUtility.Post.PostGetJson<GetMemberResult>(url, formData: formData);
            return result;
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <returns></returns>
        public GetMemberResult SendMessage(int weixinId, string openId)
        {
            return ApiConnection.Connection(() => GetMemberFunc(weixinId, openId)) as GetMemberResult;
        }
    }
}
