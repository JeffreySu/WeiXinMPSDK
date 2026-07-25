using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.WeDoc;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.WeDoc
{
    [TestClass]
    public class WeDocContractTests
    {
        [TestMethod]
        public void WeDocApiContainsFortyFiveCurrentAndThreeCompatibilitySyncAsyncEntries()
        {
            var methodNames = typeof(WeDocApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(method => method.Name).ToArray();
            var currentSyncMethods = new[]
            {
                nameof(WeDocApi.CreateDocument),
                nameof(WeDocApi.RenameDocumentOrForm),
                nameof(WeDocApi.DeleteDocumentOrForm),
                nameof(WeDocApi.GetDocumentBaseInfo),
                nameof(WeDocApi.GetShareLink),
                nameof(WeDocApi.GetDocumentAuth),
                nameof(WeDocApi.UpdateDocumentJoinRule),
                nameof(WeDocApi.UpdateDocumentMembers),
                nameof(WeDocApi.UpdateDocumentSafetySetting),
                nameof(WeDocApi.BatchAddDocumentVip),
                nameof(WeDocApi.BatchRemoveDocumentVip),
                nameof(WeDocApi.GetDocumentVipList),
                nameof(WeDocApi.CreateForm),
                nameof(WeDocApi.ModifyForm),
                nameof(WeDocApi.GetFormInfo),
                nameof(WeDocApi.GetFormStatistics),
                nameof(WeDocApi.GetFormAnswers),
                nameof(WeDocApi.BatchUpdateSpreadsheet),
                nameof(WeDocApi.GetSpreadsheetProperties),
                nameof(WeDocApi.GetSpreadsheetRangeData),
                nameof(WeDocApi.UploadDocumentImage),
                nameof(WeDocApi.GetDocumentData),
                nameof(WeDocApi.ModifyDocumentContent),
                nameof(WeDocApi.GetSmartSheetAuth),
                nameof(WeDocApi.ModifySmartSheetAuth),
                nameof(WeDocApi.GetSmartSheets),
                nameof(WeDocApi.AddSmartSheet),
                nameof(WeDocApi.DeleteSmartSheet),
                nameof(WeDocApi.UpdateSmartSheet),
                nameof(WeDocApi.GetSmartSheetViews),
                nameof(WeDocApi.AddSmartSheetView),
                nameof(WeDocApi.DeleteSmartSheetViews),
                nameof(WeDocApi.UpdateSmartSheetView),
                nameof(WeDocApi.GetSmartSheetFields),
                nameof(WeDocApi.AddSmartSheetFields),
                nameof(WeDocApi.DeleteSmartSheetFields),
                nameof(WeDocApi.UpdateSmartSheetFields),
                nameof(WeDocApi.GetSmartSheetRecords),
                nameof(WeDocApi.AddSmartSheetRecords),
                nameof(WeDocApi.DeleteSmartSheetRecords),
                nameof(WeDocApi.UpdateSmartSheetRecords),
                nameof(WeDocApi.GetSmartSheetFieldGroups),
                nameof(WeDocApi.AddSmartSheetFieldGroup),
                nameof(WeDocApi.UpdateSmartSheetFieldGroup),
                nameof(WeDocApi.DeleteSmartSheetFieldGroups)
            };
            var compatibilitySyncMethods = new[]
            {
                nameof(WeDocApi.AddDocumentAdmin),
                nameof(WeDocApi.RemoveDocumentAdmin),
                nameof(WeDocApi.GetDocumentAdminList)
            };

            Assert.AreEqual(96, methodNames.Length);
            foreach (var syncMethod in currentSyncMethods.Concat(compatibilitySyncMethods))
            {
                CollectionAssert.Contains(methodNames, syncMethod, syncMethod);
                CollectionAssert.Contains(methodNames, syncMethod + "Async", syncMethod + "Async");
            }
        }

        [TestMethod]
        public void WeDocApiUsesFortyFiveOfficialPaths()
        {
            var sourceDirectory = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "WeDoc");
            var source = string.Join(Environment.NewLine,
                Directory.GetFiles(sourceDirectory, "WeDocApi*.cs").Select(File.ReadAllText));
            var paths = new[]
            {
                "/cgi-bin/wedoc/create_doc",
                "/cgi-bin/wedoc/rename_doc",
                "/cgi-bin/wedoc/del_doc",
                "/cgi-bin/wedoc/get_doc_base_info",
                "/cgi-bin/wedoc/doc_share",
                "/cgi-bin/wedoc/doc_get_auth",
                "/cgi-bin/wedoc/mod_doc_join_rule",
                "/cgi-bin/wedoc/mod_doc_member",
                "/cgi-bin/wedoc/mod_doc_safty_setting",
                "/cgi-bin/wedoc/vip/batch_add",
                "/cgi-bin/wedoc/vip/batch_del",
                "/cgi-bin/wedoc/vip/list",
                "/cgi-bin/wedoc/create_form",
                "/cgi-bin/wedoc/modify_form",
                "/cgi-bin/wedoc/get_form_info",
                "/cgi-bin/wedoc/get_form_statistic",
                "/cgi-bin/wedoc/get_form_answer",
                "/cgi-bin/wedoc/spreadsheet/batch_update",
                "/cgi-bin/wedoc/spreadsheet/get_sheet_properties",
                "/cgi-bin/wedoc/spreadsheet/get_sheet_range_data",
                "/cgi-bin/wedoc/upload_doc_image",
                "/cgi-bin/wedoc/get_doc_data",
                "/cgi-bin/wedoc/mod_doc",
                "/cgi-bin/wedoc/smartsheet/get_sheet_auth",
                "/cgi-bin/wedoc/smartsheet/mod_sheet_auth",
                "/cgi-bin/wedoc/smartsheet/get_sheet",
                "/cgi-bin/wedoc/smartsheet/add_sheet",
                "/cgi-bin/wedoc/smartsheet/delete_sheet",
                "/cgi-bin/wedoc/smartsheet/update_sheet",
                "/cgi-bin/wedoc/smartsheet/get_views",
                "/cgi-bin/wedoc/smartsheet/add_view",
                "/cgi-bin/wedoc/smartsheet/delete_views",
                "/cgi-bin/wedoc/smartsheet/update_view",
                "/cgi-bin/wedoc/smartsheet/get_fields",
                "/cgi-bin/wedoc/smartsheet/add_fields",
                "/cgi-bin/wedoc/smartsheet/delete_fields",
                "/cgi-bin/wedoc/smartsheet/update_fields",
                "/cgi-bin/wedoc/smartsheet/get_records",
                "/cgi-bin/wedoc/smartsheet/add_records",
                "/cgi-bin/wedoc/smartsheet/delete_records",
                "/cgi-bin/wedoc/smartsheet/update_records",
                "/cgi-bin/wedoc/smartsheet/get_field_groups",
                "/cgi-bin/wedoc/smartsheet/add_field_group",
                "/cgi-bin/wedoc/smartsheet/update_field_group",
                "/cgi-bin/wedoc/smartsheet/delete_field_groups"
            };

            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            Assert.AreEqual(paths.Length, paths.Distinct().Count());
            StringAssert.Contains(source, "CommonJsonSendType.POST");
        }

        [TestMethod]
        public void RequestsUseCurrentOfficialFieldNames()
        {
            var createJson = JsonSerializer.Serialize(new WeDocCreateRequest
            {
                spaceid = "space-1",
                fatherid = "folder-1",
                doc_type = 4,
                doc_name = "经营数据",
                admin_users = new List<string> { "zhangsan" }
            });
            var ruleJson = JsonSerializer.Serialize(new WeDocModifyJoinRuleRequest
            {
                docid = "doc-1",
                enable_corp_internal = true,
                corp_internal_auth = 2,
                update_co_auth_list = true,
                co_auth_list = new List<WeDocCoAuthInfo>
                {
                    new WeDocCoAuthInfo { type = 2, departmentid = 5178368698L, auth = 1 }
                }
            });
            var memberJson = JsonSerializer.Serialize(new WeDocModifyMemberRequest
            {
                docid = "doc-1",
                update_file_member_list = new List<WeDocMember>
                {
                    new WeDocMember { type = 1, userid = "lisi", auth = 2 }
                },
                del_file_member_list = new List<WeDocMember>
                {
                    new WeDocMember { type = 3, tmp_external_userid = "external-1" }
                }
            });
            var safetyJson = JsonSerializer.Serialize(new WeDocModifySafetySettingRequest
            {
                docid = "doc-1",
                enable_readonly_copy = false,
                enable_readonly_comment = true
            });
            var vipJson = JsonSerializer.Serialize(new WeDocVipBatchRequest
            {
                userid_list = new List<string> { "zhangsan", "lisi" }
            });

            StringAssert.Contains(createJson, "\"spaceid\":\"space-1\"");
            StringAssert.Contains(createJson, "\"fatherid\":\"folder-1\"");
            StringAssert.Contains(createJson, "\"doc_type\":4");
            StringAssert.Contains(createJson, "\"admin_users\":[\"zhangsan\"]");
            StringAssert.Contains(ruleJson, "\"update_co_auth_list\":true");
            StringAssert.Contains(ruleJson, "\"departmentid\":5178368698");
            StringAssert.Contains(memberJson, "\"update_file_member_list\"");
            StringAssert.Contains(memberJson, "\"del_file_member_list\"");
            StringAssert.Contains(memberJson, "\"tmp_external_userid\":\"external-1\"");
            StringAssert.Contains(safetyJson, "\"enable_readonly_comment\":true");
            StringAssert.Contains(vipJson, "\"userid_list\":[\"zhangsan\",\"lisi\"]");
        }

        [TestMethod]
        public void ResultsPreserveLargeTimestampsDepartmentIdsAndAuthSettings()
        {
            var baseInfo = JsonSerializer.Deserialize<WeDocBaseInfoResult>(
                "{\"errcode\":0,\"doc_base_info\":{\"docid\":\"doc-1\"," +
                "\"doc_name\":\"经营数据\",\"create_time\":5178368698," +
                "\"modify_time\":5178368799,\"doc_type\":4}}");
            var auth = JsonSerializer.Deserialize<WeDocAuthResult>(
                "{\"errcode\":0,\"access_rule\":{\"enable_corp_internal\":true," +
                "\"corp_internal_auth\":2,\"ban_share_external\":true}," +
                "\"secure_setting\":{\"enable_readonly_copy\":false," +
                "\"enable_readonly_comment\":true,\"watermark\":{\"margin_type\":1," +
                "\"show_visitor_name\":true,\"show_text\":true,\"text\":\"机密\"}}," +
                "\"doc_member_list\":[{\"type\":1,\"userid\":\"zhangsan\",\"auth\":2}]," +
                "\"co_auth_list\":[{\"type\":2,\"departmentid\":5178368698,\"auth\":1}]}");

            Assert.IsNotNull(baseInfo);
            Assert.AreEqual(5178368698L, baseInfo.doc_base_info.create_time);
            Assert.AreEqual(5178368799L, baseInfo.doc_base_info.modify_time);
            Assert.IsNotNull(auth);
            Assert.IsTrue(auth.access_rule.enable_corp_internal);
            Assert.IsTrue(auth.access_rule.ban_share_external);
            Assert.AreEqual("机密", auth.secure_setting.watermark.text);
            Assert.AreEqual(5178368698L, auth.co_auth_list[0].departmentid);

            var vip = JsonSerializer.Deserialize<WeDocVipListResult>(
                "{\"errcode\":0,\"has_more\":true,\"next_cursor\":\"NEXT\"," +
                "\"userid_list\":[\"zhangsan\",\"lisi\"]}");
            Assert.IsNotNull(vip);
            Assert.IsTrue(vip.has_more);
            Assert.AreEqual("NEXT", vip.next_cursor);
            Assert.AreEqual("lisi", vip.userid_list[1]);
        }

        [TestMethod]
        public void FormRequestsPreserveQuestionsRangesAndRawStatisticArray()
        {
            var request = new WeDocFormCreateRequest
            {
                spaceid = "space-1",
                fatherid = "folder-1",
                form_info = new WeDocFormInfo
                {
                    form_title = "每日上报",
                    form_question = new WeDocFormQuestion
                    {
                        items = new List<WeDocFormQuestionItem>
                        {
                            new WeDocFormQuestionItem
                            {
                                question_id = 5178368698L,
                                title = "现场照片",
                                reply_type = 9,
                                must_reply = true,
                                question_extend_setting = new WeDocFormQuestionExtendSetting
                                {
                                    camera_only = true
                                }
                            }
                        }
                    },
                    form_setting = new WeDocFormSetting
                    {
                        fill_out_auth = 1,
                        fill_in_range = new WeDocFormFillInRange
                        {
                            userids = new List<string> { "zhangsan" },
                            departmentids = new List<long> { 5178368799L }
                        },
                        timed_finish = 5178368899L
                    }
                }
            };
            var requestJson = JsonSerializer.Serialize(request);
            var statisticJson = JsonSerializer.Serialize(new List<WeDocFormStatisticRequest>
            {
                new WeDocFormStatisticRequest
                {
                    repeated_id = "repeat-1",
                    req_type = 1,
                    start_time = 5178368698L,
                    end_time = 5178368799L,
                    limit = 100,
                    cursor = 0
                }
            });

            StringAssert.Contains(requestJson, "\"form_info\"");
            StringAssert.Contains(requestJson, "\"question_id\":5178368698");
            StringAssert.Contains(requestJson, "\"camera_only\":true");
            StringAssert.Contains(requestJson, "\"departmentids\":[5178368799]");
            StringAssert.Contains(requestJson, "\"timed_finish\":5178368899");
            Assert.IsTrue(statisticJson.StartsWith("[", StringComparison.Ordinal));
            StringAssert.Contains(statisticJson, "\"repeated_id\":\"repeat-1\"");
        }

        [TestMethod]
        public void FormResultsPreserveLargeAnswersAndAllStrongReplyShapes()
        {
            var statistic = JsonSerializer.Deserialize<WeDocFormStatisticResult>(
                "{\"errcode\":0,\"statistic_list\":[{\"fill_cnt\":5178368698," +
                "\"repeated_id\":\"repeat-1\",\"fill_user_cnt\":4000000000," +
                "\"unfill_user_cnt\":3000000000,\"submit_users\":[{\"userid\":\"zhangsan\"," +
                "\"submit_time\":5178368799,\"answer_id\":5178368899}]," +
                "\"has_more\":true,\"cursor\":5178368999}]}");
            var answer = JsonSerializer.Deserialize<WeDocFormAnswerResult>(
                "{\"errcode\":0,\"answer\":{\"answer_list\":[{\"answer_id\":5178368899," +
                "\"ctime\":5178368698,\"mtime\":5178368799,\"reply\":{\"items\":[{" +
                "\"question_id\":5178368999,\"text_reply\":\"答案\"," +
                "\"option_reply\":[1,2],\"option_extend_reply\":[{\"option_reply\":2," +
                "\"extend_text\":\"其他\"}],\"file_extend_reply\":[{\"name\":\"附件\"," +
                "\"fileid\":\"file-1\"}],\"department_reply\":{\"list\":[{" +
                "\"department_id\":5178368698}]},\"member_reply\":{\"list\":[{" +
                "\"userid\":\"lisi\"}]},\"duration_reply\":{\"begin_time\":5178368698," +
                "\"end_time\":5178368799,\"days\":1.5,\"hours\":12.25}}]}," +
                "\"answer_status\":1,\"userid\":\"zhangsan\"}]}}");

            Assert.IsNotNull(statistic);
            Assert.AreEqual(5178368698L, statistic.statistic_list[0].fill_cnt);
            Assert.AreEqual(5178368999L, statistic.statistic_list[0].cursor);
            Assert.IsNotNull(answer);
            var reply = answer.answer.answer_list[0].reply.items[0];
            Assert.AreEqual(5178368698L, reply.department_reply.list[0].department_id);
            Assert.AreEqual("lisi", reply.member_reply.list[0].userid);
            Assert.AreEqual(1.5, reply.duration_reply.days);
            Assert.AreEqual(12.25, reply.duration_reply.hours);
        }

        [TestMethod]
        public void SpreadsheetModelsPreserveBatchOperationsCellValuesAndFormats()
        {
            var requestJson = JsonSerializer.Serialize(new WeDocSpreadsheetBatchUpdateRequest
            {
                docid = "doc-1",
                requests = new List<WeDocSpreadsheetUpdateRequest>
                {
                    new WeDocSpreadsheetUpdateRequest
                    {
                        add_sheet_request = new WeDocSpreadsheetAddSheetRequest
                        {
                            title = "明细",
                            row_count = 1000,
                            column_count = 200
                        }
                    },
                    new WeDocSpreadsheetUpdateRequest
                    {
                        update_range_request = new WeDocSpreadsheetUpdateRangeRequest
                        {
                            sheet_id = "sheet-1",
                            grid_data = new WeDocSpreadsheetGridData
                            {
                                start_row = 0,
                                start_column = 0,
                                rows = new List<WeDocSpreadsheetRowData>
                                {
                                    new WeDocSpreadsheetRowData
                                    {
                                        values = new List<WeDocSpreadsheetCellData>
                                        {
                                            new WeDocSpreadsheetCellData
                                            {
                                                cell_value = new WeDocSpreadsheetCellValue
                                                {
                                                    link = new WeDocSpreadsheetLink
                                                    {
                                                        url = "https://example.test",
                                                        text = "详情"
                                                    }
                                                },
                                                cell_format = new WeDocSpreadsheetCellFormat
                                                {
                                                    text_format = new WeDocSpreadsheetTextFormat
                                                    {
                                                        font = "Arial",
                                                        font_size = 12,
                                                        bold = true,
                                                        color = new WeDocSpreadsheetColor
                                                        {
                                                            red = 255,
                                                            green = 128,
                                                            blue = 64,
                                                            alpha = 255
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
            var result = JsonSerializer.Deserialize<WeDocSpreadsheetBatchUpdateResult>(
                "{\"errcode\":0,\"add_sheet_response\":{\"properties\":{" +
                "\"sheet_id\":\"sheet-2\",\"title\":\"明细\",\"row_count\":1000," +
                "\"column_count\":200}},\"update_range_response\":{\"updated_cells\":1}}");
            var data = JsonSerializer.Deserialize<WeDocSpreadsheetDataResult>(
                "{\"errcode\":0,\"grid_data\":{\"start_row\":0,\"start_column\":0," +
                "\"rows\":[{\"values\":[{\"cell_value\":{\"text\":\"完成\"}," +
                "\"cell_format\":{\"text_format\":{\"font\":\"Arial\",\"font_size\":12," +
                "\"bold\":true,\"color\":{\"red\":255,\"green\":128," +
                "\"blue\":64,\"alpha\":255}}}}]}]}}");

            StringAssert.Contains(requestJson, "\"add_sheet_request\"");
            StringAssert.Contains(requestJson, "\"update_range_request\"");
            StringAssert.Contains(requestJson, "\"row_count\":1000");
            StringAssert.Contains(requestJson, "\"cell_value\"");
            StringAssert.Contains(requestJson, "\"text_format\"");
            Assert.IsNotNull(result);
            Assert.AreEqual("sheet-2", result.add_sheet_response.properties.sheet_id);
            Assert.AreEqual(1, result.update_range_response.updated_cells);
            Assert.IsNotNull(data);
            Assert.AreEqual("完成", data.grid_data.rows[0].values[0].cell_value.text);
            Assert.AreEqual(255, data.grid_data.rows[0].values[0].cell_format.text_format.color.alpha);
        }

        [TestMethod]
        public void ImageUploadUsesMediaMultipartFieldAndPreservesResultAliases()
        {
            var sourcePath = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "WeDoc", "WeDocApi.Upload.cs");
            var source = File.ReadAllText(sourcePath);
            var result = JsonSerializer.Deserialize<WeDocImageUploadResult>(
                "{\"errcode\":0,\"url\":\"https://example.test/a.png\"," +
                "\"image_url\":\"https://example.test/legacy.png\",\"fileid\":\"file-1\"," +
                "\"imageid\":\"image-1\",\"media_id\":\"media-1\",\"md5\":\"abc\"}");

            StringAssert.Contains(source, "[\"name\"] = \"media\"");
            StringAssert.Contains(source, "[\"filename\"] = imageFilePath");
            StringAssert.Contains(source, "PostFileGetJson<WeDocImageUploadResult>");
            StringAssert.Contains(source, "PostFileGetJsonAsync<WeDocImageUploadResult>");
            Assert.IsNotNull(result);
            Assert.AreEqual("https://example.test/a.png", result.url);
            Assert.AreEqual("https://example.test/legacy.png", result.image_url);
            Assert.AreEqual("media-1", result.media_id);
        }

        [TestMethod]
        public void DocumentContentRequestsAndResultsPreservePolymorphicBlocks()
        {
            using var operationDocument = JsonDocument.Parse(
                "{\"insert_text\":{\"location\":{\"index\":1},\"text\":\"新增内容\"}}");
            var getJson = JsonSerializer.Serialize(new WeDocContentRequest
            {
                docid = "doc-1",
                start = 10,
                limit = 50
            });
            var modifyJson = JsonSerializer.Serialize(new WeDocContentModifyRequest
            {
                docid = "doc-1",
                requests = new List<JsonElement> { operationDocument.RootElement.Clone() },
                client_token = "client-token-1"
            });
            var result = JsonSerializer.Deserialize<WeDocContentResult>(
                "{\"errcode\":0,\"docid\":\"doc-1\",\"content\":[{" +
                "\"paragraph\":{\"text\":\"正文\"}}],\"has_more\":true," +
                "\"next_cursor\":\"NEXT-2\"}");
            var aliasResult = JsonSerializer.Deserialize<WeDocContentResult>(
                "{\"errcode\":0,\"doc_content\":{\"blocks\":[1,2]}}");

            StringAssert.Contains(getJson, "\"start\":10");
            StringAssert.Contains(getJson, "\"limit\":50");
            StringAssert.Contains(modifyJson, "\"requests\":[{\"insert_text\"");
            StringAssert.Contains(modifyJson, "\"client_token\":\"client-token-1\"");
            Assert.IsNotNull(result);
            Assert.AreEqual(JsonValueKind.Array, result.content.ValueKind);
            Assert.AreEqual("正文", result.content[0].GetProperty("paragraph").GetProperty("text").GetString());
            Assert.IsTrue(result.has_more);
            Assert.AreEqual("NEXT-2", result.next_cursor);
            Assert.IsNotNull(aliasResult);
            Assert.AreEqual(2, aliasResult.doc_content.GetProperty("blocks").GetArrayLength());
        }

        [TestMethod]
        public void SmartSheetSheetAndViewRequestsMatchOfficialShapes()
        {
            var addSheetJson = JsonSerializer.Serialize(new WeDocSmartSheetAddSheetRequest
            {
                docid = "doc-1",
                properties = new WeDocSmartSheetAddSheetProperties
                {
                    title = "任务表",
                    index = 3
                }
            });
            var updateViewJson = JsonSerializer.Serialize(new WeDocSmartSheetUpdateViewRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                view_id = "view-1",
                view_title = "待处理任务",
                property = new WeDocSmartSheetViewProperty
                {
                    auto_sort = false,
                    sort_spec = new WeDocSmartSheetSortSpec
                    {
                        sort_infos = new List<WeDocSmartSheetSortItem>
                        {
                            new WeDocSmartSheetSortItem { field_id = "field-1", desc = true }
                        }
                    },
                    filter_spec = new WeDocSmartSheetFilterSpec
                    {
                        conjunction = "CONJUNCTION_AND",
                        conditions = new List<WeDocSmartSheetFilterCondition>
                        {
                            new WeDocSmartSheetFilterCondition
                            {
                                field_id = "field-1",
                                @operator = "OPERATOR_CONTAINS",
                                string_value = new WeDocSmartSheetStringFilterValue
                                {
                                    value = new List<string> { "待处理" }
                                }
                            }
                        }
                    },
                    field_visibility = new Dictionary<string, bool> { ["field-2"] = false }
                }
            });

            using var addSheetDocument = JsonDocument.Parse(addSheetJson);
            using var updateViewDocument = JsonDocument.Parse(updateViewJson);
            Assert.AreEqual("任务表", addSheetDocument.RootElement.GetProperty("properties")
                .GetProperty("title").GetString());
            Assert.AreEqual(3, addSheetDocument.RootElement.GetProperty("properties")
                .GetProperty("index").GetInt32());
            Assert.AreEqual("待处理任务", updateViewDocument.RootElement.GetProperty("view_title").GetString());
            StringAssert.Contains(updateViewJson, "\"sort_infos\":[{\"field_id\":\"field-1\",\"desc\":true}]");
            StringAssert.Contains(updateViewJson, "\"operator\":\"OPERATOR_CONTAINS\"");
            StringAssert.Contains(updateViewJson, "\"field_visibility\":{\"field-2\":false}");
        }

        [TestMethod]
        public void SmartSheetAuthFieldAndRecordRequestsMatchOfficialShapes()
        {
            using var authDocument = JsonDocument.Parse(
                "{\"mode\":\"custom\",\"columns\":[{\"field_id\":\"field-1\",\"auth\":2}]}");
            using var cellValueDocument = JsonDocument.Parse(
                "[{\"type\":\"text\",\"text\":\"已完成\"}]");
            var authJson = JsonSerializer.Serialize(new WeDocSmartSheetModifyAuthRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                auth_info = authDocument.RootElement.Clone()
            });
            var fieldJson = JsonSerializer.Serialize(new WeDocSmartSheetAddFieldsRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                fields = new List<WeDocSmartSheetField>
                {
                    new WeDocSmartSheetField
                    {
                        field_title = "进度",
                        field_type = "FIELD_TYPE_NUMBER",
                        property_number = new WeDocSmartSheetNumberFieldProperty
                        {
                            decimal_places = 2,
                            use_separate = true
                        }
                    }
                }
            });
            var recordJson = JsonSerializer.Serialize(new WeDocSmartSheetAddRecordsRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                key_type = "CELL_VALUE_KEY_TYPE_FIELD_TITLE",
                records = new List<WeDocSmartSheetRecordInput>
                {
                    new WeDocSmartSheetRecordInput
                    {
                        values = new Dictionary<string, JsonElement>
                        {
                            ["状态"] = cellValueDocument.RootElement.Clone()
                        }
                    }
                }
            });

            StringAssert.Contains(authJson, "\"auth_info\":{\"mode\":\"custom\"");
            using var fieldDocument = JsonDocument.Parse(fieldJson);
            using var recordDocument = JsonDocument.Parse(recordJson);
            var field = fieldDocument.RootElement.GetProperty("fields")[0];
            Assert.AreEqual("进度", field.GetProperty("field_title").GetString());
            Assert.AreEqual(2, field.GetProperty("property_number").GetProperty("decimal_places").GetInt32());
            Assert.IsTrue(field.GetProperty("property_number").GetProperty("use_separate").GetBoolean());
            StringAssert.Contains(recordJson, "\"key_type\":\"CELL_VALUE_KEY_TYPE_FIELD_TITLE\"");
            Assert.AreEqual("已完成", recordDocument.RootElement.GetProperty("records")[0]
                .GetProperty("values").GetProperty("状态")[0].GetProperty("text").GetString());
        }

        [TestMethod]
        public void SmartSheetResultsPreserveTypedCollectionsPropertiesAndTimestamps()
        {
            var auth = JsonSerializer.Deserialize<WeDocSmartSheetAuthResult>(
                "{\"errcode\":0,\"docid\":\"doc-1\",\"sheet_id\":\"sheet-1\"," +
                "\"auth_info\":{\"mode\":\"custom\"}}");
            var sheets = JsonSerializer.Deserialize<WeDocSmartSheetGetSheetsResult>(
                "{\"errcode\":0,\"sheet_list\":[{\"sheet_id\":\"sheet-1\"," +
                "\"title\":\"任务表\",\"type\":\"smartsheet\",\"is_visible\":true}]}");
            var views = JsonSerializer.Deserialize<WeDocSmartSheetGetViewsResult>(
                "{\"errcode\":0,\"views\":[{\"view_id\":\"view-1\"," +
                "\"view_title\":\"全部任务\",\"view_type\":\"VIEW_TYPE_GRID\"," +
                "\"property\":{\"frozen_field_count\":1}}],\"total\":1," +
                "\"has_more\":false}");
            var fields = JsonSerializer.Deserialize<WeDocSmartSheetGetFieldsResult>(
                "{\"errcode\":0,\"fields\":[{\"field_id\":\"field-1\"," +
                "\"field_title\":\"进度\",\"field_type\":\"FIELD_TYPE_NUMBER\"," +
                "\"property_number\":{\"decimal_places\":2,\"use_separate\":true}}]," +
                "\"total\":1,\"next\":10,\"has_more\":true}");
            var records = JsonSerializer.Deserialize<WeDocSmartSheetGetRecordsResult>(
                "{\"errcode\":0,\"records\":[{\"record_id\":\"record-1\"," +
                "\"create_time\":\"1715846245084\",\"update_time\":\"1715846248810\"," +
                "\"creator_name\":\"张三\",\"updater_name\":\"李四\"," +
                "\"values\":{\"进度\":88}}],\"total\":1,\"next\":100,\"has_more\":true}");

            Assert.IsNotNull(auth);
            Assert.AreEqual("custom", auth.auth_info.Value.GetProperty("mode").GetString());
            Assert.AreEqual("smartsheet", sheets.sheet_list[0].type);
            Assert.IsTrue(sheets.sheet_list[0].is_visible);
            Assert.AreEqual(1, views.views[0].property.frozen_field_count);
            Assert.AreEqual(2, fields.fields[0].property_number.decimal_places);
            Assert.IsTrue(fields.fields[0].property_number.use_separate);
            Assert.AreEqual(1715846245084L, records.records[0].create_time);
            Assert.AreEqual(88, records.records[0].values["进度"].GetInt32());
            Assert.IsTrue(records.has_more);
        }

        [TestMethod]
        public void SmartSheetFieldGroupRequestsAndResultsMatchOfficialShapes()
        {
            var getJson = JsonSerializer.Serialize(new WeDocSmartSheetGetFieldGroupsRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                offset = 0,
                limit = 10
            });
            var addJson = JsonSerializer.Serialize(new WeDocSmartSheetAddFieldGroupRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                name = "重点字段",
                children = new List<WeDocSmartSheetFieldGroupChild>
                {
                    new WeDocSmartSheetFieldGroupChild { field_id = "field-1" },
                    new WeDocSmartSheetFieldGroupChild { field_id = "field-2" }
                }
            });
            var updateJson = JsonSerializer.Serialize(new WeDocSmartSheetUpdateFieldGroupRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                field_group_id = "group-1",
                name = "核心字段",
                children = new List<WeDocSmartSheetFieldGroupChild>
                {
                    new WeDocSmartSheetFieldGroupChild { field_id = "field-2" }
                }
            });
            var deleteJson = JsonSerializer.Serialize(new WeDocSmartSheetDeleteFieldGroupsRequest
            {
                docid = "doc-1",
                sheet_id = "sheet-1",
                field_group_ids = new List<string> { "group-1", "group-2" }
            });
            var result = JsonSerializer.Deserialize<WeDocSmartSheetGetFieldGroupsResult>(
                "{\"errcode\":0,\"total\":1,\"has_more\":true,\"next\":10," +
                "\"field_groups\":[{\"field_group_id\":\"group-1\"," +
                "\"name\":\"重点字段\",\"children\":[{\"field_id\":\"field-1\"}]}]}");

            using var getDocument = JsonDocument.Parse(getJson);
            using var addDocument = JsonDocument.Parse(addJson);
            using var updateDocument = JsonDocument.Parse(updateJson);
            using var deleteDocument = JsonDocument.Parse(deleteJson);
            Assert.AreEqual(0, getDocument.RootElement.GetProperty("offset").GetInt32());
            Assert.AreEqual(10, getDocument.RootElement.GetProperty("limit").GetInt32());
            Assert.AreEqual(2, addDocument.RootElement.GetProperty("children").GetArrayLength());
            Assert.AreEqual("field-2", updateDocument.RootElement.GetProperty("children")[0]
                .GetProperty("field_id").GetString());
            Assert.AreEqual(2, deleteDocument.RootElement.GetProperty("field_group_ids").GetArrayLength());
            Assert.IsNotNull(result);
            Assert.AreEqual("group-1", result.field_groups[0].field_group_id);
            Assert.AreEqual("field-1", result.field_groups[0].children[0].field_id);
            Assert.AreEqual(10, result.next);
            Assert.IsTrue(result.has_more);
        }

        [TestMethod]
        public void NewWeDocApisUseEndpointSpecificContractsAndCompleteXmlComments()
        {
            var contracts = new[]
            {
                new { Name = nameof(WeDocApi.GetDocumentData), Request = typeof(WeDocContentRequest), Result = typeof(WeDocContentResult) },
                new { Name = nameof(WeDocApi.ModifyDocumentContent), Request = typeof(WeDocContentModifyRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.GetSmartSheetAuth), Request = typeof(WeDocSmartSheetAuthRequest), Result = typeof(WeDocSmartSheetAuthResult) },
                new { Name = nameof(WeDocApi.ModifySmartSheetAuth), Request = typeof(WeDocSmartSheetModifyAuthRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.GetSmartSheets), Request = typeof(WeDocSmartSheetGetSheetsRequest), Result = typeof(WeDocSmartSheetGetSheetsResult) },
                new { Name = nameof(WeDocApi.AddSmartSheet), Request = typeof(WeDocSmartSheetAddSheetRequest), Result = typeof(WeDocSmartSheetAddSheetResult) },
                new { Name = nameof(WeDocApi.DeleteSmartSheet), Request = typeof(WeDocSmartSheetDeleteSheetRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.UpdateSmartSheet), Request = typeof(WeDocSmartSheetUpdateSheetRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.GetSmartSheetViews), Request = typeof(WeDocSmartSheetGetViewsRequest), Result = typeof(WeDocSmartSheetGetViewsResult) },
                new { Name = nameof(WeDocApi.AddSmartSheetView), Request = typeof(WeDocSmartSheetAddViewRequest), Result = typeof(WeDocSmartSheetAddViewResult) },
                new { Name = nameof(WeDocApi.DeleteSmartSheetViews), Request = typeof(WeDocSmartSheetDeleteViewsRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.UpdateSmartSheetView), Request = typeof(WeDocSmartSheetUpdateViewRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.GetSmartSheetFields), Request = typeof(WeDocSmartSheetGetFieldsRequest), Result = typeof(WeDocSmartSheetGetFieldsResult) },
                new { Name = nameof(WeDocApi.AddSmartSheetFields), Request = typeof(WeDocSmartSheetAddFieldsRequest), Result = typeof(WeDocSmartSheetAddFieldsResult) },
                new { Name = nameof(WeDocApi.DeleteSmartSheetFields), Request = typeof(WeDocSmartSheetDeleteFieldsRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.UpdateSmartSheetFields), Request = typeof(WeDocSmartSheetUpdateFieldsRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.GetSmartSheetRecords), Request = typeof(WeDocSmartSheetGetRecordsRequest), Result = typeof(WeDocSmartSheetGetRecordsResult) },
                new { Name = nameof(WeDocApi.AddSmartSheetRecords), Request = typeof(WeDocSmartSheetAddRecordsRequest), Result = typeof(WeDocSmartSheetAddRecordsResult) },
                new { Name = nameof(WeDocApi.DeleteSmartSheetRecords), Request = typeof(WeDocSmartSheetDeleteRecordsRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.UpdateSmartSheetRecords), Request = typeof(WeDocSmartSheetUpdateRecordsRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) },
                new { Name = nameof(WeDocApi.GetSmartSheetFieldGroups), Request = typeof(WeDocSmartSheetGetFieldGroupsRequest), Result = typeof(WeDocSmartSheetGetFieldGroupsResult) },
                new { Name = nameof(WeDocApi.AddSmartSheetFieldGroup), Request = typeof(WeDocSmartSheetAddFieldGroupRequest), Result = typeof(WeDocSmartSheetAddFieldGroupResult) },
                new { Name = nameof(WeDocApi.UpdateSmartSheetFieldGroup), Request = typeof(WeDocSmartSheetUpdateFieldGroupRequest), Result = typeof(WeDocSmartSheetUpdateFieldGroupResult) },
                new { Name = nameof(WeDocApi.DeleteSmartSheetFieldGroups), Request = typeof(WeDocSmartSheetDeleteFieldGroupsRequest), Result = typeof(Senparc.Weixin.Entities.WorkJsonResult) }
            };

            foreach (var contract in contracts)
            {
                var syncMethod = typeof(WeDocApi).GetMethod(contract.Name,
                    BindingFlags.Public | BindingFlags.Static);
                var asyncMethod = typeof(WeDocApi).GetMethod(contract.Name + "Async",
                    BindingFlags.Public | BindingFlags.Static);
                Assert.IsNotNull(syncMethod, contract.Name);
                Assert.IsNotNull(asyncMethod, contract.Name + "Async");
                Assert.AreEqual(contract.Request, syncMethod.GetParameters()[1].ParameterType, contract.Name);
                Assert.AreEqual(contract.Result, syncMethod.ReturnType, contract.Name);
                Assert.AreEqual(contract.Request, asyncMethod.GetParameters()[1].ParameterType,
                    contract.Name + "Async");
                Assert.AreEqual(contract.Result, asyncMethod.ReturnType.GenericTypeArguments.Single(),
                    contract.Name + "Async");
            }

            var sourceDirectory = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "WeDoc");
            var sourceFiles = new[]
            {
                "WeDocApi.Content.cs",
                "WeDocApi.SmartSheet.cs",
                "WeDocApi.SmartSheet.Views.cs",
                "WeDocApi.SmartSheet.Fields.cs",
                "WeDocApi.SmartSheet.Records.cs",
                "WeDocApi.SmartSheet.FieldGroups.cs"
            };
            foreach (var sourceFile in sourceFiles)
            {
                var source = File.ReadAllText(Path.Combine(sourceDirectory, sourceFile));
                var publicMethodCount = source.Split('\n').Count(line =>
                    line.TrimStart().StartsWith("public static ", StringComparison.Ordinal) &&
                    !line.Contains(" class ", StringComparison.Ordinal));
                Assert.AreEqual(publicMethodCount, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"), sourceFile);
                Assert.AreEqual(publicMethodCount, CountOccurrences(source, "/// <param name=\"request\">"), sourceFile);
                Assert.AreEqual(publicMethodCount, CountOccurrences(source, "/// <param name=\"timeOut\">"), sourceFile);
                Assert.AreEqual(publicMethodCount, CountOccurrences(source, "/// <returns>"), sourceFile);
                Assert.IsTrue(source.Contains("官方接口", StringComparison.Ordinal) ||
                              source.Contains("developer.work.weixin.qq.com", StringComparison.Ordinal), sourceFile);
            }

            var modelSource = File.ReadAllText(Path.Combine(sourceDirectory,
                "WeDocSmartSheetFieldGroupJson.cs"));
            var publicDeclarationCount = modelSource.Split('\n').Count(line =>
            {
                var trimmed = line.TrimStart();
                return trimmed.StartsWith("public class ", StringComparison.Ordinal) ||
                       trimmed.StartsWith("public string ", StringComparison.Ordinal) ||
                       trimmed.StartsWith("public int ", StringComparison.Ordinal) ||
                       trimmed.StartsWith("public int? ", StringComparison.Ordinal) ||
                       trimmed.StartsWith("public bool ", StringComparison.Ordinal) ||
                       trimmed.StartsWith("public IList<", StringComparison.Ordinal) ||
                       trimmed.StartsWith("public WeDocSmartSheetFieldGroup ", StringComparison.Ordinal);
            });
            Assert.AreEqual(publicDeclarationCount, CountOccurrences(modelSource, "/// <summary>"),
                "WeDocSmartSheetFieldGroupJson.cs");
        }

        [TestMethod]
        public void PublicWeDocModelsDoNotExposeObjectProperties()
        {
            var objectProperties = typeof(WeDocCreateRequest).Assembly.GetTypes()
                .Where(type => type.IsPublic && type.Namespace == typeof(WeDocCreateRequest).Namespace)
                .SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                        BindingFlags.DeclaredOnly)
                    .Where(property => property.PropertyType == typeof(object))
                    .Select(property => type.Name + "." + property.Name))
                .ToArray();

            Assert.AreEqual(0, objectProperties.Length,
                "WeDoc models must stay strongly typed: " + string.Join(", ", objectProperties));
        }

        private static int CountOccurrences(string source, string value)
        {
            var count = 0;
            var startIndex = 0;
            while ((startIndex = source.IndexOf(value, startIndex, StringComparison.Ordinal)) >= 0)
            {
                count++;
                startIndex += value.Length;
            }

            return count;
        }

        private static string FindRepositoryRoot([CallerFilePath] string callerFilePath = null)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(callerFilePath)
                                              ?? AppContext.BaseDirectory);
            while (directory != null && !Directory.Exists(Path.Combine(directory.FullName, ".git")))
            {
                directory = directory.Parent;
            }

            return directory?.FullName
                   ?? throw new InvalidOperationException("Repository root was not found.");
        }
    }
}
