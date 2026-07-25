using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.HumanResources;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.HumanResources
{
    [TestClass]
    public class HumanResourcesContractTests
    {
        [TestMethod]
        public void HumanResourcesApiContainsThreeSyncAndAsyncEntries()
        {
            var methodNames = typeof(HumanResourcesApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var syncMethodName in new[]
            {
                nameof(HumanResourcesApi.GetFields),
                nameof(HumanResourcesApi.GetStaffInfo),
                nameof(HumanResourcesApi.UpdateStaffInfo)
            })
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void HumanResourcesApiUsesOfficialMethodsAndPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "HumanResources", "HumanResourcesApi.cs"));

            StringAssert.Contains(source, "/cgi-bin/hr/get_fields");
            StringAssert.Contains(source, "/cgi-bin/hr/get_staff_info");
            StringAssert.Contains(source, "/cgi-bin/hr/update_staff_info");
            StringAssert.Contains(source, "CommonJsonSendType.GET");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
        }

        [TestMethod]
        public void StaffInfoRequestUsesOfficialJsonFields()
        {
            var json = JsonSerializer.Serialize(new GetStaffInfoRequest
            {
                userid = "zhangsan",
                get_all = false,
                fieldids = new List<StaffFieldSelector>
                {
                    new StaffFieldSelector { fieldid = 11004, sub_idx = 0 },
                    new StaffFieldSelector { fieldid = 14001, sub_idx = 1 }
                }
            });

            StringAssert.Contains(json, "\"userid\":\"zhangsan\"");
            StringAssert.Contains(json, "\"get_all\":false");
            StringAssert.Contains(json, "\"fieldid\":11004");
            StringAssert.Contains(json, "\"sub_idx\":1");
        }

        [TestMethod]
        public void StaffValueModelsPreserveUnsignedAndSigned64BitValues()
        {
            var result = JsonSerializer.Deserialize<GetStaffInfoResult>(
                "{\"errcode\":0,\"field_info\":[" +
                "{\"fieldid\":1,\"sub_idx\":0,\"result\":1,\"value_type\":2," +
                "\"value_uint64\":18446744073709551615}," +
                "{\"fieldid\":2,\"sub_idx\":0,\"result\":1,\"value_type\":4," +
                "\"value_int64\":-5178368698}," +
                "{\"fieldid\":3,\"sub_idx\":0,\"result\":1,\"value_type\":5," +
                "\"value_mobile\":{\"value_country_code\":\"86\",\"value_mobile\":\"13800000000\"}}]}" );

            Assert.IsNotNull(result);
            Assert.AreEqual(ulong.MaxValue, result.field_info[0].value_uint64);
            Assert.AreEqual(-5178368698L, result.field_info[1].value_int64);
            Assert.AreEqual("86", result.field_info[2].value_mobile.value_country_code);
            Assert.AreEqual("13800000000", result.field_info[2].value_mobile.value_mobile);
        }

        [TestMethod]
        public void StaffUpdateSupportsAllOperationsAndOfficialResponseVariants()
        {
            var json = JsonSerializer.Serialize(new UpdateStaffInfoRequest
            {
                userid = "zhangsan",
                update_items = new List<StaffFieldValueInput>
                {
                    new StaffFieldValueInput { fieldid = 11020, sub_idx = 0, value_string = "研发" }
                },
                remove_items = new List<StaffGroupRemoveItem>
                {
                    new StaffGroupRemoveItem { group_type = 1, sub_idx = 2 }
                },
                insert_items = new List<StaffGroupInsertItem>
                {
                    new StaffGroupInsertItem
                    {
                        group_type = 4,
                        item = new List<StaffFieldValueInput>
                        {
                            new StaffFieldValueInput
                            {
                                fieldid = 17003,
                                value_mobile = new StaffMobileValue
                                {
                                    value_country_code = "86",
                                    value_mobile = "13800000000"
                                }
                            }
                        }
                    }
                }
            });
            var singular = JsonSerializer.Deserialize<UpdateStaffInfoResult>(
                "{\"errcode\":0,\"insert_result\":[{\"group_type\":4,\"idx\":0,\"result\":1}]}" );
            var plural = JsonSerializer.Deserialize<UpdateStaffInfoResult>(
                "{\"errcode\":0,\"insert_results\":[{\"group_type\":4,\"idx\":0,\"result\":1}]}" );

            StringAssert.Contains(json, "\"update_items\":[");
            StringAssert.Contains(json, "\"remove_items\":[");
            StringAssert.Contains(json, "\"insert_items\":[");
            StringAssert.Contains(json, "\"value_country_code\":\"86\"");
            Assert.IsNotNull(singular);
            Assert.AreEqual(1, singular.insert_result[0].result);
            Assert.IsNotNull(plural);
            Assert.AreEqual(1, plural.insert_results[0].result);
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
