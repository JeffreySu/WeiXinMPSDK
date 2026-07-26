/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SchoolApi.User.cs
    文件功能描述：企业微信家校配置、学生和家长管理接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增家校配置、身份查询、学生与家长管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.School
{
    /// <summary>企业微信家校配置、学生和家长管理接口。</summary>
    public static partial class SchoolApi
    {
        private const string SetTeacherViewModePath = "/cgi-bin/school/set_teacher_view_mode";
        private const string GetTeacherViewModePath = "/cgi-bin/school/get_teacher_view_mode";
        private const string SetArchSyncModePath = "/cgi-bin/school/set_arch_sync_mode";
        private const string GetSchoolUserInfoPath = "/cgi-bin/school/getuserinfo";
        private const string GetSchoolUserPath = "/cgi-bin/school/user/get";
        private const string GetSchoolUserListPath = "/cgi-bin/school/user/list";
        private const string GetSchoolParentListPath = "/cgi-bin/school/user/list_parent";
        private const string CreateStudentPath = "/cgi-bin/school/user/create_student";
        private const string DeleteStudentPath = "/cgi-bin/school/user/delete_student";
        private const string UpdateStudentPath = "/cgi-bin/school/user/update_student";
        private const string BatchCreateStudentPath = "/cgi-bin/school/user/batch_create_student";
        private const string BatchDeleteStudentPath = "/cgi-bin/school/user/batch_delete_student";
        private const string BatchUpdateStudentPath = "/cgi-bin/school/user/batch_update_student";
        private const string CreateParentPath = "/cgi-bin/school/user/create_parent";
        private const string DeleteParentPath = "/cgi-bin/school/user/delete_parent";
        private const string UpdateParentPath = "/cgi-bin/school/user/update_parent";
        private const string BatchCreateParentPath = "/cgi-bin/school/user/batch_create_parent";
        private const string BatchDeleteParentPath = "/cgi-bin/school/user/batch_delete_parent";
        private const string BatchUpdateParentPath = "/cgi-bin/school/user/batch_update_parent";

        /// <summary>设置老师可查看班级的模式。<see href="https://developer.work.weixin.qq.com/document/path/92652"/></summary>
        public static WorkJsonResult SetTeacherViewMode(string accessTokenOrAppKey,
            SchoolTeacherViewModeRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetTeacherViewModePath, request, timeOut);

        /// <summary>异步设置老师可查看班级的模式。<see href="https://developer.work.weixin.qq.com/document/path/92652"/></summary>
        public static Task<WorkJsonResult> SetTeacherViewModeAsync(string accessTokenOrAppKey,
            SchoolTeacherViewModeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetTeacherViewModePath, request, timeOut);

        /// <summary>获取老师可查看班级的模式。<see href="https://developer.work.weixin.qq.com/document/path/92652"/></summary>
        public static SchoolTeacherViewModeResult GetTeacherViewMode(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolTeacherViewModeResult>(accessTokenOrAppKey, GetTeacherViewModePath, null, timeOut);

        /// <summary>异步获取老师可查看班级的模式。<see href="https://developer.work.weixin.qq.com/document/path/92652"/></summary>
        public static Task<SchoolTeacherViewModeResult> GetTeacherViewModeAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolTeacherViewModeResult>(accessTokenOrAppKey, GetTeacherViewModePath, null, timeOut);

        /// <summary>设置家校通讯录与班级标签的自动同步模式。此设置不可逆。<see href="https://developer.work.weixin.qq.com/document/path/92345"/></summary>
        public static WorkJsonResult SetArchSyncMode(string accessTokenOrAppKey,
            SchoolArchSyncModeRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetArchSyncModePath, request, timeOut);

        /// <summary>异步设置家校通讯录与班级标签的自动同步模式。此设置不可逆。<see href="https://developer.work.weixin.qq.com/document/path/92345"/></summary>
        public static Task<WorkJsonResult> SetArchSyncModeAsync(string accessTokenOrAppKey,
            SchoolArchSyncModeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetArchSyncModePath, request, timeOut);

        /// <summary>根据授权 code 获取家校访问用户身份。<see href="https://developer.work.weixin.qq.com/document/path/95791"/></summary>
        public static SchoolUserIdentityResult GetSchoolUserInfo(string accessTokenOrAppKey, string code,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolUserIdentityResult>(accessTokenOrAppKey, GetSchoolUserInfoPath,
                "code=" + code.AsUrlData(), timeOut);

        /// <summary>异步根据授权 code 获取家校访问用户身份。<see href="https://developer.work.weixin.qq.com/document/path/95791"/></summary>
        public static Task<SchoolUserIdentityResult> GetSchoolUserInfoAsync(string accessTokenOrAppKey, string code,
            int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolUserIdentityResult>(accessTokenOrAppKey, GetSchoolUserInfoPath,
                "code=" + code.AsUrlData(), timeOut);

        /// <summary>读取学生或家长。<see href="https://developer.work.weixin.qq.com/document/path/92337"/></summary>
        public static SchoolUserResult GetSchoolUser(string accessTokenOrAppKey, string userId,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolUserResult>(accessTokenOrAppKey, GetSchoolUserPath,
                "userid=" + userId.AsUrlData(), timeOut);

        /// <summary>异步读取学生或家长。<see href="https://developer.work.weixin.qq.com/document/path/92337"/></summary>
        public static Task<SchoolUserResult> GetSchoolUserAsync(string accessTokenOrAppKey, string userId,
            int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolUserResult>(accessTokenOrAppKey, GetSchoolUserPath,
                "userid=" + userId.AsUrlData(), timeOut);

        /// <summary>获取学校部门成员详情。<see href="https://developer.work.weixin.qq.com/document/path/92338"/></summary>
        public static SchoolStudentListResult GetSchoolUserList(string accessTokenOrAppKey, long departmentId,
            bool? fetchChild = null, int timeOut = Config.TIME_OUT)
            => Get<SchoolStudentListResult>(accessTokenOrAppKey, GetSchoolUserListPath,
                "department_id=" + departmentId +
                (fetchChild.HasValue ? "&fetch_child=" + (fetchChild.Value ? 1 : 0) : string.Empty), timeOut);

        /// <summary>异步获取学校部门成员详情。<see href="https://developer.work.weixin.qq.com/document/path/92338"/></summary>
        public static Task<SchoolStudentListResult> GetSchoolUserListAsync(string accessTokenOrAppKey,
            long departmentId, bool? fetchChild = null, int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolStudentListResult>(accessTokenOrAppKey, GetSchoolUserListPath,
                "department_id=" + departmentId +
                (fetchChild.HasValue ? "&fetch_child=" + (fetchChild.Value ? 1 : 0) : string.Empty), timeOut);

        /// <summary>获取学校部门家长详情。<see href="https://developer.work.weixin.qq.com/document/path/92446"/></summary>
        public static SchoolParentListResult GetSchoolParentList(string accessTokenOrAppKey, long departmentId,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolParentListResult>(accessTokenOrAppKey, GetSchoolParentListPath,
                "department_id=" + departmentId, timeOut);

        /// <summary>异步获取学校部门家长详情。<see href="https://developer.work.weixin.qq.com/document/path/92446"/></summary>
        public static Task<SchoolParentListResult> GetSchoolParentListAsync(string accessTokenOrAppKey,
            long departmentId, int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolParentListResult>(accessTokenOrAppKey, GetSchoolParentListPath,
                "department_id=" + departmentId, timeOut);

        /// <summary>创建学生。<see href="https://developer.work.weixin.qq.com/document/path/92325"/></summary>
        public static WorkJsonResult CreateStudent(string accessTokenOrAppKey, SchoolStudent request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CreateStudentPath, request, timeOut);

        /// <summary>异步创建学生。<see href="https://developer.work.weixin.qq.com/document/path/92325"/></summary>
        public static Task<WorkJsonResult> CreateStudentAsync(string accessTokenOrAppKey, SchoolStudent request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CreateStudentPath, request, timeOut);

        /// <summary>删除学生。<see href="https://developer.work.weixin.qq.com/document/path/92326"/></summary>
        public static WorkJsonResult DeleteStudent(string accessTokenOrAppKey, string studentUserId,
            int timeOut = Config.TIME_OUT)
            => Get<WorkJsonResult>(accessTokenOrAppKey, DeleteStudentPath,
                "userid=" + studentUserId.AsUrlData(), timeOut);

        /// <summary>异步删除学生。<see href="https://developer.work.weixin.qq.com/document/path/92326"/></summary>
        public static Task<WorkJsonResult> DeleteStudentAsync(string accessTokenOrAppKey, string studentUserId,
            int timeOut = Config.TIME_OUT)
            => GetAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteStudentPath,
                "userid=" + studentUserId.AsUrlData(), timeOut);

        /// <summary>更新学生。<see href="https://developer.work.weixin.qq.com/document/path/92327"/></summary>
        public static WorkJsonResult UpdateStudent(string accessTokenOrAppKey, SchoolStudent request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateStudentPath, request, timeOut);

        /// <summary>异步更新学生。<see href="https://developer.work.weixin.qq.com/document/path/92327"/></summary>
        public static Task<WorkJsonResult> UpdateStudentAsync(string accessTokenOrAppKey, SchoolStudent request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateStudentPath, request, timeOut);

        /// <summary>批量创建学生。<see href="https://developer.work.weixin.qq.com/document/path/92328"/></summary>
        public static SchoolUserBatchResult BatchCreateStudent(string accessTokenOrAppKey,
            SchoolStudentBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolUserBatchResult>(accessTokenOrAppKey, BatchCreateStudentPath, request, timeOut);

        /// <summary>异步批量创建学生。<see href="https://developer.work.weixin.qq.com/document/path/92328"/></summary>
        public static Task<SchoolUserBatchResult> BatchCreateStudentAsync(string accessTokenOrAppKey,
            SchoolStudentBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolUserBatchResult>(accessTokenOrAppKey, BatchCreateStudentPath, request, timeOut);

        /// <summary>批量删除学生。<see href="https://developer.work.weixin.qq.com/document/path/92329"/></summary>
        public static SchoolUserBatchResult BatchDeleteStudent(string accessTokenOrAppKey,
            SchoolUserIdListRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolUserBatchResult>(accessTokenOrAppKey, BatchDeleteStudentPath, request, timeOut);

        /// <summary>异步批量删除学生。<see href="https://developer.work.weixin.qq.com/document/path/92329"/></summary>
        public static Task<SchoolUserBatchResult> BatchDeleteStudentAsync(string accessTokenOrAppKey,
            SchoolUserIdListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolUserBatchResult>(accessTokenOrAppKey, BatchDeleteStudentPath, request, timeOut);

        /// <summary>批量更新学生。<see href="https://developer.work.weixin.qq.com/document/path/92330"/></summary>
        public static SchoolUserBatchResult BatchUpdateStudent(string accessTokenOrAppKey,
            SchoolStudentBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolUserBatchResult>(accessTokenOrAppKey, BatchUpdateStudentPath, request, timeOut);

        /// <summary>异步批量更新学生。<see href="https://developer.work.weixin.qq.com/document/path/92330"/></summary>
        public static Task<SchoolUserBatchResult> BatchUpdateStudentAsync(string accessTokenOrAppKey,
            SchoolStudentBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolUserBatchResult>(accessTokenOrAppKey, BatchUpdateStudentPath, request, timeOut);

        /// <summary>创建家长。<see href="https://developer.work.weixin.qq.com/document/path/92331"/></summary>
        public static WorkJsonResult CreateParent(string accessTokenOrAppKey, SchoolParent request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, CreateParentPath, request, timeOut);

        /// <summary>异步创建家长。<see href="https://developer.work.weixin.qq.com/document/path/92331"/></summary>
        public static Task<WorkJsonResult> CreateParentAsync(string accessTokenOrAppKey, SchoolParent request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, CreateParentPath, request, timeOut);

        /// <summary>删除家长。<see href="https://developer.work.weixin.qq.com/document/path/92332"/></summary>
        public static WorkJsonResult DeleteParent(string accessTokenOrAppKey, string parentUserId,
            int timeOut = Config.TIME_OUT)
            => Get<WorkJsonResult>(accessTokenOrAppKey, DeleteParentPath,
                "userid=" + parentUserId.AsUrlData(), timeOut);

        /// <summary>异步删除家长。<see href="https://developer.work.weixin.qq.com/document/path/92332"/></summary>
        public static Task<WorkJsonResult> DeleteParentAsync(string accessTokenOrAppKey, string parentUserId,
            int timeOut = Config.TIME_OUT)
            => GetAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteParentPath,
                "userid=" + parentUserId.AsUrlData(), timeOut);

        /// <summary>更新家长。<see href="https://developer.work.weixin.qq.com/document/path/92333"/></summary>
        public static WorkJsonResult UpdateParent(string accessTokenOrAppKey, SchoolParent request,
            int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateParentPath, request, timeOut);

        /// <summary>异步更新家长。<see href="https://developer.work.weixin.qq.com/document/path/92333"/></summary>
        public static Task<WorkJsonResult> UpdateParentAsync(string accessTokenOrAppKey, SchoolParent request,
            int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateParentPath, request, timeOut);

        /// <summary>批量创建家长。<see href="https://developer.work.weixin.qq.com/document/path/92334"/></summary>
        public static SchoolUserBatchResult BatchCreateParent(string accessTokenOrAppKey,
            SchoolParentBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolUserBatchResult>(accessTokenOrAppKey, BatchCreateParentPath, request, timeOut);

        /// <summary>异步批量创建家长。<see href="https://developer.work.weixin.qq.com/document/path/92334"/></summary>
        public static Task<SchoolUserBatchResult> BatchCreateParentAsync(string accessTokenOrAppKey,
            SchoolParentBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolUserBatchResult>(accessTokenOrAppKey, BatchCreateParentPath, request, timeOut);

        /// <summary>批量删除家长。<see href="https://developer.work.weixin.qq.com/document/path/92335"/></summary>
        public static SchoolUserBatchResult BatchDeleteParent(string accessTokenOrAppKey,
            SchoolUserIdListRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolUserBatchResult>(accessTokenOrAppKey, BatchDeleteParentPath, request, timeOut);

        /// <summary>异步批量删除家长。<see href="https://developer.work.weixin.qq.com/document/path/92335"/></summary>
        public static Task<SchoolUserBatchResult> BatchDeleteParentAsync(string accessTokenOrAppKey,
            SchoolUserIdListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolUserBatchResult>(accessTokenOrAppKey, BatchDeleteParentPath, request, timeOut);

        /// <summary>批量更新家长。<see href="https://developer.work.weixin.qq.com/document/path/92336"/></summary>
        public static SchoolUserBatchResult BatchUpdateParent(string accessTokenOrAppKey,
            SchoolParentBatchRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolUserBatchResult>(accessTokenOrAppKey, BatchUpdateParentPath, request, timeOut);

        /// <summary>异步批量更新家长。<see href="https://developer.work.weixin.qq.com/document/path/92336"/></summary>
        public static Task<SchoolUserBatchResult> BatchUpdateParentAsync(string accessTokenOrAppKey,
            SchoolParentBatchRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolUserBatchResult>(accessTokenOrAppKey, BatchUpdateParentPath, request, timeOut);
    }
}
