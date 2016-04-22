/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：MailListApi.cs
    文件功能描述：通讯录接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间
  
    修改标识：Senparc - 20150319
    修改描述: 去除无效字段（tel、gender）
  
    修改标识：Senparc - 20150408
    修改描述: 添加获取标签列表接口
----------------------------------------------------------------*/

/*
    成员接口：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E6%88%90%E5%91%98
    部门接口：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E9%83%A8%E9%97%A8
    标签接口：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E6%A0%87%E7%AD%BE
 */

using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    public static class MailListApi
    {
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="name">部门名称。长度限制为1~64个字符</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateDepartmentResult CreateDepartment(string accessToken, string name, int parentId, int order = 1, int? id = null, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", accessToken.AsUrlData());

            var data = new
            {
                name = name,
                parentid = parentId,
                order = order,
                id = id
            };

            return CommonJsonSend.Send<CreateDepartmentResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static QyJsonResult UpdateDepartment(string accessToken, string id, string name, int parentId, int order = 1, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}", accessToken.AsUrlData());

            var data = new
            {
                id = id,
                name = name,
                parentid = parentId,
                order = order
            };

            return CommonJsonSend.Send<QyJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// <returns></returns>
        public static QyJsonResult DeleteDepartment(string accessToken, string id)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}", accessToken.AsUrlData(), id.AsUrlData());

            return Get.GetJson<QyJsonResult>(url);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门ID。获取指定部门ID下的子部门</param>
        /// <returns></returns>
        public static GetDepartmentListResult GetDepartmentList(string accessToken, int? id = null)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}", accessToken.AsUrlData());

            if (id.HasValue)
            {
                url += string.Format("&id={0}", id.Value);
            }

            return Get.GetJson<GetDepartmentListResult>(url);
        }

        /// <summary>
        /// 创建成员(mobile/weixinid/email三者不能同时为空)
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一</param>
        /// <param name="tel">办公电话。长度为0~64个字符</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <param name="gender">性别。gender=0表示男，=1表示女。默认gender=0</param>
        /// <param name="avatarMediaid"></param>
        /// <param name="extattr">扩展属性。扩展属性需要在WEB管理端创建后才生效，否则忽略未知属性的赋值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// accessToken、userId和name为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static QyJsonResult CreateMember(string accessToken, string userId, string name, int[] department = null,
            string position = null, string mobile = null, string email = null, string weixinId = null, /*string tel = null,
            int gender = 0,*/string avatarMediaid = null, Extattr extattr = null, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";

            var data = new
            {
                userid = userId,
                name = name,
                department = department,
                position = position,
                mobile = mobile,

                //最新的接口中去除了以下两个字段
                //gender = gender,
                //tel = tel,

                email = email,
                weixinid = weixinId,
                avatar_mediaid = avatarMediaid,
                extattr = extattr
            };

            return CommonJsonSend.Send<QyJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        ///// <param name="tel">办公电话。长度为0~64个字符</param>
        ///// <param name="gender">性别。gender=0表示男，=1表示女。默认gender=0</param>
        /// <summary>
        /// 更新成员(mobile/weixinid/email三者不能同时为空)
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <param name="enable">启用/禁用成员。1表示启用成员，0表示禁用成员</param>
        /// <param name="avatarMediaid"></param>
        /// <param name="extattr">扩展属性。扩展属性需要在WEB管理端创建后才生效，否则忽略未知属性的赋值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// accessToken和userId为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static QyJsonResult UpdateMember(string accessToken, string userId, string name = null, int[] department = null, string position = null,
            string mobile = null, string email = null, string weixinId = null, int enable = 1, /*string tel = null,
            int gender = 0,*/string avatarMediaid = null, Extattr extattr = null, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";

            var data = new
            {
                userid = userId,
                name = name,
                department = department,
                position = position,
                mobile = mobile,

                //最新的接口中去除了以下两个字段
                //gender = gender,
                //tel = tel,

                email = email,
                weixinid = weixinId,
                enable = enable,
                avatar_mediaid = avatarMediaid,
                extattr = extattr
            };

            return CommonJsonSend.Send<QyJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static QyJsonResult DeleteMember(string accessToken, string userId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());

            return Get.GetJson<QyJsonResult>(url);
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userIds"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static QyJsonResult BatchDeleteMember(string accessToken, string[] userIds, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token={0}", accessToken.AsUrlData());

            var data = new
            {
                useridlist = userIds
            };

            return CommonJsonSend.Send<QyJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static GetMemberResult GetMember(string accessToken, string userId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());

            return Get.GetJson<GetMemberResult>(url);
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <param name="maxJsonLength">设置 JavaScriptSerializer 类接受的 JSON 字符串的最大长度</param>
        /// <remarks>
        /// 2016-04-16：Zeje添加参数maxJsonLength：企业号通讯录扩容后，存在Json长度不够的情况。
        /// </remarks>
        /// <returns></returns>
        public static GetDepartmentMemberResult GetDepartmentMember(string accessToken, int departmentId, int fetchChild, int status, int? maxJsonLength = null)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken.AsUrlData(), departmentId, fetchChild, status);

            return Get.GetJson<GetDepartmentMemberResult>(url, maxJsonLength: maxJsonLength);
        }

        /// <summary>
        /// 获取部门成员(详情)
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <returns></returns>
        public static GetDepartmentMemberInfoResult GetDepartmentMemberInfo(string accessToken, int departmentId, int fetchChild, int status)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken.AsUrlData(), departmentId, fetchChild, status);

            return Get.GetJson<GetDepartmentMemberInfoResult>(url);
        }

        /// <summary>
        /// 邀请成员关注
        /// 认证号优先使用微信推送邀请关注，如果没有weixinid字段则依次对手机号，邮箱绑定的微信进行推送，全部没有匹配则通过邮件邀请关注。 邮箱字段无效则邀请失败。 非认证号只通过邮件邀请关注。邮箱字段无效则邀请失败。 已关注以及被禁用用户不允许发起邀请关注请求。
        /// 测试发现同一个邮箱只发送一封邀请关注邮件，第二次再对此邮箱发送微信会提示系统错误
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">用户的userid</param>
        /// <param name="inviteTips">推送到微信上的提示语（只有认证号可以使用）。当使用微信推送时，该字段默认为“请关注XXX企业号”，邮件邀请时，该字段无效。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static InviteMemberResult InviteMember(string accessToken, string userId, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token={0}", accessToken);

            var data = new
            {
                userid = userId,
            };

            return CommonJsonSend.Send<InviteMemberResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }


        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagName">标签名称。长度为1~64个字符，标签不可与其他同组的标签重名，也不可与全局标签重名</param>
        /// <param name="tagId">标签id，整型，指定此参数时新增的标签会生成对应的标签id，不指定时则以目前最大的id自增。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateTagResult CreateTag(string accessToken, string tagName, int? tagId = null, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token={0}";

            var data = new
            {
                tagname = tagName,
                tagid = tagId
            };

            return CommonJsonSend.Send<CreateTagResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 更新标签名字
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="tagName">标签名称。长度为0~64个字符</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static QyJsonResult UpdateTag(string accessToken, int tagId, string tagName, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token={0}";

            var data = new
            {
                tagid = tagId,
                tagname = tagName
            };

            return CommonJsonSend.Send<QyJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static QyJsonResult DeleteTag(string accessToken, int tagId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token={0}&tagid={1}", accessToken.AsUrlData(), tagId);

            return Get.GetJson<QyJsonResult>(url);
        }

        /// <summary>
        /// 获取标签成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static GetTagMemberResult GetTagMember(string accessToken, int tagId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/get?access_token={0}&tagid={1}", accessToken.AsUrlData(), tagId);

            return Get.GetJson<GetTagMemberResult>(url);
        }

        /// <summary>
        /// 增加标签成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业成员ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="partyList">企业部门ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AddTagMemberResult AddTagMember(string accessToken, int tagId, string[] userList = null, int[] partyList = null, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token={0}";

            var data = new
            {
                tagid = tagId,
                userlist = userList,
                partylist = partyList
            };

            return CommonJsonSend.Send<AddTagMemberResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除标签成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业员工ID列表</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static DelTagMemberResult DelTagMember(string accessToken, int tagId, string[] userList, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token={0}";

            var data = new
            {
                tagid = tagId,
                userlist = userList
            };

            return CommonJsonSend.Send<DelTagMemberResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetTagListResult GetTagList(string accessToken)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/list?access_token={0}", accessToken.AsUrlData());

            return Get.GetJson<GetTagListResult>(url);
        }
    }
}
