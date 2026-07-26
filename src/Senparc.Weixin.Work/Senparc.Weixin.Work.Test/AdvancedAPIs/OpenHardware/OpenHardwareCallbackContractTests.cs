using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.Work.AdvancedAPIs.OpenHardware;
using Senparc.Weixin.Work.Tencent;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.OpenHardware
{
    [TestClass]
    public class OpenHardwareCallbackContractTests
    {
        private const string Token = "OPEN-HARDWARE-TOKEN";
        private const string EncodingAesKey =
            "XtJUgDlFYncPP3z4V7W6Jv4ietcIFveUn6LP1KzOBNf";
        private const string ReceiveId = "MODEL-10001";
        private const string Timestamp = "4178368698";
        private const string Nonce = "OPEN-HARDWARE-NONCE";

        [TestMethod]
        public void ParserDispatchesAllFifteenCurrentCallbackTypes()
        {
            var callbackTypes = new[]
            {
                CallbackCase.Event<OpenHardwareBindEvent>(OpenHardwareCallbackTypes.Bind),
                CallbackCase.Event<OpenHardwareUnbindEvent>(OpenHardwareCallbackTypes.Unbind),
                CallbackCase.Event<OpenHardwareContactChangeEvent>(OpenHardwareCallbackTypes.ContactChange),
                CallbackCase.Event<OpenHardwareModelTicketEvent>(OpenHardwareCallbackTypes.ModelTicket),
                CallbackCase.Event<OpenHardwareVerifyDeviceEvent>(OpenHardwareCallbackTypes.VerifyDevice),
                CallbackCase.Command<OpenHardwareUpdateFirmwareCommand>(OpenHardwareCallbackTypes.UpdateFirmware),
                CallbackCase.Command<OpenHardwareFetchDeviceStatusCommand>(OpenHardwareCallbackTypes.FetchDeviceStatus),
                CallbackCase.Command<OpenHardwareUserScanCommand>(OpenHardwareCallbackTypes.UserScan),
                CallbackCase.Command<OpenHardwareBiometricPageCommand>(OpenHardwareCallbackTypes.EnterPage),
                CallbackCase.Command<OpenHardwareBiometricPageCommand>(OpenHardwareCallbackTypes.ExitPage),
                CallbackCase.Command<OpenHardwareRemoteOpenDoorCommand>(OpenHardwareCallbackTypes.RemoteOpenDoor),
                CallbackCase.Command<OpenHardwareBiometricPageCommand>(OpenHardwareCallbackTypes.DeleteBiometricInfo),
                CallbackCase.Command<OpenHardwarePrinterJobSubmitCommand>(OpenHardwareCallbackTypes.PrinterJobSubmit),
                CallbackCase.Command<OpenHardwarePrinterJobTranscodeCommand>(OpenHardwareCallbackTypes.PrinterJobTranscode),
                CallbackCase.Command<OpenHardwarePrinterJobDeleteCommand>(OpenHardwareCallbackTypes.PrinterJobDelete)
            };

            Assert.AreEqual(15, callbackTypes.Length);
            foreach (var callback in callbackTypes)
            {
                var payloadName = callback.MessageType == "event"
                    ? "event"
                    : "command";
                var typeName = callback.MessageType == "event"
                    ? "event_type"
                    : "command_type";
                var plaintext = "{\"msg_type\":\"" + callback.MessageType +
                    "\",\"base_info\":{\"req_id\":\"REQ-1\"," +
                    "\"createtime\":4178368698},\"" + payloadName +
                    "\":{\"" + typeName + "\":\"" + callback.CallbackType +
                    "\"}}";

                var message = OpenHardwareCallbackHandler.ParsePlaintext(plaintext);

                Assert.AreEqual(callback.ExpectedType, message.GetType(),
                    callback.CallbackType);
                Assert.AreEqual(4178368698L, message.base_info.createtime,
                    callback.CallbackType);
            }
        }

        [TestMethod]
        public void ParserPreservesNestedCollectionsAndLargeNumericFields()
        {
            const string contactJson =
                "{\"msg_type\":\"event\",\"base_info\":{" +
                "\"req_id\":\"REQ-CONTACT\",\"createtime\":4178368698}," +
                "\"event\":{\"event_type\":\"contact_change\"," +
                "\"perm_version\":5000000000," +
                "\"create_user\":[{\"open_userid\":\"USER-1\"," +
                "\"user_type\":2}]}}";
            var contact = (OpenHardwareEventCallback<OpenHardwareContactChangeEvent>)
                OpenHardwareCallbackHandler.ParsePlaintext(contactJson);
            Assert.AreEqual(5000000000L, contact.@event.perm_version);
            Assert.AreEqual("USER-1", contact.@event.create_user[0].open_userid);

            const string printerJson =
                "{\"msg_type\":\"command\",\"base_info\":{" +
                "\"req_id\":\"REQ-PRINT\",\"createtime\":4178368698}," +
                "\"command\":{\"command_type\":\"printer_job_trans\"," +
                "\"jobid\":\"JOB-1\",\"doc_size\":5000000000," +
                "\"encoding_aeskey\":\"FILE-KEY\",\"trans_setting\":{" +
                "\"version\":6000000000,\"setting_list\":[{" +
                "\"key\":\"paper_size\",\"value\":[\"A4\"]}]}}}";
            var printer = (OpenHardwareCommandCallback<OpenHardwarePrinterJobTranscodeCommand>)
                OpenHardwareCallbackHandler.ParsePlaintext(printerJson);
            Assert.AreEqual(5000000000L, printer.command.doc_size);
            Assert.AreEqual(6000000000L, printer.command.trans_setting.version);
            Assert.AreEqual("A4",
                printer.command.trans_setting.setting_list[0].value[0]);
        }

        [TestMethod]
        public void EncryptedRequestCanBeVerifiedDecryptedAndDispatched()
        {
            const string plaintext =
                "{\"msg_type\":\"event\",\"base_info\":{" +
                "\"req_id\":\"REQ-BIND\",\"model_id\":\"MODEL-10001\"}," +
                "\"event\":{\"event_type\":\"bind\"," +
                "\"auth_code\":\"AUTH-CODE\",\"verif_code\":\"9527\"}}";
            var crypt = new WXBizMsgCrypt(Token, EncodingAesKey, ReceiveId);
            BotEncryptedReply encrypted = null;
            Assert.AreEqual(0, crypt.EncryptJsonMsg(plaintext, Timestamp, Nonce,
                ref encrypted));
            var encryptedBody = JsonConvert.SerializeObject(
                new OpenHardwareEncryptedCallbackRequest
                {
                    tousername = ReceiveId,
                    encrypt = encrypted.encrypt
                });

            var result = OpenHardwareCallbackHandler.DecryptAndParse(Token,
                EncodingAesKey, ReceiveId, encrypted.msgsignature, Timestamp,
                Nonce, encryptedBody);

            Assert.AreEqual(ReceiveId, result.tousername);
            Assert.AreEqual(plaintext, result.plaintext);
            var bind = (OpenHardwareEventCallback<OpenHardwareBindEvent>)
                result.message;
            Assert.AreEqual("AUTH-CODE", bind.@event.auth_code);
            Assert.AreEqual("9527", bind.@event.verif_code);
        }

        [TestMethod]
        public void PassiveResponseCanBeEncryptedAndVerifiedByOfficialCryptFlow()
        {
            var encrypted = OpenHardwareCallbackHandler.EncryptResponse(Token,
                EncodingAesKey, ReceiveId, Timestamp, Nonce,
                new OpenHardwareBindEventResponse
                {
                    errcode = 0,
                    errmsg = "ok"
                });

            Assert.AreEqual(Timestamp, encrypted.timestamp);
            Assert.AreEqual(Nonce, encrypted.nonce);
            var plaintext = string.Empty;
            var crypt = new WXBizMsgCrypt(Token, EncodingAesKey, ReceiveId);
            Assert.AreEqual(0, crypt.DecryptJsonMsg(encrypted.msgsignature,
                encrypted.timestamp, encrypted.nonce, encrypted.encrypt,
                ref plaintext));
            var response = JsonConvert
                .DeserializeObject<OpenHardwareBindEventResponse>(plaintext);
            Assert.AreEqual(0, response.errcode);
            Assert.AreEqual("ok", response.errmsg);
        }

        [TestMethod]
        public void UnknownCallbacksRemainLosslessAndPublicSurfaceIsDocumented()
        {
            const string unknownJson =
                "{\"msg_type\":\"command\",\"base_info\":{" +
                "\"req_id\":\"REQ-UNKNOWN\"},\"command\":{" +
                "\"command_type\":\"future_command\",\"new_field\":123}}";
            var unknown = (OpenHardwareUnknownCallbackMessage)
                OpenHardwareCallbackHandler.ParsePlaintext(unknownJson);
            Assert.AreEqual("future_command", unknown.callback_type);
            Assert.AreEqual(unknownJson, unknown.raw_json);
            Assert.AreEqual("REQ-UNKNOWN", unknown.base_info.req_id);

            var sources = new[]
            {
                ReadRepositoryFile("src", "Senparc.Weixin.Work",
                    "Senparc.Weixin.Work", "AdvancedAPIs", "OpenHardware",
                    "OpenHardwareCallbackJson.cs"),
                ReadRepositoryFile("src", "Senparc.Weixin.Work",
                    "Senparc.Weixin.Work", "AdvancedAPIs", "OpenHardware",
                    "OpenHardwareCallbackHandler.cs")
            };
            var combinedSource = string.Join("\n", sources);
            foreach (var documentId in new[]
            {
                "95987", "95988", "95989", "96011", "96053", "96130",
                "97079", "97390", "96009", "96010", "96116", "96414",
                "97075", "97370", "96062"
            })
            {
                StringAssert.Contains(combinedSource, "/document/path/" + documentId);
            }

            foreach (var source in sources)
            {
                Assert.IsFalse(source.Contains("public object "));
                Assert.IsFalse(source.Contains("public dynamic "));
                var lines = source.Split(new[] { "\r\n", "\n" },
                    StringSplitOptions.None);
                for (var index = 0; index < lines.Length; index++)
                {
                    if (!lines[index].TrimStart().StartsWith("public ",
                        StringComparison.Ordinal))
                    {
                        continue;
                    }

                    var previous = index - 1;
                    while (previous >= 0 &&
                        string.IsNullOrWhiteSpace(lines[previous]))
                    {
                        previous--;
                    }

                    Assert.IsTrue(previous >= 0 &&
                        lines[previous].TrimStart().StartsWith("///",
                            StringComparison.Ordinal), lines[index].Trim());
                }
            }
        }

        private static string ReadRepositoryFile(params string[] pathParts)
            => File.ReadAllText(Path.Combine(
                new[] { FindRepositoryRoot() }.Concat(pathParts).ToArray()));

        private static string FindRepositoryRoot(
            [CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath)
                    ? null
                    : new DirectoryInfo(startPath);
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

        private sealed class CallbackCase
        {
            public string MessageType { get; private set; }

            public string CallbackType { get; private set; }

            public Type ExpectedType { get; private set; }

            public static CallbackCase Event<TPayload>(string callbackType)
                where TPayload : OpenHardwareEventPayload
                => new CallbackCase
                {
                    MessageType = "event",
                    CallbackType = callbackType,
                    ExpectedType = typeof(OpenHardwareEventCallback<TPayload>)
                };

            public static CallbackCase Command<TPayload>(string callbackType)
                where TPayload : OpenHardwareCommandPayload
                => new CallbackCase
                {
                    MessageType = "command",
                    CallbackType = callbackType,
                    ExpectedType = typeof(OpenHardwareCommandCallback<TPayload>)
                };
        }
    }
}
