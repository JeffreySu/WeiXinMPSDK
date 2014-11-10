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
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", accessToken);

            var data = new
            {
                name = name,
                parentid = parentId
            };

            return CommonJsonSend.Send<CreateDepartmentResult>(null, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <returns></returns>
        public static WxJsonResult UpdateDepartment(string accessToken, string id, string name, int parentId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}", accessToken);

            var data = new
            {
                id = id,
                name = name,
                parentid = parentId
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// <returns></returns>
        public static WxJsonResult DeleteDepartment(string accessToken, string id)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}", accessToken, id);

            return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.GET);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public static GetDepartmentListResult GetDepartmentList(string accessToken)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}", accessToken);

            return CommonJsonSend.Send<GetDepartmentListResult>(null, url, null, CommonJsonSendType.GET);
        }
    }
}
