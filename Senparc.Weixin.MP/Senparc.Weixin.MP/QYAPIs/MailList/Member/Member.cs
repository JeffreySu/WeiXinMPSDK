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

    public static class Member
    {
        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一，mobile/weixinid/email三者不能同时为空</param>
        /// <param name="tel">办公电话。长度为0~64个字符</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <param name="gender">性别。gender=0表示男，=1表示女。默认gender=0</param>
        /// accessToken和userId为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static WxJsonResult CreateMember(string accessToken, string userId, string name, int[] department, string position, string mobile, string tel, string email, string weixinId, int gender = 0)
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
                weixinid = weixinId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一，mobile/weixinid/email三者不能同时为空</param>
        /// <param name="tel">办公电话。长度为0~64个字符</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <param name="enable">启用/禁用成员。1表示启用成员，0表示禁用成员</param>
        /// <param name="gender">性别。gender=0表示男，=1表示女。默认gender=0</param>
        /// accessToken和userId为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static WxJsonResult UpdateMember(string accessToken, string userId, string name, int[] department, string position, string mobile, string tel, string email, string weixinId,int enable, int gender = 0)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0]";

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
                enable = enable
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
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static GetMemberResult GetMember(string accessToken, string userId)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}";

            return CommonJsonSend.Send<GetMemberResult>(accessToken, url, null, CommonJsonSendType.GET);
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
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}";

            return CommonJsonSend.Send<GetDepartmentMemberResult>(accessToken, url, null, CommonJsonSendType.GET);
        }
    }
}
