using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Todo;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Todo
{
    [TestClass]
    public class TodoContractTests
    {
        [TestMethod]
        public void TodoApiContainsTwoSyncAndAsyncEntries()
        {
            var methodNames = typeof(TodoApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            CollectionAssert.Contains(methodNames, nameof(TodoApi.GetTodo));
            CollectionAssert.Contains(methodNames, nameof(TodoApi.GetTodoAsync));
            CollectionAssert.Contains(methodNames, nameof(TodoApi.UpdateTodo));
            CollectionAssert.Contains(methodNames, nameof(TodoApi.UpdateTodoAsync));
        }

        [TestMethod]
        public void TodoApiUsesOfficialPostPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Todo", "TodoApi.cs"));

            StringAssert.Contains(source, "/cgi-bin/todo/get");
            StringAssert.Contains(source, "/cgi-bin/todo/update");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
        }

        [TestMethod]
        public void TodoUpdateRequestUsesOfficialJsonFields()
        {
            var request = new UpdateTodoRequest
            {
                todo_id = "todo-1",
                status = 1,
                attendees = new List<TodoAttendee>
                {
                    new TodoAttendee { userid = "zhangsan", status = 0 }
                }
            };

            var json = JsonSerializer.Serialize(request);

            StringAssert.Contains(json, "\"todo_id\":\"todo-1\"");
            StringAssert.Contains(json, "\"status\":1");
            StringAssert.Contains(json, "\"attendees\"");
            StringAssert.Contains(json, "\"userid\":\"zhangsan\"");
        }

        [TestMethod]
        public void TodoResultPreservesOfficialFieldsAndLargeTimestamps()
        {
            var result = JsonSerializer.Deserialize<GetTodoResult>(
                "{\"errcode\":0,\"content\":\"完成述职PPT\",\"creator\":\"zhangsan\"," +
                "\"status\":1,\"create_time\":5178368698,\"end_time\":5178368799," +
                "\"attendees\":[{\"userid\":\"lisi\",\"status\":0}]," +
                "\"reminders\":[{\"remind_time\":5178368600}]}" );

            Assert.IsNotNull(result);
            Assert.AreEqual("完成述职PPT", result.content);
            Assert.AreEqual(5178368698L, result.create_time);
            Assert.AreEqual(5178368799L, result.end_time);
            Assert.AreEqual("lisi", result.attendees[0].userid);
            Assert.AreEqual(5178368600L, result.reminders[0].remind_time);
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
