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

    public static class Department
    {
        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="name">部门名称。长度限制为1~64个字符</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <returns></returns>
        public static CreateDepartmentResult CreateDepartment(string accessToken, string name, int parentId)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}";

            var data = new
            {
                name = name,
                parentid = parentId
            };

            return CommonJsonSend.Send<CreateDepartmentResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <returns></returns>
        public static WxJsonResult UpdateDepartment(string accessToken, string id, string name)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0]";

            var data = new
            {
                id = id,
                name = name
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// 可以一次性删除一个或多个部门（删除多个时传入id数组）
        /// <returns></returns>
        public static WxJsonResult DeleteDepartment(string accessToken, string[] id)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0]";

            for (int i = 0; i < id.Length; i++)
            {
                url += string.Format("&id={0}", i + 1);
            }

            return CommonJsonSend.Send<WxJsonResult>(accessToken, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public static GetDepartmentListResult GetDepartmentList(string accessToken)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token=={0]";

            return CommonJsonSend.Send<GetDepartmentListResult>(accessToken, url, null, CommonJsonSendType.GET);
        }
    }
}
