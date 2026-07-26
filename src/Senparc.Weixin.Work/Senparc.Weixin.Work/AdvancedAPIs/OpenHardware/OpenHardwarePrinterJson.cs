/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwarePrinterJson.cs
    文件功能描述：企业微信智慧硬件打印扫描强类型模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智慧硬件打印扫描强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>获取打印任务列表请求。</summary>
    public class OpenHardwareGetPrinterJobListRequest
    {
        /// <summary>提交打印任务的成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>任务状态；零未打印，一成功，二失败；不填时查询全部。</summary>
        public int? status { get; set; }

        /// <summary>分页游标；首次请求可不填。</summary>
        public string cursor { get; set; }

        /// <summary>本次返回任务数，默认一百，最大二百。</summary>
        public int? limit { get; set; }

        /// <summary>打印任务起始时间，Unix 时间戳。</summary>
        public long? begin_time { get; set; }

        /// <summary>打印任务结束时间，Unix 时间戳。</summary>
        public long? end_time { get; set; }

        /// <summary>指定任务 ID 列表，最多二百项；填写后忽略其他筛选条件。</summary>
        public List<string> jobid_list { get; set; }
    }

    /// <summary>获取打印任务列表结果。</summary>
    public class OpenHardwareGetPrinterJobListResult : WorkJsonResult
    {
        /// <summary>打印任务列表。</summary>
        public List<OpenHardwarePrinterJob> printer_job_list { get; set; }

        /// <summary>下一页游标；没有更多任务时为空。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>智慧硬件打印任务。</summary>
    public class OpenHardwarePrinterJob
    {
        /// <summary>任务提交者 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>任务提交时间，Unix 时间戳。</summary>
        public long createtime { get; set; }

        /// <summary>是否为扫码后提交：零否，一是。</summary>
        public int submitted { get; set; }

        /// <summary>扫码打印时由设备传入并透传的状态值。</summary>
        public string state { get; set; }

        /// <summary>任务状态：零未打印，一成功，二失败。</summary>
        public int status { get; set; }

        /// <summary>该任务的错误码。</summary>
        public int errcode { get; set; }

        /// <summary>该任务的错误描述。</summary>
        public string errmsg { get; set; }

        /// <summary>打印文档名称。</summary>
        public string doc_name { get; set; }

        /// <summary>打印文档大小，单位为字节。</summary>
        public long doc_size { get; set; }

        /// <summary>打印任务 ID。</summary>
        public string jobid { get; set; }

        /// <summary>打印设置列表。</summary>
        public List<OpenHardwarePrinterSetting> setting_list { get; set; }
    }

    /// <summary>打印任务设置项。</summary>
    public class OpenHardwarePrinterSetting
    {
        /// <summary>设置项名称，使用 UTF-8 编码。</summary>
        public string key { get; set; }

        /// <summary>设置项值列表，可表示复选值。</summary>
        public List<string> value { get; set; }
    }

    /// <summary>仅包含打印任务 ID 的请求。</summary>
    public class OpenHardwarePrinterJobRequest
    {
        /// <summary>打印任务 ID。</summary>
        public string jobid { get; set; }
    }

    /// <summary>获取打印文件结果。</summary>
    public class OpenHardwarePrinterJobDownloadResult : WorkJsonResult
    {
        /// <summary>打印文件下载地址。</summary>
        public string download_url { get; set; }

        /// <summary>Base64 编码的 AES-256-CBC 文件解密密钥。</summary>
        public string encoding_aeskey { get; set; }
    }

    /// <summary>上报打印任务状态请求。</summary>
    public class OpenHardwareReportPrinterJobStatusRequest : OpenHardwarePrinterJobRequest
    {
        /// <summary>任务状态：一打印失败，二打印成功。</summary>
        public int status { get; set; }

        /// <summary>设备自定义错误码；失败时必填。</summary>
        public int? errcode { get; set; }

        /// <summary>UTF-8 编码的错误描述；失败时必填。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>上传扫描文件请求。</summary>
    public class OpenHardwarePushScanFileRequest
    {
        /// <summary>接收扫描文件的成员 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>扫描文件名。</summary>
        public string filename { get; set; }

        /// <summary>扫描文件临时素材 MediaId；与下载地址二选一。</summary>
        public string media_id { get; set; }

        /// <summary>厂商提供的 CDN 下载地址；填写时优先于 MediaId。</summary>
        public string download_url { get; set; }

        /// <summary>用户扫描指令下发的上传授权码。</summary>
        public string auth_code { get; set; }
    }

    /// <summary>返回打印转码结果请求。</summary>
    public class OpenHardwarePrinterJobTransResultRequest : OpenHardwarePrinterJobRequest
    {
        /// <summary>预览转码设置版本号。</summary>
        public int setting_version { get; set; }

        /// <summary>转码文件类型：零 PDF，一 JPG；默认零。</summary>
        public int? type { get; set; }

        /// <summary>JPG 对应的当前页码，从一开始；类型为 JPG 时必填。</summary>
        public int? page { get; set; }

        /// <summary>转码后 PDF 总页数或图片总张数。</summary>
        public int page_size { get; set; }

        /// <summary>转码文件临时素材 MediaId；与下载地址二选一。</summary>
        public string media_id { get; set; }

        /// <summary>厂商提供的 CDN 下载地址；填写时优先于 MediaId。</summary>
        public string download_url { get; set; }

        /// <summary>转码结果错误码，零表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>转码结果错误描述。</summary>
        public string errmsg { get; set; }
    }
}
