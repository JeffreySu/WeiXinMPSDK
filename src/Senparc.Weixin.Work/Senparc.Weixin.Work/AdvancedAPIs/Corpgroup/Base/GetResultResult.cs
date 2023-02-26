using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// 获取异步任务结果 响应参数
    /// </summary>
    public class GetResultResult : WorkJsonResult
    {
        /// <summary>
        /// 任务状态，整型，1表示任务开始，2表示任务进行中，3表示任务已完成
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 详细的处理结果。当任务完成后此字段有效
        /// </summary>
        public GetResultResult_Result result { get; set; }
    }

    public class GetResultResult_Result
    {
        /// <summary>
        /// 上下游id
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 导入状态。1:全部企业导入成功，2:部分企业导入成功，3:全部企业导入失败
        /// </summary>
        public int import_status { get; set; }

        /// <summary>
        /// 导入失败结果列表 。当企业中有联系人导入失败时，本次导入该企业所有联系人的导入都会被阻断。
        /// </summary>
        public List<GetResultResult_Result_FailList> fail_list { get; set; }
    }

    public class GetResultResult_Result_FailList
    {
        /// <summary>
        /// 自定义企业id
        /// </summary>
        public string custom_id { get; set; }

        /// <summary>
        /// 	自定义企业名称
        /// </summary>
        public string corp_name { get; set; }

        /// <summary>
        /// 	该企业导入操作的结果错误码
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 	该企业导入操作的结果错误码描述
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 导入失败的联系人结果
        /// </summary>
        public List<GetResultResult_Result_FailList_ContactInfoList> contact_info_list { get; set; }
    }

    public class GetResultResult_Result_FailList_ContactInfoList
    {
        /// <summary>
        /// 	导入失败的联系人手机号。有此联系人相关的错误时才会返回
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 	导入失败的联系人错误码描述。有此联系人相关的错误时才会返回
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 导入失败的联系人错误码。有此联系人相关的错误时才会返回
        /// </summary>
        public int errcode { get; set; }
    }
}
