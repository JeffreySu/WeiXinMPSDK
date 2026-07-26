/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareCallbackJson.cs
    文件功能描述：企业微信开放硬件加密 JSON 回调强类型模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐开放硬件事件、指令和被动响应模型

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 开放硬件回调消息及指令类型常量。
    /// </summary>
    public static class OpenHardwareCallbackTypes
    {
        /// <summary>企业绑定设备事件。</summary>
        public const string Bind = "bind";

        /// <summary>企业解绑设备事件。</summary>
        public const string Unbind = "unbind";

        /// <summary>设备可见范围通讯录变更事件。</summary>
        public const string ContactChange = "contact_change";

        /// <summary>设备型号调用票据推送事件。</summary>
        public const string ModelTicket = "model_ticket";

        /// <summary>查询设备序列号合法性事件。</summary>
        public const string VerifyDevice = "verify_device";

        /// <summary>固件升级指令。</summary>
        public const string UpdateFirmware = "update_firmware";

        /// <summary>获取设备状态指令。</summary>
        public const string FetchDeviceStatus = "fetch_device_status";

        /// <summary>用户扫描设备二维码指令。</summary>
        public const string UserScan = "user_scan";

        /// <summary>进入识别信息录入页面指令。</summary>
        public const string EnterPage = "enter_page";

        /// <summary>退出识别信息录入页面指令。</summary>
        public const string ExitPage = "exit_page";

        /// <summary>远程开门指令。</summary>
        public const string RemoteOpenDoor = "remote_open_door";

        /// <summary>删除识别信息指令。</summary>
        public const string DeleteBiometricInfo = "delete_bio_info";

        /// <summary>打印任务提交指令。</summary>
        public const string PrinterJobSubmit = "printer_job_submit";

        /// <summary>打印文件转码指令。</summary>
        public const string PrinterJobTranscode = "printer_job_trans";

        /// <summary>打印任务删除指令。</summary>
        public const string PrinterJobDelete = "printer_job_del";
    }

    /// <summary>
    /// 企业微信开放硬件加密回调请求外层结构。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96062"/></para>
    /// </summary>
    public class OpenHardwareEncryptedCallbackRequest
    {
        /// <summary>获取或设置接收方标识；服务商通用地址为 CorpId，型号地址为 ModelId。</summary>
        public string tousername { get; set; }

        /// <summary>获取或设置经过企业微信 AES 协议加密的回调正文。</summary>
        public string encrypt { get; set; }
    }

    /// <summary>
    /// 企业微信开放硬件被动响应的加密外层结构。
    /// </summary>
    public class OpenHardwareEncryptedCallbackReply
    {
        /// <summary>获取或设置加密后的响应正文。</summary>
        public string encrypt { get; set; }

        /// <summary>获取或设置响应消息签名。</summary>
        public string msgsignature { get; set; }

        /// <summary>获取或设置生成签名使用的时间戳。</summary>
        public string timestamp { get; set; }

        /// <summary>获取或设置生成签名使用的随机字符串。</summary>
        public string nonce { get; set; }
    }

    /// <summary>
    /// 开放硬件回调解密与解析结果。
    /// </summary>
    public class OpenHardwareCallbackParseResult
    {
        /// <summary>获取或设置回调外层携带的接收方标识。</summary>
        public string tousername { get; set; }

        /// <summary>获取或设置完成验签和解密后的原始 JSON。</summary>
        public string plaintext { get; set; }

        /// <summary>获取或设置按事件或指令类型解析后的强类型消息。</summary>
        public OpenHardwareCallbackMessageBase message { get; set; }
    }

    /// <summary>
    /// 开放硬件回调验签或加解密异常。
    /// </summary>
    public class OpenHardwareCallbackCryptException : Exception
    {
        /// <summary>
        /// 使用企业微信加解密库错误码初始化异常。
        /// </summary>
        /// <param name="errorCode">企业微信加解密库错误码。</param>
        public OpenHardwareCallbackCryptException(int errorCode)
            : base("企业微信开放硬件回调验签或加解密失败，错误码：" + errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>获取企业微信加解密库错误码。</summary>
        public int ErrorCode { get; }
    }

    /// <summary>
    /// 开放硬件事件或指令的固定基础信息。
    /// </summary>
    public class OpenHardwareCallbackBaseInfo
    {
        /// <summary>获取或设置请求唯一标识，可用于回调排重。</summary>
        public string req_id { get; set; }

        /// <summary>获取或设置设备序列号；型号级事件可能不返回。</summary>
        public string device_sn { get; set; }

        /// <summary>获取或设置事件触发 Unix 时间戳（秒）。</summary>
        public long createtime { get; set; }

        /// <summary>获取或设置设备型号 ModelId。</summary>
        public string model_id { get; set; }

        /// <summary>获取或设置设备绑定企业 CorpId。</summary>
        public string auth_corpid { get; set; }

        /// <summary>获取或设置硬件服务商企业 CorpId。</summary>
        public string service_corpid { get; set; }
    }

    /// <summary>
    /// 开放硬件回调消息基类。
    /// </summary>
    public abstract class OpenHardwareCallbackMessageBase
    {
        /// <summary>获取或设置消息类型，取值为 event 或 command。</summary>
        public string msg_type { get; set; }

        /// <summary>获取或设置回调固定基础信息。</summary>
        public OpenHardwareCallbackBaseInfo base_info { get; set; }
    }

    /// <summary>
    /// 开放硬件事件消息。
    /// </summary>
    /// <typeparam name="TEvent">事件数据类型。</typeparam>
    public class OpenHardwareEventCallback<TEvent> : OpenHardwareCallbackMessageBase
        where TEvent : OpenHardwareEventPayload
    {
        /// <summary>获取或设置事件数据。</summary>
        public TEvent @event { get; set; }
    }

    /// <summary>
    /// 开放硬件指令消息。
    /// </summary>
    /// <typeparam name="TCommand">指令数据类型。</typeparam>
    public class OpenHardwareCommandCallback<TCommand> : OpenHardwareCallbackMessageBase
        where TCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置指令数据。</summary>
        public TCommand command { get; set; }
    }

    /// <summary>
    /// SDK 尚未识别的新开放硬件事件或指令。
    /// </summary>
    public class OpenHardwareUnknownCallbackMessage : OpenHardwareCallbackMessageBase
    {
        /// <summary>获取或设置未知的 event_type 或 command_type。</summary>
        public string callback_type { get; set; }

        /// <summary>获取或设置未丢失字段的原始明文 JSON。</summary>
        public string raw_json { get; set; }
    }

    /// <summary>
    /// 开放硬件事件数据基类。
    /// </summary>
    public abstract class OpenHardwareEventPayload
    {
        /// <summary>获取或设置事件类型。</summary>
        public string event_type { get; set; }
    }

    /// <summary>
    /// 开放硬件指令数据基类。
    /// </summary>
    public abstract class OpenHardwareCommandPayload
    {
        /// <summary>获取或设置指令类型。</summary>
        public string command_type { get; set; }
    }

    /// <summary>
    /// 企业绑定设备事件数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95987"/></para>
    /// </summary>
    public class OpenHardwareBindEvent : OpenHardwareEventPayload
    {
        /// <summary>获取或设置十分钟内有效且只能使用一次的设备授权码。</summary>
        public string auth_code { get; set; }

        /// <summary>获取或设置用户绑定设备时输入的验证码。</summary>
        public string verif_code { get; set; }
    }

    /// <summary>
    /// 企业解绑设备事件数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95988"/></para>
    /// </summary>
    public class OpenHardwareUnbindEvent : OpenHardwareEventPayload
    {
    }

    /// <summary>
    /// 开放硬件回调中的成员引用。
    /// </summary>
    public class OpenHardwareCallbackUser
    {
        /// <summary>获取或设置成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>获取或设置成员类型；0 为企业员工，2 为学生。</summary>
        public int user_type { get; set; }
    }

    /// <summary>
    /// 设备可见范围通讯录变更事件数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/95989"/></para>
    /// </summary>
    public class OpenHardwareContactChangeEvent : OpenHardwareEventPayload
    {
        /// <summary>获取或设置当前云端通讯录权限版本号。</summary>
        public long perm_version { get; set; }

        /// <summary>获取或设置新增成员列表。</summary>
        public IList<OpenHardwareCallbackUser> create_user { get; set; }

        /// <summary>获取或设置更新成员列表。</summary>
        public IList<OpenHardwareCallbackUser> update_user { get; set; }

        /// <summary>获取或设置删除成员列表。</summary>
        public IList<OpenHardwareCallbackUser> delete_user { get; set; }
    }

    /// <summary>
    /// 设备型号调用票据推送事件数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96053"/></para>
    /// </summary>
    public class OpenHardwareModelTicketEvent : OpenHardwareEventPayload
    {
        /// <summary>获取或设置最长 512 字节、定时更新的 ModelTicket。</summary>
        public string model_ticket { get; set; }
    }

    /// <summary>
    /// 查询设备序列号合法性事件数据。
    /// <para>官方示例将该回调定义为 event：<see href="https://developer.work.weixin.qq.com/document/path/96130"/></para>
    /// </summary>
    public class OpenHardwareVerifyDeviceEvent : OpenHardwareEventPayload
    {
        /// <summary>获取或设置待核验设备序列号的 MD5 值。</summary>
        public string device_sn_md5 { get; set; }
    }

    /// <summary>
    /// 固件升级指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96011"/></para>
    /// </summary>
    public class OpenHardwareUpdateFirmwareCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置本次固件升级操作标识。</summary>
        public string oper_id { get; set; }
    }

    /// <summary>
    /// 获取设备状态指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97079"/></para>
    /// </summary>
    public class OpenHardwareFetchDeviceStatusCommand : OpenHardwareCommandPayload
    {
    }

    /// <summary>
    /// 用户扫描设备二维码指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97390"/></para>
    /// </summary>
    public class OpenHardwareUserScanCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置本次扫码操作标识。</summary>
        public string oper_id { get; set; }

        /// <summary>获取或设置扫码成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>获取或设置二维码携带的自定义 State。</summary>
        public string state { get; set; }

        /// <summary>获取或设置扫码成员类型；0 为普通成员，1 为管理员。</summary>
        public int user_type { get; set; }

        /// <summary>获取或设置用于特定接口调用的授权码。</summary>
        public string auth_code { get; set; }

        /// <summary>获取或设置授权码有效时间（秒）。</summary>
        public long expires_in { get; set; }

        /// <summary>获取或设置授权码类型；1 表示扫描文件。</summary>
        public int auth_code_type { get; set; }
    }

    /// <summary>
    /// 进入、退出或删除成员识别信息的指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96009"/>、<see href="https://developer.work.weixin.qq.com/document/path/96116"/></para>
    /// </summary>
    public class OpenHardwareBiometricPageCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置页面类型；fa 为人脸，fp 为指纹。</summary>
        public string page_type { get; set; }

        /// <summary>获取或设置本次识别信息操作标识。</summary>
        public string oper_id { get; set; }

        /// <summary>获取或设置成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>获取或设置成员类型；0 为企业员工，2 为学生。</summary>
        public int user_type { get; set; }
    }

    /// <summary>
    /// 远程开门指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96010"/></para>
    /// </summary>
    public class OpenHardwareRemoteOpenDoorCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置发起远程开门的成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>获取或设置本次远程开门操作标识。</summary>
        public string oper_id { get; set; }
    }

    /// <summary>
    /// 打印任务提交指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/96414"/></para>
    /// </summary>
    public class OpenHardwarePrinterJobSubmitCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置发起打印任务的成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>获取或设置打印任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// 打印文件转码指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97075"/></para>
    /// </summary>
    public class OpenHardwarePrinterJobTranscodeCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置发起打印预览的成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>获取或设置打印任务 ID。</summary>
        public string jobid { get; set; }

        /// <summary>获取或设置十分钟内有效的加密文件下载地址。</summary>
        public string download_url { get; set; }

        /// <summary>获取或设置官方示例字段 encoding_aeskey 中的文件解密密钥。</summary>
        public string encoding_aeskey { get; set; }

        /// <summary>获取或设置原始文档名称。</summary>
        public string doc_name { get; set; }

        /// <summary>获取或设置原始文档大小（字节）。</summary>
        public long doc_size { get; set; }

        /// <summary>获取或设置本次打印转码配置。</summary>
        public OpenHardwarePrinterTranscodeSetting trans_setting { get; set; }
    }

    /// <summary>
    /// 打印转码配置。
    /// </summary>
    public class OpenHardwarePrinterTranscodeSetting
    {
        /// <summary>获取或设置配置版本号；用户修改选项后递增。</summary>
        public long version { get; set; }

        /// <summary>获取或设置打印配置项列表。</summary>
        public IList<OpenHardwarePrinterSetting> setting_list { get; set; }
    }

    /// <summary>
    /// 打印任务删除指令数据。
    /// <para>参考文档：<see href="https://developer.work.weixin.qq.com/document/path/97370"/></para>
    /// </summary>
    public class OpenHardwarePrinterJobDeleteCommand : OpenHardwareCommandPayload
    {
        /// <summary>获取或设置待删除的打印任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// 企业绑定设备事件的被动响应。
    /// </summary>
    public class OpenHardwareBindEventResponse
    {
        /// <summary>获取或设置绑定检查结果；0 表示成功，1 至 7 及 1999 为官方失败码。</summary>
        public int errcode { get; set; }

        /// <summary>获取或设置绑定检查结果说明。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>
    /// 查询设备序列号合法性事件的被动响应。
    /// </summary>
    public class OpenHardwareVerifyDeviceResponse
    {
        /// <summary>获取或设置设备序列号是否属于当前服务商。</summary>
        public bool is_valid { get; set; }

        /// <summary>获取或设置合法设备所属的 ModelId。</summary>
        public string modelid { get; set; }
    }

    /// <summary>
    /// 用户扫描设备二维码指令的被动响应。
    /// </summary>
    public class OpenHardwareUserScanResponse
    {
        /// <summary>获取或设置设备在线状态；1 为在线，2 为离线。</summary>
        public int online_status { get; set; }
    }
}
