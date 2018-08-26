/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MailListApi.cs
    文件功能描述：通讯录同步接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间
  
    修改标识：Senparc - 20150319
    修改描述: 去除无效字段（tel、gender）
  
    修改标识：Senparc - 20150408
    修改描述: 添加获取标签列表接口

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
 
-----------------------------------
    
    修改标识：Senparc - 20170616
    修改描述：从QY移植，同步Work接口

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造

    修改标识：Senparc - 20180821
    修改描述：v0.4.1 更新MailListApi.GetDepartmentMemberInfo()参数
 
    修改标识：Senparc - 20171017
    修改描述：v1.2.0 部门id改为long类型

    修改标识：Senparc - 20171220
    修改描述：v1.2.9 为OAuth Url添加agendId参数（可选）

----------------------------------------------------------------*/

/*
    创建成员：http://work.weixin.qq.com/api/doc#10018
    读取成员：http://work.weixin.qq.com/api/doc#10019
    更新成员：http://work.weixin.qq.com/api/doc#10020
    删除成员：http://work.weixin.qq.com/api/doc#10030
    批量删除成员：http://work.weixin.qq.com/api/doc#10060
    获取部门成员：http://work.weixin.qq.com/api/doc#10061
    获取部门成员详情：http://work.weixin.qq.com/api/doc#10063
 */

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using Senparc.Weixin.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList.Member;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    public static class MailListApi
    {
        #region 同步方法

        #region 成员管理

        /// <summary>
        /// 创建成员(mobile/weixinid/email三者不能同时为空)【QY移植修改】
        /// 文档：http://work.weixin.qq.com/api/doc#10018
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="memberCreateRequest">创建成员信息请求包</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult CreateMember(string accessTokenOrAppKey, MemberCreateRequest memberCreateRequest, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/user/create?access_token={0}";

                JsonSetting jsonSetting = new JsonSetting(true);

                return CommonJsonSend.Send<WorkJsonResult>(accessToken, url, memberCreateRequest, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);

                /*
                   返回结果：
                   {
                       "errcode": 0,
                       "errmsg": "created"
                    }
                */
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static GetMemberResult GetMember(string accessTokenOrAppKey, string userId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/get?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());

                return Get.GetJson<GetMemberResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 更新成员(mobile/weixinid/email三者不能同时为空)【QY移植修改】
        /// 权限说明：系统应用须拥有指定部门、成员的管理权限。注意，每个部门下的节点不能超过3万个。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        ///<param name="memberUpdateRequest">更新成员信息请求包</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// accessToken和userId为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static WorkJsonResult UpdateMember(string accessTokenOrAppKey, MemberUpdateRequest memberUpdateRequest, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/user/update?access_token={0}";

                JsonSetting jsonSetting = new JsonSetting(true);
                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, memberUpdateRequest, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


            /*
             *  返回结果：
                {
                   "errcode": 0,
                   "errmsg": "updated"
                }
            */
        }


        /// <summary>
        /// 删除成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static WorkJsonResult DeleteMember(string accessTokenOrAppKey, string userId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/delete?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());

                return Get.GetJson<WorkJsonResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 批量删除成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="useridlist">成员UserID列表。对应管理端的帐号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult BatchDeleteMember(string accessTokenOrAppKey, string[] useridlist, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/batchdelete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    useridlist = useridlist
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取部门成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        ///// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <param name="maxJsonLength">设置 JavaScriptSerializer 类接受的 JSON 字符串的最大长度</param>
        /// <remarks>
        /// 2016-04-16：Zeje添加参数maxJsonLength：企业号通讯录扩容后，存在Json长度不够的情况。
        /// </remarks>
        /// <returns></returns>
        public static GetDepartmentMemberResult GetDepartmentMember(string accessTokenOrAppKey, long departmentId, int fetchChild /*,int status,*/ /*int? maxJsonLength = null*/)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                //var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken.AsUrlData(), departmentId, fetchChild, status);

                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}", accessToken.AsUrlData(), departmentId, fetchChild);

                return Get.GetJson<GetDepartmentMemberResult>(url/*, maxJsonLength: maxJsonLength*/);
            }, accessTokenOrAppKey);


        }

        /* <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>*/
        /// <summary>
        /// 获取部门成员(详情)【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">（）1/0：是否递归获取子部门下面的成员</param>
        /// <param name="maxJsonLength">设置 JavaScriptSerializer 类接受的 JSON 字符串的最大长度</param>
        /// <remarks>
        /// 2016-05-03：Zeje添加参数maxJsonLength：企业号通讯录扩容后，存在Json长度不够的情况。
        /// </remarks>
        /// <returns></returns>
        public static GetDepartmentMemberInfoResult GetDepartmentMemberInfo(string accessTokenOrAppKey, long departmentId, int? fetchChild /*, int status, *//*int? maxJsonLength = null*/)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                //var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken.AsUrlData(), departmentId, fetchChild, status);

                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}", accessToken.AsUrlData(), departmentId, fetchChild);

                return Get.GetJson<GetDepartmentMemberInfoResult>(url/*, maxJsonLength: maxJsonLength*/);
            }, accessTokenOrAppKey);
        }


        #endregion

        #region 部门管理

        /// <summary>
        /// 创建部门【QY移植修改】
        /// 系统应用须拥有父部门的管理权限。
        /// 注意，部门的最大层级为15层；部门总数不能超过3万个；每个部门下的节点不能超过3万个。建议保证创建的部门和对应部门成员是串行化处理。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="name">部门名称。长度限制为1~64个字节，字符不能包括\:?”<>｜</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateDepartmentResult CreateDepartment(string accessTokenOrAppKey, string name, long parentId, int order = 1, long? id = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    name = name,
                    parentid = parentId,
                    order = order,
                    id = id
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<CreateDepartmentResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 更新部门【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult UpdateDepartment(string accessTokenOrAppKey, long id, string name, long parentId, int order = 1, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    id = id,
                    name = name,
                    parentid = parentId,
                    order = order
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 删除部门【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// <returns></returns>
        public static WorkJsonResult DeleteDepartment(string accessTokenOrAppKey, long id)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/delete?access_token={0}&id={1}", accessToken.AsUrlData(), id);

                return Get.GetJson<WorkJsonResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取部门列表【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id。获取指定部门及其下的子部门。 如果不填，默认获取全量组织架构</param>
        /// <returns></returns>
        public static GetDepartmentListResult GetDepartmentList(string accessTokenOrAppKey, long? id = null)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/list?access_token={0}", accessToken.AsUrlData());

                if (id.HasValue)
                {
                    url += string.Format("&id={0}", id.Value);
                }

                return Get.GetJson<GetDepartmentListResult>(url);
            }, accessTokenOrAppKey);


        }

        #endregion

        #region 标签管理

        /// <summary>
        /// 创建标签【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagName">标签名称。长度为1~64个字符，标签不可与其他同组的标签重名，也不可与全局标签重名</param>
        /// <param name="tagId">标签id，非负整型，指定此参数时新增的标签会生成对应的标签id，不指定时则以目前最大的id自增。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CreateTagResult CreateTag(string accessTokenOrAppKey, string tagName, int? tagId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    tagname = tagName,
                    tagid = tagId
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<CreateTagResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }


        /// <summary>
        /// 更新标签名字【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="tagName">标签名称。长度为0~64个字符</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult UpdateTag(string accessTokenOrAppKey, int tagId, string tagName, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    tagid = tagId,
                    tagname = tagName
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 删除标签【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static WorkJsonResult DeleteTag(string accessTokenOrAppKey, int tagId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/delete?access_token={0}&tagid={1}", accessToken.AsUrlData(), tagId);

                return Get.GetJson<WorkJsonResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取标签成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static GetTagMemberResult GetTagMember(string accessTokenOrAppKey, int tagId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/get?access_token={0}&tagid={1}", accessToken.AsUrlData(), tagId);

                return Get.GetJson<GetTagMemberResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 增加标签成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业成员ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="partyList">企业部门ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AddTagMemberResult AddTagMember(string accessTokenOrAppKey, int tagId, string[] userList = null, long[] partyList = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/addtagusers?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    tagid = tagId,
                    userlist = userList,
                    partylist = partyList
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<AddTagMemberResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 删除标签成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业成员ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="partylist">企业部门ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static DelTagMemberResult DelTagMember(string accessTokenOrAppKey, int tagId, string[] userList = null, long[] partylist = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/deltagusers?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    tagid = tagId,
                    userlist = userList
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<DelTagMemberResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取标签列表【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <returns></returns>
        public static GetTagListResult GetTagList(string accessTokenOrAppKey)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/list?access_token={0}", accessToken.AsUrlData());

                return Get.GetJson<GetTagListResult>(url);
            }, accessTokenOrAppKey);


        }

        #endregion

        /// <summary>
        /// 【Work中未定义】邀请成员关注
        /// 认证号优先使用微信推送邀请关注，如果没有weixinid字段则依次对手机号，邮箱绑定的微信进行推送，全部没有匹配则通过邮件邀请关注。 邮箱字段无效则邀请失败。 非认证号只通过邮件邀请关注。邮箱字段无效则邀请失败。 已关注以及被禁用用户不允许发起邀请关注请求。
        /// 测试发现同一个邮箱只发送一封邀请关注邮件，第二次再对此邮箱发送微信会提示系统错误
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">用户的userid</param>
        /// <param name="inviteTips">推送到微信上的提示语（只有认证号可以使用）。当使用微信推送时，该字段默认为“请关注XXX企业号”，邮件邀请时，该字段无效。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static InviteMemberResult InviteMember(string accessTokenOrAppKey, string userId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/invite/send?access_token={0}", accessToken);

                var data = new
                {
                    userid = userId,
                };

                return CommonJsonSend.Send<InviteMemberResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }


        #endregion

#if !NET35 && !NET40
        #region 异步方法

        #region 成员管理

        /// <summary>
        /// 【异步方法】创建成员(mobile/weixinid/email三者不能同时为空)【QY移植修改】
        /// 文档：http://work.weixin.qq.com/api/doc#10018
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="memberCreateRequest">创建成员信息请求包</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> CreateMemberAsync(string accessTokenOrAppKey, MemberCreateRequest memberCreateRequest, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/user/create?access_token={0}";

                JsonSetting jsonSetting = new JsonSetting(true);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, memberCreateRequest, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);

                /*
                   返回结果：
                   {
                       "errcode": 0,
                       "errmsg": "created"
                    }
                */
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】获取成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static async Task<GetMemberResult> GetMemberAsync(string accessTokenOrAppKey, string userId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/get?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());

                return await Get.GetJsonAsync<GetMemberResult>(url);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 更新成员(mobile/weixinid/email三者不能同时为空)【QY移植修改】
        /// 权限说明：系统应用须拥有指定部门、成员的管理权限。注意，每个部门下的节点不能超过3万个。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        ///<param name="memberUpdateRequest">更新成员信息请求包</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// accessToken和userId为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateMemberAsync(string accessTokenOrAppKey, MemberUpdateRequest memberUpdateRequest, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/user/update?access_token={0}";

                JsonSetting jsonSetting = new JsonSetting(true);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, memberUpdateRequest, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }

            /*
            *  返回结果：
               {
                  "errcode": 0,
                  "errmsg": "updated"
               }
           */
            , accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】删除成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteMemberAsync(string accessTokenOrAppKey, string userId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/delete?access_token={0}&userid={1}", accessToken.AsUrlData(), userId.AsUrlData());

                return await Get.GetJsonAsync<WorkJsonResult>(url);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 【异步方法】批量删除成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="useridlist"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> BatchDeleteMemberAsync(string accessTokenOrAppKey, string[] useridlist, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/batchdelete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    useridlist = useridlist
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }


        /// <summary>
        /// 【异步方法】获取部门成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        ///// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <param name="maxJsonLength">设置 JavaScriptSerializer 类接受的 JSON 字符串的最大长度</param>
        /// <remarks>
        /// 2016-04-16：Zeje添加参数maxJsonLength：企业号通讯录扩容后，存在Json长度不够的情况。
        /// </remarks>
        /// <returns></returns>
        public static async Task<GetDepartmentMemberResult> GetDepartmentMemberAsync(string accessTokenOrAppKey, long departmentId, int fetchChild /*,int status,*//* int? maxJsonLength = null*/)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                //var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken.AsUrlData(), departmentId, fetchChild, status);

                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}", accessToken.AsUrlData(), departmentId, fetchChild);

                return await Get.GetJsonAsync<GetDepartmentMemberResult>(url/*, maxJsonLength: maxJsonLength*/);
            }, accessTokenOrAppKey);


        }

        /*/// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>*/
        /// <summary>
        /// 【异步方法】获取部门成员(详情)【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">（非必填）1/0：是否递归获取子部门下面的成员</param>
        /// <param name="maxJsonLength">设置 JavaScriptSerializer 类接受的 JSON 字符串的最大长度</param>
        /// <remarks>
        /// 2016-05-03：Zeje添加参数maxJsonLength：企业号通讯录扩容后，存在Json长度不够的情况。
        /// </remarks>
        /// <returns></returns>
        public static async Task<GetDepartmentMemberInfoResult> GetDepartmentMemberInfoAsync(string accessTokenOrAppKey, long departmentId, int? fetchChild /*, int status, *//*int? maxJsonLength = null*/)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                //var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken.AsUrlData(), departmentId, fetchChild, status);

                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}", accessToken.AsUrlData(), departmentId, fetchChild);

                return await Get.GetJsonAsync<GetDepartmentMemberInfoResult>(url/*, maxJsonLength: maxJsonLength*/);
            }, accessTokenOrAppKey);


        }


        #endregion

        #region 部门管理


        /// <summary>
        /// 【异步方法】创建部门【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="name">部门名称。长度限制为1~64个字节，字符不能包括\:?”<>｜</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CreateDepartmentResult> CreateDepartmentAsync(string accessTokenOrAppKey, string name, long parentId, int order = 1, long? id = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    name = name,
                    parentid = parentId,
                    order = order,
                    id = id
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CreateDepartmentResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】更新部门【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateDepartmentAsync(string accessTokenOrAppKey, long id, string name, long parentId, int order = 1, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    id = id,
                    name = name,
                    parentid = parentId,
                    order = order
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】删除部门【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteDepartmentAsync(string accessTokenOrAppKey, long id)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/delete?access_token={0}&id={1}", accessToken.AsUrlData(), id);

                return await Get.GetJsonAsync<WorkJsonResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取部门列表【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="id">部门ID。获取指定部门ID下的子部门</param>
        /// <returns></returns>
        public static async Task<GetDepartmentListResult> GetDepartmentListAsync(string accessTokenOrAppKey, long? id = null)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/department/list?access_token={0}", accessToken.AsUrlData());

                if (id.HasValue)
                {
                    url += string.Format("&id={0}", id.Value);
                }

                return await Get.GetJsonAsync<GetDepartmentListResult>(url);
            }, accessTokenOrAppKey);


        }

        #endregion

        #region 标签管理

        /// <summary>
        /// 【异步方法】创建标签【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagName">标签名称。长度为1~64个字符，标签不可与其他同组的标签重名，也不可与全局标签重名</param>
        /// <param name="tagId">标签id，整型，指定此参数时新增的标签会生成对应的标签id，不指定时则以目前最大的id自增。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CreateTagResult> CreateTagAsync(string accessTokenOrAppKey, string tagName, int? tagId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/create?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    tagname = tagName,
                    tagid = tagId
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<CreateTagResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }


        /// <summary>
        /// 【异步方法】更新标签名字【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="tagName">标签名称。长度为0~64个字符</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateTagAsync(string accessTokenOrAppKey, int tagId, string tagName, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/tag/update?access_token={0}";

                var data = new
                {
                    tagid = tagId,
                    tagname = tagName
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】删除标签【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteTagAsync(string accessTokenOrAppKey, int tagId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/delete?access_token={0}&tagid={1}", accessToken.AsUrlData(), tagId);

                return await Get.GetJsonAsync<WorkJsonResult>(url);
            }, accessTokenOrAppKey);


        }


        /// <summary>
        /// 【异步方法】获取标签成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public static async Task<GetTagMemberResult> GetTagMemberAsync(string accessTokenOrAppKey, int tagId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/get?access_token={0}&tagid={1}", accessToken.AsUrlData(), tagId);

                return await Get.GetJsonAsync<GetTagMemberResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        ///【异步方法】 增加标签成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业成员ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="partyList">企业部门ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<AddTagMemberResult> AddTagMemberAsync(string accessTokenOrAppKey, int tagId, string[] userList = null, long[] partyList = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/addtagusers?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    tagid = tagId,
                    userlist = userList,
                    partylist = partyList
                };
                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddTagMemberResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        ///【异步方法】 删除标签成员【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业员工ID列表</param>
        /// <param name="partylist">企业部门ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<DelTagMemberResult> DelTagMemberAsync(string accessTokenOrAppKey, int tagId, string[] userList, long[] partylist = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/deltagusers?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    tagid = tagId,
                    userlist = userList
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<DelTagMemberResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, accessTokenOrAppKey);



        }

        /// <summary>
        ///【异步方法】 获取标签列表【QY移植修改】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <returns></returns>
        public static async Task<GetTagListResult> GetTagListAsync(string accessTokenOrAppKey)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/tag/list?access_token={0}", accessToken.AsUrlData());

                return await Get.GetJsonAsync<GetTagListResult>(url);
            }, accessTokenOrAppKey);


        }

        #endregion


        /// <summary>
        /// 【异步方法】【Work中未定义】邀请成员关注
        /// 认证号优先使用微信推送邀请关注，如果没有weixinid字段则依次对手机号，邮箱绑定的微信进行推送，全部没有匹配则通过邮件邀请关注。 邮箱字段无效则邀请失败。 非认证号只通过邮件邀请关注。邮箱字段无效则邀请失败。 已关注以及被禁用用户不允许发起邀请关注请求。
        /// 测试发现同一个邮箱只发送一封邀请关注邮件，第二次再对此邮箱发送微信会提示系统错误
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="userId">用户的userid</param>
        /// <param name="inviteTips">推送到微信上的提示语（只有认证号可以使用）。当使用微信推送时，该字段默认为“请关注XXX企业号”，邮件邀请时，该字段无效。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<InviteMemberResult> InviteMemberAsync(string accessTokenOrAppKey, string userId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/invite/send?access_token={0}", accessToken);

                var data = new
                {
                    userid = userId,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<InviteMemberResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }


        #endregion
#endif
    }
}
