namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// 提交批量导入上下游联系人任务 请求参数
    /// </summary>
    public class ImportChainContactRequest
    {
        /// <summary>
        /// 上下游id。文件中的联系人将会被导入此上下游中
        /// 必填
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 微信客户的openid
        /// 必填
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 需要换取的企业corpid，不填则拉取所有企业
        /// 必填
        /// </summary>
        public string corpid { get; set; }
    }

    public class ImportChainContactRequest_ContactList
    {
        /// <summary>
        /// 上下游企业名称。长度为1-32个utf8字符。只能由中文、字母、数字和“ -_()（）”六种字符组成
        /// 必填
        /// </summary>
        public string corp_name { get; set; }

        /// <summary>
        /// 导入后企业所在分组。分组为空的企业会放在根分组下。仅针对新导入企业生效，不会修改已导入企业的分组。
        /// 非必填
        /// </summary>
        public string group_path { get; set; }

        /// <summary>
        /// 上下游企业自定义 id。长度为0～64 个字节，只能由数字和字母组成
        /// 非必填
        /// </summary>
        public string custom_id { get; set; }

        /// <summary>
        /// 上下游联系人信息列表
        /// 必填
        /// </summary>
        public ImportChainContactRequest_ContactInfoList contact_info_list { get; set; }
    }



    public class ImportChainContactRequest_ContactInfoList
    {
        /// <summary>
        /// 上下游联系人姓名。长度为1～32个utf8字符
        /// 必填
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 联系人身份类型。1:成员，2:负责人。
        /// 必填
        /// </summary>
        public int identity_type { get; set; }

        /// <summary>
        /// 手机号。支持国内、国际手机号（国内手机号直接输入手机号即可，格式示例：“138****0001”；国际手机号必须包含加号以及国家地区码，格式示例：“+85259****45”
        /// 必填
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 上下游用户自定义 id。类型为字符串，暂时只支持传入64比特无符号整型，取值范围1到2^64-2，必须是全数字，不得传入前置0，且不能为11位或13位数字。
        /// 非必填
        /// </summary>
        public string user_custom_id { get; set; }
    }
}
