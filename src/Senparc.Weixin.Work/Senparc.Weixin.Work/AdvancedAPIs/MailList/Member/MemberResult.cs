/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：MemberResult.cs
    文件功能描述：成员接口返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20180828
    修改描述：v0.4.2 添加 GetMemberResult.order 属性

    修改标识：Senparc - 20171017
    修改描述：v1.2.0 部门id改为long类型
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.MailList.Member;

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList
{
    /// <summary>
    /// GetMemberResult【QY移植修改】
    /// </summary>
    public class GetMemberResult : WorkJsonResult
    {
        /// <summary>
        /// 员工UserID 
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 成员所属部门id列表
        /// </summary>
        public long[] department { get; set; }
        /// <summary>
        /// 部门内的排序值，默认为0。数量必须和department一致，数值越大排序越前面。值范围是[0, 2^32)
        /// </summary>
        public int[] order { get; set; }
        /// <summary>
        /// 职位信息
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 性别。gender=0表示男，=1表示女 
        /// </summary>
        public int gender { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 上级字段，标识是否为上级。第三方暂不支持
        /// </summary>
        public int isleader { get; set; }
        /// <summary>
        /// 头像url。注：小图将url最后的"/0"改成"/64"
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 激活状态: 1=已激活，2=已禁用，4=未激活 已激活代表已激活企业微信或已关注微信插件。未激活代表既未激活企业微信又未关注微信插件。
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 座机。第三方暂不支持
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 英文名。第三方暂不支持
        /// </summary>
        public string english_name { get; set; }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public Extattr extattr { get; set; }
        /// <summary>
        /// 启用/禁用成员，第三方不可获取。1表示启用成员，0表示禁用成员
        /// </summary>
        public int enable { get; set; }
        /// <summary>
        /// 关注微信插件的状态: 1=已关注，0=未关注
        /// </summary>
        public string wxplugin_status { get; set; }
    }

    public class GetDepartmentMemberResult : WorkJsonResult
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        public List<UserList_Simple> userlist { get; set; }
    }

    /// <summary>
    /// UserList_Simple【QY移植修改】
    /// </summary>
    public class UserList_Simple
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 成员所属部门
        /// </summary>
        public long[] department { get; set; }
    }


    /// <summary>
    /// 获取部门成员(详情)返回结果
    /// </summary>
    public class GetDepartmentMemberInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        public List<GetMemberResult> userlist { get; set; }
    }

    public class InviteMemberResult : WorkJsonResult
    {
        /// <summary>
        /// 1:微信邀请 2.邮件邀请
        /// </summary>
        public int type { get; set; }
    }

    /// <summary>
    /// 邀请成员返回结果
    /// </summary>
    public class InviteMemberListResultJson : WorkJsonResult
    {
        public string[] invaliduser { get; set; }
        public string[] invalidparty { get; set; }
        public string[] invalidtag { get; set; }
    }

}
