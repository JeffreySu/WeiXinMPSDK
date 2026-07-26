using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.School;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.School
{
    [TestClass]
    public class SchoolUserContractTests
    {
        [TestMethod]
        public void SchoolApiContainsConfigurationStudentAndParentPairs()
        {
            var methodNames = typeof(SchoolApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var syncMethodName in new[]
            {
                nameof(SchoolApi.SetTeacherViewMode),
                nameof(SchoolApi.GetTeacherViewMode),
                nameof(SchoolApi.SetArchSyncMode),
                nameof(SchoolApi.GetSchoolUserInfo),
                nameof(SchoolApi.GetSchoolUser),
                nameof(SchoolApi.GetSchoolUserList),
                nameof(SchoolApi.GetSchoolParentList),
                nameof(SchoolApi.CreateStudent),
                nameof(SchoolApi.DeleteStudent),
                nameof(SchoolApi.UpdateStudent),
                nameof(SchoolApi.BatchCreateStudent),
                nameof(SchoolApi.BatchDeleteStudent),
                nameof(SchoolApi.BatchUpdateStudent),
                nameof(SchoolApi.CreateParent),
                nameof(SchoolApi.DeleteParent),
                nameof(SchoolApi.UpdateParent),
                nameof(SchoolApi.BatchCreateParent),
                nameof(SchoolApi.BatchDeleteParent),
                nameof(SchoolApi.BatchUpdateParent)
            })
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void SchoolUserApiUsesOfficialPathsQueriesAndEncoding()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "School",
                "SchoolApi.User.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/school/set_teacher_view_mode",
                "/cgi-bin/school/get_teacher_view_mode",
                "/cgi-bin/school/set_arch_sync_mode",
                "/cgi-bin/school/getuserinfo",
                "/cgi-bin/school/user/get",
                "/cgi-bin/school/user/list",
                "/cgi-bin/school/user/list_parent",
                "/cgi-bin/school/user/create_student",
                "/cgi-bin/school/user/delete_student",
                "/cgi-bin/school/user/update_student",
                "/cgi-bin/school/user/batch_create_student",
                "/cgi-bin/school/user/batch_delete_student",
                "/cgi-bin/school/user/batch_update_student",
                "/cgi-bin/school/user/create_parent",
                "/cgi-bin/school/user/delete_parent",
                "/cgi-bin/school/user/update_parent",
                "/cgi-bin/school/user/batch_create_parent",
                "/cgi-bin/school/user/batch_delete_parent",
                "/cgi-bin/school/user/batch_update_parent"
            })
            {
                StringAssert.Contains(source, path, path);
            }

            StringAssert.Contains(source, "\"code=\" + code.AsUrlData()");
            StringAssert.Contains(source, "\"userid=\" + studentUserId.AsUrlData()");
            StringAssert.Contains(source, "\"userid=\" + parentUserId.AsUrlData()");
            StringAssert.Contains(source, "\"department_id=\" + departmentId");
            StringAssert.Contains(source, "\"&fetch_child=\"");
        }

        [TestMethod]
        public void ConfigurationAndIdentityModelsUseOfficialFields()
        {
            var teacherJson = JsonSerializer.Serialize(new SchoolTeacherViewModeRequest { view_mode = 1 });
            var archJson = JsonSerializer.Serialize(new SchoolArchSyncModeRequest { arch_sync_mode = 3 });
            var identity = JsonSerializer.Deserialize<SchoolUserIdentityResult>(
                "{\"errcode\":0,\"parent_userid\":\"parent-1\"," +
                "\"student_userid\":\"student-1\",\"DeviceId\":\"device-1\"}");

            StringAssert.Contains(teacherJson, "\"view_mode\":1");
            StringAssert.Contains(archJson, "\"arch_sync_mode\":3");
            Assert.IsNotNull(identity);
            Assert.AreEqual("parent-1", identity.parent_userid);
            Assert.AreEqual("student-1", identity.student_userid);
            Assert.AreEqual("device-1", identity.DeviceId);
        }

        [TestMethod]
        public void StudentAndParentRequestsPreserveCurrentFieldsAnd64BitDepartmentIds()
        {
            var studentsJson = JsonSerializer.Serialize(new SchoolStudentBatchRequest
            {
                students = new List<SchoolStudent>
                {
                    new SchoolStudent
                    {
                        student_userid = "student-1",
                        new_student_userid = "student-2",
                        name = "张三",
                        department = new List<long> { 5178368698L },
                        to_invite = true,
                        mobile = "13800000000"
                    }
                }
            });
            var parentsJson = JsonSerializer.Serialize(new SchoolParentBatchRequest
            {
                parents = new List<SchoolParent>
                {
                    new SchoolParent
                    {
                        parent_userid = "parent-1",
                        new_parent_userid = "parent-2",
                        mobile = "13900000000",
                        to_invite = false,
                        children = new List<SchoolParentChild>
                        {
                            new SchoolParentChild { student_userid = "student-1", relation = "父亲" }
                        }
                    }
                }
            });
            var deleteJson = JsonSerializer.Serialize(new SchoolUserIdListRequest
            {
                useridlist = new List<string> { "student-1", "student-2" }
            });

            StringAssert.Contains(studentsJson, "\"new_student_userid\":\"student-2\"");
            StringAssert.Contains(studentsJson, "\"department\":[5178368698]");
            StringAssert.Contains(studentsJson, "\"to_invite\":true");
            StringAssert.Contains(parentsJson, "\"new_parent_userid\":\"parent-2\"");
            var parents = JsonSerializer.Deserialize<SchoolParentBatchRequest>(parentsJson);
            Assert.IsNotNull(parents);
            Assert.AreEqual("父亲", parents.parents[0].children[0].relation);
            StringAssert.Contains(deleteJson, "\"useridlist\":[\"student-1\",\"student-2\"]");
        }

        [TestMethod]
        public void SchoolUserAndBatchResultsPreserveNestedProtocolFields()
        {
            var studentResult = JsonSerializer.Deserialize<SchoolUserResult>(
                "{\"errcode\":0,\"user_type\":1,\"student\":{" +
                "\"student_userid\":\"student-1\",\"name\":\"张三\"," +
                "\"department\":[5178368698],\"parents\":[{" +
                "\"parent_userid\":\"parent-1\",\"relation\":\"父亲\"," +
                "\"mobile\":\"13800000000\",\"is_subscribe\":1," +
                "\"external_userid\":\"wm-parent\"}]}}");
            var parentList = JsonSerializer.Deserialize<SchoolParentListResult>(
                "{\"errcode\":0,\"parents\":[{\"parent_userid\":\"parent-1\"," +
                "\"mobile\":\"13800000000\",\"is_subscribe\":1," +
                "\"external_userid\":\"wm-parent\",\"children\":[{" +
                "\"student_userid\":\"student-1\",\"relation\":\"父亲\"," +
                "\"name\":\"张三\"}]}]}");
            var batchResult = JsonSerializer.Deserialize<SchoolUserBatchResult>(
                "{\"errcode\":1,\"errmsg\":\"partial failed\",\"result_list\":[{" +
                "\"student_userid\":\"student-1\",\"errcode\":40058," +
                "\"errmsg\":\"invalid userid\"}]}");

            Assert.IsNotNull(studentResult);
            Assert.AreEqual(5178368698L, studentResult.student.department[0]);
            Assert.AreEqual("wm-parent", studentResult.student.parents[0].external_userid);
            Assert.IsNotNull(parentList);
            Assert.AreEqual("张三", parentList.parents[0].children[0].name);
            Assert.IsNotNull(batchResult);
            Assert.AreEqual("student-1", batchResult.result_list[0].student_userid);
            Assert.AreEqual(40058, batchResult.result_list[0].ErrorCodeValue);
        }

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath) ? null : new DirectoryInfo(startPath);
                while (directory != null)
                {
                    if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                    {
                        return directory.FullName;
                    }

                    directory = directory.Parent;
                }
            }

            Assert.Fail("无法定位仓库根目录。");
            return null;
        }
    }
}
