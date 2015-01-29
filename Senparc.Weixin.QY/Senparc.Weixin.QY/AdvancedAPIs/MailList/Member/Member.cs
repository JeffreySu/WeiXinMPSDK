using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    //官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E9%83%A8%E9%97%A8

    public static class Member
    {
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
        /// <param name="extattr">扩展属性。扩展属性需要在WEB管理端创建后才生效，否则忽略未知属性的赋值</param>
        /// accessToken、userId和name为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static WxJsonResult CreateMember(string accessToken, string userId, string name, int[] department = null,
            string position = null, string mobile = null, string tel = null, string email = null, string weixinId = null,
            int gender = 0, Extattr extattr = null)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";

            var data = new
            {
                userid = userId,
                name = name,
                department = department,
                position = position,
                mobile = mobile,
                gender = gender,
                tel = tel,
                email = email,
                weixinid = weixinId,
                extattr = extattr
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 更新成员(mobile/weixinid/email三者不能同时为空)
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
        /// <param name="enable">启用/禁用成员。1表示启用成员，0表示禁用成员</param>
        /// <param name="gender">性别。gender=0表示男，=1表示女。默认gender=0</param>
        /// <param name="extattr">扩展属性。扩展属性需要在WEB管理端创建后才生效，否则忽略未知属性的赋值</param>
        /// accessToken和userId为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static WxJsonResult UpdateMember(string accessToken, string userId, string name = null, int[] department = null, string position = null,
            string mobile = null, string tel = null, string email = null, string weixinId = null, int enable = 1,
            int gender = 0, Extattr extattr = null)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";

            var data = new
            {
                userid = userId,
                name = name,
                department = department,
                position = position,
                mobile = mobile,
                gender = gender,
                tel = tel,
                email = email,
                weixinid = weixinId,
                enable = enable,
                extattr = extattr
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static WxJsonResult DeleteMember(string accessToken, string userId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}", accessToken, userId);

            return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static WxJsonResult BatchDeleteMember(string accessToken, string[] userIds)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token={0}", accessToken);

            var data = new
                {
                    useridlist = userIds
                };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static GetMemberResult GetMember(string accessToken, string userId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", accessToken, userId);

            return CommonJsonSend.Send<GetMemberResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <returns></returns>
        public static GetDepartmentMemberResult GetDepartmentMember(string accessToken, int departmentId, int fetchChild, int status)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}",accessToken,departmentId,fetchChild,status);

            return CommonJsonSend.Send<GetDepartmentMemberResult>(null, url, null, CommonJsonSendType.GET);
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
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken, departmentId, fetchChild, status);

            return CommonJsonSend.Send<GetDepartmentMemberInfoResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 邀请成员关注
        /// 认证号优先使用微信推送邀请关注，如果没有weixinid字段则依次对手机号，邮箱绑定的微信进行推送，全部没有匹配则通过邮件邀请关注。 邮箱字段无效则邀请失败。 非认证号只通过邮件邀请关注。邮箱字段无效则邀请失败。 已关注以及被禁用用户不允许发起邀请关注请求。
        /// 测试发现同一个邮箱只发送一封邀请关注邮件，第二次再对此邮箱发送微信会提示系统错误
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">用户的userid</param>
        /// <param name="inviteTips">推送到微信上的提示语（只有认证号可以使用）。当使用微信推送时，该字段默认为“请关注XXX企业号”，邮件邀请时，该字段无效。</param>
        /// <returns></returns>
        public static GetDepartmentMemberInfoResult InviteMember(string accessToken, string userId, string inviteTips = null)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token={0}", accessToken);

            var data = new
                {
                    userid = userId,
                    invite_tips = inviteTips
                };

            return CommonJsonSend.Send<GetDepartmentMemberInfoResult>(null, url, data, CommonJsonSendType.POST);
        }
    }
}
