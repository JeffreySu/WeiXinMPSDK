using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Meeting;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Meeting
{
    [TestClass]
    public class MeetingLayoutContractTests
    {
        [TestMethod]
        public void LayoutApiContainsFifteenSyncAndAsyncEntries()
        {
            var getParameterTypes = new[] { typeof(string), typeof(int) };
            var getMethod = typeof(MeetingApi).GetMethod(nameof(MeetingApi.GetMeetingLayoutTemplates),
                getParameterTypes);
            var getAsyncMethod = typeof(MeetingApi).GetMethod(
                nameof(MeetingApi.GetMeetingLayoutTemplatesAsync), getParameterTypes);

            Assert.IsNotNull(getMethod);
            Assert.AreEqual(typeof(GetMeetingLayoutTemplatesResult), getMethod.ReturnType);
            Assert.IsNotNull(getAsyncMethod);
            Assert.AreEqual(typeof(Task<GetMeetingLayoutTemplatesResult>), getAsyncMethod.ReturnType);

            var contracts = new[]
            {
                (nameof(MeetingApi.AddMeetingLayouts), typeof(AddMeetingLayoutsRequest),
                    typeof(AddMeetingLayoutsResult)),
                (nameof(MeetingApi.UpdateMeetingLayout), typeof(UpdateMeetingLayoutRequest),
                    typeof(UpdateMeetingLayoutResult)),
                (nameof(MeetingApi.SetDefaultMeetingLayout), typeof(SetDefaultMeetingLayoutRequest),
                    typeof(SetDefaultMeetingLayoutResult)),
                (nameof(MeetingApi.AddMeetingAdvancedLayouts), typeof(AddMeetingAdvancedLayoutsRequest),
                    typeof(AddMeetingAdvancedLayoutsResult)),
                (nameof(MeetingApi.UpdateMeetingAdvancedLayout), typeof(UpdateMeetingAdvancedLayoutRequest),
                    typeof(UpdateMeetingAdvancedLayoutResult)),
                (nameof(MeetingApi.ApplyMeetingAdvancedLayout), typeof(ApplyMeetingAdvancedLayoutRequest),
                    typeof(ApplyMeetingAdvancedLayoutResult)),
                (nameof(MeetingApi.GetMeetingAdvancedLayouts), typeof(GetMeetingAdvancedLayoutsRequest),
                    typeof(GetMeetingAdvancedLayoutsResult)),
                (nameof(MeetingApi.GetMeetingUserLayout), typeof(GetMeetingUserLayoutRequest),
                    typeof(GetMeetingUserLayoutResult)),
                (nameof(MeetingApi.DeleteMeetingAdvancedLayouts), typeof(DeleteMeetingAdvancedLayoutsRequest),
                    typeof(DeleteMeetingAdvancedLayoutsResult)),
                (nameof(MeetingApi.AddMeetingLayoutBackgrounds), typeof(AddMeetingLayoutBackgroundsRequest),
                    typeof(AddMeetingLayoutBackgroundsResult)),
                (nameof(MeetingApi.SetDefaultMeetingLayoutBackground),
                    typeof(SetDefaultMeetingLayoutBackgroundRequest),
                    typeof(SetDefaultMeetingLayoutBackgroundResult)),
                (nameof(MeetingApi.GetMeetingLayoutBackgrounds), typeof(GetMeetingLayoutBackgroundsRequest),
                    typeof(GetMeetingLayoutBackgroundsResult)),
                (nameof(MeetingApi.DeleteMeetingLayoutBackground),
                    typeof(DeleteMeetingLayoutBackgroundRequest),
                    typeof(DeleteMeetingLayoutBackgroundResult)),
                (nameof(MeetingApi.DeleteMeetingLayoutBackgrounds),
                    typeof(DeleteMeetingLayoutBackgroundsRequest),
                    typeof(DeleteMeetingLayoutBackgroundsResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = new[] { typeof(string), contract.Item2, typeof(int) };
                var syncMethod = typeof(MeetingApi).GetMethod(contract.Item1, parameterTypes);
                var asyncMethod = typeof(MeetingApi).GetMethod(contract.Item1 + "Async", parameterTypes);

                Assert.IsNotNull(syncMethod, contract.Item1);
                Assert.AreEqual(contract.Item3, syncMethod.ReturnType, contract.Item1);
                Assert.IsNotNull(asyncMethod, contract.Item1 + "Async");
                Assert.AreEqual(typeof(Task<>).MakeGenericType(contract.Item3), asyncMethod.ReturnType,
                    contract.Item1 + "Async");
            }
        }

        [TestMethod]
        public void LayoutApiUsesOfficialPathsDocumentsMethodsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.Layout.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/layout/list_template",
                "/cgi-bin/meeting/layout/add",
                "/cgi-bin/meeting/layout/update",
                "/cgi-bin/meeting/layout/set_default",
                "/cgi-bin/meeting/advanced_layout/add",
                "/cgi-bin/meeting/advanced_layout/update",
                "/cgi-bin/meeting/advanced_layout/apply",
                "/cgi-bin/meeting/advanced_layout/list",
                "/cgi-bin/meeting/advanced_layout/get_user_layout",
                "/cgi-bin/meeting/advanced_layout/batch_delete",
                "/cgi-bin/meeting/layout/add_background",
                "/cgi-bin/meeting/layout/set_default_background",
                "/cgi-bin/meeting/layout/list_background",
                "/cgi-bin/meeting/layout/delete_background",
                "/cgi-bin/meeting/layout/batch_delete_background"
            };
            var documentIds = new[]
            {
                98844, 98845, 98846, 98847, 98861, 98868, 98869, 98862,
                98865, 98866, 98851, 98852, 98856, 98853, 98854
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId),
                    documentId.ToString());
            }

            Assert.AreEqual(30, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(30, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(28, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(30, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(30, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(1, CountOccurrences(source, "=> Get<"));
            Assert.AreEqual(1, CountOccurrences(source, "=> GetAsync<"));
            Assert.AreEqual(14, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(14, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void BasicLayoutRequestsAndResultsPreserveOfficialShape()
        {
            var request = new AddMeetingLayoutsRequest
            {
                meetingid = "meeting-1",
                default_layout_order = 1,
                layout_list = new List<MeetingLayoutDefinition>
                {
                    new MeetingLayoutDefinition
                    {
                        page_list = new List<MeetingLayoutPage>
                        {
                            new MeetingLayoutPage
                            {
                                layout_template_id = "template-1",
                                user_seat_list = new List<MeetingLayoutSeat>
                                {
                                    new MeetingLayoutSeat
                                    {
                                        grid_id = "grid-1", grid_type = 2, userid = "zhangsan",
                                        tmp_openid = "tmp-1", nick_name = "张三", tool_sdkid = "tool-1"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
            var root = document.RootElement;
            var seat = root.GetProperty("layout_list")[0].GetProperty("page_list")[0]
                .GetProperty("user_seat_list")[0];

            Assert.AreEqual("meeting-1", root.GetProperty("meetingid").GetString());
            Assert.AreEqual(1, root.GetProperty("default_layout_order").GetInt32());
            Assert.AreEqual("template-1", root.GetProperty("layout_list")[0]
                .GetProperty("page_list")[0].GetProperty("layout_template_id").GetString());
            Assert.AreEqual("zhangsan", seat.GetProperty("userid").GetString());
            Assert.AreEqual("tool-1", seat.GetProperty("tool_sdkid").GetString());

            var result = JsonSerializer.Deserialize<AddMeetingLayoutsResult>(
                "{\"errcode\":0,\"selected_layout_id\":\"layout-1\",\"layout_list\":[{" +
                "\"layout_id\":\"layout-1\",\"page_list\":[{\"layout_template_id\":\"template-1\"," +
                "\"user_seat_list\":[{\"grid_id\":\"grid-1\",\"grid_type\":2," +
                "\"tmp_openid\":\"tmp-1\"}]}]}]}");

            Assert.IsNotNull(result);
            Assert.AreEqual("layout-1", result.selected_layout_id);
            Assert.AreEqual("layout-1", result.layout_list[0].layout_id);
            Assert.AreEqual("grid-1", result.layout_list[0].page_list[0].user_seat_list[0].grid_id);
        }

        [TestMethod]
        public void AdvancedLayoutPreservesPollingSeatsUsersAndApplicationTargets()
        {
            var request = new AddMeetingAdvancedLayoutsRequest
            {
                meetingid = "meeting-1",
                layout_list = new List<MeetingAdvancedLayoutDefinition>
                {
                    new MeetingAdvancedLayoutDefinition
                    {
                        layout_name = "主持人视图",
                        page_list = new List<MeetingAdvancedLayoutPage>
                        {
                            new MeetingAdvancedLayoutPage
                            {
                                layout_template_id = "template-2",
                                enable_polling = true,
                                polling_setting = new MeetingAdvancedLayoutPollingSetting
                                {
                                    polling_interval_unit = 2,
                                    polling_interval = 30,
                                    ignore_user_novideo = true,
                                    ignore_user_absence = false
                                },
                                user_seat_list = new List<MeetingAdvancedLayoutSeat>
                                {
                                    new MeetingAdvancedLayoutSeat
                                    {
                                        grid_id = "grid-2", grid_type = 3, video_type = 1,
                                        user_list = new List<MeetingAdvancedLayoutUser>
                                        {
                                            new MeetingAdvancedLayoutUser
                                            {
                                                userid = "lisi", tmp_openid = "tmp-2", nick_name = "李四"
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
            var page = document.RootElement.GetProperty("layout_list")[0].GetProperty("page_list")[0];
            var polling = page.GetProperty("polling_setting");
            var seat = page.GetProperty("user_seat_list")[0];

            Assert.AreEqual(JsonValueKind.True, page.GetProperty("enable_polling").ValueKind);
            Assert.AreEqual(30, polling.GetProperty("polling_interval").GetInt32());
            Assert.AreEqual(JsonValueKind.True, polling.GetProperty("ignore_user_novideo").ValueKind);
            Assert.AreEqual(JsonValueKind.False, polling.GetProperty("ignore_user_absence").ValueKind);
            Assert.AreEqual(1, seat.GetProperty("video_type").GetInt32());
            Assert.AreEqual("tmp-2", seat.GetProperty("user_list")[0]
                .GetProperty("tmp_openid").GetString());

            var apply = new ApplyMeetingAdvancedLayoutRequest
            {
                meetingid = "meeting-1",
                layout_id = "layout-2",
                user_list = new List<MeetingAdvancedLayoutApplyUser>
                {
                    new MeetingAdvancedLayoutApplyUser { tmp_openid = "tmp-2" }
                }
            };
            using var applyDocument = JsonDocument.Parse(JsonSerializer.Serialize(apply));
            Assert.AreEqual("tmp-2", applyDocument.RootElement.GetProperty("user_list")[0]
                .GetProperty("tmp_openid").GetString());

            var userLayout = JsonSerializer.Deserialize<GetMeetingUserLayoutResult>(
                "{\"errcode\":0,\"selected_layout_id\":\"layout-2\",\"layout_name\":\"主持人视图\"," +
                "\"layout_type\":2,\"page_list\":[{\"layout_template_id\":\"template-2\"," +
                "\"enable_polling\":true,\"polling_setting\":{\"polling_interval_unit\":2," +
                "\"polling_interval\":30,\"ignore_user_novideo\":true," +
                "\"ignore_user_absence\":false},\"user_seat_list\":[]}]}");

            Assert.IsNotNull(userLayout);
            Assert.AreEqual("layout-2", userLayout.selected_layout_id);
            Assert.AreEqual(2, userLayout.layout_type);
            Assert.AreEqual(30, userLayout.page_list[0].polling_setting.polling_interval);
        }

        [TestMethod]
        public void LayoutBackgroundRequestsAndResultsPreserveOfficialShape()
        {
            var request = new AddMeetingLayoutBackgroundsRequest
            {
                meetingid = "meeting-1",
                default_image_order = 0,
                image_list = new List<MeetingLayoutBackgroundImageRequest>
                {
                    new MeetingLayoutBackgroundImageRequest
                    {
                        image_url = "https://example.test/background.png",
                        image_md5 = "0123456789abcdef"
                    }
                }
            };
            using var requestDocument = JsonDocument.Parse(JsonSerializer.Serialize(request));
            var image = requestDocument.RootElement.GetProperty("image_list")[0];
            Assert.AreEqual(0, requestDocument.RootElement.GetProperty("default_image_order").GetInt32());
            Assert.AreEqual("https://example.test/background.png", image.GetProperty("image_url").GetString());
            Assert.AreEqual("0123456789abcdef", image.GetProperty("image_md5").GetString());

            var result = JsonSerializer.Deserialize<GetMeetingLayoutBackgroundsResult>(
                "{\"errcode\":0,\"selected_background_id\":\"background-1\"," +
                "\"background_list\":[{\"background_id\":\"background-1\"," +
                "\"image_md5\":\"0123456789abcdef\"}]}");
            var batchDelete = new DeleteMeetingLayoutBackgroundsRequest
            {
                meetingid = "meeting-1", background_id_list = new List<string> { "background-1" }
            };
            using var deleteDocument = JsonDocument.Parse(JsonSerializer.Serialize(batchDelete));

            Assert.IsNotNull(result);
            Assert.AreEqual("background-1", result.selected_background_id);
            Assert.AreEqual("0123456789abcdef", result.background_list[0].image_md5);
            Assert.AreEqual("background-1", deleteDocument.RootElement.GetProperty("background_id_list")[0]
                .GetString());
        }

        [TestMethod]
        public void LayoutPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(MeetingLayoutTemplate), typeof(GetMeetingLayoutTemplatesResult),
                typeof(MeetingLayoutSeat), typeof(MeetingLayoutPage), typeof(MeetingLayoutDefinition),
                typeof(MeetingLayoutInfo), typeof(AddMeetingLayoutsRequest), typeof(AddMeetingLayoutsResult),
                typeof(UpdateMeetingLayoutRequest), typeof(UpdateMeetingLayoutResult),
                typeof(SetDefaultMeetingLayoutRequest), typeof(SetDefaultMeetingLayoutResult),
                typeof(MeetingAdvancedLayoutUser), typeof(MeetingAdvancedLayoutSeat),
                typeof(MeetingAdvancedLayoutPollingSetting), typeof(MeetingAdvancedLayoutPage),
                typeof(MeetingAdvancedLayoutDefinition), typeof(MeetingAdvancedLayoutInfo),
                typeof(AddMeetingAdvancedLayoutsRequest), typeof(AddMeetingAdvancedLayoutsResult),
                typeof(UpdateMeetingAdvancedLayoutRequest), typeof(UpdateMeetingAdvancedLayoutResult),
                typeof(MeetingAdvancedLayoutApplyUser), typeof(ApplyMeetingAdvancedLayoutRequest),
                typeof(ApplyMeetingAdvancedLayoutResult), typeof(GetMeetingAdvancedLayoutsRequest),
                typeof(GetMeetingAdvancedLayoutsResult), typeof(GetMeetingUserLayoutRequest),
                typeof(GetMeetingUserLayoutResult), typeof(DeleteMeetingAdvancedLayoutsRequest),
                typeof(DeleteMeetingAdvancedLayoutsResult), typeof(MeetingLayoutBackgroundImageRequest),
                typeof(MeetingLayoutBackgroundInfo), typeof(AddMeetingLayoutBackgroundsRequest),
                typeof(AddMeetingLayoutBackgroundsResult), typeof(SetDefaultMeetingLayoutBackgroundRequest),
                typeof(SetDefaultMeetingLayoutBackgroundResult), typeof(GetMeetingLayoutBackgroundsRequest),
                typeof(GetMeetingLayoutBackgroundsResult), typeof(DeleteMeetingLayoutBackgroundRequest),
                typeof(DeleteMeetingLayoutBackgroundResult), typeof(DeleteMeetingLayoutBackgroundsRequest),
                typeof(DeleteMeetingLayoutBackgroundsResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
                if (property.PropertyType.IsGenericType)
                {
                    CollectionAssert.DoesNotContain(property.PropertyType.GetGenericArguments(), typeof(object));
                }
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingLayoutJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ", StringComparison.Ordinal) ||
                               line.StartsWith("public ", StringComparison.Ordinal) &&
                               line.Contains("{ get; set; }", StringComparison.Ordinal));
            Assert.AreEqual(declarationCount, CountOccurrences(source, "/// <summary>"));
        }

        private static int CountOccurrences(string source, string value)
            => source.Split(new[] { value }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(sourceFilePath));
            while (directory != null)
            {
                if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            Assert.Fail("Unable to locate repository root.");
            return null;
        }
    }
}
