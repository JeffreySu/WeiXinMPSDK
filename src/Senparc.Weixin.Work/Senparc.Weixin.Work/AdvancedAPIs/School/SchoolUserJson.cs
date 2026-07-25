/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SchoolUserJson.cs
    文件功能描述：企业微信家校配置、学生和家长管理强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增家校配置、学生与家长管理强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.School
{
    /// <summary>设置老师可查看班级的模式请求。</summary>
    public class SchoolTeacherViewModeRequest
    {
        /// <summary>查看模式，取值遵循企业微信协议。</summary>
        public int view_mode { get; set; }
    }

    /// <summary>老师可查看班级的模式结果。</summary>
    public class SchoolTeacherViewModeResult : WorkJsonResult
    {
        /// <summary>当前查看模式。</summary>
        public int view_mode { get; set; }
    }

    /// <summary>设置家校通讯录自动同步模式请求。</summary>
    public class SchoolArchSyncModeRequest
    {
        /// <summary>同步模式：1、2、3 分别关闭一个方向或双向同步；设置后不可逆。</summary>
        public int arch_sync_mode { get; set; }
    }

    /// <summary>家校访问用户身份结果。</summary>
    public class SchoolUserIdentityResult : WorkJsonResult
    {
        /// <summary>家长 UserId；当前身份为家长时返回。</summary>
        public string parent_userid { get; set; }

        /// <summary>学生 UserId；当前身份为学生时返回。</summary>
        public string student_userid { get; set; }

        /// <summary>手机设备号。</summary>
        public string DeviceId { get; set; }
    }

    /// <summary>学生写入模型。</summary>
    public class SchoolStudent
    {
        /// <summary>学生 UserId。</summary>
        public string student_userid { get; set; }

        /// <summary>更新后的学生 UserId，仅更新时使用。</summary>
        public string new_student_userid { get; set; }

        /// <summary>学生姓名。</summary>
        public string name { get; set; }

        /// <summary>学生所属学校部门 ID 列表。</summary>
        public IList<long> department { get; set; }

        /// <summary>是否邀请家长关注学校通知。</summary>
        public bool? to_invite { get; set; }

        /// <summary>用于邀请家长的手机号。</summary>
        public string mobile { get; set; }
    }

    /// <summary>学生批量写入请求。</summary>
    public class SchoolStudentBatchRequest
    {
        /// <summary>学生列表。</summary>
        public IList<SchoolStudent> students { get; set; }
    }

    /// <summary>家长关联的学生。</summary>
    public class SchoolParentChild
    {
        /// <summary>学生 UserId。</summary>
        public string student_userid { get; set; }

        /// <summary>家长与学生的关系。</summary>
        public string relation { get; set; }
    }

    /// <summary>家长写入模型。</summary>
    public class SchoolParent
    {
        /// <summary>家长 UserId。</summary>
        public string parent_userid { get; set; }

        /// <summary>更新后的家长 UserId，仅更新时使用。</summary>
        public string new_parent_userid { get; set; }

        /// <summary>家长手机号。</summary>
        public string mobile { get; set; }

        /// <summary>是否邀请家长关注学校通知。</summary>
        public bool? to_invite { get; set; }

        /// <summary>关联学生列表。</summary>
        public IList<SchoolParentChild> children { get; set; }
    }

    /// <summary>家长批量写入请求。</summary>
    public class SchoolParentBatchRequest
    {
        /// <summary>家长列表。</summary>
        public IList<SchoolParent> parents { get; set; }
    }

    /// <summary>批量删除学生或家长请求。</summary>
    public class SchoolUserIdListRequest
    {
        /// <summary>待删除的学生或家长 UserId 列表。</summary>
        public IList<string> useridlist { get; set; }
    }

    /// <summary>学生家长摘要。</summary>
    public class SchoolStudentParentInfo
    {
        /// <summary>家长 UserId。</summary>
        public string parent_userid { get; set; }

        /// <summary>家长与学生的关系。</summary>
        public string relation { get; set; }

        /// <summary>家长手机号。</summary>
        public string mobile { get; set; }

        /// <summary>是否已关注学校通知，使用协议定义的 0/1。</summary>
        public int is_subscribe { get; set; }

        /// <summary>家长对应的外部联系人 ID。</summary>
        public string external_userid { get; set; }
    }

    /// <summary>学生详情。</summary>
    public class SchoolStudentInfo
    {
        /// <summary>学生 UserId。</summary>
        public string student_userid { get; set; }

        /// <summary>学生姓名。</summary>
        public string name { get; set; }

        /// <summary>学生所属学校部门 ID 列表。</summary>
        public IList<long> department { get; set; }

        /// <summary>学生家长列表。</summary>
        public IList<SchoolStudentParentInfo> parents { get; set; }
    }

    /// <summary>家长子女详情。</summary>
    public class SchoolParentChildInfo : SchoolParentChild
    {
        /// <summary>学生姓名；部门家长列表接口会返回。</summary>
        public string name { get; set; }
    }

    /// <summary>家长详情。</summary>
    public class SchoolParentInfo
    {
        /// <summary>家长 UserId。</summary>
        public string parent_userid { get; set; }

        /// <summary>家长手机号。</summary>
        public string mobile { get; set; }

        /// <summary>是否已关注学校通知，使用协议定义的 0/1。</summary>
        public int is_subscribe { get; set; }

        /// <summary>家长对应的外部联系人 ID。</summary>
        public string external_userid { get; set; }

        /// <summary>关联学生列表。</summary>
        public IList<SchoolParentChildInfo> children { get; set; }
    }

    /// <summary>读取学生或家长结果。</summary>
    public class SchoolUserResult : WorkJsonResult
    {
        /// <summary>用户类型：1 为学生，2 为家长。</summary>
        public int user_type { get; set; }

        /// <summary>学生详情；读取学生时返回。</summary>
        public SchoolStudentInfo student { get; set; }

        /// <summary>家长详情；读取家长时返回。</summary>
        public SchoolParentInfo parent { get; set; }
    }

    /// <summary>学校部门学生列表结果。</summary>
    public class SchoolStudentListResult : WorkJsonResult
    {
        /// <summary>学生列表。</summary>
        public IList<SchoolStudentInfo> students { get; set; }
    }

    /// <summary>学校部门家长列表结果。</summary>
    public class SchoolParentListResult : WorkJsonResult
    {
        /// <summary>家长列表。</summary>
        public IList<SchoolParentInfo> parents { get; set; }
    }

    /// <summary>批量学生或家长操作的单项结果。</summary>
    public class SchoolUserBatchOperationResult : WorkJsonResult
    {
        /// <summary>学生 UserId；学生操作时返回。</summary>
        public string student_userid { get; set; }

        /// <summary>家长 UserId；家长操作时返回。</summary>
        public string parent_userid { get; set; }
    }

    /// <summary>批量学生或家长操作结果。</summary>
    public class SchoolUserBatchResult : WorkJsonResult
    {
        /// <summary>逐项操作结果。</summary>
        public IList<SchoolUserBatchOperationResult> result_list { get; set; }
    }
}
