using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.WeDrive;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.WeDrive
{
    [TestClass]
    public class WeDriveContractTests
    {
        [TestMethod]
        public void WeDriveApiContainsThirtyOneSyncAndAsyncEntries()
        {
            var methodNames = typeof(WeDriveApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(method => method.Name).ToArray();
            var syncMethodNames = new[]
            {
                nameof(WeDriveApi.CreateSpace),
                nameof(WeDriveApi.RenameSpace),
                nameof(WeDriveApi.DismissSpace),
                nameof(WeDriveApi.GetLegacySpaceInfo),
                nameof(WeDriveApi.GetSpaceInfo),
                nameof(WeDriveApi.UpdateSpaceSetting),
                nameof(WeDriveApi.GetSpaceShareLink),
                nameof(WeDriveApi.AddSpaceMembers),
                nameof(WeDriveApi.RemoveSpaceMembers),
                nameof(WeDriveApi.GetFileList),
                nameof(WeDriveApi.UploadFile),
                nameof(WeDriveApi.DownloadFile),
                nameof(WeDriveApi.InitializeFileUpload),
                nameof(WeDriveApi.UploadFilePart),
                nameof(WeDriveApi.FinishFileUpload),
                nameof(WeDriveApi.CreateFile),
                nameof(WeDriveApi.RenameFile),
                nameof(WeDriveApi.MoveFile),
                nameof(WeDriveApi.DeleteFile),
                nameof(WeDriveApi.GetFileInfo),
                nameof(WeDriveApi.SetFileShareSetting),
                nameof(WeDriveApi.UpdateFileSecureSetting),
                nameof(WeDriveApi.GetFileShareLink),
                nameof(WeDriveApi.GetFilePermission),
                nameof(WeDriveApi.AddFileMembers),
                nameof(WeDriveApi.RemoveFileMembers),
                nameof(WeDriveApi.GetProfessionalInfo),
                nameof(WeDriveApi.GetCapacity),
                nameof(WeDriveApi.AddVipAccounts),
                nameof(WeDriveApi.RemoveVipAccounts),
                nameof(WeDriveApi.GetVipAccountList)
            };

            Assert.AreEqual(62, methodNames.Length);
            foreach (var syncMethodName in syncMethodNames)
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void WeDriveApiUsesThirtyOneOfficialPostPaths()
        {
            var directory = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "WeDrive");
            var source = string.Join(Environment.NewLine,
                Directory.GetFiles(directory, "WeDriveApi*.cs").Select(File.ReadAllText));
            var paths = new[]
            {
                "/cgi-bin/wedrive/space_create",
                "/cgi-bin/wedrive/space_rename",
                "/cgi-bin/wedrive/space_dismiss",
                "/cgi-bin/wedrive/space_info",
                "/cgi-bin/wedrive/new_space_info",
                "/cgi-bin/wedrive/space_setting",
                "/cgi-bin/wedrive/space_share",
                "/cgi-bin/wedrive/space_acl_add",
                "/cgi-bin/wedrive/space_acl_del",
                "/cgi-bin/wedrive/file_list",
                "/cgi-bin/wedrive/file_upload",
                "/cgi-bin/wedrive/file_download",
                "/cgi-bin/wedrive/file_upload_init",
                "/cgi-bin/wedrive/file_upload_part",
                "/cgi-bin/wedrive/file_upload_finish",
                "/cgi-bin/wedrive/file_create",
                "/cgi-bin/wedrive/file_rename",
                "/cgi-bin/wedrive/file_move",
                "/cgi-bin/wedrive/file_delete",
                "/cgi-bin/wedrive/file_info",
                "/cgi-bin/wedrive/file_setting",
                "/cgi-bin/wedrive/file_secure_setting",
                "/cgi-bin/wedrive/file_share",
                "/cgi-bin/wedrive/get_file_permission",
                "/cgi-bin/wedrive/file_acl_add",
                "/cgi-bin/wedrive/file_acl_del",
                "/cgi-bin/wedrive/mng_pro_info",
                "/cgi-bin/wedrive/mng_capacity",
                "/cgi-bin/wedrive/vip/batch_add",
                "/cgi-bin/wedrive/vip/batch_del",
                "/cgi-bin/wedrive/vip/list"
            };

            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            Assert.AreEqual(paths.Length, paths.Distinct().Count());
            StringAssert.Contains(source, "CommonJsonSendType.POST");
        }

        [TestMethod]
        public void SpaceAndDownloadRequestsUseCurrentOfficialFields()
        {
            var spaceJson = JsonSerializer.Serialize(new WeDriveCreateSpaceRequest
            {
                space_name = "研发资料",
                space_sub_type = 0,
                auth_info = new List<WeDriveAuthInfo>
                {
                    new WeDriveAuthInfo
                    {
                        type = 2,
                        departmentid = 5178368698L,
                        auth = 200,
                        customize_auth = new WeDriveCustomizeAuth
                        {
                            enable_operation_upload = true,
                            enable_operation_delete = false
                        }
                    }
                }
            });
            var downloadJson = JsonSerializer.Serialize(new WeDriveFileDownloadRequest
            {
                fileid = "file-1",
                selected_ticket = "ticket-1"
            });

            using var spaceDocument = JsonDocument.Parse(spaceJson);
            Assert.AreEqual("研发资料", spaceDocument.RootElement.GetProperty("space_name").GetString());
            StringAssert.Contains(spaceJson, "\"space_sub_type\":0");
            StringAssert.Contains(spaceJson, "\"departmentid\":5178368698");
            StringAssert.Contains(spaceJson, "\"customize_auth\"");
            StringAssert.Contains(downloadJson, "\"fileid\":\"file-1\"");
            StringAssert.Contains(downloadJson, "\"selected_ticket\":\"ticket-1\"");
            Assert.IsFalse(downloadJson.Contains("\"userid\""));
        }

        [TestMethod]
        public void UploadRequestsPreserveBase64AndLargeMultipartFields()
        {
            var uploadJson = JsonSerializer.Serialize(new WeDriveFileUploadRequest
            {
                spaceid = "space-1",
                fatherid = "folder-1",
                file_name = "report.pdf",
                file_base64_content = "YmFzZTY0"
            });
            var initializeJson = JsonSerializer.Serialize(new WeDriveFileUploadInitializeRequest
            {
                selected_ticket = "ticket-1",
                file_name = "archive.zip",
                size = 20_000_000_000L,
                block_sha = new List<string> { "sha-1", "sha-2" },
                skip_push_card = true
            });
            var partJson = JsonSerializer.Serialize(new WeDriveFileUploadPartRequest
            {
                upload_key = "upload-key",
                index = 7,
                file_base64_content = "cGFydA=="
            });

            StringAssert.Contains(uploadJson, "\"file_base64_content\":\"YmFzZTY0\"");
            StringAssert.Contains(initializeJson, "\"selected_ticket\":\"ticket-1\"");
            StringAssert.Contains(initializeJson, "\"size\":20000000000");
            StringAssert.Contains(initializeJson, "\"block_sha\":[\"sha-1\",\"sha-2\"]");
            StringAssert.Contains(partJson, "\"upload_key\":\"upload-key\"");
            StringAssert.Contains(partJson, "\"index\":7");
            StringAssert.Contains(partJson, "\"file_base64_content\":\"cGFydA==\"");
        }

        [TestMethod]
        public void FileModelsPreserveLargeSizesAndTimestamps()
        {
            var result = JsonSerializer.Deserialize<WeDriveFileListResult>(
                "{\"errcode\":0,\"has_more\":true,\"next_start\":100," +
                "\"file_list\":{\"item\":[{\"fileid\":\"file-1\",\"file_name\":\"归档.zip\"," +
                "\"spaceid\":\"space-1\",\"fatherid\":\"folder-1\",\"file_size\":20000000000," +
                "\"ctime\":5178368698,\"mtime\":5178368799,\"file_type\":2}]}}" );

            Assert.IsNotNull(result);
            Assert.IsTrue(result.has_more);
            Assert.AreEqual(100, result.next_start);
            Assert.AreEqual(20_000_000_000L, result.file_list.item[0].file_size);
            Assert.AreEqual(5178368698L, result.file_list.item[0].ctime);
            Assert.AreEqual(5178368799L, result.file_list.item[0].mtime);
        }

        [TestMethod]
        public void NewSpaceInfoPreservesSecurityAndDepartmentFields()
        {
            var result = JsonSerializer.Deserialize<WeDriveSpaceInfoResult>(
                "{\"errcode\":0,\"space_info\":{\"spaceid\":\"space-1\",\"space_name\":\"研发资料\"," +
                "\"space_sub_type\":0,\"auth_list\":{\"auth_info\":[{\"type\":2," +
                "\"departmentid\":5178368698,\"auth\":7}],\"quit_userid\":[\"left-user\"]}," +
                "\"secure_setting\":{\"enable_watermark\":true,\"add_member_only_admin\":true," +
                "\"enable_share_url\":true,\"share_url_no_approve\":false," +
                "\"share_url_no_approve_default_auth\":4,\"enable_share_external\":false," +
                "\"enable_share_external_admin\":true,\"enable_space_add_external_member\":false," +
                "\"enable_space_add_external_member_admin\":true,\"enable_confidential_mode\":true," +
                "\"default_file_scope\":1,\"create_file_only_admin\":false," +
                "\"ban_share_external\":true}}}" );

            Assert.IsNotNull(result);
            Assert.AreEqual(5178368698L, result.space_info.auth_list.auth_info[0].departmentid);
            Assert.AreEqual("left-user", result.space_info.auth_list.quit_userid[0]);
            Assert.IsTrue(result.space_info.secure_setting.enable_watermark);
            Assert.IsTrue(result.space_info.secure_setting.enable_confidential_mode);
            Assert.IsTrue(result.space_info.secure_setting.ban_share_external);
        }

        [TestMethod]
        public void FilePermissionPreservesNestedSecurityAndWatermarkFields()
        {
            var result = JsonSerializer.Deserialize<WeDriveFilePermissionResult>(
                "{\"errcode\":0,\"share_range\":{\"enable_corp_internal\":true," +
                "\"corp_internal_auth\":4,\"corp_internal_approve_only_by_admin\":true," +
                "\"enable_corp_external\":false,\"corp_external_auth\":1}," +
                "\"secure_setting\":{\"enable_readonly_copy\":true,\"modify_only_by_admin\":true," +
                "\"enable_readonly_comment\":false,\"ban_share_external\":true}," +
                "\"inherit_father_auth\":{\"inherit\":true,\"auth_list\":[{\"type\":1," +
                "\"userid\":\"zhangsan\",\"auth\":2}]},\"file_member_list\":[{\"type\":2," +
                "\"departmentid\":5178368698,\"auth\":4}],\"co_auth_list\":[{\"type\":1," +
                "\"userid\":\"lisi\",\"auth\":2}],\"watermark\":{\"text\":\"机密\"," +
                "\"show_text\":true,\"show_visitor_name\":true}}" );

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.share_range.corp_internal_auth);
            Assert.IsTrue(result.secure_setting.enable_readonly_copy);
            Assert.IsTrue(result.inherit_father_auth.inherit);
            Assert.AreEqual("zhangsan", result.inherit_father_auth.auth_list[0].userid);
            Assert.AreEqual(5178368698L, result.file_member_list[0].departmentid);
            Assert.AreEqual("lisi", result.co_auth_list[0].userid);
            Assert.AreEqual("机密", result.watermark.text);
        }

        [TestMethod]
        public void ManagementModelsPreserveCapacityExpiryAndVipPagingFields()
        {
            var professional = JsonSerializer.Deserialize<WeDriveProfessionalInfoResult>(
                "{\"errcode\":0,\"is_pro\":true,\"total_vip_acct_num\":200," +
                "\"use_vip_acct_num\":120,\"pro_expire_time\":5178368698}" );
            var capacity = JsonSerializer.Deserialize<WeDriveCapacityResult>(
                "{\"errcode\":0,\"total_capacity_for_all\":20000000000," +
                "\"total_capacity_for_vip\":30000000000,\"rest_capacity_for_all\":15000000000," +
                "\"rest_capacity_for_vip\":25000000000}" );
            var vipList = JsonSerializer.Deserialize<WeDriveVipListResult>(
                "{\"errcode\":0,\"userid_list\":[\"zhangsan\",\"lisi\"]," +
                "\"has_more\":true,\"next_cursor\":\"cursor-2\"}" );

            Assert.IsNotNull(professional);
            Assert.AreEqual(5178368698L, professional.pro_expire_time);
            Assert.IsNotNull(capacity);
            Assert.AreEqual(20_000_000_000L, capacity.total_capacity_for_all);
            Assert.AreEqual(25_000_000_000L, capacity.rest_capacity_for_vip);
            Assert.IsNotNull(vipList);
            CollectionAssert.AreEqual(new[] { "zhangsan", "lisi" }, vipList.userid_list.ToArray());
            Assert.IsTrue(vipList.has_more);
            Assert.AreEqual("cursor-2", vipList.next_cursor);
        }

        [TestMethod]
        public void WeDriveCallbacksPreserveRepeatedIdsAndExposeHandlerExtensions()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "Entities", "Request", "Event",
                "RequestMessageEvent_WeDrive.cs"));
            foreach (var documentId in new[] { "97898", "97899", "97900", "97901", "97902", "97903" })
            {
                StringAssert.Contains(source, "/document/path/" + documentId);
            }
            Assert.AreEqual(12, source.Split(new[] { "/// <summary>" },
                StringSplitOptions.None).Length - 1);

            var spaceDocument = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[wedrive_space_change]]></Event>
<ChangeType><![CDATA[space_member_change]]></ChangeType>
<SpaceId><![CDATA[space-a]]></SpaceId>
<SpaceId><![CDATA[space-b]]></SpaceId>
</xml>");
            var spaceRequest = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), spaceDocument) as
                RequestMessageEvent_WeDrive_Space_Change;

            Assert.IsNotNull(spaceRequest);
            Assert.AreEqual(Event.wedrive_space_change, spaceRequest.Event);
            Assert.AreEqual("space_member_change", spaceRequest.ChangeType);
            CollectionAssert.AreEqual(new[] { "space-a", "space-b" }, spaceRequest.SpaceIds);

            var fileDocument = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[wedrive_file_change]]></Event>
<ChangeType><![CDATA[move_file]]></ChangeType>
<FileId><![CDATA[file-a]]></FileId>
<FileId><![CDATA[file-b]]></FileId>
</xml>");
            var fileRequest = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), fileDocument) as
                RequestMessageEvent_WeDrive_File_Change;

            Assert.IsNotNull(fileRequest);
            Assert.AreEqual(Event.wedrive_file_change, fileRequest.Event);
            Assert.AreEqual("move_file", fileRequest.ChangeType);
            CollectionAssert.AreEqual(new[] { "file-a", "file-b" }, fileRequest.FileIds);

            var capacityDocument = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[wedrive_insufficient_capacity]]></Event>
</xml>");
            var capacityRequest = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), capacityDocument) as
                RequestMessageEvent_WeDrive_Insufficient_Capacity;

            Assert.IsNotNull(capacityRequest);
            Assert.AreEqual(Event.wedrive_insufficient_capacity, capacityRequest.Event);
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod(
                "OnEvent_WeDriveInsufficientCapacityRequest"));
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod(
                "OnEvent_WeDriveInsufficientCapacityRequestAsync"));
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod("OnEvent_WeDriveSpaceChangeRequest"));
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod("OnEvent_WeDriveSpaceChangeRequestAsync"));
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod("OnEvent_WeDriveFileChangeRequest"));
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod("OnEvent_WeDriveFileChangeRequestAsync"));
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
