using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;
using Senparc.Weixin.MP.QYPIs;

namespace Senparc.Weixin.MP.QYAPIs
{
    //官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E9%83%A8%E9%97%A8

    public static class Tag
    {
        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagName">标签名称。长度为1~64个字符，标签不可与其他同组的标签重名，也不可与全局标签重名</param>
        /// <returns></returns>
        public static CreateTagResult CreateTag(string accessToken, string tagName)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token={0}";

            var data = new
            {
                tagname = tagName
            };

            return CommonJsonSend.Send<CreateTagResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 更新标签名字
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="tagName">标签名称。长度为0~64个字符</param>
        /// <returns></returns>
        public static WxJsonResult UpdateTag(string accessToken, int tagId, string tagName)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token={0}";

            var data = new
            {
                tagid = tagId,
                tagname = tagName
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId"> 标签ID </param>
        /// <returns></returns>
        public static WxJsonResult DeleteTag(string accessToken, int tagId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token={0}&tagid={1}", accessToken, tagId);

            return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取标签成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static GetTagMemberResult GetTagMember(string accessToken, string tagId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/get?access_token={0}&tagid={1}", accessToken, tagId);

            return CommonJsonSend.Send<GetTagMemberResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 增加标签成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业员工ID列表</param>
        /// <returns></returns>
        public static AddTagMemberResult AddTagMember(string accessToken, string tagId, string[] userList)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token={0}";

            var data = new
            {
                tagid = tagId,
                userlist = userList
            };

            return CommonJsonSend.Send<AddTagMemberResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 删除标签成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业员工ID列表</param>
        /// <returns></returns>
        public static DelTagMemberResult DelTagMember(string accessToken, string tagId, string[] userList)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token={0}";

            var data = new
            {
                tagid = tagId,
                userlist = userList
            };

            return CommonJsonSend.Send<DelTagMemberResult>(accessToken, url, data, CommonJsonSendType.POST);
        }
    }
}
